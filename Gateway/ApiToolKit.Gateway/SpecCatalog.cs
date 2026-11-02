using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace ApiToolKit.Gateway;

public record ModuleSpec(ModuleOptions Module, JsonObject? Document, bool IsLive, DateTime? CachedAt);

/// <summary>
/// Loads each module's swagger.json (live when the module is running, otherwise the cached copy),
/// builds the per-platform group documents, and keeps the path → module routing table for the proxy.
/// </summary>
public class SpecCatalog
{
    private static readonly string[] HttpMethods = { "get", "put", "post", "delete", "patch", "options", "head", "trace" };
    private static readonly JsonSerializerOptions Indented = new() { WriteIndented = true };

    private readonly GatewayOptions _options;
    private readonly IHttpClientFactory _httpFactory;
    private readonly string _cacheDir;
    private readonly ILogger<SpecCatalog> _logger;
    private readonly ConcurrentDictionary<string, List<string>> _routePrefixes = new();

    public SpecCatalog(IOptions<GatewayOptions> options, IHttpClientFactory httpFactory, IWebHostEnvironment env, ILogger<SpecCatalog> logger)
    {
        _options = options.Value;
        _httpFactory = httpFactory;
        _cacheDir = Path.Combine(env.ContentRootPath, "specs");
        _logger = logger;
        Directory.CreateDirectory(_cacheDir);
    }

    public IReadOnlyList<ModuleOptions> Modules => _options.Modules;
    public IReadOnlyList<GroupOptions> Groups => _options.Groups;
    public ModuleOptions? FindModule(string key) => _options.Modules.FirstOrDefault(m => m.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

    // ─── Module specs ────────────────────────────────────────────────────────

    // A stopped module costs ~2 s per probe on Windows (refused connections are retried), so remember
    // each result briefly; opening several groups in a row then stays fast.
    private static readonly TimeSpan RecentWindow = TimeSpan.FromSeconds(30);
    private readonly ConcurrentDictionary<string, (ModuleSpec Spec, DateTime At)> _recent = new();

    public async Task<ModuleSpec> GetModuleSpecAsync(ModuleOptions module, CancellationToken ct = default)
    {
        if (_recent.TryGetValue(module.Key, out var hit) && DateTime.UtcNow - hit.At < RecentWindow)
            return hit.Spec;
        var spec = await LoadModuleSpecAsync(module, ct);
        _recent[module.Key] = (spec, DateTime.UtcNow);
        return spec;
    }

    private async Task<ModuleSpec> LoadModuleSpecAsync(ModuleOptions module, CancellationToken ct)
    {
        var cachePath = Path.Combine(_cacheDir, $"{module.Key}.json");
        try
        {
            var http = _httpFactory.CreateClient("spec");
            var text = await http.GetStringAsync(module.BaseUrl.TrimEnd('/') + module.SwaggerPath, ct);
            if (JsonNode.Parse(text) is JsonObject live)
            {
                await File.WriteAllTextAsync(cachePath, live.ToJsonString(Indented), ct);
                RegisterRoutes(module, live);
                return new ModuleSpec(module, live, true, DateTime.Now);
            }
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or JsonException)
        {
            // Module not running (or not serving Swagger) — fall back to the cached copy.
        }

        if (File.Exists(cachePath) && JsonNode.Parse(await File.ReadAllTextAsync(cachePath, ct)) is JsonObject cached)
        {
            RegisterRoutes(module, cached);
            return new ModuleSpec(module, cached, false, File.GetLastWriteTime(cachePath));
        }
        return new ModuleSpec(module, null, false, null);
    }

    public async Task<bool> IsRunningAsync(ModuleOptions module, CancellationToken ct = default)
    {
        try
        {
            using var response = await _httpFactory.CreateClient("spec").GetAsync(module.BaseUrl.TrimEnd('/') + module.SwaggerPath, ct);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException) { return false; }
    }

    /// <summary>Loads every module once so the proxy can route even before a group page was opened.</summary>
    public async Task WarmUpAsync(CancellationToken ct = default) =>
        await Task.WhenAll(_options.Modules.Select(m => GetModuleSpecAsync(m, ct)));

    // ─── Routing table ───────────────────────────────────────────────────────

    // Each operation path contributes its literal part up to the first "{"; a request is routed to
    // the module that owns the longest matching prefix.
    private void RegisterRoutes(ModuleOptions module, JsonObject doc)
    {
        if (doc["paths"] is not JsonObject paths) return;
        _routePrefixes[module.Key] = paths.Select(p => p.Key.Split('{')[0]).Distinct().ToList();
    }

    public ModuleOptions? Route(string requestPath)
    {
        string? bestKey = null;
        var bestLength = -1;
        foreach (var (key, prefixes) in _routePrefixes)
            foreach (var prefix in prefixes)
                if (prefix.Length > bestLength && Matches(requestPath, prefix))
                {
                    bestKey = key;
                    bestLength = prefix.Length;
                }
        return bestKey == null ? null : FindModule(bestKey);
    }

    private static bool Matches(string requestPath, string prefix) =>
        prefix.EndsWith('/')
            ? requestPath.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
            : requestPath.Equals(prefix, StringComparison.OrdinalIgnoreCase)
              || requestPath.StartsWith(prefix + "/", StringComparison.OrdinalIgnoreCase);

    // ─── Group documents ─────────────────────────────────────────────────────

    public async Task<JsonObject?> BuildGroupAsync(string groupKey, CancellationToken ct = default)
    {
        var group = _options.Groups.FirstOrDefault(g => g.Key.Equals(groupKey, StringComparison.OrdinalIgnoreCase));
        if (group == null) return null;

        // Probe all of the group's modules in parallel (a merged group like Google has 15).
        var sources = (await Task.WhenAll(group.Sources
                .Where(s => FindModule(s.Module) != null)
                .Select(async s => (Source: s, Spec: await GetModuleSpecAsync(FindModule(s.Module)!, ct)))))
            .ToList();

        var multiModule = group.Sources.Select(s => s.Module).Distinct().Count() > 1;
        var paths = new JsonObject();
        var schemas = new JsonObject();
        var securitySchemes = new JsonObject();
        var tags = new List<string>();

        foreach (var (source, spec) in sources)
        {
            if (spec.Document == null) continue;
            // Work on a copy; prefix schema names when modules are merged so identical names don't collide.
            var doc = (JsonObject)spec.Document.DeepClone();
            if (multiModule) PrefixSchemas(doc, spec.Module.Key);

            var include = source.IncludeTags != null ? new Regex(source.IncludeTags, RegexOptions.IgnoreCase) : null;
            var exclude = source.ExcludeTags != null ? new Regex(source.ExcludeTags, RegexOptions.IgnoreCase) : null;

            foreach (var (path, pathNode) in (doc["paths"] as JsonObject ?? new JsonObject()).ToList())
            {
                if (pathNode is not JsonObject pathItem) continue;
                var kept = new JsonObject();
                foreach (var (name, value) in pathItem.ToList())
                {
                    if (!HttpMethods.Contains(name)) { if (name != "servers") kept[name] = value?.DeepClone(); continue; }
                    if (value is not JsonObject operation) continue;

                    var opTags = (operation["tags"] as JsonArray)?.Select(t => t?.GetValue<string>() ?? "").ToList() ?? new List<string>();
                    if (include != null && !opTags.Any(include.IsMatch)) continue;
                    if (exclude != null && opTags.Any(exclude.IsMatch)) continue;

                    if (source.TagPrefix != null)
                    {
                        opTags = opTags.Count == 0 ? new List<string> { source.TagPrefix.TrimEnd(' ', '·') } : opTags.Select(t => source.TagPrefix + t).ToList();
                        operation["tags"] = new JsonArray(opTags.Select(t => (JsonNode)JsonValue.Create(t)!).ToArray());
                    }
                    operation.Remove("servers");
                    tags.AddRange(opTags);
                    kept[name] = operation.DeepClone();
                }
                if (!kept.Any(k => HttpMethods.Contains(k.Key))) continue;

                if (paths[path] is JsonObject existing)
                    foreach (var (name, value) in kept) existing[name] ??= value?.DeepClone();
                else
                    paths[path] = kept;
            }

            if (doc["components"]?["schemas"] is JsonObject moduleSchemas)
                foreach (var (name, value) in moduleSchemas) schemas[name] ??= value?.DeepClone();
            if (doc["components"]?["securitySchemes"] is JsonObject moduleSecurity)
                foreach (var (name, value) in moduleSecurity) securitySchemes[name] ??= value?.DeepClone();
        }

        RemoveUnusedSchemas(paths, schemas);

        var components = new JsonObject { ["schemas"] = schemas };
        if (securitySchemes.Count > 0) components["securitySchemes"] = securitySchemes;

        return new JsonObject
        {
            ["openapi"] = "3.0.1",
            ["info"] = new JsonObject
            {
                ["title"] = $"ApiToolKit — {group.Title}",
                ["version"] = "v1",
                ["description"] = Describe(group, sources.Select(s => s.Spec).ToList(), paths)
            },
            // "Try it out" calls the gateway, which forwards them to the module that owns the path.
            ["servers"] = new JsonArray(new JsonObject { ["url"] = "/", ["description"] = "ApiToolKit Gateway (forwards to the module)" }),
            ["tags"] = new JsonArray(tags.Distinct().OrderBy(t => t, StringComparer.OrdinalIgnoreCase)
                .Select(t => (JsonNode)new JsonObject { ["name"] = t }).ToArray()),
            ["paths"] = paths,
            ["components"] = components
        };
    }

    private static string Describe(GroupOptions group, List<ModuleSpec> specs, JsonObject paths)
    {
        var operations = paths.Sum(p => (p.Value as JsonObject)?.Count(k => HttpMethods.Contains(k.Key)) ?? 0);
        var sb = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(group.Description)) sb.AppendLine(group.Description).AppendLine();
        if (specs.Count == 0) return sb.ToString();

        sb.AppendLine($"**{operations} endpoints** from {specs.Count} module(s).").AppendLine();
        sb.AppendLine("| Module | Status | Base URL | Start command |");
        sb.AppendLine("|---|---|---|---|");
        foreach (var s in specs.DistinctBy(s => s.Module.Key))
        {
            var status = s.IsLive ? "🟢 running"
                : s.Document != null ? $"⚪ stopped — docs from cache ({s.CachedAt:yyyy-MM-dd HH:mm})"
                : "🔴 stopped — no cached docs yet";
            sb.AppendLine($"| {s.Module.Name} | {status} | {s.Module.BaseUrl} | `.\\Gateway\\start-modules.ps1 {s.Module.Key}` |");
        }
        sb.AppendLine().AppendLine("\"Try it out\" only works while the module is running.");
        return sb.ToString();
    }

    private static void PrefixSchemas(JsonObject doc, string prefix)
    {
        if (doc["components"]?["schemas"] is not JsonObject schemas) return;
        var renamed = new JsonObject();
        foreach (var (name, value) in schemas.ToList())
        {
            schemas.Remove(name);
            renamed[$"{prefix}_{name}"] = value;
        }
        doc["components"]!["schemas"] = renamed;
        RewriteRefs(doc, prefix);
    }

    private static void RewriteRefs(JsonNode? node, string prefix)
    {
        switch (node)
        {
            case JsonObject obj:
                foreach (var (key, value) in obj.ToList())
                {
                    if (key == "$ref" && value is JsonValue v && v.TryGetValue<string>(out var reference)
                        && reference.StartsWith("#/components/schemas/"))
                        obj["$ref"] = "#/components/schemas/" + prefix + "_" + reference["#/components/schemas/".Length..];
                    else
                        RewriteRefs(value, prefix);
                }
                break;
            case JsonArray array:
                foreach (var item in array) RewriteRefs(item, prefix);
                break;
        }
    }

    // Filtering drops operations, so also drop schemas nothing references any more (keeps each group small).
    private static void RemoveUnusedSchemas(JsonObject paths, JsonObject schemas)
    {
        var used = new HashSet<string>();
        var queue = new Queue<JsonNode?>();
        queue.Enqueue(paths);
        while (queue.Count > 0)
        {
            switch (queue.Dequeue())
            {
                case JsonObject obj:
                    foreach (var (key, value) in obj)
                    {
                        if (key == "$ref" && value is JsonValue v && v.TryGetValue<string>(out var reference)
                            && reference.StartsWith("#/components/schemas/"))
                        {
                            var name = reference["#/components/schemas/".Length..];
                            if (used.Add(name)) queue.Enqueue(schemas[name]);
                        }
                        else queue.Enqueue(value);
                    }
                    break;
                case JsonArray array:
                    foreach (var item in array) queue.Enqueue(item);
                    break;
            }
        }
        foreach (var name in schemas.Select(s => s.Key).Where(n => !used.Contains(n)).ToList())
            schemas.Remove(name);
    }
}

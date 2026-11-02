using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

/// <summary>Builds Graph field modifiers such as <c>.phone_numbers(["1","2"])</c>.</summary>
internal static class WhatsAppFieldModifiers
{
    public static string List(string name, IEnumerable<string>? values, bool quote = true)
    {
        var list = values?.Where(v => !string.IsNullOrWhiteSpace(v)).ToList();
        if (list == null || list.Count == 0) return string.Empty;
        var items = quote ? list.Select(v => $"\"{v}\"") : list;
        return $".{name}([{string.Join(",", items)}])";
    }
}

/// <summary>Formats list query parameters the way the Graph API expects them (JSON arrays).</summary>
internal static class WhatsAppQuery
{
    public static string JsonArray(IEnumerable<string> values) => JsonConvert.SerializeObject(values);

    public static Dictionary<string, string> Paging(int? limit, string? after, string? before)
    {
        var q = new Dictionary<string, string>();
        if (limit.HasValue) q["limit"] = limit.Value.ToString();
        if (!string.IsNullOrEmpty(after)) q["after"] = after;
        if (!string.IsNullOrEmpty(before)) q["before"] = before;
        return q;
    }
}

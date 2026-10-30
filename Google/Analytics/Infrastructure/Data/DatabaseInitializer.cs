using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace ApiToolKit.Infrastructure.Data;

/// <summary>
/// Runs the module's Database\*.sql scripts on startup, so the database, tables and stored procedures
/// are created without a manual sqlcmd step. The scripts are idempotent, so running them on every start is safe.
/// Set "Database:AutoInitialize": false in appsettings to turn this off.
/// </summary>
public static class DatabaseInitializer
{
    private static readonly Regex GoSeparator = new(@"^\s*GO\s*;?\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

    /// <param name="services">Application services.</param>
    /// <param name="scriptDatabaseName">Database name written in the scripts; replaced when the connection string names another database.</param>
    public static void InitializeDatabase(this IServiceProvider services, string scriptDatabaseName)
    {
        var configuration = services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(DatabaseInitializer));
        if (string.Equals(configuration["Database:AutoInitialize"], "false", StringComparison.OrdinalIgnoreCase))
            return;

        var scriptsFolder = Path.Combine(AppContext.BaseDirectory, "Database");
        if (!Directory.Exists(scriptsFolder))
        {
            logger.LogWarning("No Database folder found at {Folder}; skipping database initialization.", scriptsFolder);
            return;
        }

        var target = new SqlConnectionStringBuilder(configuration.GetConnectionString("DefaultConnection"));
        var databaseName = string.IsNullOrEmpty(target.InitialCatalog) ? scriptDatabaseName : target.InitialCatalog;
        var master = new SqlConnectionStringBuilder(target.ConnectionString) { InitialCatalog = "master" };

        try
        {
            using var connection = new SqlConnection(master.ConnectionString);
            connection.Open();

            foreach (var file in Directory.GetFiles(scriptsFolder, "*.sql").OrderBy(f => f, StringComparer.Ordinal))
            {
                var script = File.ReadAllText(file).Replace(scriptDatabaseName, databaseName);
                foreach (var batch in GoSeparator.Split(script).Where(b => !string.IsNullOrWhiteSpace(b)))
                {
                    using var command = new SqlCommand(batch, connection) { CommandTimeout = 120 };
                    command.ExecuteNonQuery();
                }
                logger.LogInformation("Database script {Script} applied to {Database}.", Path.GetFileName(file), databaseName);
            }
        }
        catch (Exception ex) when (ex is SqlException or IOException or InvalidOperationException)
        {
            // Keep the API running (Swagger, auth URL) and say exactly what to fix.
            logger.LogError(ex, "Database initialization failed for {Database}. Check ConnectionStrings:DefaultConnection.", databaseName);
        }
    }
}

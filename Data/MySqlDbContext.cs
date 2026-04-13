using MySqlConnector;

namespace management_kos.Data;

public class MySqlDbContext
{
    private readonly string _databaseName;
    private readonly string _serverConnectionString;
    private readonly int _commandTimeoutSeconds;

    public MySqlDbContext()
    {
        var env = LoadEnvFile();

        var server = GetEnvValue(env, "DB_HOST", "localhost");
        var port = GetEnvValue(env, "DB_PORT", "3306");
        var user = GetEnvValue(env, "DB_USER", "root");
        var password = GetEnvValue(env, "DB_PASSWORD", string.Empty);
        _databaseName = GetEnvValue(env, "DB_NAME", "management_kos");
        _commandTimeoutSeconds = GetPositiveIntEnvValue(env, "DB_COMMAND_TIMEOUT_SECONDS", 30);

        _serverConnectionString = $"Server={server};Port={port};User ID={user};Password={password};";
    }

    public MySqlConnection CreateConnection()
    {
        var connection = new MySqlConnection($"{_serverConnectionString}Database={_databaseName};");
        connection.Open();
        return connection;
    }

    public int GetCommandTimeoutSeconds()
    {
        return _commandTimeoutSeconds;
    }

    public void InitializeDatabase()
    {
        EnsureDatabaseExists();
        using var connection = CreateConnection();
        EnsureMigrationTable(connection);
        ApplyMigrations(connection);
    }

    private void EnsureDatabaseExists()
    {
        using var serverConnection = new MySqlConnection(_serverConnectionString);
        serverConnection.Open();
        using var createDbCommand = serverConnection.CreateCommand();
        createDbCommand.CommandText = $"CREATE DATABASE IF NOT EXISTS `{_databaseName}`;";
        createDbCommand.ExecuteNonQuery();
    }

    private static void EnsureMigrationTable(MySqlConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS SchemaMigrations (
                MigrationId VARCHAR(150) NOT NULL PRIMARY KEY,
                AppliedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
            ) ENGINE=InnoDB;";
        command.ExecuteNonQuery();
    }

    private void ApplyMigrations(MySqlConnection connection)
    {
        var appliedMigrations = GetAppliedMigrationIds(connection);

        foreach (var migrationFile in GetMigrationFiles())
        {
            var migrationId = Path.GetFileNameWithoutExtension(migrationFile);
            if (string.IsNullOrWhiteSpace(migrationId) || appliedMigrations.Contains(migrationId))
            {
                continue;
            }

            var sql = File.ReadAllText(migrationFile);
            if (string.IsNullOrWhiteSpace(sql))
            {
                continue;
            }

            using var transaction = connection.BeginTransaction();

            using var migrationCommand = connection.CreateCommand();
            migrationCommand.Transaction = transaction;
            migrationCommand.CommandText = sql;
            migrationCommand.ExecuteNonQuery();

            using var insertCommand = connection.CreateCommand();
            insertCommand.Transaction = transaction;
            insertCommand.CommandText = "INSERT INTO SchemaMigrations (MigrationId) VALUES (@MigrationId);";
            insertCommand.Parameters.AddWithValue("@MigrationId", migrationId);
            insertCommand.ExecuteNonQuery();

            transaction.Commit();
        }
    }

    private static HashSet<string> GetAppliedMigrationIds(MySqlConnection connection)
    {
        var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT MigrationId FROM SchemaMigrations;";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(reader.GetString(0));
        }

        return result;
    }

    private List<string> GetMigrationFiles()
    {
        var migrationsDirectory = FindMigrationsDirectoryPath();
        if (migrationsDirectory is null)
        {
            return new List<string>();
        }

        return Directory
            .GetFiles(migrationsDirectory, "*.sql", SearchOption.TopDirectoryOnly)
            .OrderBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private static string? FindMigrationsDirectoryPath()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            var candidate = Path.Combine(directory.FullName, "Data", "Migrations");
            if (Directory.Exists(candidate))
            {
                return candidate;
            }

            directory = directory.Parent;
        }

        var workingDirCandidate = Path.Combine(Environment.CurrentDirectory, "Data", "Migrations");
        return Directory.Exists(workingDirCandidate) ? workingDirCandidate : null;
    }

    private static Dictionary<string, string> LoadEnvFile()
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        var envPath = FindEnvFilePath();
        if (envPath is null)
        {
            return result;
        }

        foreach (var line in File.ReadAllLines(envPath))
        {
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith('#'))
            {
                continue;
            }

            var separatorIndex = line.IndexOf('=');
            if (separatorIndex <= 0)
            {
                continue;
            }

            var key = line[..separatorIndex].Trim();
            var value = line[(separatorIndex + 1)..].Trim().Trim('"');

            if (!string.IsNullOrWhiteSpace(key))
            {
                result[key] = value;
            }
        }

        return result;
    }

    private static string? FindEnvFilePath()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            var candidate = Path.Combine(directory.FullName, ".env");
            if (File.Exists(candidate))
            {
                return candidate;
            }

            directory = directory.Parent;
        }

        var workingDirCandidate = Path.Combine(Environment.CurrentDirectory, ".env");
        if (File.Exists(workingDirCandidate))
        {
            return workingDirCandidate;
        }

        return null;
    }

    private static string GetEnvValue(Dictionary<string, string> env, string key, string defaultValue)
    {
        if (Environment.GetEnvironmentVariable(key) is { Length: > 0 } osValue)
        {
            return osValue;
        }

        return env.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value)
            ? value
            : defaultValue;
    }

    private static int GetPositiveIntEnvValue(Dictionary<string, string> env, string key, int defaultValue)
    {
        var raw = GetEnvValue(env, key, defaultValue.ToString());
        return int.TryParse(raw, out var parsed) && parsed > 0
            ? parsed
            : defaultValue;
    }
}

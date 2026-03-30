using MySqlConnector;

namespace management_kos.Data;

public class MySqlDbContext
{
    private readonly string _databaseName;
    private readonly string _serverConnectionString;

    public MySqlDbContext()
    {
        var env = LoadEnvFile();

        var server = GetEnvValue(env, "DB_HOST", "localhost");
        var port = GetEnvValue(env, "DB_PORT", "3306");
        var user = GetEnvValue(env, "DB_USER", "root");
        var password = GetEnvValue(env, "DB_PASSWORD", string.Empty);
        _databaseName = GetEnvValue(env, "DB_NAME", "management_kos");

        _serverConnectionString = $"Server={server};Port={port};User ID={user};Password={password};";
    }

    public MySqlConnection CreateConnection()
    {
        var connection = new MySqlConnection($"{_serverConnectionString}Database={_databaseName};");
        connection.Open();
        return connection;
    }

    public void InitializeDatabase()
    {
        using (var serverConnection = new MySqlConnection(_serverConnectionString))
        {
            serverConnection.Open();
            using var createDbCommand = serverConnection.CreateCommand();
            createDbCommand.CommandText = $"CREATE DATABASE IF NOT EXISTS `{_databaseName}`;";
            createDbCommand.ExecuteNonQuery();
        }

        using var connection = CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Kos (
                Id INT AUTO_INCREMENT PRIMARY KEY,
                NamaKos VARCHAR(200) NOT NULL,
                Alamat VARCHAR(300) NOT NULL,
                HargaDasar DECIMAL(18,2) NOT NULL,
                JumlahKamar INT NOT NULL,
                NamaPemilik VARCHAR(200) NOT NULL,
                NomorTelepon VARCHAR(30) NOT NULL,
                Catatan TEXT NULL
            ) ENGINE=InnoDB;

            CREATE TABLE IF NOT EXISTS Kamar (
                Id INT AUTO_INCREMENT PRIMARY KEY,
                KosId INT NOT NULL,
                NomorKamar VARCHAR(50) NOT NULL,
                Status VARCHAR(30) NOT NULL,
                CONSTRAINT FK_Kamar_Kos FOREIGN KEY (KosId) REFERENCES Kos(Id) ON DELETE CASCADE
            ) ENGINE=InnoDB;
        ";

        command.ExecuteNonQuery();
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
}

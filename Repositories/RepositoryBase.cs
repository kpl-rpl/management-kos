using management_kos.Data;
using MySqlConnector;

namespace management_kos.Repositories;

public abstract class RepositoryBase
{
    private readonly MySqlDbContext _dbContext;

    protected RepositoryBase(MySqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected List<T> QueryList<T>(
        string sql,
        Func<MySqlDataReader, T> map,
        Action<MySqlCommand>? configureParameters = null)
    {
        var result = new List<T>();

        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        configureParameters?.Invoke(command);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(map(reader));
        }

        return result;
    }

    protected T? QuerySingle<T>(
        string sql,
        Func<MySqlDataReader, T> map,
        Action<MySqlCommand>? configureParameters = null)
    {
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        configureParameters?.Invoke(command);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return default;
        }

        return map(reader);
    }

    protected int Execute(string sql, Action<MySqlCommand>? configureParameters = null)
    {
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        configureParameters?.Invoke(command);
        return command.ExecuteNonQuery();
    }
}

using management_kos.Data;
using management_kos.Models;
using MySqlConnector;

namespace management_kos.Repositories;

public class MetodePembayaranRefRepository : RepositoryBase, IMetodePembayaranRefRepository
{
    public MetodePembayaranRefRepository(MySqlDbContext dbContext) : base(dbContext)
    {
    }

    public List<MetodePembayaranRef> GetAll()
    {
        return QueryList(@"
            SELECT Id, NamaMetode, IsActive
            FROM MetodePembayaranRef
            WHERE IsActive = 1
            ORDER BY NamaMetode;", Map);
    }

    public MetodePembayaranRef? GetById(int id)
    {
        return QuerySingle(@"
            SELECT Id, NamaMetode, IsActive
            FROM MetodePembayaranRef
            WHERE Id = @Id;",
            Map,
            command => command.Parameters.AddWithValue("@Id", id));
    }

    public MetodePembayaranRef? GetByName(string namaMetode)
    {
        return QuerySingle(@"
            SELECT Id, NamaMetode, IsActive
            FROM MetodePembayaranRef
            WHERE NamaMetode = @NamaMetode;",
            Map,
            command => command.Parameters.AddWithValue("@NamaMetode", namaMetode));
    }

    public void Insert(MetodePembayaranRef metode)
    {
        Execute(@"
            INSERT INTO MetodePembayaranRef (NamaMetode, IsActive)
            VALUES (@NamaMetode, @IsActive);",
            command =>
            {
                command.Parameters.AddWithValue("@NamaMetode", metode.NamaMetode);
                command.Parameters.AddWithValue("@IsActive", metode.IsActive);
            });
    }

    public void Update(MetodePembayaranRef metode)
    {
        Execute(@"
            UPDATE MetodePembayaranRef
            SET NamaMetode = @NamaMetode,
                IsActive = @IsActive
            WHERE Id = @Id;",
            command =>
            {
                command.Parameters.AddWithValue("@Id", metode.Id);
                command.Parameters.AddWithValue("@NamaMetode", metode.NamaMetode);
                command.Parameters.AddWithValue("@IsActive", metode.IsActive);
            });
    }

    public void Delete(int id)
    {
        Execute("UPDATE MetodePembayaranRef SET IsActive = 0 WHERE Id = @Id;",
            command => command.Parameters.AddWithValue("@Id", id));
    }

    private static MetodePembayaranRef Map(MySqlDataReader reader)
    {
        return new MetodePembayaranRef
        {
            Id = reader.GetInt32(0),
            NamaMetode = reader.GetString(1),
            IsActive = reader.GetBoolean(2)
        };
    }
}

using management_kos.Data;
using management_kos.Models;

namespace management_kos.Repositories;

public class KosRepository : IKosRepository
{
    private readonly MySqlDbContext _dbContext;

    public KosRepository(MySqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Kos> GetAll()
    {
        var result = new List<Kos>();

        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, NamaKos, Alamat, HargaDasar, JumlahKamar, NamaPemilik, NomorTelepon, Catatan
            FROM Kos
            ORDER BY Id DESC;";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(new Kos
            {
                Id = reader.GetInt32(0),
                NamaKos = reader.GetString(1),
                Alamat = reader.GetString(2),
                HargaDasar = reader.GetDecimal(3),
                JumlahKamar = reader.GetInt32(4),
                NamaPemilik = reader.GetString(5),
                NomorTelepon = reader.GetString(6),
                Catatan = reader.IsDBNull(7) ? null : reader.GetString(7)
            });
        }

        return result;
    }

    public Kos? GetById(int id)
    {
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, NamaKos, Alamat, HargaDasar, JumlahKamar, NamaPemilik, NomorTelepon, Catatan
            FROM Kos
            WHERE Id = @Id;";
        command.Parameters.AddWithValue("@Id", id);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return new Kos
        {
            Id = reader.GetInt32(0),
            NamaKos = reader.GetString(1),
            Alamat = reader.GetString(2),
            HargaDasar = reader.GetDecimal(3),
            JumlahKamar = reader.GetInt32(4),
            NamaPemilik = reader.GetString(5),
            NomorTelepon = reader.GetString(6),
            Catatan = reader.IsDBNull(7) ? null : reader.GetString(7)
        };
    }

    public void Insert(Kos kos)
    {
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Kos (NamaKos, Alamat, HargaDasar, JumlahKamar, NamaPemilik, NomorTelepon, Catatan)
            VALUES (@NamaKos, @Alamat, @HargaDasar, @JumlahKamar, @NamaPemilik, @NomorTelepon, @Catatan);";

        command.Parameters.AddWithValue("@NamaKos", kos.NamaKos);
        command.Parameters.AddWithValue("@Alamat", kos.Alamat);
        command.Parameters.AddWithValue("@HargaDasar", kos.HargaDasar);
        command.Parameters.AddWithValue("@JumlahKamar", kos.JumlahKamar);
        command.Parameters.AddWithValue("@NamaPemilik", kos.NamaPemilik);
        command.Parameters.AddWithValue("@NomorTelepon", kos.NomorTelepon);
        command.Parameters.AddWithValue("@Catatan", (object?)kos.Catatan ?? DBNull.Value);

        command.ExecuteNonQuery();
    }

    public void Update(Kos kos)
    {
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE Kos
            SET NamaKos = @NamaKos,
                Alamat = @Alamat,
                HargaDasar = @HargaDasar,
                JumlahKamar = @JumlahKamar,
                NamaPemilik = @NamaPemilik,
                NomorTelepon = @NomorTelepon,
                Catatan = @Catatan
            WHERE Id = @Id;";

        command.Parameters.AddWithValue("@Id", kos.Id);
        command.Parameters.AddWithValue("@NamaKos", kos.NamaKos);
        command.Parameters.AddWithValue("@Alamat", kos.Alamat);
        command.Parameters.AddWithValue("@HargaDasar", kos.HargaDasar);
        command.Parameters.AddWithValue("@JumlahKamar", kos.JumlahKamar);
        command.Parameters.AddWithValue("@NamaPemilik", kos.NamaPemilik);
        command.Parameters.AddWithValue("@NomorTelepon", kos.NomorTelepon);
        command.Parameters.AddWithValue("@Catatan", (object?)kos.Catatan ?? DBNull.Value);

        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Kos WHERE Id = @Id;";
        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();
    }
}

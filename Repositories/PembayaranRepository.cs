using management_kos.Data;
using management_kos.Models;
using MySqlConnector;

namespace management_kos.Repositories;

public class PembayaranRepository : IPembayaranRepository
{
    private readonly MySqlDbContext _dbContext;

    public PembayaranRepository(MySqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Pembayaran> GetAll()
    {
        var result = new List<Pembayaran>();

        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, KontrakSewaId, Periode, TanggalBayar, JumlahTagihan, JumlahDibayar, MetodePembayaran, Status, Catatan
            FROM Pembayaran
            ORDER BY Id DESC;";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(Map(reader));
        }

        return result;
    }

    public Pembayaran? GetById(int id)
    {
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, KontrakSewaId, Periode, TanggalBayar, JumlahTagihan, JumlahDibayar, MetodePembayaran, Status, Catatan
            FROM Pembayaran
            WHERE Id = @Id;";
        command.Parameters.AddWithValue("@Id", id);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return Map(reader);
    }

    public List<Pembayaran> GetByKontrakSewaId(int kontrakSewaId)
    {
        var result = new List<Pembayaran>();

        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, KontrakSewaId, Periode, TanggalBayar, JumlahTagihan, JumlahDibayar, MetodePembayaran, Status, Catatan
            FROM Pembayaran
            WHERE KontrakSewaId = @KontrakSewaId
            ORDER BY Periode DESC;";
        command.Parameters.AddWithValue("@KontrakSewaId", kontrakSewaId);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(Map(reader));
        }

        return result;
    }

    public void Insert(Pembayaran pembayaran)
    {
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Pembayaran (KontrakSewaId, Periode, TanggalBayar, JumlahTagihan, JumlahDibayar, MetodePembayaran, Status, Catatan)
            VALUES (@KontrakSewaId, @Periode, @TanggalBayar, @JumlahTagihan, @JumlahDibayar, @MetodePembayaran, @Status, @Catatan);";

        command.Parameters.AddWithValue("@KontrakSewaId", pembayaran.KontrakSewaId);
        command.Parameters.AddWithValue("@Periode", pembayaran.Periode);
        command.Parameters.AddWithValue("@TanggalBayar", (object?)pembayaran.TanggalBayar ?? DBNull.Value);
        command.Parameters.AddWithValue("@JumlahTagihan", pembayaran.JumlahTagihan);
        command.Parameters.AddWithValue("@JumlahDibayar", pembayaran.JumlahDibayar);
        command.Parameters.AddWithValue("@MetodePembayaran", pembayaran.MetodePembayaran);
        command.Parameters.AddWithValue("@Status", pembayaran.Status);
        command.Parameters.AddWithValue("@Catatan", (object?)pembayaran.Catatan ?? DBNull.Value);

        command.ExecuteNonQuery();
    }

    public void Update(Pembayaran pembayaran)
    {
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE Pembayaran
            SET KontrakSewaId = @KontrakSewaId,
                Periode = @Periode,
                TanggalBayar = @TanggalBayar,
                JumlahTagihan = @JumlahTagihan,
                JumlahDibayar = @JumlahDibayar,
                MetodePembayaran = @MetodePembayaran,
                Status = @Status,
                Catatan = @Catatan
            WHERE Id = @Id;";

        command.Parameters.AddWithValue("@Id", pembayaran.Id);
        command.Parameters.AddWithValue("@KontrakSewaId", pembayaran.KontrakSewaId);
        command.Parameters.AddWithValue("@Periode", pembayaran.Periode);
        command.Parameters.AddWithValue("@TanggalBayar", (object?)pembayaran.TanggalBayar ?? DBNull.Value);
        command.Parameters.AddWithValue("@JumlahTagihan", pembayaran.JumlahTagihan);
        command.Parameters.AddWithValue("@JumlahDibayar", pembayaran.JumlahDibayar);
        command.Parameters.AddWithValue("@MetodePembayaran", pembayaran.MetodePembayaran);
        command.Parameters.AddWithValue("@Status", pembayaran.Status);
        command.Parameters.AddWithValue("@Catatan", (object?)pembayaran.Catatan ?? DBNull.Value);

        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Pembayaran WHERE Id = @Id;";
        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();
    }

    private static Pembayaran Map(MySqlDataReader reader)
    {
        return new Pembayaran
        {
            Id = reader.GetInt32(0),
            KontrakSewaId = reader.GetInt32(1),
            Periode = reader.GetString(2),
            TanggalBayar = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
            JumlahTagihan = reader.GetDecimal(4),
            JumlahDibayar = reader.GetDecimal(5),
            MetodePembayaran = reader.GetString(6),
            Status = reader.GetString(7),
            Catatan = reader.IsDBNull(8) ? null : reader.GetString(8)
        };
    }
}

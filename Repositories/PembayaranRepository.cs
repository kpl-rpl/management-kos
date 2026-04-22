using management_kos.Data;
using management_kos.Models;
using MySqlConnector;

namespace management_kos.Repositories;

public class PembayaranRepository : RepositoryBase, IPembayaranRepository
{
    public PembayaranRepository(MySqlDbContext dbContext)
        : base(dbContext)
    {
    }

    public List<Pembayaran> GetAll()
    {
        return QueryList(@"
            SELECT Id, KontrakSewaId, Periode, TanggalBayar, JumlahTagihan, JumlahDibayar, MetodePembayaran, Status, Catatan
            FROM Pembayaran
            ORDER BY Id DESC;", Map);
    }

    public Pembayaran? GetById(int id)
    {
        return QuerySingle(@"
            SELECT Id, KontrakSewaId, Periode, TanggalBayar, JumlahTagihan, JumlahDibayar, MetodePembayaran, Status, Catatan
            FROM Pembayaran
            WHERE Id = @Id;",
            Map,
            command => command.Parameters.AddWithValue("@Id", id));
    }

    public List<Pembayaran> GetByKontrakSewaId(int kontrakSewaId)
    {
        return QueryList(@"
            SELECT Id, KontrakSewaId, Periode, TanggalBayar, JumlahTagihan, JumlahDibayar, MetodePembayaran, Status, Catatan
            FROM Pembayaran
            WHERE KontrakSewaId = @KontrakSewaId
            ORDER BY Periode DESC;",
            Map,
            command => command.Parameters.AddWithValue("@KontrakSewaId", kontrakSewaId));
    }

    public void Insert(Pembayaran pembayaran)
    {
        Execute(@"
            INSERT INTO Pembayaran (KontrakSewaId, Periode, TanggalBayar, JumlahTagihan, JumlahDibayar, MetodePembayaran, Status, Catatan)
            VALUES (@KontrakSewaId, @Periode, @TanggalBayar, @JumlahTagihan, @JumlahDibayar, @MetodePembayaran, @Status, @Catatan);",
            command =>
            {
                command.Parameters.AddWithValue("@KontrakSewaId", pembayaran.KontrakSewaId);
                command.Parameters.AddWithValue("@Periode", pembayaran.Periode);
                command.Parameters.AddWithValue("@TanggalBayar", (object?)pembayaran.TanggalBayar ?? DBNull.Value);
                command.Parameters.AddWithValue("@JumlahTagihan", pembayaran.JumlahTagihan);
                command.Parameters.AddWithValue("@JumlahDibayar", pembayaran.JumlahDibayar);
                command.Parameters.AddWithValue("@MetodePembayaran", pembayaran.MetodePembayaran);
                command.Parameters.AddWithValue("@Status", pembayaran.Status);
                command.Parameters.AddWithValue("@Catatan", (object?)pembayaran.Catatan ?? DBNull.Value);
            });
    }

    public void Update(Pembayaran pembayaran)
    {
        Execute(@"
            UPDATE Pembayaran
            SET KontrakSewaId = @KontrakSewaId,
                Periode = @Periode,
                TanggalBayar = @TanggalBayar,
                JumlahTagihan = @JumlahTagihan,
                JumlahDibayar = @JumlahDibayar,
                MetodePembayaran = @MetodePembayaran,
                Status = @Status,
                Catatan = @Catatan
            WHERE Id = @Id;",
            command =>
            {
                command.Parameters.AddWithValue("@Id", pembayaran.Id);
                command.Parameters.AddWithValue("@KontrakSewaId", pembayaran.KontrakSewaId);
                command.Parameters.AddWithValue("@Periode", pembayaran.Periode);
                command.Parameters.AddWithValue("@TanggalBayar", (object?)pembayaran.TanggalBayar ?? DBNull.Value);
                command.Parameters.AddWithValue("@JumlahTagihan", pembayaran.JumlahTagihan);
                command.Parameters.AddWithValue("@JumlahDibayar", pembayaran.JumlahDibayar);
                command.Parameters.AddWithValue("@MetodePembayaran", pembayaran.MetodePembayaran);
                command.Parameters.AddWithValue("@Status", pembayaran.Status);
                command.Parameters.AddWithValue("@Catatan", (object?)pembayaran.Catatan ?? DBNull.Value);
            });
    }

    public void Delete(int id)
    {
        Execute(
            "DELETE FROM Pembayaran WHERE Id = @Id;",
            command => command.Parameters.AddWithValue("@Id", id));
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

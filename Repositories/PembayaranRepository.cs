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
        try
        {
            return QueryList(@"
                SELECT Id, KontrakSewaId, Periode, TanggalBayar, JumlahTagihan, JumlahDibayar, MetodePembayaran, Status, Catatan
                FROM Pembayaran
                ORDER BY Id DESC;", Map) ?? new List<Pembayaran>();
        }
        catch (Exception)
        {
            // Log exception jika ada mekanisme logging
            return new List<Pembayaran>();
        }
    }

    public Pembayaran? GetById(int id)
    {
        if (id <= 0) return null;
        try
        {
            return QuerySingle(@"
                SELECT Id, KontrakSewaId, Periode, TanggalBayar, JumlahTagihan, JumlahDibayar, MetodePembayaran, Status, Catatan
                FROM Pembayaran
                WHERE Id = @Id;",
                Map,
                command => command.Parameters.AddWithValue("@Id", id));
        }
        catch (Exception)
        {
            // Log exception jika ada mekanisme logging
            return null;
        }
    }

    public List<Pembayaran> GetByKontrakSewaId(int kontrakSewaId)
    {
        if (kontrakSewaId <= 0) return new List<Pembayaran>();
        try
        {
            return QueryList(@"
                SELECT Id, KontrakSewaId, Periode, TanggalBayar, JumlahTagihan, JumlahDibayar, MetodePembayaran, Status, Catatan
                FROM Pembayaran
                WHERE KontrakSewaId = @KontrakSewaId
                ORDER BY Periode DESC;",
                Map,
                command => command.Parameters.AddWithValue("@KontrakSewaId", kontrakSewaId)) ?? new List<Pembayaran>();
        }
        catch (Exception)
        {
            // Log exception jika ada mekanisme logging
            return new List<Pembayaran>();
        }
    }

    public void Insert(Pembayaran pembayaran)
    {
        if (pembayaran == null)
            throw new ArgumentNullException(nameof(pembayaran));
        if (pembayaran.KontrakSewaId <= 0)
            throw new ArgumentException("KontrakSewaId harus lebih dari 0", nameof(pembayaran.KontrakSewaId));
        if (string.IsNullOrWhiteSpace(pembayaran.Periode))
            throw new ArgumentException("Periode tidak boleh kosong", nameof(pembayaran.Periode));
        if (string.IsNullOrWhiteSpace(pembayaran.MetodePembayaran))
            throw new ArgumentException("MetodePembayaran tidak boleh kosong", nameof(pembayaran.MetodePembayaran));
        if (string.IsNullOrWhiteSpace(pembayaran.Status))
            throw new ArgumentException("Status tidak boleh kosong", nameof(pembayaran.Status));
        try
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
        catch (Exception)
        {
            // Log exception jika ada mekanisme logging
            throw;
        }
    }

    public void Update(Pembayaran pembayaran)
    {
        if (pembayaran == null)
            throw new ArgumentNullException(nameof(pembayaran));
        if (pembayaran.Id <= 0)
            throw new ArgumentException("Id harus lebih dari 0", nameof(pembayaran.Id));
        if (pembayaran.KontrakSewaId <= 0)
            throw new ArgumentException("KontrakSewaId harus lebih dari 0", nameof(pembayaran.KontrakSewaId));
        if (string.IsNullOrWhiteSpace(pembayaran.Periode))
            throw new ArgumentException("Periode tidak boleh kosong", nameof(pembayaran.Periode));
        if (string.IsNullOrWhiteSpace(pembayaran.MetodePembayaran))
            throw new ArgumentException("MetodePembayaran tidak boleh kosong", nameof(pembayaran.MetodePembayaran));
        if (string.IsNullOrWhiteSpace(pembayaran.Status))
            throw new ArgumentException("Status tidak boleh kosong", nameof(pembayaran.Status));
        try
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
        catch (Exception)
        {
            // Log exception jika ada mekanisme logging
            throw;
        }
    }

    public void Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Id harus lebih dari 0", nameof(id));
        try
        {
            Execute(
                "DELETE FROM Pembayaran WHERE Id = @Id;",
                command => command.Parameters.AddWithValue("@Id", id));
        }
        catch (Exception)
        {
            // Log exception jika ada mekanisme logging
            throw;
        }
    }

    private static Pembayaran Map(MySqlDataReader reader)
    {
        try
        {
            return new Pembayaran
            {
                Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                KontrakSewaId = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
                Periode = !reader.IsDBNull(2) ? reader.GetString(2) : string.Empty,
                TanggalBayar = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                JumlahTagihan = !reader.IsDBNull(4) ? reader.GetDecimal(4) : 0,
                JumlahDibayar = !reader.IsDBNull(5) ? reader.GetDecimal(5) : 0,
                MetodePembayaran = !reader.IsDBNull(6) ? reader.GetString(6) : string.Empty,
                Status = !reader.IsDBNull(7) ? reader.GetString(7) : string.Empty,
                Catatan = reader.IsDBNull(8) ? null : reader.GetString(8)
            };
        }
        catch (Exception)
        {
            // Log exception jika ada mekanisme logging
            return new Pembayaran();
        }
    }
}

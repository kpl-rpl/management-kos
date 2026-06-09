using management_kos.Data;
using management_kos.Models;
using MySqlConnector;
using System;

namespace management_kos.Repositories;

public class KontrakSewaRepository : RepositoryBase, IKontrakSewaRepository
{
    // Table-driven: tabel pemetaan nama kolom ke indeks posisi dalam hasil SELECT.
    // Jika urutan kolom berubah, cukup perbarui tabel ini — tidak perlu ubah Map().
    private static readonly Dictionary<string, int> Col = new()
    {
        { "Id",               0 },
        { "PenghuniId",       1 },
        { "KamarId",          2 },
        { "TanggalMulai",     3 },
        { "TanggalSelesai",   4 },
        { "HargaSewaBulanan", 5 },
        { "Deposit",          6 },
        { "Status",           7 },
        { "Catatan",          8 },
    };

    private const string SelectColumns =
        "Id, PenghuniId, KamarId, TanggalMulai, TanggalSelesai, HargaSewaBulanan, Deposit, Status, Catatan";

    public KontrakSewaRepository(MySqlDbContext dbContext) : base(dbContext) { }

    public List<KontrakSewa> GetAll() =>
        QueryList($"SELECT {SelectColumns} FROM KontrakSewa ORDER BY Id DESC;", Map);

    public KontrakSewa? GetById(int id) =>
        QuerySingle(
            $"SELECT {SelectColumns} FROM KontrakSewa WHERE Id = @Id;",
            Map,
            cmd => cmd.Parameters.AddWithValue("@Id", id));

    public List<KontrakSewa> GetByPenghuniId(int penghuniId) =>
        QueryList(
            $"SELECT {SelectColumns} FROM KontrakSewa WHERE PenghuniId = @PenghuniId ORDER BY Id DESC;",
            Map,
            cmd => cmd.Parameters.AddWithValue("@PenghuniId", penghuniId));

    public List<KontrakSewa> GetByKamarId(int kamarId) =>
        QueryList(
            $"SELECT {SelectColumns} FROM KontrakSewa WHERE KamarId = @KamarId ORDER BY Id DESC;",
            Map,
            cmd => cmd.Parameters.AddWithValue("@KamarId", kamarId));

    public List<KontrakSewa> GetByStatus(string status) =>
        QueryList(
            $"SELECT {SelectColumns} FROM KontrakSewa WHERE Status = @Status ORDER BY Id DESC;",
            Map,
            cmd => cmd.Parameters.AddWithValue("@Status", status));

    public void Insert(KontrakSewa k)
    {
        var newId = ExecuteAndGetLastInsertedId(@"
            INSERT INTO KontrakSewa
                (PenghuniId, KamarId, TanggalMulai, TanggalSelesai, HargaSewaBulanan, Deposit, Status, Catatan)
            VALUES
                (@PenghuniId, @KamarId, @TanggalMulai, @TanggalSelesai, @HargaSewaBulanan, @Deposit, @Status, @Catatan);",
            cmd => BindParams(cmd, k));

        k.Id = Convert.ToInt32(newId);
    }

    public void Update(KontrakSewa k) =>
        Execute(@"
            UPDATE KontrakSewa
            SET PenghuniId       = @PenghuniId,
                KamarId          = @KamarId,
                TanggalMulai     = @TanggalMulai,
                TanggalSelesai   = @TanggalSelesai,
                HargaSewaBulanan = @HargaSewaBulanan,
                Deposit          = @Deposit,
                Status           = @Status,
                Catatan          = @Catatan
            WHERE Id = @Id;",
            cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", k.Id);
                BindParams(cmd, k);
            });

    public void Delete(int id) =>
        Execute("DELETE FROM KontrakSewa WHERE Id = @Id;",
            cmd => cmd.Parameters.AddWithValue("@Id", id));

    // Semua binding parameter dikelola di satu tempat (table-driven parameter binding).
    private static void BindParams(MySqlCommand cmd, KontrakSewa k)
    {
        cmd.Parameters.AddWithValue("@PenghuniId", k.PenghuniId);
        cmd.Parameters.AddWithValue("@KamarId", k.KamarId);
        cmd.Parameters.AddWithValue("@TanggalMulai", k.TanggalMulai.Date);
        cmd.Parameters.AddWithValue("@TanggalSelesai", k.TanggalSelesai.Date);
        cmd.Parameters.AddWithValue("@HargaSewaBulanan", k.HargaSewaBulanan);
        cmd.Parameters.AddWithValue("@Deposit", (object?)k.Deposit ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Status", k.Status.ToString());
        cmd.Parameters.AddWithValue("@Catatan", (object?)k.Catatan ?? DBNull.Value);
    }

    // Mapping dikontrol oleh tabel Col — urutan kolom tidak perlu diingat per-field.
    private static KontrakSewa Map(MySqlDataReader r) => new()
    {
        Id               = r.GetInt32(Col["Id"]),
        PenghuniId       = r.GetInt32(Col["PenghuniId"]),
        KamarId          = r.GetInt32(Col["KamarId"]),
        TanggalMulai     = r.GetDateTime(Col["TanggalMulai"]),
        TanggalSelesai   = r.GetDateTime(Col["TanggalSelesai"]),
        HargaSewaBulanan = r.GetDecimal(Col["HargaSewaBulanan"]),
        Deposit          = r.IsDBNull(Col["Deposit"]) ? null : r.GetDecimal(Col["Deposit"]),
        Status           = Enum.TryParse(r.GetString(Col["Status"]), true, out KontrakStatus parsedStatus)
                           ? parsedStatus
                           : KontrakStatus.Aktif,
        Catatan          = r.IsDBNull(Col["Catatan"]) ? null : r.GetString(Col["Catatan"]),
    };
}

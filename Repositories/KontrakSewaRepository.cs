using management_kos.Data;
using management_kos.Models;
using MySqlConnector;

namespace management_kos.Repositories;

public class KontrakSewaRepository : RepositoryBase, IKontrakSewaRepository
{
    private const string SelectColumns = @"
        ks.Id,
        ks.PenghuniId,
        ks.KamarId,
        ks.TanggalMulai,
        ks.TanggalSelesai,
        ks.DurasiBulanInput,
        ks.JumlahBulanTagihan,
        ks.HargaSewaBulanan,
        ks.TotalTagihan,
        ks.Deposit,
        ks.Status,
        ks.Catatan,
        ks.IsActive,
        p.Nama AS NamaPenghuni,
        CONCAT(kos.NamaKos, ' - ', k.NomorKamar) AS InfoKamar";

    private const string FromClause = @"
        FROM KontrakSewa ks
        INNER JOIN Penghuni p ON p.Id = ks.PenghuniId
        INNER JOIN Kamar k ON k.Id = ks.KamarId
        INNER JOIN Kos kos ON kos.Id = k.KosId";

    public KontrakSewaRepository(MySqlDbContext dbContext) : base(dbContext) { }

    public List<KontrakSewa> GetAll() =>
        QueryList($@"
            SELECT {SelectColumns}
            {FromClause}
            WHERE ks.IsActive = 1
            ORDER BY ks.Id DESC;", Map);

    public KontrakSewa? GetById(int id) =>
        QuerySingle(
            $@"
            SELECT {SelectColumns}
            {FromClause}
            WHERE ks.Id = @Id AND ks.IsActive = 1;",
            Map,
            cmd => cmd.Parameters.AddWithValue("@Id", id));

    public List<KontrakSewa> GetByPenghuniId(int penghuniId) =>
        QueryList(
            $@"
            SELECT {SelectColumns}
            {FromClause}
            WHERE ks.PenghuniId = @PenghuniId AND ks.IsActive = 1
            ORDER BY ks.Id DESC;",
            Map,
            cmd => cmd.Parameters.AddWithValue("@PenghuniId", penghuniId));

    public List<KontrakSewa> GetByKamarId(int kamarId) =>
        QueryList(
            $@"
            SELECT {SelectColumns}
            {FromClause}
            WHERE ks.KamarId = @KamarId AND ks.IsActive = 1
            ORDER BY ks.Id DESC;",
            Map,
            cmd => cmd.Parameters.AddWithValue("@KamarId", kamarId));

    public List<KontrakSewa> GetByStatus(string status) =>
        QueryList(
            $@"
            SELECT {SelectColumns}
            {FromClause}
            WHERE ks.Status = @Status AND ks.IsActive = 1
            ORDER BY ks.Id DESC;",
            Map,
            cmd => cmd.Parameters.AddWithValue("@Status", status));

    public List<KontrakSewa> Search(string keyword)
    {
        var trimmed = keyword.Trim();
        if (string.IsNullOrWhiteSpace(trimmed))
        {
            return GetAll();
        }

        return QueryList(
            $@"
            SELECT {SelectColumns}
            {FromClause}
            WHERE ks.IsActive = 1
              AND (
                    CAST(ks.Id AS CHAR) LIKE @Keyword
                 OR p.Nama LIKE @Keyword
                 OR p.NomorTelepon LIKE @Keyword
                 OR k.NomorKamar LIKE @Keyword
                 OR kos.NamaKos LIKE @Keyword
                 OR ks.Status LIKE @Keyword
              )
            ORDER BY ks.Id DESC;",
            Map,
            cmd => cmd.Parameters.AddWithValue("@Keyword", $"%{trimmed}%"));
    }

    public void Insert(KontrakSewa k)
    {
        var newId = ExecuteAndGetLastInsertedId(@"
            INSERT INTO KontrakSewa
                (PenghuniId, KamarId, TanggalMulai, TanggalSelesai, DurasiBulanInput,
                 JumlahBulanTagihan, HargaSewaBulanan, TotalTagihan, Deposit, Status, Catatan)
            VALUES
                (@PenghuniId, @KamarId, @TanggalMulai, @TanggalSelesai, @DurasiBulanInput,
                 @JumlahBulanTagihan, @HargaSewaBulanan, @TotalTagihan, @Deposit, @Status, @Catatan);",
            cmd => BindParams(cmd, k));

        k.Id = Convert.ToInt32(newId);
    }

    public void Update(KontrakSewa k) =>
        Execute(@"
            UPDATE KontrakSewa
            SET PenghuniId = @PenghuniId,
                KamarId = @KamarId,
                TanggalMulai = @TanggalMulai,
                TanggalSelesai = @TanggalSelesai,
                DurasiBulanInput = @DurasiBulanInput,
                JumlahBulanTagihan = @JumlahBulanTagihan,
                HargaSewaBulanan = @HargaSewaBulanan,
                TotalTagihan = @TotalTagihan,
                Deposit = @Deposit,
                Status = @Status,
                Catatan = @Catatan,
                IsActive = @IsActive
            WHERE Id = @Id;",
            cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", k.Id);
                BindParams(cmd, k);
            });

    public void Delete(int id) =>
        Execute("UPDATE KontrakSewa SET IsActive = 0 WHERE Id = @Id;",
            cmd => cmd.Parameters.AddWithValue("@Id", id));

    private static void BindParams(MySqlCommand cmd, KontrakSewa k)
    {
        cmd.Parameters.AddWithValue("@PenghuniId", k.PenghuniId);
        cmd.Parameters.AddWithValue("@KamarId", k.KamarId);
        cmd.Parameters.AddWithValue("@TanggalMulai", k.TanggalMulai.Date);
        cmd.Parameters.AddWithValue("@TanggalSelesai", k.TanggalSelesai.Date);
        cmd.Parameters.AddWithValue("@DurasiBulanInput", k.DurasiBulanInput);
        cmd.Parameters.AddWithValue("@JumlahBulanTagihan", k.JumlahBulanTagihan);
        cmd.Parameters.AddWithValue("@HargaSewaBulanan", k.HargaSewaBulanan);
        cmd.Parameters.AddWithValue("@TotalTagihan", k.TotalTagihan);
        cmd.Parameters.AddWithValue("@Deposit", (object?)k.Deposit ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Status", k.Status.ToString());
        cmd.Parameters.AddWithValue("@Catatan", (object?)k.Catatan ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@IsActive", k.IsActive);
    }

    private static KontrakSewa Map(MySqlDataReader r) => new()
    {
        Id = r.GetInt32(0),
        PenghuniId = r.GetInt32(1),
        KamarId = r.GetInt32(2),
        TanggalMulai = r.GetDateTime(3),
        TanggalSelesai = r.GetDateTime(4),
        DurasiBulanInput = r.GetDecimal(5),
        JumlahBulanTagihan = r.GetInt32(6),
        HargaSewaBulanan = r.GetDecimal(7),
        TotalTagihan = r.GetDecimal(8),
        Deposit = r.IsDBNull(9) ? null : r.GetDecimal(9),
        Status = Enum.TryParse(r.GetString(10), true, out KontrakStatus parsedStatus)
            ? parsedStatus
            : KontrakStatus.Aktif,
        Catatan = r.IsDBNull(11) ? null : r.GetString(11),
        IsActive = r.GetBoolean(12),
        NamaPenghuni = r.IsDBNull(13) ? null : r.GetString(13),
        InfoKamar = r.IsDBNull(14) ? null : r.GetString(14),
    };
}

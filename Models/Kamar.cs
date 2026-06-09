namespace management_kos.Models;

public class Kamar
{
    public int Id { get; set; }
    public int KosId { get; set; }
    public string NomorKamar { get; set; } = string.Empty;
    public int HargaKamar { get; set; }
    public KamarStatus Status { get; set; } = KamarStatus.Kosong;
    public bool IsActive { get; set; } = true;

    public string? NamaKos { get; set; }

    public string DisplayText =>
        string.IsNullOrWhiteSpace(NamaKos)
            ? NomorKamar
            : $"{NamaKos} - {NomorKamar}";
}

public enum KamarStatus
{
    Kosong,
    Terisi,
    Dipesan,
    Perbaikan
}

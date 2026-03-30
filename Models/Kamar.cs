namespace management_kos.Models;

public class Kamar
{
    public int Id { get; set; }
    public int KosId { get; set; }
    public string NomorKamar { get; set; } = string.Empty;
    public string Status { get; set; } = "Kosong";
}

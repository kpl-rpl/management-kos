namespace management_kos.Models;

public class AppUser
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string NamaLengkap { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string? NamaRole { get; set; }
}

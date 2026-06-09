using System.Security.Cryptography;
using System.Text;
using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services;

public class AppUserService
{
    private readonly IAppUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public AppUserService(IAppUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public List<AppUser> GetAll() => _userRepository.GetAll();

    public AppUser? GetById(int id)
    {
        Guard.Positive(id, "ID User");
        return _userRepository.GetById(id);
    }

    public void TambahUser(AppUser user, string password)
    {
        ArgumentNullException.ThrowIfNull(user);
        Normalize(user);
        Validate(user);
        ValidatePassword(password);
        EnsureRoleExists(user.RoleId);
        EnsureUsernameUnique(user.Username, null);

        user.PasswordHash = HashPassword(password);
        _userRepository.Insert(user);
    }

    public void UbahUser(AppUser user, string? passwordBaru = null)
    {
        ArgumentNullException.ThrowIfNull(user);
        Guard.Positive(user.Id, "ID User");

        var existing = _userRepository.GetById(user.Id)
            ?? throw new ArgumentException("User tidak ditemukan.");

        Normalize(user);
        Validate(user);
        EnsureRoleExists(user.RoleId);
        EnsureUsernameUnique(user.Username, user.Id);

        user.PasswordHash = string.IsNullOrWhiteSpace(passwordBaru)
            ? existing.PasswordHash
            : HashPassword(passwordBaru);

        _userRepository.Update(user);
    }

    public void NonaktifkanUser(int id)
    {
        Guard.Positive(id, "ID User");
        _userRepository.Delete(id);
    }

    public AppUser? Authenticate(string username, string password)
    {
        username = username.Trim();
        var user = _userRepository.GetByUsername(username);
        if (user is null || !user.IsActive)
        {
            return null;
        }

        return user.PasswordHash == HashPassword(password) ? user : null;
    }

    private void EnsureRoleExists(int roleId)
    {
        var role = _roleRepository.GetById(roleId);
        if (role is null || !role.IsActive)
        {
            throw new ArgumentException("Role tidak ditemukan atau tidak aktif.");
        }
    }

    private void EnsureUsernameUnique(string username, int? currentId)
    {
        var existing = _userRepository.GetByUsername(username);
        if (existing is not null && existing.Id != currentId)
        {
            throw new ArgumentException("Username sudah digunakan.");
        }
    }

    private static void Normalize(AppUser user)
    {
        user.Username = user.Username.Trim();
        user.NamaLengkap = user.NamaLengkap.Trim();
    }

    private static void Validate(AppUser user)
    {
        if (user.RoleId <= 0)
        {
            throw new ArgumentException("Role wajib dipilih.");
        }

        if (string.IsNullOrWhiteSpace(user.Username))
        {
            throw new ArgumentException("Username wajib diisi.");
        }

        if (string.IsNullOrWhiteSpace(user.NamaLengkap))
        {
            throw new ArgumentException("Nama lengkap wajib diisi.");
        }
    }

    private static void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
        {
            throw new ArgumentException("Password minimal 6 karakter.");
        }
    }

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }
}

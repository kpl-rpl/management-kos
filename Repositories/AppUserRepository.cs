using management_kos.Data;
using management_kos.Models;
using MySqlConnector;

namespace management_kos.Repositories;

public class AppUserRepository : RepositoryBase, IAppUserRepository
{
    public AppUserRepository(MySqlDbContext dbContext) : base(dbContext)
    {
    }

    public List<AppUser> GetAll()
    {
        return QueryList(@"
            SELECT u.Id, u.RoleId, u.Username, u.PasswordHash, u.NamaLengkap, u.IsActive, r.NamaRole
            FROM AppUser u
            INNER JOIN Role r ON r.Id = u.RoleId
            WHERE u.IsActive = 1
            ORDER BY u.Username;", Map);
    }

    public AppUser? GetById(int id)
    {
        return QuerySingle(@"
            SELECT u.Id, u.RoleId, u.Username, u.PasswordHash, u.NamaLengkap, u.IsActive, r.NamaRole
            FROM AppUser u
            INNER JOIN Role r ON r.Id = u.RoleId
            WHERE u.Id = @Id;",
            Map,
            command => command.Parameters.AddWithValue("@Id", id));
    }

    public AppUser? GetByUsername(string username)
    {
        return QuerySingle(@"
            SELECT u.Id, u.RoleId, u.Username, u.PasswordHash, u.NamaLengkap, u.IsActive, r.NamaRole
            FROM AppUser u
            INNER JOIN Role r ON r.Id = u.RoleId
            WHERE u.Username = @Username;",
            Map,
            command => command.Parameters.AddWithValue("@Username", username));
    }

    public void Insert(AppUser user)
    {
        Execute(@"
            INSERT INTO AppUser (RoleId, Username, PasswordHash, NamaLengkap, IsActive)
            VALUES (@RoleId, @Username, @PasswordHash, @NamaLengkap, @IsActive);",
            command =>
            {
                command.Parameters.AddWithValue("@RoleId", user.RoleId);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@NamaLengkap", user.NamaLengkap);
                command.Parameters.AddWithValue("@IsActive", user.IsActive);
            });
    }

    public void Update(AppUser user)
    {
        Execute(@"
            UPDATE AppUser
            SET RoleId = @RoleId,
                Username = @Username,
                PasswordHash = @PasswordHash,
                NamaLengkap = @NamaLengkap,
                IsActive = @IsActive,
                UpdatedAt = CURRENT_TIMESTAMP
            WHERE Id = @Id;",
            command =>
            {
                command.Parameters.AddWithValue("@Id", user.Id);
                command.Parameters.AddWithValue("@RoleId", user.RoleId);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@NamaLengkap", user.NamaLengkap);
                command.Parameters.AddWithValue("@IsActive", user.IsActive);
            });
    }

    public void Delete(int id)
    {
        Execute("UPDATE AppUser SET IsActive = 0, UpdatedAt = CURRENT_TIMESTAMP WHERE Id = @Id;",
            command => command.Parameters.AddWithValue("@Id", id));
    }

    private static AppUser Map(MySqlDataReader reader)
    {
        return new AppUser
        {
            Id = reader.GetInt32(0),
            RoleId = reader.GetInt32(1),
            Username = reader.GetString(2),
            PasswordHash = reader.GetString(3),
            NamaLengkap = reader.GetString(4),
            IsActive = reader.GetBoolean(5),
            NamaRole = reader.IsDBNull(6) ? null : reader.GetString(6)
        };
    }
}

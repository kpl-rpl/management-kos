using management_kos.Data;
using management_kos.Models;
using MySqlConnector;

namespace management_kos.Repositories;

public class RoleRepository : RepositoryBase, IRoleRepository
{
    public RoleRepository(MySqlDbContext dbContext) : base(dbContext)
    {
    }

    public List<Role> GetAll()
    {
        return QueryList(@"
            SELECT Id, NamaRole, Deskripsi, IsActive
            FROM Role
            WHERE IsActive = 1
            ORDER BY NamaRole;", Map);
    }

    public Role? GetById(int id)
    {
        return QuerySingle(@"
            SELECT Id, NamaRole, Deskripsi, IsActive
            FROM Role
            WHERE Id = @Id;",
            Map,
            command => command.Parameters.AddWithValue("@Id", id));
    }

    public Role? GetByName(string namaRole)
    {
        return QuerySingle(@"
            SELECT Id, NamaRole, Deskripsi, IsActive
            FROM Role
            WHERE NamaRole = @NamaRole;",
            Map,
            command => command.Parameters.AddWithValue("@NamaRole", namaRole));
    }

    public void Insert(Role role)
    {
        Execute(@"
            INSERT INTO Role (NamaRole, Deskripsi, IsActive)
            VALUES (@NamaRole, @Deskripsi, @IsActive);",
            command =>
            {
                command.Parameters.AddWithValue("@NamaRole", role.NamaRole);
                command.Parameters.AddWithValue("@Deskripsi", (object?)role.Deskripsi ?? DBNull.Value);
                command.Parameters.AddWithValue("@IsActive", role.IsActive);
            });
    }

    public void Update(Role role)
    {
        Execute(@"
            UPDATE Role
            SET NamaRole = @NamaRole,
                Deskripsi = @Deskripsi,
                IsActive = @IsActive
            WHERE Id = @Id;",
            command =>
            {
                command.Parameters.AddWithValue("@Id", role.Id);
                command.Parameters.AddWithValue("@NamaRole", role.NamaRole);
                command.Parameters.AddWithValue("@Deskripsi", (object?)role.Deskripsi ?? DBNull.Value);
                command.Parameters.AddWithValue("@IsActive", role.IsActive);
            });
    }

    public void Delete(int id)
    {
        Execute("UPDATE Role SET IsActive = 0 WHERE Id = @Id;",
            command => command.Parameters.AddWithValue("@Id", id));
    }

    private static Role Map(MySqlDataReader reader)
    {
        return new Role
        {
            Id = reader.GetInt32(0),
            NamaRole = reader.GetString(1),
            Deskripsi = reader.IsDBNull(2) ? null : reader.GetString(2),
            IsActive = reader.GetBoolean(3)
        };
    }
}

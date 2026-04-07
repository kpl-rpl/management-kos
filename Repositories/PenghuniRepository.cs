using management_kos.Data;
using management_kos.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace management_kos.Repositories
{
    public class PenghuniRepository : IPenghuniRepository
    {
        private readonly MySqlDbContext _dbContext;

        public PenghuniRepository(MySqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Penghuni> GetAll()
        {
            var result = new List<Penghuni>();

            using var connection = _dbContext.CreateConnection();
            using var command = connection.CreateCommand();

            command.CommandText = @"
            SELECT Id, KamarId, Nama, NomorTelepon, Email, TanggalMasuk, TanggalKeluar, Catatan
            FROM Penghuni
            ORDER BY Id DESC;";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(Map(reader));
            }

            return result;
        }

        public Penghuni? GetById(int id)
        {
            using var connection = _dbContext.CreateConnection();
            using var command = connection.CreateCommand();

            command.CommandText = @"
            SELECT Id, KamarId, Nama, NomorTelepon, Email, TanggalMasuk, TanggalKeluar, Catatan
            FROM Penghuni
            WHERE Id = @Id;";

            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            return Map(reader);
        }

        public List<Penghuni> GetByKamarId(int kamarId)
        {
            var result = new List<Penghuni>();

            using var connection = _dbContext.CreateConnection();
            using var command = connection.CreateCommand();

            command.CommandText = @"
            SELECT Id, KamarId, Nama, NomorTelepon, Email, TanggalMasuk, TanggalKeluar, Catatan
            FROM Penghuni
            WHERE KamarId = @KamarId
            ORDER BY Id DESC;";

            command.Parameters.AddWithValue("@KamarId", kamarId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(Map(reader));
            }

            return result;
        }

        public void Insert(Penghuni penghuni)
        {
            using var connection = _dbContext.CreateConnection();
            using var command = connection.CreateCommand();

            command.CommandText = @"
            INSERT INTO Penghuni (KamarId, Nama, NomorTelepon, Email, TanggalMasuk, TanggalKeluar, Catatan)
            VALUES (@KamarId, @Nama, @NomorTelepon, @Email, @TanggalMasuk, @TanggalKeluar, @Catatan);";

            command.Parameters.AddWithValue("@KamarId", penghuni.KamarId);
            command.Parameters.AddWithValue("@Nama", penghuni.Nama);
            command.Parameters.AddWithValue("@NomorTelepon", penghuni.NomorTelepon);
            command.Parameters.AddWithValue("@Email", (object?)penghuni.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@TanggalMasuk", penghuni.TanggalMasuk);
            command.Parameters.AddWithValue("@TanggalKeluar", (object?)penghuni.TanggalKeluar ?? DBNull.Value);
            command.Parameters.AddWithValue("@Catatan", (object?)penghuni.Catatan ?? DBNull.Value);

            command.ExecuteNonQuery();
        }

        public void Update(Penghuni penghuni)
        {
            using var connection = _dbContext.CreateConnection();
            using var command = connection.CreateCommand();

            command.CommandText = @"
            UPDATE Penghuni
                SET KamarId = @KamarId,
                    Nama = @Nama,
                    NomorTelepon = @NomorTelepon,
                    Email = @Email,
                    TanggalMasuk = @TanggalMasuk,
                    TanggalKeluar = @TanggalKeluar,
                    Catatan = @Catatan
            WHERE Id = @Id;";

            command.Parameters.AddWithValue("@Id", penghuni.Id);
            command.Parameters.AddWithValue("@KamarId", penghuni.KamarId);
            command.Parameters.AddWithValue("@Nama", penghuni.Nama);
            command.Parameters.AddWithValue("@NomorTelepon", penghuni.NomorTelepon);
            command.Parameters.AddWithValue("@Email", (object?)penghuni.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@TanggalMasuk", penghuni.TanggalMasuk);
            command.Parameters.AddWithValue("@TanggalKeluar", (object?)penghuni.TanggalKeluar ?? DBNull.Value);
            command.Parameters.AddWithValue("@Catatan", (object?)penghuni.Catatan ?? DBNull.Value);

            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = _dbContext.CreateConnection();
            using var command = connection.CreateCommand();

            command.CommandText = "DELETE FROM Penghuni WHERE Id = @Id;";
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }

        private static Penghuni Map(MySqlConnector.MySqlDataReader reader)
        {
            return new Penghuni
            {
                Id = reader.GetInt32(0),
                KamarId = reader.GetInt32(1),
                Nama = reader.GetString(2),
                NomorTelepon = reader.GetString(3),
                Email = reader.IsDBNull(4) ? null : reader.GetString(4),
                TanggalMasuk = reader.GetDateTime(5),
                TanggalKeluar = reader.IsDBNull(6) ? null : reader.GetDateTime(6),
                Catatan = reader.IsDBNull(7) ? null : reader.GetString(7)
            };
        }
    }
}

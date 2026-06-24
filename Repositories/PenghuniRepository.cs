using management_kos.Data;
using management_kos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
            SELECT p.Id,
                   p.Nama,
                   p.NomorTelepon,
                   p.Email,
                   p.TanggalMasuk,
                   p.TanggalKeluar,
                   p.Catatan,
                   p.IsActive,
                   (
                       SELECT CONCAT(kos.NamaKos, ' - ', k.NomorKamar)
                       FROM KontrakSewa ks
                       INNER JOIN Kamar k ON k.Id = ks.KamarId
                       INNER JOIN Kos kos ON kos.Id = k.KosId
                       WHERE ks.PenghuniId = p.Id
                         AND ks.Status IN ('Aktif', 'Dipesan')
                       ORDER BY CASE ks.Status WHEN 'Aktif' THEN 0 ELSE 1 END,
                                ks.TanggalMulai DESC
                       LIMIT 1
                   ) AS InfoKamar
            FROM Penghuni p
            WHERE p.IsActive = 1
            ORDER BY p.Id DESC;";

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
            SELECT p.Id,
                   p.Nama,
                   p.NomorTelepon,
                   p.Email,
                   p.TanggalMasuk,
                   p.TanggalKeluar,
                   p.Catatan,
                   p.IsActive,
                   (
                       SELECT CONCAT(kos.NamaKos, ' - ', k.NomorKamar)
                       FROM KontrakSewa ks
                       INNER JOIN Kamar k ON k.Id = ks.KamarId
                       INNER JOIN Kos kos ON kos.Id = k.KosId
                       WHERE ks.PenghuniId = p.Id
                         AND ks.Status IN ('Aktif', 'Dipesan')
                       ORDER BY CASE ks.Status WHEN 'Aktif' THEN 0 ELSE 1 END,
                                ks.TanggalMulai DESC
                       LIMIT 1
                   ) AS InfoKamar
            FROM Penghuni p
            WHERE p.Id = @Id;";

            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            return Map(reader);
        }

        public void Insert(Penghuni penghuni)
        {
            ValidatePenghuni(penghuni);

            using var connection = _dbContext.CreateConnection();
            using var command = connection.CreateCommand();

            command.CommandText = @"
            INSERT INTO Penghuni (Nama, NomorTelepon, Email, Catatan, IsActive)
            VALUES (@Nama, @NomorTelepon, @Email, @Catatan, @IsActive);";

            command.Parameters.AddWithValue("@Nama", penghuni.Nama);
            command.Parameters.AddWithValue("@NomorTelepon", penghuni.NomorTelepon);
            command.Parameters.AddWithValue("@Email", (object?)penghuni.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@Catatan", (object?)penghuni.Catatan ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsActive", penghuni.IsActive);

            command.ExecuteNonQuery();
            penghuni.Id = Convert.ToInt32(command.LastInsertedId);
        }

        public void Update(Penghuni penghuni)
        {
            ValidatePenghuni(penghuni);

            using var connection = _dbContext.CreateConnection();
            using var command = connection.CreateCommand();

            command.CommandText = @"
            UPDATE Penghuni
                SET Nama = @Nama,
                    NomorTelepon = @NomorTelepon,
                    Email = @Email,
                    Catatan = @Catatan,
                    IsActive = @IsActive
            WHERE Id = @Id;";

            command.Parameters.AddWithValue("@Id", penghuni.Id);
            command.Parameters.AddWithValue("@Nama", penghuni.Nama);
            command.Parameters.AddWithValue("@NomorTelepon", penghuni.NomorTelepon);
            command.Parameters.AddWithValue("@Email", (object?)penghuni.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@Catatan", (object?)penghuni.Catatan ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsActive", penghuni.IsActive);

            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = _dbContext.CreateConnection();
            using var command = connection.CreateCommand();

            command.CommandText = "UPDATE Penghuni SET IsActive = 0 WHERE Id = @Id;";
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }

        private static void ValidatePenghuni(Penghuni penghuni)
        {
            if (string.IsNullOrWhiteSpace(penghuni.Nama))
            {
                throw new ArgumentException("Nama penghuni tidak boleh kosong.");
            }

            if (!string.IsNullOrWhiteSpace(penghuni.Email))
            {
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!emailRegex.IsMatch(penghuni.Email))
                {
                    throw new ArgumentException("Format email tidak valid.");
                }
            }

            if (!string.IsNullOrWhiteSpace(penghuni.NomorTelepon))
            {
                var phoneRegex = new Regex(@"^\+?[0-9]{10,15}$");
                if (!phoneRegex.IsMatch(penghuni.NomorTelepon))
                {
                    throw new ArgumentException("Format nomor telepon tidak valid.");
                }
            }

            if (penghuni.TanggalKeluar.HasValue &&
                penghuni.TanggalMasuk.HasValue &&
                penghuni.TanggalKeluar < penghuni.TanggalMasuk)
            {
                throw new ArgumentException("Tanggal keluar tidak boleh lebih awal dari tanggal masuk.");
            }
        }

        private static Penghuni Map(MySqlConnector.MySqlDataReader reader)
        {
            return new Penghuni
            {
                Id = reader.GetInt32(0),
                Nama = reader.GetString(1),
                NomorTelepon = reader.GetString(2),
                Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                TanggalMasuk = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                TanggalKeluar = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                Catatan = reader.IsDBNull(6) ? null : reader.GetString(6),
                IsActive = reader.GetBoolean(7),
                InfoKamar = reader.FieldCount > 8 && !reader.IsDBNull(8)
                    ? reader.GetString(8)
                    : null
            };
        }
    }
}

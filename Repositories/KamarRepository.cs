using management_kos.Data;
using management_kos.Models;
using MySqlConnector;
using System.Collections.Generic;

namespace management_kos.Repositories;

public class KamarRepository : IKamarRepository
{
    private readonly MySqlDbContext _dbContext;

    public KamarRepository(MySqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Kamar> GetAll()
    {
        var result = new List<Kamar>();
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, KosId, NomorKamar, HargaKamar, Status FROM Kamar ORDER BY Id DESC;";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(new Kamar
            {
                Id = reader.GetInt32(0),
                KosId = reader.GetInt32(1),
                NomorKamar = reader.GetString(2),
                HargaKamar = reader.GetInt32(3),
                Status = reader.GetString(4)
            });
        }
        return result;
    }

    public Kamar? GetById(int id)
    {
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, KosId, NomorKamar, HargaKamar, Status FROM Kamar WHERE Id = @Id;";
        command.Parameters.AddWithValue("@Id", id);
        using var reader = command.ExecuteReader();
        if (!reader.Read()) return null;
        return new Kamar
        {
            Id = reader.GetInt32(0),
            KosId = reader.GetInt32(1),
            NomorKamar = reader.GetString(2),
            HargaKamar = reader.GetInt32(3),
            Status = reader.GetString(4)
        };
    }

    public List<Kamar> GetByKosId(int kosId)
    {
        var result = new List<Kamar>();
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, KosId, NomorKamar, HargaKamar, Status FROM Kamar WHERE KosId = @KosId ORDER BY Id DESC;";
        command.Parameters.AddWithValue("@KosId", kosId);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(new Kamar
            {
                Id = reader.GetInt32(0),
                KosId = reader.GetInt32(1),
                NomorKamar = reader.GetString(2),
                HargaKamar = reader.GetInt32(3),
                Status = reader.GetString(4)
            });
        }
        return result;
    }

    public void Insert(Kamar kamar)
    {
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Kamar (KosId, NomorKamar, HargaKamar, Status)
            VALUES (@KosId, @NomorKamar, @HargaKamar, @Status);";
        command.Parameters.AddWithValue("@KosId", kamar.KosId);
        command.Parameters.AddWithValue("@NomorKamar", kamar.NomorKamar);
        command.Parameters.AddWithValue("@HargaKamar", kamar.HargaKamar);   
        command.Parameters.AddWithValue("@Status", kamar.Status);
        command.ExecuteNonQuery();
    }

    public void Update(Kamar kamar)
    {
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE Kamar
            SET KosId = @KosId, NomorKamar = @NomorKamar, HargaKamar = @HargaKamar, Status = @Status
            WHERE Id = @Id;";
        command.Parameters.AddWithValue("@Id", kamar.Id);
        command.Parameters.AddWithValue("@KosId", kamar.KosId);
        command.Parameters.AddWithValue("@NomorKamar", kamar.NomorKamar);
        command.Parameters.AddWithValue("@HargaKamar", kamar.HargaKamar);
        command.Parameters.AddWithValue("@Status", kamar.Status);
        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var connection = _dbContext.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Kamar WHERE Id = @Id;";
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();
    }
}
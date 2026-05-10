using System;
using System.Collections.Generic;
using System.IO;
using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services;

public class KamarService
{
    private readonly IKamarRepository _kamarRepository;
    private readonly IKosRepository _kosRepository;
    private readonly int _maxHargaKamar; // Runtime configuration

    public KamarService(IKamarRepository kamarRepository, IKosRepository kosRepository)
    {
        _kamarRepository = kamarRepository;
        _kosRepository = kosRepository;

        // Runtime configuration: membaca batas harga kamar dari .env, default jika key tidak ada
        _maxHargaKamar = 10_000_000;
        if (File.Exists(".env"))
        {
            foreach (var line in File.ReadAllLines(".env"))
            {
                if (line.StartsWith("MAX_HARGA_KAMAR=") &&
                    int.TryParse(line.Split('=')[1], out var parsed))
                {
                    _maxHargaKamar = parsed;
                    break;
                }
            }
        }
    }

    public List<Kamar> GetAllKamar()
    {
        return _kamarRepository.GetAll();
    }

    public List<Kamar> GetKamarByKosId(int kosId)
    {
        if (kosId <= 0)
        {
            throw new ArgumentException("KosId tidak valid.");
        }

        return _kamarRepository.GetByKosId(kosId);
    }

    public void TambahKamar(Kamar kamar)
    {
        Validate(kamar);
        EnsureKosExists(kamar.KosId);

        _kamarRepository.Insert(kamar);
    }

    public void UbahKamar(Kamar kamar)
    {
        if (kamar.Id <= 0)
        {
            throw new ArgumentException("ID Kamar tidak valid.");
        }

        Validate(kamar);
        EnsureKosExists(kamar.KosId);

        _kamarRepository.Update(kamar);
    }

    public void HapusKamar(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID Kamar tidak valid.");
        }

        _kamarRepository.Delete(id);
    }

    private void EnsureKosExists(int kosId)
    {
        var kos = _kosRepository.GetById(kosId);
        if (kos is null)
        {
            throw new ArgumentException("KosId tidak ditemukan. Pastikan data kos sudah ada.");
        }
    }

    private void Validate(Kamar kamar)
    {
        if (kamar.KosId <= 0)
        {
            throw new ArgumentException("KosId wajib diisi dan harus lebih dari 0.");
        }

        if (string.IsNullOrWhiteSpace(kamar.NomorKamar))
        {
            throw new ArgumentException("Nomor Kamar wajib diisi.");
        }

        kamar.NomorKamar = kamar.NomorKamar.Trim();

        if (kamar.HargaKamar > _maxHargaKamar)
        {
            throw new ArgumentException($"Harga kamar tidak boleh lebih dari {_maxHargaKamar}.");
        }

        // Automata: Validasi status kamar menggunakan enum
        if (!Enum.TryParse(kamar.Status, true, out KamarStatus status))
        {
            throw new ArgumentException(
                "Status kamar tidak valid. Gunakan salah satu: Kosong, Terisi, Dipesan, Perbaikan."
            );
        }

        kamar.Status = status.ToString();
    }
}

// Enum untuk status kamar
public enum KamarStatus
{
    Kosong,
    Terisi,
    Dipesan,
    Perbaikan
}
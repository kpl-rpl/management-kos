using System;
using System.Collections.Generic;
using System.Linq;
using management_kos.Data;
using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services;
public static class KamarServiceTest
{
    public static void Run()
    {
        Console.WriteLine("== TEST KamarService ==");
        Console.WriteLine("----------------------------------------");

        var dbContext = new MySqlDbContext();
        dbContext.InitializeDatabase();

        IKosRepository kosRepository = new KosRepository(dbContext);

        IKamarRepository kamarRepository = new KamarRepository();

        var kamarService = new KamarService(kamarRepository, kosRepository);

        var kosId1 = EnsureKosSeed(kosRepository, "Kos Test A", "Alamat A", "081234567890");
        var kosId2 = EnsureKosSeed(kosRepository, "Kos Test B", "Alamat B", "081298765432");

        // ==============
        // TEST CREATE
        // ==============
        Console.WriteLine("\n[1] Test Create (TambahKamar)");
        Try("Tambah kamar A-01", delegate
        {
            kamarService.TambahKamar(new Kamar
            {
                KosId = kosId1,
                NomorKamar = "A-01",
                Status = "Kosong"
            });
        });

        Try("Tambah kamar A-02", delegate
        {
            kamarService.TambahKamar(new Kamar
            {
                KosId = kosId1,
                NomorKamar = "A-02",
                Status = "Terisi"
            });
        });

        Try("Tambah kamar B-01", delegate
        {
            kamarService.TambahKamar(new Kamar
            {
                KosId = kosId2,
                NomorKamar = "B-01",
                Status = "Dipesan"
            });
        });

        // ==============
        // TEST GET ALL
        // ==============
        Console.WriteLine("\n[2] Test Get All (GetAllKamar)");
        PrintKamarList(kamarService.GetAllKamar());

        // ==============
        // TEST GET BY KOS ID
        // ==============
        Console.WriteLine("\n[3] Test Get By KosId (GetKamarByKosId)");
        Console.WriteLine("KosId = " + kosId1);
        PrintKamarList(kamarService.GetKamarByKosId(kosId1));

        // ==============
        // TEST UPDATE
        // ==============
        Console.WriteLine("\n[4] Test Update (UbahKamar)");
        Try("Update kamar Id=2 status jadi Kosong", delegate
        {
            kamarService.UbahKamar(new Kamar
            {
                Id = 2,
                KosId = kosId1,
                NomorKamar = "A-02",
                Status = "Kosong"
            });
        });

        Console.WriteLine("\nHasil setelah update:");
        PrintKamarList(kamarService.GetAllKamar());

        // ==============
        // TEST DELETE
        // ==============
        Console.WriteLine("\n[5] Test Delete (HapusKamar)");
        Try("Hapus kamar Id=1", delegate
        {
            kamarService.HapusKamar(1);
        });

        Console.WriteLine("\nHasil setelah delete:");
        PrintKamarList(kamarService.GetAllKamar());

        // ==============
        // TEST VALIDATION ERROR
        // ==============
        Console.WriteLine("\n[6] Test Validasi Error: KosId tidak ada (harus error)");
        TryExpectError("Tambah kamar KosId invalid", delegate
        {
            kamarService.TambahKamar(new Kamar
            {
                KosId = 999999,
                NomorKamar = "X-01",
                Status = "Kosong"
            });
        });

        Console.WriteLine("\n[7] Test Validasi Error: Status invalid (harus error)");
        TryExpectError("Tambah kamar status invalid", delegate
        {
            kamarService.TambahKamar(new Kamar
            {
                KosId = kosId1,
                NomorKamar = "A-03",
                Status = "RandomStatus"
            });
        });

        Console.WriteLine("\n[8] Test Validasi Error: NomorKamar kosong (harus error)");
        TryExpectError("Tambah kamar nomor kosong", delegate
        {
            kamarService.TambahKamar(new Kamar
            {
                KosId = kosId1,
                NomorKamar = "   ",
                Status = "Kosong"
            });
        });

        Console.WriteLine("\n[9] Test Validasi Error: Update Id invalid (harus error)");
        TryExpectError("Update kamar Id=0", delegate
        {
            kamarService.UbahKamar(new Kamar
            {
                Id = 0,
                KosId = kosId1,
                NomorKamar = "A-01",
                Status = "Kosong"
            });
        });

        Console.WriteLine("\n== SELESAI TEST ==");
    }
    private static int EnsureKosSeed(IKosRepository kosRepository, string namaKos, string alamat, string nomorTelepon)
    {
        kosRepository.Insert(new Kos
        {
            NamaKos = namaKos,
            Alamat = alamat,
            HargaDasar = 1000000,
            JumlahKamar = 5,
            NamaPemilik = "Tester",
            NomorTelepon = nomorTelepon,
            Catatan = "Seed untuk test"
        });
        var all = kosRepository.GetAll();
        if (all == null || all.Count == 0)
        {
            throw new InvalidOperationException("Gagal membuat seed Kos. Pastikan koneksi DB valid.");
        }

        return all[0].Id;
    }

    private static void PrintKamarList(List<Kamar> list)
    {
        if (list == null || list.Count == 0)
        {
            Console.WriteLine("(kosong)");
            return;
        }

        foreach (var k in list)
        {
            Console.WriteLine(
                "Id=" + k.Id +
                ", KosId=" + k.KosId +
                ", NomorKamar=" + k.NomorKamar +
                ", Status=" + k.Status
            );
        }
    }

    private static void Try(string title, Action action)
    {
        try
        {
            action();
            Console.WriteLine("OK - " + title);
        }
        catch (Exception ex)
        {
            Console.WriteLine("FAIL - " + title + " | " + ex.Message);
        }
    }

    private static void TryExpectError(string title, Action action)
    {
        try
        {
            action();
            Console.WriteLine("FAIL - " + title + " | Seharusnya error, tapi tidak.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("OK - " + title + " | Error: " + ex.Message);
        }
    }
}

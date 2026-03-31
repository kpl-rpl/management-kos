using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using management_kos.Data;
using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services;

internal static class KamarServiceTestRunner
{
    [DllImport("kernel32.dll")]
    private static extern bool AttachConsole(int dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    private const int AttachParentProcess = -1;

    public static int Main(string[] args)
    {
        TryEnableConsole();

        Console.WriteLine("== TEST KamarService (Terminal) ==");
        Console.WriteLine("----------------------------------");

        var dbContext = new MySqlDbContext();
        dbContext.InitializeDatabase();

        IKosRepository kosRepository = new KosRepository(dbContext);

        IKamarRepository kamarRepository = new KamarRepositoryDummy();

        var kamarService = new KamarService(kamarRepository, kosRepository);

        var kosId1 = SeedKosAndGetLatestId(kosRepository, "Kos Test A", "Alamat A", "081234567890");
        var kosId2 = SeedKosAndGetLatestId(kosRepository, "Kos Test B", "Alamat B", "081298765432");

        Console.WriteLine("\n[1] Create (TambahKamar)");
        Try("Tambah kamar A-01", delegate
        {
            kamarService.TambahKamar(new Kamar { KosId = kosId1, NomorKamar = "A-01", Status = "Kosong" });
        });

        Try("Tambah kamar A-02", delegate
        {
            kamarService.TambahKamar(new Kamar { KosId = kosId1, NomorKamar = "A-02", Status = "Terisi" });
        });

        Try("Tambah kamar B-01", delegate
        {
            kamarService.TambahKamar(new Kamar { KosId = kosId2, NomorKamar = "B-01", Status = "Dipesan" });
        });

        Console.WriteLine("\n[2] GetAll (GetAllKamar)");
        PrintKamarList(kamarService.GetAllKamar());

        Console.WriteLine("\n[3] GetByKosId (GetKamarByKosId) KosId=" + kosId1);
        PrintKamarList(kamarService.GetKamarByKosId(kosId1));

        Console.WriteLine("\n[4] Update (UbahKamar) Id=2");
        Try("Ubah kamar Id=2 status jadi Kosong", delegate
        {
            kamarService.UbahKamar(new Kamar { Id = 2, KosId = kosId1, NomorKamar = "A-02", Status = "Kosong" });
        });

        Console.WriteLine("\nHasil setelah update:");
        PrintKamarList(kamarService.GetAllKamar());

        Console.WriteLine("\n[5] Delete (HapusKamar) Id=1");
        Try("Hapus kamar Id=1", delegate
        {
            kamarService.HapusKamar(1);
        });

        Console.WriteLine("\nHasil setelah delete:");
        PrintKamarList(kamarService.GetAllKamar());

        Console.WriteLine("\n[6] Validasi error KosId tidak ada (harus error)");
        TryExpectError("Tambah kamar KosId invalid", delegate
        {
            kamarService.TambahKamar(new Kamar { KosId = 999999, NomorKamar = "X-01", Status = "Kosong" });
        });

        Console.WriteLine("\n[7] Validasi error status invalid (harus error)");
        TryExpectError("Tambah kamar status invalid", delegate
        {
            kamarService.TambahKamar(new Kamar { KosId = kosId1, NomorKamar = "A-03", Status = "RandomStatus" });
        });

        Console.WriteLine("\n== SELESAI ==");
        return 0;
    }

    private static void TryEnableConsole()
    {
        var attached = AttachConsole(AttachParentProcess);
        if (!attached)
        {
            AllocConsole();
        }
    }

    private static int SeedKosAndGetLatestId(IKosRepository kosRepository, string namaKos, string alamat, string nomorTelepon)
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
            throw new InvalidOperationException("Seed Kos gagal. Periksa koneksi DB / .env.");
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
            Console.WriteLine("Id=" + k.Id + ", KosId=" + k.KosId + ", NomorKamar=" + k.NomorKamar + ", Status=" + k.Status);
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

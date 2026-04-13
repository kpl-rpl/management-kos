using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using management_kos.Data;
using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services;

internal static class KosServiceTestRunner
{
    [DllImport("kernel32.dll")]
    private static extern bool AttachConsole(int dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    private const int AttachParentProcess = -1;

    public static int Main(string[] args)
    {
        TryEnableConsole();

        Console.WriteLine("== TEST KosService (Terminal) ==");
        Console.WriteLine("--------------------------------");

        var dbContext = new MySqlDbContext();
        dbContext.InitializeDatabase();

        IKosRepository kosRepository = new KosRepository(dbContext);
        var kosService = new KosService(kosRepository);

        //TambahKos 
        Console.WriteLine("\n[1] Create (TambahKos)");

        Try("Tambah Kos A (data lengkap)", () =>
        {
            kosService.TambahKos(new Kos
            {
                NamaKos = "Kos Melati",
                Alamat = "Jl. Mawar No. 1, Sidoarjo",
                HargaDasar = 800_000,
                JumlahKamar = 10,
                NamaPemilik = "Budi Santoso",
                NomorTelepon = "081234567890",
                Catatan = "Dekat kampus"
            });
        });

        Try("Tambah Kos B (tanpa catatan)", () =>
        {
            kosService.TambahKos(new Kos
            {
                NamaKos = "Kos Kenanga",
                Alamat = "Jl. Kenanga No. 5, Surabaya",
                HargaDasar = 1_200_000,
                JumlahKamar = 6,
                NamaPemilik = "Siti Rahayu",
                NomorTelepon = "+6281298765432"
            });
        });

        // GetAll 
        Console.WriteLine("\n[2] GetAll (GetAllKos)");
        PrintKosList(kosService.GetAllKos());

        var semuaKos = kosService.GetAllKos();
        var idKosA = semuaKos.Count >= 2 ? semuaKos[^1].Id : semuaKos[0].Id;
        var idKosB = semuaKos[0].Id;

        // UbahKos 
        Console.WriteLine($"\n[3] Update (UbahKos) Id={idKosA}");
        Try($"Ubah nama dan harga Kos Id={idKosA}", () =>
        {
            kosService.UbahKos(new Kos
            {
                Id = idKosA,
                NamaKos = "Kos Melati (Renovasi)",
                Alamat = "Jl. Mawar No. 1, Sidoarjo",
                HargaDasar = 900_000,
                JumlahKamar = 12,
                NamaPemilik = "Budi Santoso",
                NomorTelepon = "081234567890",
                Catatan = "Setelah renovasi"
            });
        });

        Console.WriteLine("\nHasil setelah update:");
        PrintKosList(kosService.GetAllKos());

        // HapusKos
        Console.WriteLine($"\n[4] Delete (HapusKos) Id={idKosB}");
        Try($"Hapus Kos Id={idKosB}", () =>
        {
            kosService.HapusKos(idKosB);
        });

        Console.WriteLine("\nHasil setelah delete:");
        PrintKosList(kosService.GetAllKos());

        // Validasi error
        Console.WriteLine("\n[5] Validasi error - NamaKos kosong (harus error)");
        TryExpectError("NamaKos kosong", () =>
        {
            kosService.TambahKos(new Kos
            {
                NamaKos = "   ",
                Alamat = "Jl. Test",
                HargaDasar = 500_000,
                JumlahKamar = 5,
                NamaPemilik = "Test",
                NomorTelepon = "08123456789"
            });
        });

        Console.WriteLine("\n[6] Validasi error - HargaDasar = 0 (harus error)");
        TryExpectError("HargaDasar nol", () =>
        {
            kosService.TambahKos(new Kos
            {
                NamaKos = "Kos Test",
                Alamat = "Jl. Test",
                HargaDasar = 0,
                JumlahKamar = 5,
                NamaPemilik = "Test",
                NomorTelepon = "08123456789"
            });
        });

        Console.WriteLine("\n[7] Validasi error - JumlahKamar = 0 (harus error)");
        TryExpectError("JumlahKamar nol", () =>
        {
            kosService.TambahKos(new Kos
            {
                NamaKos = "Kos Test",
                Alamat = "Jl. Test",
                HargaDasar = 500_000,
                JumlahKamar = 0,
                NamaPemilik = "Test",
                NomorTelepon = "08123456789"
            });
        });

        Console.WriteLine("\n[8] Validasi error - NomorTelepon format salah (harus error)");
        TryExpectError("NomorTelepon format salah", () =>
        {
            kosService.TambahKos(new Kos
            {
                NamaKos = "Kos Test",
                Alamat = "Jl. Test",
                HargaDasar = 500_000,
                JumlahKamar = 5,
                NamaPemilik = "Test",
                NomorTelepon = "abc-xyz"
            });
        });

        Console.WriteLine("\n[9] Validasi error - UbahKos dengan Id = 0 (harus error)");
        TryExpectError("UbahKos Id invalid", () =>
        {
            kosService.UbahKos(new Kos
            {
                Id = 0,
                NamaKos = "Kos Test",
                Alamat = "Jl. Test",
                HargaDasar = 500_000,
                JumlahKamar = 5,
                NamaPemilik = "Test",
                NomorTelepon = "08123456789"
            });
        });

        Console.WriteLine("\n[10] Validasi error - HapusKos dengan Id negatif (harus error)");
        TryExpectError("HapusKos Id negatif", () =>
        {
            kosService.HapusKos(-1);
        });

        Console.WriteLine("\n== SELESAI ==");
        return 0;
    }

    // Helpers 

    private static void TryEnableConsole()
    {
        if (!AttachConsole(AttachParentProcess))
        {
            AllocConsole();
        }
    }

    private static void PrintKosList(List<Kos> list)
    {
        if (list == null || list.Count == 0)
        {
            Console.WriteLine("  (kosong)");
            return;
        }

        foreach (var k in list)
        {
            Console.WriteLine(
                $"  Id={k.Id}, NamaKos={k.NamaKos}, Alamat={k.Alamat}, " +
                $"HargaDasar={k.HargaDasar:N0}, JumlahKamar={k.JumlahKamar}, " +
                $"Pemilik={k.NamaPemilik}, Telp={k.NomorTelepon}");
        }
    }

    private static void Try(string title, Action action)
    {
        try
        {
            action();
            Console.WriteLine($"  OK   - {title}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  FAIL - {title} | {ex.Message}");
        }
    }

    private static void TryExpectError(string title, Action action)
    {
        try
        {
            action();
            Console.WriteLine($"  FAIL - {title} | Seharusnya error, tapi tidak.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  OK   - {title} | Error: {ex.Message}");
        }
    }
}
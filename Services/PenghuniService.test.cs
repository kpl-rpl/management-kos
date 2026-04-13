using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using management_kos.Data;
using management_kos.Models;
using management_kos.Repositories;

namespace management_kos.Services;

internal static class PenghuniServiceTestRunner
{
    [DllImport("kernel32.dll")]
    private static extern bool AttachConsole(int dwProcessId);

    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    private const int AttachParentProcess = -1;

    public static int Main(string[] args)
    {
        TryEnableConsole();

        Console.WriteLine("== TEST PenghuniService (Terminal) ==");
        Console.WriteLine("-------------------------------------");

        var dbContext = new MySqlDbContext();
        dbContext.InitializeDatabase();

        IKosRepository kosRepository = new KosRepository(dbContext);
        IKamarRepository kamarRepository = new KamarRepository(dbContext);
        IPenghuniRepository penghuniRepository = new PenghuniRepository(dbContext);

        var kamarService = new KamarService(kamarRepository, kosRepository);
        var penghuniService = new PenghuniService(penghuniRepository, kamarRepository);

        // Seed data 
        var kosId = SeedKos(kosRepository);
        var kamarId1 = SeedKamar(kamarRepository, kosId, "K-01", "Kosong");
        var kamarId2 = SeedKamar(kamarRepository, kosId, "K-02", "Kosong");
        var kamarIdPerbaikan = SeedKamar(kamarRepository, kosId, "K-03", "Perbaikan");

        Console.WriteLine($"\n  Seed: KosId={kosId}, KamarId1={kamarId1}, KamarId2={kamarId2}, KamarIdPerbaikan={kamarIdPerbaikan}");

        // TambahPenghuni 
        Console.WriteLine("\n[1] Create (TambahPenghuni)");

        Try("Tambah penghuni di kamar K-01", () =>
        {
            penghuniService.TambahPenghuni(new Penghuni
            {
                KamarId = kamarId1,
                Nama = "Andi Pratama",
                NomorTelepon = "081234567890",
                Email = "andi@email.com",
                TanggalMasuk = DateTime.Today
            });
        });

        Console.WriteLine($"\n  Status kamar K-01 setelah tambah penghuni (harusnya Terisi):");
        PrintKamarStatus(kamarRepository, kamarId1);

        Try("Tambah penghuni kedua di kamar K-02", () =>
        {
            penghuniService.TambahPenghuni(new Penghuni
            {
                KamarId = kamarId2,
                Nama = "Budi Setiawan",
                NomorTelepon = "+6281298765432",
                TanggalMasuk = DateTime.Today.AddDays(-10)
            });
        });

        // GetAll & GetByKamarId 
        Console.WriteLine("\n[2] GetAll (GetAllPenghuni)");
        PrintPenghuniList(penghuniService.GetAllPenghuni());

        Console.WriteLine($"\n[3] GetByKamarId (kamarId={kamarId1})");
        PrintPenghuniList(penghuniService.GetPenghuniByKamarId(kamarId1));

        // UbahPenghuni 
        var semuaPenghuni = penghuniService.GetAllPenghuni();
        var idPenghuni1 = semuaPenghuni.Count >= 2 ? semuaPenghuni[^1].Id : semuaPenghuni[0].Id;

        Console.WriteLine($"\n[4] Update (UbahPenghuni) Id={idPenghuni1}");
        Try($"Ubah nomor telepon penghuni Id={idPenghuni1}", () =>
        {
            penghuniService.UbahPenghuni(new Penghuni
            {
                Id = idPenghuni1,
                KamarId = kamarId1,
                Nama = "Andi Pratama",
                NomorTelepon = "089999999999",
                Email = "andi.baru@email.com",
                TanggalMasuk = DateTime.Today
            });
        });

        Console.WriteLine("\nHasil setelah update:");
        PrintPenghuniList(penghuniService.GetAllPenghuni());

        // CheckOut 
        Console.WriteLine($"\n[5] CheckOut penghuni Id={idPenghuni1}");
        Try($"CheckOut penghuni Id={idPenghuni1}", () =>
        {
            penghuniService.CheckOutPenghuni(idPenghuni1, DateTime.Today);
        });

        Console.WriteLine($"\n  Status kamar K-01 setelah checkout (harusnya Kosong):");
        PrintKamarStatus(kamarRepository, kamarId1);

        // HapusPenghuni 
        var idPenghuni2 = penghuniService.GetAllPenghuni()[0].Id;
        Console.WriteLine($"\n[6] Delete (HapusPenghuni) Id={idPenghuni2}");
        Try($"Hapus penghuni Id={idPenghuni2}", () =>
        {
            penghuniService.HapusPenghuni(idPenghuni2);
        });

        Console.WriteLine($"\n  Status kamar K-02 setelah hapus penghuni (harusnya Kosong):");
        PrintKamarStatus(kamarRepository, kamarId2);

        Console.WriteLine("\nHasil setelah delete:");
        PrintPenghuniList(penghuniService.GetAllPenghuni());

        // Validasi error
        Console.WriteLine("\n[7] Validasi error - Kamar sedang Perbaikan (harus error)");
        TryExpectError("Tambah penghuni di kamar Perbaikan", () =>
        {
            penghuniService.TambahPenghuni(new Penghuni
            {
                KamarId = kamarIdPerbaikan,
                Nama = "Test User",
                NomorTelepon = "08123456789",
                TanggalMasuk = DateTime.Today
            });
        });

        Console.WriteLine("\n[8] Validasi error - Kamar sudah Terisi (harus error)");
        // Isi dulu kamar1 lagi
        Try("Isi ulang kamar K-01", () =>
        {
            penghuniService.TambahPenghuni(new Penghuni
            {
                KamarId = kamarId1,
                Nama = "Citra Dewi",
                NomorTelepon = "085712345678",
                TanggalMasuk = DateTime.Today
            });
        });
        TryExpectError("Tambah penghuni di kamar yang sudah Terisi", () =>
        {
            penghuniService.TambahPenghuni(new Penghuni
            {
                KamarId = kamarId1,
                Nama = "Orang Lain",
                NomorTelepon = "085799999999",
                TanggalMasuk = DateTime.Today
            });
        });

        Console.WriteLine("\n[9] Validasi error - Nama kosong (harus error)");
        TryExpectError("Nama kosong", () =>
        {
            penghuniService.TambahPenghuni(new Penghuni
            {
                KamarId = kamarId2,
                Nama = "   ",
                NomorTelepon = "08123456789",
                TanggalMasuk = DateTime.Today
            });
        });

        Console.WriteLine("\n[10] Validasi error - Format email salah (harus error)");
        TryExpectError("Format email salah", () =>
        {
            penghuniService.TambahPenghuni(new Penghuni
            {
                KamarId = kamarId2,
                Nama = "Test User",
                NomorTelepon = "08123456789",
                Email = "bukan-email",
                TanggalMasuk = DateTime.Today
            });
        });

        Console.WriteLine("\n[11] Validasi error - TanggalKeluar sebelum TanggalMasuk (harus error)");
        TryExpectError("TanggalKeluar sebelum TanggalMasuk", () =>
        {
            penghuniService.TambahPenghuni(new Penghuni
            {
                KamarId = kamarId2,
                Nama = "Test User",
                NomorTelepon = "08123456789",
                TanggalMasuk = DateTime.Today,
                TanggalKeluar = DateTime.Today.AddDays(-5)
            });
        });

        Console.WriteLine("\n[12] Validasi error - KamarId tidak ada (harus error)");
        TryExpectError("KamarId tidak ditemukan di DB", () =>
        {
            penghuniService.TambahPenghuni(new Penghuni
            {
                KamarId = 999999,
                Nama = "Test User",
                NomorTelepon = "08123456789",
                TanggalMasuk = DateTime.Today
            });
        });

        Console.WriteLine("\n[13] Validasi error - HapusPenghuni Id tidak ada (harus error)");
        TryExpectError("HapusPenghuni Id tidak ditemukan", () =>
        {
            penghuniService.HapusPenghuni(999999);
        });

        Console.WriteLine("\n[14] Validasi error - CheckOut tanggal lebih awal dari masuk (harus error)");
        var aktif = penghuniService.GetAllPenghuni().Find(p => p.TanggalKeluar == null);
        if (aktif != null)
        {
            TryExpectError("CheckOut tanggal tidak valid", () =>
            {
                penghuniService.CheckOutPenghuni(aktif.Id, aktif.TanggalMasuk.AddDays(-1));
            });
        }
        else
        {
            Console.WriteLine("  SKIP - tidak ada penghuni aktif untuk test ini");
        }

        Console.WriteLine("\n== SELESAI ==");
        return 0;
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static void TryEnableConsole()
    {
        if (!AttachConsole(AttachParentProcess))
        {
            AllocConsole();
        }
    }

    private static int SeedKos(IKosRepository kosRepository)
    {
        kosRepository.Insert(new Kos
        {
            NamaKos = "Kos Test Penghuni",
            Alamat = "Jl. Test No. 1",
            HargaDasar = 800_000,
            JumlahKamar = 5,
            NamaPemilik = "Tester",
            NomorTelepon = "081234567890"
        });

        var all = kosRepository.GetAll();
        if (all == null || all.Count == 0)
            throw new InvalidOperationException("Seed Kos gagal.");

        return all[0].Id;
    }

    private static int SeedKamar(IKamarRepository kamarRepository, int kosId, string nomor, string status)
    {
        kamarRepository.Insert(new Kamar
        {
            KosId = kosId,
            NomorKamar = nomor,
            HargaKamar = 800_000,
            Status = status
        });

        var all = kamarRepository.GetByKosId(kosId);
        return all[0].Id; // ORDER BY Id DESC, jadi index 0 = yang baru diinsert
    }

    private static void PrintKamarStatus(IKamarRepository kamarRepository, int kamarId)
    {
        var kamar = kamarRepository.GetById(kamarId);
        if (kamar is null)
        {
            Console.WriteLine("  (kamar tidak ditemukan)");
            return;
        }
        Console.WriteLine($"  Id={kamar.Id}, NomorKamar={kamar.NomorKamar}, Status={kamar.Status}");
    }

    private static void PrintPenghuniList(List<Penghuni> list)
    {
        if (list == null || list.Count == 0)
        {
            Console.WriteLine("  (kosong)");
            return;
        }

        foreach (var p in list)
        {
            Console.WriteLine(
                $"  Id={p.Id}, KamarId={p.KamarId}, Nama={p.Nama}, " +
                $"Telp={p.NomorTelepon}, Masuk={p.TanggalMasuk:yyyy-MM-dd}, " +
                $"Keluar={(p.TanggalKeluar.HasValue ? p.TanggalKeluar.Value.ToString("yyyy-MM-dd") : "-")}");
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
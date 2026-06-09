# Diagram Aplikasi Management KOS

## 1. ERD (Entity Relationship Diagram)

```mermaid
erDiagram
    Role {
        int Id PK
        varchar NamaRole UK
        varchar Deskripsi
        bool IsActive
        datetime CreatedAt
    }

    AppUser {
        int Id PK
        int RoleId FK
        varchar Username UK
        varchar PasswordHash
        varchar NamaLengkap
        bool IsActive
        datetime CreatedAt
        datetime UpdatedAt
    }

    Kos {
        int Id PK
        varchar NamaKos
        varchar Alamat
        decimal HargaDasar
        int JumlahKamar
        varchar NamaPemilik
        varchar NomorTelepon
        text Catatan
        bool IsActive
    }

    Kamar {
        int Id PK
        int KosId FK
        varchar NomorKamar
        int HargaKamar
        varchar Status
        bool IsActive
    }

    Penghuni {
        int Id PK
        varchar Nama
        varchar NomorTelepon
        varchar Email
        text Catatan
        bool IsActive
    }

    KontrakSewa {
        int Id PK
        int PenghuniId FK
        int KamarId FK
        date TanggalMulai
        date TanggalSelesai
        decimal HargaSewaBulanan
        decimal Deposit
        varchar Status
        text Catatan
    }

    Pembayaran {
        int Id PK
        int KontrakSewaId FK
        date TanggalBayar
        decimal JumlahDibayar
        varchar MetodePembayaran
        text Catatan
    }

    MetodePembayaranRef {
        int Id PK
        varchar NamaMetode UK
        bool IsActive
        datetime CreatedAt
    }

    Role ||--o{ AppUser : "dimiliki oleh"
    Kos ||--o{ Kamar : "memiliki"
    Penghuni ||--o{ KontrakSewa : "menyewa melalui"
    Kamar ||--o{ KontrakSewa : "disewa dalam"
    KontrakSewa ||--o{ Pembayaran : "menghasilkan"
```

Catatan:

- `AppUser` adalah akun admin/operator yang boleh masuk aplikasi.
- `Role` dan `MetodePembayaranRef` adalah data reference.
- `Kos`, `Kamar`, dan `Penghuni` adalah data master.
- `KontrakSewa` dan `Pembayaran` adalah data transaksi.
- `Penghuni` tidak lagi menentukan kamar langsung. Penghuni baru mendapatkan kamar saat admin membuat `KontrakSewa`.
- Harga sewa kontrak mengambil `Kamar.HargaKamar`, bukan input manual di form kontrak.
- Data user dan master memakai soft delete melalui `IsActive`.

---

## 2. Flowchart POV Admin

```mermaid
flowchart TD
    START([Admin membuka aplikasi]) --> CEK_DB{MySQL aktif?}
    CEK_DB -- Tidak --> DB_ERR[/Tampil pesan koneksi gagal/]
    DB_ERR --> EXIT([Aplikasi keluar])

    CEK_DB -- Ya --> INIT[Init database dan jalankan migration]
    INIT --> LOGIN[Form Login]
    LOGIN --> AUTH{Username dan password valid?}
    AUTH -- Tidak --> LOGIN_ERR[/Tampil error login/]
    LOGIN_ERR --> LOGIN
    AUTH -- Batal --> EXIT
    AUTH -- Ya --> DASH[Dashboard Admin]

    DASH --> SETUP{Data awal sudah lengkap?}
    SETUP -- Belum ada Kos --> KOS[Kelola Data Kos]
    KOS --> KOS_CRUD[Tambah / edit / nonaktifkan Kos]
    KOS_CRUD --> SETUP

    SETUP -- Belum ada Kamar --> KAMAR[Kelola Data Kamar]
    KAMAR --> PILIH_KOS[Pilih Kos]
    PILIH_KOS --> KAMAR_CRUD[Tambah / edit / nonaktifkan Kamar]
    KAMAR_CRUD --> SETUP

    SETUP -- Belum ada Penghuni --> PENGHUNI[Kelola Data Penghuni]
    PENGHUNI --> PENGHUNI_CRUD[Tambah / edit / nonaktifkan data identitas penghuni]
    PENGHUNI_CRUD --> SETUP

    SETUP -- Lengkap --> KONTRAK[Menu Kontrak Sewa]
    KONTRAK --> PILIH_PENGHUNI[Pilih Penghuni]
    PILIH_PENGHUNI --> PILIH_KOS_KON[Pilih Kos]
    PILIH_KOS_KON --> FILTER_KAMAR[Tampilkan kamar dari Kos terpilih]
    FILTER_KAMAR --> PILIH_KAMAR[Pilih Kamar]
    PILIH_KAMAR --> HARGA_AUTO[Harga kontrak otomatis dari harga kamar]
    HARGA_AUTO --> PILIH_METODE[Pilih metode pembayaran dari MetodePembayaranRef]
    PILIH_METODE --> STATUS{Status kontrak?}

    STATUS -- Dipesan --> DEPOSIT_INPUT[Deposit boleh diisi]
    DEPOSIT_INPUT --> SIMPAN_KONTRAK[Simpan Kontrak Dipesan]
    SIMPAN_KONTRAK --> PAY_AWAL_DPS[Create Pembayaran awal dari deposit atau harga sewa]
    PAY_AWAL_DPS --> KAMAR_DIPESAN[Kamar berstatus Dipesan]

    STATUS -- Aktif --> DEPOSIT_ZERO[Deposit otomatis 0 dan field deposit disable]
    DEPOSIT_ZERO --> SIMPAN_AKTIF[Simpan Kontrak Aktif]
    SIMPAN_AKTIF --> PAY_AWAL_AKTIF[Create Pembayaran awal sebesar harga sewa]
    PAY_AWAL_AKTIF --> KAMAR_TERISI[Kamar berstatus Terisi]

    KAMAR_DIPESAN --> MONITOR[Admin memantau daftar kontrak]
    KAMAR_TERISI --> MONITOR

    MONITOR --> AKSI{Aksi berikutnya?}
    AKSI -- Edit kontrak --> EDIT_KONTRAK[Update data kontrak]
    EDIT_KONTRAK --> MONITOR

    AKSI -- Batalkan kontrak --> BATAL[Batal kontrak]
    BATAL --> RESET_KAMAR{Masih ada kontrak aktif/dipesan di kamar?}
    RESET_KAMAR -- Tidak --> KAMAR_KOSONG[Kamar kembali Kosong]
    RESET_KAMAR -- Ya --> MONITOR
    KAMAR_KOSONG --> MONITOR

    AKSI -- Catat lunas --> LUNAS[Klik tombol Lunas]
    LUNAS --> POPUP_METODE[/Popup pilih metode pembayaran/]
    POPUP_METODE --> CREATE_PAY[Create Pembayaran]
    CREATE_PAY --> SET_LUNAS[Set JumlahDibayar = HargaSewaBulanan]
    SET_LUNAS --> MONITOR

    AKSI -- Selesai --> END([Operasional selesai])
```

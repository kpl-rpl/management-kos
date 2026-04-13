# Aplikasi Management Kos (C# WinForms)
Project ini adalah **aplikasi Management Kos**.
Arsitektur disusun sederhana agar mudah dipahami, mudah dirawat, dan mudah dikembangkan untuk modul lanjutan.

## 1. Daftar Modul Sistem dan Status Implementasi

Berikut daftar modul utama dalam aplikasi ini beserta status implementasinya saat ini:

- ✅ **Data Kos** (`Kos`) — **Sudah terimplementasi**
- ✅ **Data Kamar** (`Kamar`) — **Sudah terimplementasi**
- ✅ **Data Penghuni** (`Penghuni`) — **Sudah terimplementasi**
- ⏳ **Kontrak Sewa** (`KontrakSewa`) — **Belum terimplementasi**
- ⏳ **Check In / Check Out** (`CheckInCheckOut`) — **Belum terimplementasi**
- ⏳ **Pembayaran** (`Pembayaran`) — **Belum terimplementasi**
- ⏳ **Tagihan Non Sewa** (`TagihanNonSewa`) — **Belum terimplementasi**
- ⏳ **Maintenance Kamar** (`MaintenanceKamar`) — **Belum terimplementasi**
- ⏳ **Notifikasi / Reminder** (`NotifikasiReminder`) — **Belum terimplementasi**
- ⏳ **Laporan / Dashboard** (`LaporanDashboard`) — **Belum terimplementasi**

## 2. Struktur Project

```text
management-kos/
├── .env.example
├── Data/
│   ├── Migrations/
│   │   ├── 202604010001_create_kos_table.sql
│   │   ├── 202604010002_create_kamar_table.sql
│   │   └── 202604010003_add_harga_kamar_to_kamar.sql
│   └── MySqlDbContext.cs
├── Models/
│   ├── Kos.cs
│   └── Kamar.cs
├── Repositories/
│   ├── IKosRepository.cs
│   └── KosRepository.cs
├── Services/
│   └── KosService.cs
├── UI/
│   ├── FormKos.cs
│   └── FormKos.Designer.cs
├── Program.cs
└── README.md
```


Fungsi folder:
- `UI/`: tampilan form, navigasi, dan event user.
- `Services/`: validasi dan business logic.
- `Models/`: representasi data/domain.
- `Repositories/`: akses database (CRUD).
- `Data/`: koneksi database dan runner migration.

## 3. Implementasi Boilerplate Modul Data Kos

Sudah diimplementasikan:
- **Model**: `Kos`, `Kamar`
- **Repository**: `IKosRepository`, `KosRepository` (`GetAll`, `GetById`, `Insert`, `Update`, `Delete`)
- **Service**: `KosService` (validasi + logic sederhana)
- **UI**: `FormKos` dengan:
  - TextBox input
  - Button `Tambah`, `Update`, `Hapus`, `Reset`
  - `DataGridView` untuk daftar data

## 4. Alur Arsitektur

Alur data:
`UI -> Service -> Repository -> Database`

Contoh tambah data:
1. User isi form lalu klik **Tambah**.
2. `FormKos` kirim data ke `KosService.TambahKos(...)`.
3. `KosService` validasi input.
4. Jika valid, `KosService` panggil `KosRepository.Insert(...)`.
5. `KosRepository` menjalankan SQL ke MySQL.
6. UI refresh daftar data.

## 5. Dokumentasi Penggunaan

### Cara menjalankan
1. Buka solution di Visual Studio.
2. Pastikan MySQL Server aktif (default: `localhost:3306`).
3. Copy `.env.example` menjadi `.env` lalu isi konfigurasi MySQL.
4. Pastikan restore NuGet berhasil (`MySqlConnector`).
5. Jalankan project (`F5`).

Contoh isi `.env`:

```env
DB_HOST=localhost
DB_PORT=3306
DB_USER=root
DB_PASSWORD=
DB_NAME=management_kos
```

### Cara menggunakan fitur Data Kos
1. Isi form data kos.
2. Klik **Tambah** untuk simpan.
3. Klik baris pada grid untuk memilih data.
4. Edit data lalu klik **Update**.
5. Klik **Hapus** untuk menghapus data terpilih.
6. Klik **Reset Form** untuk membersihkan input.

### Cara menambah modul baru
1. Buat model baru di `Models/` (contoh: `Penyewa`).
2. Buat interface + repository di `Repositories/`.
3. Buat service baru di `Services/` untuk validasi dan logic.
4. Buat form baru di `UI/`.
5. Hubungkan form dengan service di `Program.cs` atau navigasi menu utama.

### Cara migration per-file (mirip Laravel)

Setiap perubahan schema dibuat dalam **1 file SQL** di folder `Data/Migrations`.
File akan dijalankan otomatis saat aplikasi start jika belum tercatat di tabel `SchemaMigrations`.

Generate file migration baru:

```powershell
powershell -ExecutionPolicy Bypass -File .\scripts\new-migration.ps1 -Name "add lantai kamar"
```

Contoh hasil file:

```text
Data/Migrations/20260401124530_add_lantai_kamar.sql
```

Lalu isi SQL di file tersebut, contoh:

```sql
ALTER TABLE Kamar ADD COLUMN Lantai INT NOT NULL DEFAULT 1;
```

## 6. Standar Coding

- **Naming convention**
  - Class/Method/Property: `PascalCase`
  - Variabel lokal/field private: `camelCase` (field private boleh `_camelCase`)
  - Interface: prefix `I` (contoh: `IKosRepository`)
- **Struktur method**
  - Method pendek, 1 tanggung jawab.
  - Validasi diletakkan di service, bukan di repository.
  - SQL hanya di repository.
- **Penambahan fitur baru**
  - Ikuti urutan: `Model -> Repository -> Service -> UI`.
  - Hindari akses database langsung dari UI.
  - Jangan campur validasi bisnis ke layer UI.

## 7. Batasan Implementasi

- Bahasa: Indonesia
- Desain: sederhana, fokus pembelajaran
- Data access: ADO.NET (MySQL)
- Tidak menggunakan framework berat/ORM kompleks

## 8. Database Plan

Bagian ini adalah rencana struktur database yang saya pakai sebagai acuan pengembangan lanjutan, supaya schema tetap rapi saat modul bertambah.

### 8.1 Tabel yang sudah ada

1. **Kos**
   - `Id` (PK)
   - `NamaKos`
   - `Alamat`
   - `HargaDasar`
   - `JumlahKamar`
   - `NamaPemilik`
   - `NomorTelepon`
   - `Catatan` (nullable)

2. **Kamar**
   - `Id` (PK)
   - `KosId` (FK -> `Kos.Id`)
   - `NomorKamar`
   - `HargaKamar`
   - `Status`

3. **Penghuni**
   - `Id` (PK)
   - `KamarId` (FK -> `Kamar.Id`)
   - `Nama`
   - `NomorTelepon`
   - `Email` (nullable)
   - `TanggalMasuk`
   - `TanggalKeluar` (nullable)
   - `Catatan` (nullable)

### 8.2 Tabel inti berikutnya

1. **KontrakSewa**
   - `Id` (PK)
   - `PenghuniId` (FK -> `Penghuni.Id`)
   - `KamarId` (FK -> `Kamar.Id`)
   - `TanggalMulai` (DATE)
   - `TanggalSelesai` (DATE)
   - `HargaSewaBulanan` (DECIMAL)
   - `Deposit` (DECIMAL, nullable)
   - `Status` (Aktif/Selesai/Dibatalkan)
   - `Catatan` (TEXT, nullable)

2. **Pembayaran**
   - `Id` (PK)
   - `KontrakSewaId` (FK -> `KontrakSewa.Id`)
   - `Periode` (VARCHAR, contoh: `2026-04`)
   - `TanggalBayar` (DATE, nullable jika belum bayar)
   - `JumlahTagihan` (DECIMAL)
   - `JumlahDibayar` (DECIMAL)
   - `MetodePembayaran` (Transfer/Tunai/E-Wallet)
   - `Status` (BelumBayar/Lunas/Parsial)
   - `Catatan` (TEXT, nullable)

3. **TagihanTambahan**
   - `Id` (PK)
   - `KontrakSewaId` (FK)
   - `JenisTagihan` (Listrik/Air/Internet/Denda/Lainnya)
   - `Nominal` (DECIMAL)
   - `Periode` (VARCHAR)
   - `Status` (BelumBayar/Lunas)

4. **MaintenanceKamar**
   - `Id` (PK)
   - `KamarId` (FK)
   - `TanggalLapor` (DATE)
   - `Deskripsi` (TEXT)
   - `EstimasiBiaya` (DECIMAL, nullable)
   - `BiayaAktual` (DECIMAL, nullable)
   - `Status` (Dilaporkan/Proses/Selesai)

### 8.3 Relasi utama (high-level)
- `Kos` 1..N `Kamar`
- `Kamar` 1..N `KontrakSewa`
- `Penghuni` 1..N `KontrakSewa`
- `KontrakSewa` 1..N `Pembayaran`
- `KontrakSewa` 1..N `TagihanTambahan`
- `Kamar` 1..N `MaintenanceKamar`

### 8.4 Strategi migration
- Satu perubahan schema = satu file migration SQL di `Data/Migrations`.
- Gunakan naming: `yyyyMMddHHmmss_nama_migration.sql`.
- Jangan edit migration lama yang sudah pernah dijalankan.
- Jika ada perubahan, buat migration baru (incremental).

Contoh command generate migration:

```powershell
powershell -ExecutionPolicy Bypass -File .\scripts\new-migration.ps1 -Name "create table kontrak sewa"
```

## 9. Implementasi Requirement Penilaian Tugas Besar

Bagian ini merangkum requirement tugas besar yang diterapkan pada project ini.

### 9.1 Requirement tingkat kelompok

1. **Remote repository (10%)**
   - Project dikelola menggunakan Git dan remote repository (`origin`).

2. **Laporan tugas besar (10%)**
   - `README.md` ini digunakan sebagai dasar dokumentasi teknis proyek.
   - Laporan final dapat menggunakan struktur pada README ini (arsitektur, modul, database plan, pengujian).

### 9.2 Requirement tingkat individu

1. **Version control per anggota (70%)**
   - Setiap anggota wajib melakukan penambahan/perbaikan fitur dengan branch terpisah
   - Format nama branch `nama_contributor/nama_fitur` (contoh 'dipras/UI-Kos').
   - Setiap anggota wajib melakukan pull request ke `main/master` untuk merge fitur.
   - Setiap anggota wajib punya riwayat commit yang jelas sebelum merge ke `main/master`.

2. **Unit testing per modul**
   - Setiap service wajib memiliki test runner.
   - Contoh yang sudah ada: `KamarServiceTestRunner`.
   - Contoh run test via terminal:

   ```powershell
   dotnet run --project .\management-kos.csproj -p:StartupObject=management_kos.Services.KamarServiceTestRunner
   ```

3. **Performance testing pada halaman/modul**
   - Setiap anggota wajib melakukan performance testing pada bagian yang dikerjakan.
   - Minimal yang dicatat: skenario uji, metrik waktu respons, dan hasil sebelum/sesudah perbaikan.

4. **Defensive programming / Design by Contract (DbC)**
   - Validasi input dan guard clause wajib ada di layer service.
   - Contoh implementasi: validasi pada `KosService`, `KamarService`, `PenghuniService`.

5. **Dua teknik konstruksi (pilih sesuai pembagian anggota)**
   - Teknik yang dapat dipakai:
     - `Automata`
     - `Table-driven construction`
     - `Parameterization/generics`
     - `Runtime configuration`
     - `Code reuse/library`
     - `API`
   - Catatan: tiap teknik hanya boleh dipakai maksimal oleh dua mahasiswa sesuai aturan tugas.

### 9.3 Pembagian teknik per anggota

Pembagian berikut dipakai sebagai acuan tim, dengan aturan tiap teknik maksimal digunakan oleh dua orang.

1. **Dipras (Leader)**
   - `Runtime configuration` ✅
   - `Code reuse/library`
   - Catatan: `Runtime configuration` ditetapkan untuk Dipras karena menangani setup project.

2. **Renjiro**
   - `Automata`
   - `Table-driven construction`

3. **Alif**
   - `Parameterization/generics`
   - `Code reuse/library`

4. **Aqsa**
   - `Automata`
   - `Runtime configuration`

5. **Aurel**
   - `Table-driven construction`
   - `Parameterization/generics`

Ringkasan kuota teknik:
- `Automata` → 2 orang
- `Table-driven construction` → 2 orang
- `Parameterization/generics` → 2 orang
- `Runtime configuration` → 2 orang
- `Code reuse/library` → 2 orang
- `API` → belum dipakai (opsional)

### 9.4 Checklist implementasi tim

- [x] Remote repository aktif
- [x] Dokumentasi teknis dasar tersedia di README
- [x] Struktur branch + commit per anggota diterapkan
- [x] Unit test runner per service mulai diterapkan
- [ ] Performance testing per modul didokumentasikan
- [x] Defensive programming diterapkan pada service
- [ ] Dua teknik konstruksi per anggota didokumentasikan di laporan final

### 9.5 Aturan Rotasi Role dan Dependensi Kerja

Bagian ini dipakai sebagai aturan kerja tim saat role dirotasi (`UI`, `Service`, `Repository`, `Tester`).

#### A. Urutan kerja per modul (default)

1. `Service`
2. `Repository`
3. `UI`
4. `Tester`

Catatan: urutan ini adalah default supaya alur kerja stabil. Dalam praktik, beberapa aktivitas bisa paralel jika kontrak method sudah disepakati.

#### B. Batas mulai kerja tiap role (Definition of Ready)

1. **Service boleh mulai jika:**
   - Model sudah ada / disepakati.
   - Kebutuhan fitur dan validasi sudah jelas.

2. **Repository boleh mulai jika:**
   - Kontrak interface repository sudah disepakati.
   - Struktur tabel/migration untuk modul tersebut sudah tersedia.

3. **UI boleh mulai jika:**
   - Kontrak method service untuk CRUD sudah jelas.
   - Struktur data yang ditampilkan di form sudah disepakati.

4. **Tester boleh mulai jika:**
   - **Tidak harus menunggu semua role selesai.**
   - Untuk **unit test service**: cukup menunggu method service dan interface dependency tersedia.
   - Untuk **integration test**: menunggu service + repository + migration siap dijalankan.

#### C. Jawaban singkat untuk role Tester

- Tester **tidak wajib menunggu UI selesai**.
- Tester minimal menunggu **Service siap** (untuk unit test).
- Jika test menyentuh database asli, tester menunggu **Repository + Migration siap**.

#### D. Output wajib tiap role per putaran

1. **UI**
   - Perubahan form/view + event handler.
   - Bukti screenshot atau demo flow.

2. **Service**
   - Business logic + validasi/guard clause.

3. **Repository**
   - Implementasi query terparameter + migration (jika ubah schema).

4. **Tester**
   - Test case berbasis assertion.
   - Hasil run test + catatan bug (jika ada).

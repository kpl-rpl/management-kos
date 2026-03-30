# Boilerplate Management Kos (C# WinForms)

Project ini adalah template awal aplikasi **Management Kos** dengan fokus modul pertama: **Data Kos**.
Arsitektur dibuat sederhana agar mudah dipahami mahasiswa dan mudah dikembangkan tim.

## 1. Requirement Modul Data Kos

### Field `Kos`
- `Id` (int, auto increment)
- `NamaKos` (string)
- `Alamat` (string)
- `HargaDasar` (decimal)
- `JumlahKamar` (int)
- `NamaPemilik` (string)
- `NomorTelepon` (string)
- `Catatan` (string, opsional)

### Validasi
- `NamaKos`, `Alamat`, `NamaPemilik`, `NomorTelepon` tidak boleh kosong.
- `HargaDasar` wajib angka dan > 0.
- `JumlahKamar` wajib angka bulat dan > 0.
- `NomorTelepon` format dasar: angka / `+` / `-` / spasi, panjang 8-20 karakter.

### Fitur utama
- Tambah data kos
- Lihat daftar kos
- Edit data kos
- Hapus data kos

### Relasi sederhana
- Satu `Kos` memiliki banyak `Kamar` (relasi `1:N`).
- Tabel `Kamar` sudah disiapkan sebagai dasar modul berikutnya.

## 2. Struktur Project

```text
management-kos/
├── .env.example
├── Data/
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
- `Data/`: koneksi database dan inisialisasi schema.

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

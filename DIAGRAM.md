# Diagram Aplikasi Management KOS

## 1. ERD (Entity Relationship Diagram)

```mermaid
erDiagram
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
        int KamarId FK
        varchar Nama
        varchar NomorTelepon
        varchar Email
        date TanggalMasuk
        date TanggalKeluar
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
        varchar Periode
        date TanggalBayar
        decimal JumlahTagihan
        decimal JumlahDibayar
        varchar MetodePembayaran
        varchar Status
        text Catatan
    }

    Role {
        int Id PK
        varchar NamaRole
        varchar Deskripsi
        bool IsActive
    }

    AppUser {
        int Id PK
        int RoleId FK
        varchar Username
        varchar PasswordHash
        varchar NamaLengkap
        bool IsActive
        datetime CreatedAt
        datetime UpdatedAt
    }

    MetodePembayaranRef {
        int Id PK
        varchar NamaMetode
        bool IsActive
    }

    Kos ||--o{ Kamar : "memiliki"
    Kamar ||--o{ Penghuni : "ditempati oleh"
    Kamar ||--o{ KontrakSewa : "terikat dalam"
    Penghuni ||--o{ KontrakSewa : "membuat"
    KontrakSewa ||--o{ Pembayaran : "menghasilkan"
    Role ||--o{ AppUser : "memberi hak akses"
```

### Catatan Normalisasi dan Requirement Basis Data

- `Role` dan `MetodePembayaranRef` adalah tabel reference.
- `AppUser` adalah tabel user/autentikasi.
- `Kos`, `Kamar`, dan `Penghuni` adalah data master.
- `KontrakSewa` dan `Pembayaran` adalah data transaksi.
- Data user dan master memakai soft delete melalui `IsActive`, bukan hapus fisik.
- Trigger database pada `KontrakSewa` menjaga sinkronisasi dasar ke `Kamar.Status` saat `INSERT`, `UPDATE`, dan `DELETE`.

---

## 2. Sequence Diagram

### 2.1 Tambah Kos

```mermaid
sequenceDiagram
    actor User
    participant Form as FormKos
    participant Svc as KosService
    participant Repo as KosRepository
    participant DB as MySQL

    User->>Form: Isi form & klik Tambah
    Form->>Svc: TambahKos(kos)
    Svc->>Svc: Validasi field (nama, alamat, harga, dll)
    alt Validasi gagal
        Svc-->>Form: throw Exception
        Form-->>User: MessageBox error
    else Validasi OK
        Svc->>Repo: Insert(kos)
        Repo->>DB: INSERT INTO Kos (NamaKos, Alamat, HargaDasar, ...) VALUES (...)
        DB-->>Repo: OK
        Repo-->>Svc: void
        Svc-->>Form: void
        Form->>Repo: GetAll()
        Repo->>DB: SELECT * FROM Kos ORDER BY Id DESC
        DB-->>Repo: rows
        Repo-->>Form: List<Kos>
        Form-->>User: Refresh grid
    end
```

### 2.2 Tambah Kamar

```mermaid
sequenceDiagram
    actor User
    participant Form as FormKamar
    participant Svc as KamarService
    participant KamRepo as KamarRepository
    participant KosRepo as KosRepository
    participant DB as MySQL

    User->>Form: Pilih Kos, isi form & klik Tambah
    Form->>Svc: TambahKamar(kamar)
    Svc->>Svc: Validasi field (nomorKamar, harga, status)
    Svc->>KosRepo: GetById(kosId)
    KosRepo->>DB: SELECT * FROM Kos WHERE Id = @Id
    DB-->>KosRepo: row / null
    alt Kos tidak ditemukan
        KosRepo-->>Svc: null
        Svc-->>Form: throw Exception
        Form-->>User: MessageBox error
    else Kos ada
        Svc->>KamRepo: Insert(kamar)
        KamRepo->>DB: INSERT INTO Kamar (KosId, NomorKamar, HargaKamar, Status) VALUES (...)
        DB-->>KamRepo: OK
        KamRepo-->>Svc: void
        Svc-->>Form: void
        Form->>KamRepo: GetByKosId(kosId)
        KamRepo->>DB: SELECT * FROM Kamar WHERE KosId = @KosId
        DB-->>KamRepo: rows
        KamRepo-->>Form: List<Kamar>
        Form-->>User: Refresh grid
    end
```

### 2.3 Tambah Penghuni

```mermaid
sequenceDiagram
    actor User
    participant Form as FormPenghuni
    participant Svc as PenghuniService
    participant PenRepo as PenghuniRepository
    participant KamRepo as KamarRepository
    participant DB as MySQL

    User->>Form: Pilih Kamar, isi form & klik Tambah
    Form->>Svc: TambahPenghuni(penghuni)
    Svc->>Svc: Validasi field (nama, telepon, tanggal)
    Svc->>KamRepo: GetById(kamarId)
    KamRepo->>DB: SELECT * FROM Kamar WHERE Id = @Id
    DB-->>KamRepo: row
    alt Kamar Perbaikan / Terisi
        Svc-->>Form: throw Exception
        Form-->>User: MessageBox error
    else Kamar tersedia
        Svc->>PenRepo: Insert(penghuni)
        PenRepo->>DB: INSERT INTO Penghuni (KamarId, Nama, NomorTelepon, ...) VALUES (...)
        DB-->>PenRepo: OK
        Svc->>KamRepo: GetById(kamarId)
        KamRepo->>DB: SELECT * FROM Kamar WHERE Id = @Id
        DB-->>KamRepo: row
        Svc->>KamRepo: Update(kamar{Status=Terisi})
        KamRepo->>DB: UPDATE Kamar SET Status='Terisi' WHERE Id = @Id
        DB-->>KamRepo: OK
        Svc-->>Form: void
        Form->>PenRepo: GetAll()
        PenRepo->>DB: SELECT * FROM Penghuni ORDER BY Id DESC
        DB-->>PenRepo: rows
        PenRepo-->>Form: List<Penghuni>
        Form-->>User: Refresh grid
    end
```

### 2.4 Hapus Penghuni

```mermaid
sequenceDiagram
    actor User
    participant Form as FormPenghuni
    participant Svc as PenghuniService
    participant PenRepo as PenghuniRepository
    participant KamRepo as KamarRepository
    participant DB as MySQL

    User->>Form: Pilih penghuni & klik Hapus
    Form->>Svc: HapusPenghuni(id)
    Svc->>PenRepo: GetById(id)
    PenRepo->>DB: SELECT * FROM Penghuni WHERE Id = @Id
    DB-->>PenRepo: row
    Svc->>PenRepo: Delete(id)
    PenRepo->>DB: DELETE FROM Penghuni WHERE Id = @Id
    DB-->>PenRepo: OK
    Note over Svc: Recalculate status kamar lama
    Svc->>PenRepo: GetByKamarId(kamarId)
    PenRepo->>DB: SELECT * FROM Penghuni WHERE KamarId = @KamarId
    DB-->>PenRepo: rows
    alt Tidak ada penghuni aktif tersisa
        Svc->>KamRepo: GetById(kamarId)
        KamRepo->>DB: SELECT * FROM Kamar WHERE Id = @Id
        DB-->>KamRepo: row
        Svc->>KamRepo: Update(kamar{Status=Kosong})
        KamRepo->>DB: UPDATE Kamar SET Status='Kosong' WHERE Id = @Id
        DB-->>KamRepo: OK
    end
    Svc-->>Form: void
    Form-->>User: Refresh grid
```

### 2.5 Tambah Kontrak Sewa

```mermaid
sequenceDiagram
    actor User
    participant Form as FormKontrakSewa
    participant Svc as KontrakSewaService
    participant KonRepo as KontrakSewaRepository
    participant PenRepo as PenghuniRepository
    participant KamRepo as KamarRepository
    participant DB as MySQL

    User->>Form: Pilih Penghuni & Kamar, isi form & klik Tambah
    Form->>Svc: TambahKontrak(kontrak)
    Svc->>Svc: Validasi field (harga, tanggal, deposit)
    Svc->>PenRepo: GetById(penghuniId)
    PenRepo->>DB: SELECT * FROM Penghuni WHERE Id = @Id
    DB-->>PenRepo: row
    Svc->>KamRepo: GetById(kamarId)
    KamRepo->>DB: SELECT * FROM Kamar WHERE Id = @Id
    DB-->>KamRepo: row
    alt Kamar Perbaikan atau ada kontrak aktif konflik
        Svc->>KonRepo: GetByKamarId(kamarId)
        KonRepo->>DB: SELECT * FROM KontrakSewa WHERE KamarId = @KamarId
        DB-->>KonRepo: rows
        Svc-->>Form: throw Exception
        Form-->>User: MessageBox error
    else OK
        Svc->>KonRepo: Insert(kontrak)
        KonRepo->>DB: INSERT INTO KontrakSewa (PenghuniId, KamarId, TanggalMulai, ...) VALUES (...)
        DB-->>KonRepo: OK
        Note over Svc: UpdateKamarStatusForKontrak
        Svc->>KamRepo: GetById(kamarId)
        KamRepo->>DB: SELECT * FROM Kamar WHERE Id = @Id
        DB-->>KamRepo: row
        alt Status kontrak = Aktif
            Svc->>KamRepo: Update(kamar{Status=Terisi})
            KamRepo->>DB: UPDATE Kamar SET Status='Terisi' WHERE Id = @Id
        else Status kontrak = Dipesan
            Svc->>KamRepo: Update(kamar{Status=Dipesan})
            KamRepo->>DB: UPDATE Kamar SET Status='Dipesan' WHERE Id = @Id
        end
        DB-->>KamRepo: OK
        Svc-->>Form: void
        Form->>KonRepo: GetAll()
        KonRepo->>DB: SELECT * FROM KontrakSewa ORDER BY Id DESC
        DB-->>KonRepo: rows
        KonRepo-->>Form: List<KontrakSewa>
        Form-->>User: Refresh grid
    end
```

### 2.6 Selesaikan / Batalkan Kontrak Sewa

```mermaid
sequenceDiagram
    actor User
    participant Form as FormKontrakSewa
    participant Svc as KontrakSewaService
    participant KonRepo as KontrakSewaRepository
    participant KamRepo as KamarRepository
    participant DB as MySQL

    User->>Form: Pilih kontrak & klik Selesai / Batal
    alt Selesaikan
        Form->>Svc: SelesaikanKontrak(id)
    else Batalkan
        Form->>Svc: BatalkanKontrak(id)
    end
    Svc->>KonRepo: GetById(id)
    KonRepo->>DB: SELECT * FROM KontrakSewa WHERE Id = @Id
    DB-->>KonRepo: row
    Svc->>KonRepo: Update(kontrak{Status=Selesai/Dibatalkan})
    KonRepo->>DB: UPDATE KontrakSewa SET Status=@Status WHERE Id = @Id
    DB-->>KonRepo: OK
    Note over Svc: ResetKamarIfNoActiveOrBooked
    Svc->>KonRepo: GetByKamarId(kamarId)
    KonRepo->>DB: SELECT * FROM KontrakSewa WHERE KamarId = @KamarId
    DB-->>KonRepo: rows
    alt Tidak ada kontrak Aktif/Dipesan tersisa
        Svc->>KamRepo: GetById(kamarId)
        KamRepo->>DB: SELECT * FROM Kamar WHERE Id = @Id
        DB-->>KamRepo: row
        Svc->>KamRepo: Update(kamar{Status=Kosong})
        KamRepo->>DB: UPDATE Kamar SET Status='Kosong' WHERE Id = @Id
        DB-->>KamRepo: OK
    end
    Svc-->>Form: void
    Form-->>User: Refresh grid
```

### 2.7 Tambah Tagihan Pembayaran

```mermaid
sequenceDiagram
    actor User
    participant Form as FormPembayaran
    participant Svc as PembayaranService
    participant Repo as PembayaranRepository
    participant DB as MySQL

    User->>Form: Pilih Kontrak, isi Periode & Tagihan, klik Tambah
    Form->>Svc: TambahTagihan(pembayaran)
    Svc->>Svc: Validasi field (kontrakId, periode, jumlahTagihan)
    Note over Svc: DetermineEvent(dibayar, tagihan)
    Note over Svc: GetNextState(BelumBayar, event) → Status
    Svc->>Repo: Insert(pembayaran)
    Repo->>DB: INSERT INTO Pembayaran (KontrakSewaId, Periode, JumlahTagihan, JumlahDibayar, Status, ...) VALUES (...)
    DB-->>Repo: OK
    Repo-->>Svc: void
    Svc-->>Form: void
    Form->>Repo: GetByKontrakSewaId(kontrakId)
    Repo->>DB: SELECT * FROM Pembayaran WHERE KontrakSewaId = @KontrakSewaId
    DB-->>Repo: rows
    Repo-->>Form: List<Pembayaran>
    Form-->>User: Refresh grid
```

### 2.8 Bayar Tagihan

```mermaid
sequenceDiagram
    actor User
    participant Form as FormPembayaran
    participant Svc as PembayaranService
    participant Repo as PembayaranRepository
    participant DB as MySQL

    User->>Form: Pilih tagihan, isi nominal & metode, klik Bayar
    Form->>Svc: BayarTagihan(id, nominal, metode)
    Svc->>Repo: GetById(id)
    Repo->>DB: SELECT * FROM Pembayaran WHERE Id = @Id
    DB-->>Repo: row
    Svc->>Svc: Update JumlahDibayar & TanggalBayar
    Note over Svc: DetermineEvent(dibayar, tagihan)
    Note over Svc: GetNextState(statusLama, event) → statusBaru
    alt statusBaru = Lunas
        Note over Svc: Status → Lunas
    else statusBaru = Dicicil
        Note over Svc: Status → Dicicil
    else statusBaru = BelumBayar
        Note over Svc: Status → BelumBayar
    end
    Svc->>Repo: Update(pembayaran)
    Repo->>DB: UPDATE Pembayaran SET JumlahDibayar=@JumlahDibayar, Status=@Status, TanggalBayar=@TanggalBayar, MetodePembayaran=@MetodePembayaran WHERE Id = @Id
    DB-->>Repo: OK
    Repo-->>Svc: void
    Svc-->>Form: void
    Form->>Repo: GetByKontrakSewaId(kontrakId)
    Repo->>DB: SELECT * FROM Pembayaran WHERE KontrakSewaId = @KontrakSewaId
    DB-->>Repo: rows
    Repo-->>Form: List<Pembayaran>
    Form-->>User: Refresh grid
```

---

## 3. Flowchart Alur Aplikasi

```mermaid
flowchart TD
    START([Aplikasi Start]) --> CEK_DB{MySQL aktif?}
    CEK_DB -- Tidak --> ALERT[/Alert: Koneksi Gagal/]
    ALERT --> EXIT([Aplikasi Keluar])
    CEK_DB -- Ya --> INIT[Init Database & Migrasi]
    INIT --> MAIN[FormMain - Dashboard]

    MAIN --> M_KOS[Menu: Data Kos]
    MAIN --> M_KAMAR[Menu: Data Kamar]
    MAIN --> M_PENGHUNI[Menu: Data Penghuni]
    MAIN --> M_KONTRAK[Menu: Kontrak Sewa]
    MAIN --> M_BAYAR[Menu: Pembayaran]

    %% ── MODUL KOS ──
    M_KOS --> KOS_LIST[Tampil Daftar Kos]
    KOS_LIST --> KOS_TAMBAH[Tambah Kos]
    KOS_LIST --> KOS_EDIT[Edit Kos]
    KOS_LIST --> KOS_HAPUS[Hapus Kos]
    KOS_TAMBAH & KOS_EDIT --> KOS_VALID{Validasi}
    KOS_VALID -- Gagal --> KOS_ERR[/Tampil Error/]
    KOS_VALID -- OK --> KOS_SAVE[Simpan ke DB]
    KOS_SAVE --> KOS_LIST

    %% ── MODUL KAMAR ──
    M_KAMAR --> KAM_LIST[Tampil Daftar Kamar per Kos]
    KAM_LIST --> KAM_TAMBAH[Tambah Kamar]
    KAM_LIST --> KAM_EDIT[Edit Kamar]
    KAM_LIST --> KAM_HAPUS[Hapus Kamar]
    KAM_TAMBAH & KAM_EDIT --> KAM_VALID{Validasi}
    KAM_VALID -- Gagal --> KAM_ERR[/Tampil Error/]
    KAM_VALID -- OK --> KAM_SAVE[Simpan ke DB]
    KAM_SAVE --> KAM_LIST

    %% ── MODUL PENGHUNI ──
    M_PENGHUNI --> PEN_LIST[Tampil Daftar Penghuni]
    PEN_LIST --> PEN_TAMBAH[Tambah Penghuni]
    PEN_LIST --> PEN_EDIT[Edit Penghuni]
    PEN_LIST --> PEN_HAPUS[Hapus Penghuni]

    PEN_TAMBAH --> PEN_VALID{Validasi}
    PEN_VALID -- Gagal --> PEN_ERR[/Tampil Error/]
    PEN_VALID -- OK --> PEN_CEK{Kamar\ntersedia?}
    PEN_CEK -- Tidak --> PEN_ERR
    PEN_CEK -- Ya --> PEN_SAVE[Simpan Penghuni]
    PEN_SAVE --> UPDATE_KAM1[Kamar → Terisi]
    UPDATE_KAM1 --> PEN_LIST

    PEN_HAPUS --> PEN_DEL[Hapus Penghuni]
    PEN_DEL --> CEK_AKT1{Masih ada\npenghuni aktif\ndi kamar?}
    CEK_AKT1 -- Tidak --> UPDATE_KAM2[Kamar → Kosong]
    CEK_AKT1 -- Ya --> PEN_LIST
    UPDATE_KAM2 --> PEN_LIST

    %% ── MODUL KONTRAK ──
    M_KONTRAK --> KON_LIST[Tampil Daftar Kontrak]
    KON_LIST --> KON_TAMBAH[Tambah Kontrak]
    KON_LIST --> KON_EDIT[Edit Kontrak]
    KON_LIST --> KON_SELESAI[Selesaikan Kontrak]
    KON_LIST --> KON_BATAL[Batalkan Kontrak]
    KON_LIST --> KON_HAPUS[Hapus Kontrak]

    KON_TAMBAH --> KON_VALID{Validasi}
    KON_VALID -- Gagal --> KON_ERR[/Tampil Error/]
    KON_VALID -- OK --> KON_CEK{Konflik\nkontrak\naktif?}
    KON_CEK -- Ada --> KON_ERR
    KON_CEK -- Tidak --> KON_SAVE[Simpan Kontrak]
    KON_SAVE --> KON_STATUS{Status\nKontrak?}
    KON_STATUS -- Aktif --> UPDATE_KAM3[Kamar → Terisi]
    KON_STATUS -- Dipesan --> UPDATE_KAM4[Kamar → Dipesan]
    UPDATE_KAM3 & UPDATE_KAM4 --> KON_LIST

    KON_SELESAI --> KON_SEL_SAVE[Status → Selesai]
    KON_BATAL --> KON_BAT_SAVE[Status → Dibatalkan]
    KON_SEL_SAVE & KON_BAT_SAVE --> CEK_AKT2{Masih ada\nkontrak aktif\ndi kamar?}
    CEK_AKT2 -- Tidak --> UPDATE_KAM5[Kamar → Kosong]
    CEK_AKT2 -- Ya --> KON_LIST
    UPDATE_KAM5 --> KON_LIST

    %% ── MODUL PEMBAYARAN ──
    M_BAYAR --> BAY_LIST[Tampil Daftar Tagihan per Kontrak]
    BAY_LIST --> BAY_TAMBAH[Tambah Tagihan]
    BAY_LIST --> BAY_BAYAR[Bayar Tagihan]
    BAY_LIST --> BAY_HAPUS[Hapus Tagihan]

    BAY_TAMBAH --> BAY_VALID{Validasi}
    BAY_VALID -- Gagal --> BAY_ERR[/Tampil Error/]
    BAY_VALID -- OK --> BAY_AUTO[Auto-hitung Status]
    BAY_AUTO --> BAY_LIST

    BAY_BAYAR --> INPUT_NOM[Input Nominal & Metode]
    INPUT_NOM --> BAYAR_HITUNG{JumlahDibayar\nvs Tagihan}
    BAYAR_HITUNG -- = 0 --> ST_BELUM[Status → BelumBayar]
    BAYAR_HITUNG -- Sebagian --> ST_CICIL[Status → Dicicil]
    BAYAR_HITUNG -- Lunas --> ST_LUNAS[Status → Lunas]
    ST_BELUM & ST_CICIL & ST_LUNAS --> BAY_LIST
```

---

## 3. State Diagram: Status Kamar

```mermaid
stateDiagram-v2
    [*] --> Kosong : Kamar dibuat

    Kosong --> Dipesan : Kontrak Dipesan dibuat
    Kosong --> Terisi : Penghuni/Kontrak Aktif ditambah

    Dipesan --> Terisi : Kontrak diaktifkan
    Dipesan --> Kosong : Kontrak dibatalkan

    Terisi --> Kosong : Semua penghuni/kontrak aktif dihapus

    Kosong --> Perbaikan : Diset manual
    Perbaikan --> Kosong : Diset manual
```

---

## 4. State Diagram: Status Pembayaran

```mermaid
stateDiagram-v2
    [*] --> BelumBayar : Tagihan dibuat\n(dibayar = 0)

    BelumBayar --> Dicicil : Bayar sebagian\n(0 < dibayar < tagihan)
    BelumBayar --> Lunas : Bayar penuh\n(dibayar >= tagihan)

    Dicicil --> Lunas : Bayar sisa\n(dibayar >= tagihan)
    Dicicil --> BelumBayar : Reset pembayaran

    Lunas --> [*]
```

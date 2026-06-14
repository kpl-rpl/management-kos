ALTER TABLE Kamar
    MODIFY COLUMN Status ENUM('Kosong','Terisi','Dipesan','Perbaikan') NOT NULL;

ALTER TABLE KontrakSewa
    MODIFY COLUMN Status ENUM('Dipesan','Aktif','Selesai','Dibatalkan') NOT NULL;

ALTER TABLE Pembayaran
    MODIFY COLUMN MetodePembayaran ENUM('Transfer','Tunai','QRIS') NOT NULL;

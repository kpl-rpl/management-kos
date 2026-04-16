CREATE TABLE IF NOT EXISTS Pembayaran (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    KontrakSewaId INT NOT NULL,
    Periode VARCHAR(20) NOT NULL,
    TanggalBayar DATE NULL,
    JumlahTagihan DECIMAL(18,2) NOT NULL,
    JumlahDibayar DECIMAL(18,2) NOT NULL,
    MetodePembayaran VARCHAR(30) NOT NULL,
    Status VARCHAR(20) NOT NULL,
    Catatan TEXT NULL,
    CONSTRAINT FK_Pembayaran_KontrakSewa
        FOREIGN KEY (KontrakSewaId) REFERENCES KontrakSewa(Id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE INDEX IF NOT EXISTS IX_Pembayaran_KontrakSewaId ON Pembayaran (KontrakSewaId);
CREATE INDEX IF NOT EXISTS IX_Pembayaran_Periode ON Pembayaran (Periode);

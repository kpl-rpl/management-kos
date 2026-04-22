CREATE TABLE IF NOT EXISTS KontrakSewa (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    PenghuniId INT NOT NULL,
    KamarId INT NOT NULL,
    TanggalMulai DATE NOT NULL,
    TanggalSelesai DATE NOT NULL,
    HargaSewaBulanan DECIMAL(18,2) NOT NULL,
    Deposit DECIMAL(18,2) NULL,
    Status VARCHAR(30) NOT NULL,
    Catatan TEXT NULL,
    CONSTRAINT FK_KontrakSewa_Penghuni
        FOREIGN KEY (PenghuniId) REFERENCES Penghuni(Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_KontrakSewa_Kamar
        FOREIGN KEY (KamarId) REFERENCES Kamar(Id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE INDEX IF NOT EXISTS IX_KontrakSewa_PenghuniId ON KontrakSewa (PenghuniId);
CREATE INDEX IF NOT EXISTS IX_KontrakSewa_KamarId ON KontrakSewa (KamarId);

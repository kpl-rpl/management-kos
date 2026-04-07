CREATE TABLE IF NOT EXISTS Penghuni (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    KamarId INT NOT NULL,
    Nama VARCHAR(200) NOT NULL,
    NomorTelepon VARCHAR(30) NOT NULL,
    Email VARCHAR(200) NULL,
    TanggalMasuk DATE NOT NULL,
    TanggalKeluar DATE NULL,
    Catatan TEXT NULL,
    CONSTRAINT FK_Penghuni_Kamar
        FOREIGN KEY (KamarId) REFERENCES Kamar(Id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE INDEX IF NOT EXISTS IX_Penghuni_KamarId ON Penghuni (KamarId);
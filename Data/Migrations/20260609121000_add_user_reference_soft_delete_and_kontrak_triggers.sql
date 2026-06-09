CREATE TABLE IF NOT EXISTS Role (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NamaRole VARCHAR(50) NOT NULL,
    Deskripsi VARCHAR(200) NULL,
    IsActive TINYINT(1) NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT UQ_Role_NamaRole UNIQUE (NamaRole)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS AppUser (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    RoleId INT NOT NULL,
    Username VARCHAR(50) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    NamaLengkap VARCHAR(200) NOT NULL,
    IsActive TINYINT(1) NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NULL,
    CONSTRAINT UQ_AppUser_Username UNIQUE (Username),
    CONSTRAINT FK_AppUser_Role
        FOREIGN KEY (RoleId) REFERENCES Role(Id)
        ON DELETE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS MetodePembayaranRef (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NamaMetode VARCHAR(50) NOT NULL,
    IsActive TINYINT(1) NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT UQ_MetodePembayaranRef_NamaMetode UNIQUE (NamaMetode)
) ENGINE=InnoDB;

INSERT IGNORE INTO Role (NamaRole, Deskripsi) VALUES
    ('Admin', 'Pengelola utama aplikasi'),
    ('Operator', 'Pengguna operasional aplikasi');

INSERT IGNORE INTO MetodePembayaranRef (NamaMetode) VALUES
    ('Transfer'),
    ('Tunai'),
    ('QRIS');

INSERT IGNORE INTO AppUser (RoleId, Username, PasswordHash, NamaLengkap)
SELECT Id, 'admin', '240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9', 'Administrator'
FROM Role
WHERE NamaRole = 'Admin';

ALTER TABLE Kos
    ADD COLUMN IF NOT EXISTS IsActive TINYINT(1) NOT NULL DEFAULT 1;

ALTER TABLE Kamar
    ADD COLUMN IF NOT EXISTS IsActive TINYINT(1) NOT NULL DEFAULT 1;

ALTER TABLE Penghuni
    ADD COLUMN IF NOT EXISTS IsActive TINYINT(1) NOT NULL DEFAULT 1;

ALTER TABLE Pembayaran
    MODIFY COLUMN MetodePembayaran VARCHAR(30) NULL;

DROP TRIGGER IF EXISTS trg_kontrak_sewa_after_insert;
DROP TRIGGER IF EXISTS trg_kontrak_sewa_after_update;
DROP TRIGGER IF EXISTS trg_kontrak_sewa_after_delete;

CREATE TRIGGER trg_kontrak_sewa_after_insert
AFTER INSERT ON KontrakSewa
FOR EACH ROW
UPDATE Kamar
SET Status = CASE
    WHEN NEW.Status = 'Aktif' THEN 'Terisi'
    WHEN NEW.Status = 'Dipesan' THEN 'Dipesan'
    ELSE Status
END
WHERE Id = NEW.KamarId
  AND NEW.Status IN ('Aktif', 'Dipesan');

CREATE TRIGGER trg_kontrak_sewa_after_update
AFTER UPDATE ON KontrakSewa
FOR EACH ROW
UPDATE Kamar
SET Status = CASE
    WHEN NEW.Status = 'Aktif' THEN 'Terisi'
    WHEN NEW.Status = 'Dipesan' THEN 'Dipesan'
    ELSE 'Kosong'
END
WHERE Id = NEW.KamarId;

CREATE TRIGGER trg_kontrak_sewa_after_delete
AFTER DELETE ON KontrakSewa
FOR EACH ROW
UPDATE Kamar
SET Status = 'Kosong'
WHERE Id = OLD.KamarId;

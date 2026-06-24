ALTER TABLE KontrakSewa
    ADD COLUMN IF NOT EXISTS DurasiBulanInput DECIMAL(5,2) NOT NULL DEFAULT 1 AFTER TanggalSelesai,
    ADD COLUMN IF NOT EXISTS JumlahBulanTagihan INT NOT NULL DEFAULT 1 AFTER DurasiBulanInput,
    ADD COLUMN IF NOT EXISTS TotalTagihan DECIMAL(18,2) NOT NULL DEFAULT 0 AFTER HargaSewaBulanan;

UPDATE KontrakSewa
SET JumlahBulanTagihan = GREATEST(1, TIMESTAMPDIFF(MONTH, TanggalMulai, TanggalSelesai)),
    DurasiBulanInput = GREATEST(1, TIMESTAMPDIFF(MONTH, TanggalMulai, TanggalSelesai)),
    TotalTagihan = HargaSewaBulanan * GREATEST(1, TIMESTAMPDIFF(MONTH, TanggalMulai, TanggalSelesai))
WHERE TotalTagihan = 0;

DROP TRIGGER IF EXISTS trg_kontrak_sewa_after_insert;
DROP TRIGGER IF EXISTS trg_kontrak_sewa_after_update;
DROP TRIGGER IF EXISTS trg_kontrak_sewa_after_delete;

SET @dropPenghuniKamarFk = (
    SELECT IF(
        COUNT(*) > 0,
        'ALTER TABLE Penghuni DROP FOREIGN KEY FK_Penghuni_Kamar',
        'SELECT 1'
    )
    FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS
    WHERE CONSTRAINT_SCHEMA = DATABASE()
      AND TABLE_NAME = 'Penghuni'
      AND CONSTRAINT_NAME = 'FK_Penghuni_Kamar'
);
PREPARE stmt FROM @dropPenghuniKamarFk;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SET @dropPenghuniKamarIndex = (
    SELECT IF(
        COUNT(*) > 0,
        'ALTER TABLE Penghuni DROP INDEX IX_Penghuni_KamarId',
        'SELECT 1'
    )
    FROM INFORMATION_SCHEMA.STATISTICS
    WHERE TABLE_SCHEMA = DATABASE()
      AND TABLE_NAME = 'Penghuni'
      AND INDEX_NAME = 'IX_Penghuni_KamarId'
);
PREPARE stmt FROM @dropPenghuniKamarIndex;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SET @dropPenghuniKamarColumn = (
    SELECT IF(
        COUNT(*) > 0,
        'ALTER TABLE Penghuni DROP COLUMN KamarId',
        'SELECT 1'
    )
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA = DATABASE()
      AND TABLE_NAME = 'Penghuni'
      AND COLUMN_NAME = 'KamarId'
);
PREPARE stmt FROM @dropPenghuniKamarColumn;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

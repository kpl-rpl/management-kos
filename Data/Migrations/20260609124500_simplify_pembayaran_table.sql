SET @dropPeriodeIndex = (
    SELECT IF(
        COUNT(*) > 0,
        'ALTER TABLE Pembayaran DROP INDEX IX_Pembayaran_Periode',
        'SELECT 1'
    )
    FROM INFORMATION_SCHEMA.STATISTICS
    WHERE TABLE_SCHEMA = DATABASE()
      AND TABLE_NAME = 'Pembayaran'
      AND INDEX_NAME = 'IX_Pembayaran_Periode'
);
PREPARE stmt FROM @dropPeriodeIndex;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SET @dropPeriodeColumn = (
    SELECT IF(
        COUNT(*) > 0,
        'ALTER TABLE Pembayaran DROP COLUMN Periode',
        'SELECT 1'
    )
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA = DATABASE()
      AND TABLE_NAME = 'Pembayaran'
      AND COLUMN_NAME = 'Periode'
);
PREPARE stmt FROM @dropPeriodeColumn;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SET @dropJumlahTagihanColumn = (
    SELECT IF(
        COUNT(*) > 0,
        'ALTER TABLE Pembayaran DROP COLUMN JumlahTagihan',
        'SELECT 1'
    )
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA = DATABASE()
      AND TABLE_NAME = 'Pembayaran'
      AND COLUMN_NAME = 'JumlahTagihan'
);
PREPARE stmt FROM @dropJumlahTagihanColumn;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SET @dropStatusColumn = (
    SELECT IF(
        COUNT(*) > 0,
        'ALTER TABLE Pembayaran DROP COLUMN Status',
        'SELECT 1'
    )
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA = DATABASE()
      AND TABLE_NAME = 'Pembayaran'
      AND COLUMN_NAME = 'Status'
);
PREPARE stmt FROM @dropStatusColumn;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

ALTER TABLE Pembayaran
    MODIFY COLUMN MetodePembayaran VARCHAR(30) NOT NULL;

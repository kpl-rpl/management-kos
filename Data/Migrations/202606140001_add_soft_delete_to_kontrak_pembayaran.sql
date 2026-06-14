ALTER TABLE KontrakSewa
    ADD COLUMN IF NOT EXISTS IsActive TINYINT(1) NOT NULL DEFAULT 1;

ALTER TABLE Pembayaran
    ADD COLUMN IF NOT EXISTS IsActive TINYINT(1) NOT NULL DEFAULT 1;

DROP TRIGGER IF EXISTS trg_kontrak_sewa_after_update;

CREATE TRIGGER trg_kontrak_sewa_after_update
AFTER UPDATE ON KontrakSewa
FOR EACH ROW
UPDATE Kamar
SET Status = CASE
    WHEN NEW.IsActive = 0 THEN 'Kosong'
    WHEN NEW.Status = 'Aktif' THEN 'Terisi'
    WHEN NEW.Status = 'Dipesan' THEN 'Dipesan'
    ELSE 'Kosong'
END
WHERE Id = NEW.KamarId;

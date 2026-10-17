-- Tạo bảng lớp học. Chạy file này trước sinhvien.sql.
USE quanlysinhvien;

CREATE TABLE IF NOT EXISTS lophoc (
    MaLop VARCHAR(20) NOT NULL,
    TenLop VARCHAR(100) NOT NULL,
    PRIMARY KEY (MaLop),
    CONSTRAINT chk_lophoc_malop CHECK (CHAR_LENGTH(TRIM(MaLop)) > 0),
    CONSTRAINT chk_lophoc_tenlop CHECK (CHAR_LENGTH(TRIM(TenLop)) > 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

SHOW CREATE TABLE lophoc;


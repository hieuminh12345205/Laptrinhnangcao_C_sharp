-- Chạy trong MySQL Workbench, database quanlysinhvien.
USE quanlysinhvien;

-- Một lớp học có nhiều sinh viên.
CREATE TABLE IF NOT EXISTS lophoc (
    MaLop VARCHAR(20) NOT NULL,
    TenLop VARCHAR(100) NOT NULL,
    PRIMARY KEY (MaLop),
    CONSTRAINT chk_lophoc_malop CHECK (CHAR_LENGTH(TRIM(MaLop)) > 0),
    CONSTRAINT chk_lophoc_tenlop CHECK (CHAR_LENGTH(TRIM(TenLop)) > 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS sinhvien (
    MaSV VARCHAR(20) NOT NULL,
    HoTen VARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh VARCHAR(3) NOT NULL,
    Email VARCHAR(254) NOT NULL,
    SDT VARCHAR(10) NOT NULL,
    MaLop VARCHAR(20) NOT NULL,
    Diem DECIMAL(3,1) NOT NULL DEFAULT 0.0,
    TrangThai VARCHAR(30) NOT NULL DEFAULT 'Đang học',
    PRIMARY KEY (MaSV),
    CONSTRAINT fk_sinhvien_lophoc FOREIGN KEY (MaLop)
        REFERENCES lophoc(MaLop) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT chk_sinhvien_masv CHECK (CHAR_LENGTH(TRIM(MaSV)) > 0),
    CONSTRAINT chk_sinhvien_hoten CHECK (CHAR_LENGTH(TRIM(HoTen)) > 0),
    CONSTRAINT chk_sinhvien_email CHECK (CHAR_LENGTH(TRIM(Email)) > 0),
    CONSTRAINT chk_sinhvien_sdt CHECK (SDT REGEXP '^0[0-9]{9}$'),
    CONSTRAINT chk_sinhvien_gioitinh CHECK (GioiTinh IN ('Nam', 'Nữ')),
    CONSTRAINT chk_sinhvien_diem CHECK (Diem BETWEEN 0 AND 10),
    CONSTRAINT chk_sinhvien_trangthai CHECK (TrangThai IN ('Đang học', 'Bảo lưu', 'Đã tốt nghiệp'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Xem hai bảng vừa tạo và cấu trúc thực tế của chúng.
SHOW TABLES;
SHOW CREATE TABLE lophoc;
SHOW CREATE TABLE sinhvien;

-- Email hợp lệ và ngày sinh không ở tương lai tiếp tục được kiểm tra trong BLL.
-- Script chỉ tạo bảng, chưa thêm dữ liệu mẫu hoặc chuyển DAO sang SQL.

-- Chạy sau lophoc.sql và sinhvien.sql để ComboBox có lớp học lựa chọn.
-- Chỉ thêm mã chưa có; giữ nguyên tên lớp nếu đã được người dùng chỉnh sửa.
USE quanlysinhvien;

INSERT INTO lophoc (MaLop, TenLop)
SELECT 'CSE0001', 'Khoa học máy tính 01'
WHERE NOT EXISTS (SELECT 1 FROM lophoc WHERE MaLop = 'CSE0001');

INSERT INTO lophoc (MaLop, TenLop)
SELECT 'CSE0002', 'Khoa học máy tính 02'
WHERE NOT EXISTS (SELECT 1 FROM lophoc WHERE MaLop = 'CSE0002');

INSERT INTO lophoc (MaLop, TenLop)
SELECT 'CSE0003', 'Khoa học máy tính 03'
WHERE NOT EXISTS (SELECT 1 FROM lophoc WHERE MaLop = 'CSE0003');

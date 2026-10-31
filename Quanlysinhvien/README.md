# Quản lý sinh viên WinForms

Ngày làm: **03.10.2026**.

Thiết kế form quản lý sinh viên, quan hệ lớp học - sinh viên, validation bằng Data Annotations,
thêm/sửa/xóa, làm mới và tìm kiếm. Dữ liệu lưu trong MySQL, còn nguyên sau khi đóng ứng dụng.

Mở `Quanlysinhvien.slnx` trong Visual Studio và nhấn F5 để chạy.
Xem `HUONG_DAN.md` để biết cách sử dụng.

## Cấu trúc dự án

```text
Quanlysinhvien/
├── BLL/
│   ├── LopHocBLL.cs
│   └── SinhVienBLL.cs
├── Data/
│   ├── DAL/
│   │   ├── LopHocDAL.cs
│   │   └── SinhVienDAL.cs
│   ├── DAO/
│   │   ├── LopHocDAO.cs
│   │   └── SinhVienDAO.cs
│   └── Entity/
│       ├── LopHoc.cs
│       └── SinhVien.cs
├── Views/
│   ├── frmQuanLySV.cs
│   ├── frmQuanLySV.Designer.cs
│   └── frmQuanLySV.resx
├── Program.cs
└── Quanlysinhvien.csproj
```

- Views: giao diện, đọc dữ liệu nhập và hiển thị thông báo.
- BLL: xử lý nghiệp vụ, kiểm tra dữ liệu, làm tròn điểm và tìm kiếm.
- DAL: cung cấp các hàm truy cập dữ liệu cho BLL, gọi DAO để thực hiện.
- DAO: dùng DataHelper chạy SQL có tham số, đọc/ghi MySQL.
- Entity: các lớp mô tả dữ liệu, quan hệ lớp học - sinh viên và Data Annotations.

Luồng xử lý của bài: **Views → BLL → DAL → DAO**.

Phần lớp học có đầy đủ lấy danh sách, tìm theo mã, thêm, sửa tên và xóa.
LopHocBLL kiểm tra Data Annotations, từ chối mã lớp trùng và không cho xóa lớp đang có sinh viên.
ComboBox lấy danh sách từ LopHocBLL. Khóa ngoại MaLop trong MySQL giữ quan hệ 1-n.
Các hàm thêm/sửa/xóa lớp hiện dùng từ code; giao diện hiện tại phục vụ quản lý sinh viên.

## MySQL và chống SQL injection — 17.10.2026

1. Điền mysql.local.json theo mysql.example.json; file mật khẩu được Git bỏ qua.
2. Khi cài trên máy khác, chạy Data/Sql/lophoc.sql, sinhvien.sql, rồi dulieumau.sql.
3. Chạy form: nút Thêm/Sửa/Xóa gọi AddSinhVien/UpdateSinhVien/DeleteSinhVien qua BLL → DAL → DAO.

Mọi dữ liệu nhập được truyền bằng MySqlParameter có kiểu dữ liệu. Không ghép mã sinh viên,
họ tên, mã lớp hoặc từ khóa vào chuỗi SQL. Các câu UPDATE/DELETE có WHERE theo khóa chính.
Kiểm tra Data Annotations ở BLL và ràng buộc khóa chính/khóa ngoại ở database cùng bảo vệ dữ liệu.
Đã chạy 34 kiểm tra tích hợp MySQL, gồm dữ liệu Unicode, dấu nháy, payload SQLi, CRUD và các luồng form.

Yêu cầu cuối về ngoại lệ: các hàm ghi BLL trả void khi thành công, ném ValidationException khi
vi phạm nghiệp vụ; lỗi kỹ thuật từ DAO/DAL tự truyền qua BLL. View bắt ValidationException
và Exception để hiển thị thông báo; không đưa mật khẩu/câu SQL vào thông báo lỗi kỹ thuật.
Đã cập nhật cách gọi trên form, giữ xác nhận Sửa/Xóa và kiểm tra chưa thay đổi thông tin.
Bộ kiểm tra mới nhất: 59 kiểm tra MySQL/SQLi đã qua; build 0 lỗi, 0 cảnh báo.

## EF Core — 24.10.2026

Đã bổ sung `QuanLySinhVienDbContext` để ánh xạ `LopHoc` và `SinhVien` sang hai bảng MySQL hiện có,
cùng factory cho công cụ Migration. Kết nối dùng chung `mysql.local.json`; mật khẩu không đưa vào mã nguồn.
EF Core và MySQL provider dùng bản 10.0.9 phù hợp với .NET 10 và MySql.Data 26.7.0.
Đã qua 32 kiểm tra mapping/kết nối chỉ đọc; CRUD trên form tiếp tục dùng ADO.NET.
Xem [hướng dẫn EF Core](HUONG_DAN_EF_CORE.md) để kiểm tra bằng Get-DbContext và chuẩn bị bước Migration.

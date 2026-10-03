# Quản lý sinh viên WinForms

Ngày làm: **03.10.2026**.

Thiết kế form quản lý sinh viên, quan hệ lớp học - sinh viên, validation bằng Data Annotations,
thêm/sửa/xóa, làm mới và tìm kiếm. Dữ liệu lưu trong bộ nhớ.

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
- DAO: trực tiếp giữ danh sách và thêm/sửa/xóa dữ liệu trong bộ nhớ.
- Entity: các lớp mô tả dữ liệu, quan hệ lớp học - sinh viên và Data Annotations.

Luồng xử lý của bài: **Views → BLL → DAL → DAO**.

Phần lớp học có đầy đủ lấy danh sách, tìm theo mã, thêm, sửa tên và xóa.
LopHocBLL kiểm tra Data Annotations, từ chối mã lớp trùng và không cho xóa lớp đang có sinh viên.
ComboBox lấy danh sách từ LopHocBLL. Phần lớp và sinh viên dùng chung đối tượng lớp để giữ quan hệ 1-n.
Các hàm thêm/sửa/xóa lớp hiện dùng từ code; giao diện hiện tại phục vụ quản lý sinh viên.

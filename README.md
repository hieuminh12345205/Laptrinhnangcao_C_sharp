# Lập trình nâng cao C# — Quản lý sinh viên

Repository lưu các bài thực hành môn Lập trình nâng cao C#. Dự án chính là **Quản lý sinh viên**,
ứng dụng Windows Forms sử dụng C#/.NET 10, MySQL và ADO.NET, tổ chức theo các tầng View, Business và Data Access.

## Dự án chính: Quản lý sinh viên

Thư mục: [Quanlysinhvien](Quanlysinhvien/) · Cập nhật: **17.10.2026**.

### Chức năng

- Xem danh sách sinh viên, chọn một dòng hoặc nhập mã để xem thông tin.
- Thêm, sửa, xóa sinh viên và lưu trực tiếp vào MySQL.
- Tìm kiếm theo mã, họ tên, email hoặc số điện thoại; lọc theo lớp và điểm tối thiểu.
- Hiển thị danh sách lớp trong ComboBox; mỗi lớp có nhiều sinh viên.
- Kiểm tra dữ liệu bằng Data Annotations, hiển thị lỗi tại ô nhập bằng ErrorProvider.
- Tự bật/tắt nút Thêm/Sửa/Xóa theo mã sinh viên; làm mới form và đặt lại tiêu điểm.
- Hỏi xác nhận trước khi sửa, xóa hoặc đóng; thông báo khi bấm Sửa mà chưa thay đổi thông tin.
- Tầng Business có các hàm quản lý lớp học, kiểm tra mã trùng và chặn xóa lớp đang có sinh viên.
  Giao diện hiện tại phục vụ quản lý sinh viên.

### Phân tầng và truy cập dữ liệu

```text
View → BLL → DAL → DAO → DataHelper → MySQL
```

| Thành phần | Vai trò |
| --- | --- |
| `Views` | Giao diện WinForms, nhận dữ liệu nhập, bắt ngoại lệ và hiển thị thông báo. |
| `BLL` | Kiểm tra nghiệp vụ, chuẩn hóa dữ liệu, ném ngoại lệ khi vi phạm quy tắc. |
| `Data/DAL` | Chuyển yêu cầu từ Business đến DAO; truyền ngoại lệ lên tầng gọi. |
| `Data/DAO` | `LopHocDAO` và `SinhVienDAO` thực hiện truy vấn ADO.NET có tham số. |
| `Data/Entity` | `LopHoc`, `SinhVien`, quan hệ 1-n và Data Annotations. |
| `Data/DataHelper.cs` | Đọc cấu hình, mở kết nối, thực thi truy vấn và giải phóng tài nguyên. |

Dự án có hai cách đọc để đối chiếu khi học:

- **Hướng kết nối:** `ExecuteReader()` trả `MySqlDataReader`, DAO đọc từng dòng bằng `Read()`.
- **Phi kết nối:** `ExecuteQuery()` tải kết quả vào `DataTable`, sau đó duyệt các dòng trong bộ nhớ.

Các câu lệnh nhận dữ liệu người dùng qua `MySqlParameter` để chống SQL injection.
Ngoại lệ từ DAO/DAL được truyền qua BLL đến View; View xử lý và hiển thị thông báo an toàn.

### Cài đặt và chạy

Yêu cầu: Windows, .NET 10 SDK, Visual Studio có workload **.NET desktop development** và MySQL Server 8.4.
Có thể dùng MySQL Workbench để chạy các script SQL.

1. Trong MySQL, tạo database:

   ```sql
   CREATE DATABASE IF NOT EXISTS quanlysinhvien
       CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
   ```

2. Chạy các script theo thứ tự:
   - [lophoc.sql](Quanlysinhvien/Data/Sql/lophoc.sql): tạo bảng lớp học.
   - [sinhvien.sql](Quanlysinhvien/Data/Sql/sinhvien.sql): tạo bảng sinh viên và khóa ngoại.
   - [dulieumau.sql](Quanlysinhvien/Data/Sql/dulieumau.sql): thêm các lớp học mẫu còn thiếu.

3. Sao chép `Quanlysinhvien/mysql.example.json` thành `Quanlysinhvien/mysql.local.json`.
   Điền địa chỉ máy chủ, cổng, tên database, tài khoản và mật khẩu MySQL của máy bạn.
   File `mysql.local.json` được Git bỏ qua để tránh đưa mật khẩu lên repository.

4. Mở [Quanlysinhvien.slnx](Quanlysinhvien/Quanlysinhvien.slnx) bằng Visual Studio,
   build và nhấn **F5**. Hoặc chạy từ thư mục gốc repository:

   ```powershell
   dotnet run --project .\Quanlysinhvien\Quanlysinhvien.csproj
   ```

Xem [hướng dẫn chi tiết](Quanlysinhvien/HUONG_DAN.md) để tìm hiểu cách sử dụng và đối chiếu code với bài giảng.

## Các bài thực hành trước

| Bài | Ngày làm | Nội dung |
| --- | --- | --- |
| [Bài 1](Bai_1/) | 12.09.2026 | Nhập ba số nguyên, kiểm tra dữ liệu nhập và tính tổng. |
| [Bài 2](Bai_2/) | 12.09.2026 | Quản lý hình tròn, hình chữ nhật, tam giác; áp dụng interface và đa hình để tính diện tích, chu vi. |

Hai bài Console có thể mở bằng `ConsoleApp1.slnx` hoặc chạy riêng:

```powershell
dotnet run --project .\Bai_1\Bai_1.csproj
dotnet run --project .\Bai_2\Bai_2.csproj
```

## Cấu trúc repository

```text
Laptrinhnangcao_C_sharp/
├── Quanlysinhvien/           # Dự án chính: WinForms + MySQL
│   ├── BLL/
│   ├── Data/
│   │   ├── DAL/
│   │   ├── DAO/
│   │   ├── Entity/
│   │   ├── Sql/
│   │   └── DataHelper.cs
│   ├── Views/
│   ├── mysql.example.json
│   ├── Quanlysinhvien.csproj
│   └── Quanlysinhvien.slnx
├── KiemTraMySql/             # Công cụ Console kiểm tra kết nối MySQL
├── Bai_1/                   # Bài tập nhập số và tính tổng
├── Bai_2/                   # Bài tập interface và đa hình
├── ConsoleApp1.slnx          # Solution cho Bài 1 và Bài 2
└── README.md
```

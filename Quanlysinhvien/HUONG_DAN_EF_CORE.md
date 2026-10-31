# EF Core — Kết nối và ánh xạ MySQL

Ngày thực hiện: **24.10.2026**.

Đây là bước chuẩn bị cho Database Migration: khai báo DbContext và ánh xạ hai entity hiện có.
Form tiếp tục dùng luồng BLL → DAL → DAO bằng ADO.NET. DbContext có thể đọc cùng database
và được công cụ EF Core sử dụng khi làm Migration ở bước tiếp theo.

## Package đã cài

| Package | Phiên bản | Mục đích |
| --- | --- | --- |
| MySql.EntityFrameworkCore | 10.0.9 | Provider EF Core cho MySQL, tương thích MySql.Data 26.7.0 đang dùng. |
| Microsoft.EntityFrameworkCore | 10.0.9 | DbContext, DbSet và truy vấn LINQ. |
| Microsoft.EntityFrameworkCore.Design | 10.0.9 | Hỗ trợ công cụ ở design time. |
| Microsoft.EntityFrameworkCore.Tools | 10.0.9 | Các lệnh trong Package Manager Console của Visual Studio. |

Dự án dùng .NET 10 nên không chép phiên bản EF Core 7.0.11 của ví dụ .NET 7.
Tham khảo dependency chính thức: https://www.nuget.org/packages/MySql.EntityFrameworkCore/10.0.9

## Các file chính

- `Data/QuanLySinhVienDbContext.cs`: kết nối MySQL, khai báo DbSet và cấu hình mapping.
- `Data/QuanLySinhVienDbContextFactory.cs`: tạo context cho công cụ EF mà không mở WinForms.
- `Data/DataHelper.cs`: cung cấp chung `LayChuoiKetNoi()` từ cấu hình riêng của máy.
- `mysql.local.json`: giữ địa chỉ, database, tài khoản và mật khẩu; đã được Git bỏ qua.

DbContext mặc định đọc file cấu hình trong thư mục chạy ứng dụng.
Factory đọc file cạnh DLL đã build để chạy được cả khi thư mục hiện tại của terminal khác thư mục dự án.
Không ghi mật khẩu vào DbContext, factory hoặc các file migration; không bật sensitive data logging.

## Ánh xạ

| C# | MySQL |
| --- | --- |
| DbSet<LopHoc> LopHocs | Bảng `lophoc`, khóa chính `MaLop`. |
| DbSet<SinhVien> SinhViens | Bảng `sinhvien`, khóa chính `MaSV`. |
| LopHoc.SinhViens ↔ SinhVien.LopHoc | Quan hệ 1-n qua khóa ngoại `sinhvien.MaLop`. |
| SinhVien.NgaySinh | `date`. |
| SinhVien.Diem | `decimal(3,1)`. |
| SinhVien.TenLop | Thuộc tính hiển thị, không tạo cột trong `sinhvien`. |

Các cột chuỗi được chỉ rõ độ dài; cấu hình có giá trị mặc định, các CHECK hiện có,
charset utf8mb4 và collation utf8mb4_unicode_ci. Xóa lớp có sinh viên dùng Restrict.

`MaLop` là **shadow property**: cột khóa ngoại được EF quản lý dù entity SinhVien hiện chỉ có
navigation `LopHoc`. Khi gán `sv.LopHoc`, EF xác định khóa ngoại từ lớp đó.
Nhờ vậy không cần thêm thuộc tính mới hoặc thay đổi các ô nhập của bài ADO.NET.
Khi cần truy vấn trực tiếp khóa ngoại trong LINQ, dùng `EF.Property<string>(sv, "MaLop")`.

## Xem trong Visual Studio

1. Mở `Quanlysinhvien.slnx`, trong Solution Explorer mở `Data` → `QuanLySinhVienDbContext.cs`.
2. Build project để khôi phục package và sao chép cấu hình vào thư mục chạy.
3. Chọn **Tools → NuGet Package Manager → Package Manager Console**.
4. Chọn **Default project: Quanlysinhvien**, chạy:

   ```powershell
   Get-DbContext -Context QuanLySinhVienDbContext
   ```

Lệnh này kiểm tra công cụ nhận diện DbContext; chưa tạo hoặc cập nhật bảng.

Ví dụ đọc dữ liệu ở code C#:

```csharp
using Microsoft.EntityFrameworkCore;
using Quanlysinhvien.Data;

using var db = new QuanLySinhVienDbContext();
bool ketNoiDuoc = db.Database.CanConnect();
var danhSachLop = db.LopHocs.AsNoTracking().ToList();
var danhSachSV = db.SinhViens.AsNoTracking()
    .Include(sv => sv.LopHoc)
    .OrderBy(sv => sv.MaSV)
    .ToList();
```

Khi gọi từ View, đặt thao tác trong try/catch như luồng xử lý ngoại lệ hiện tại.
Data Annotations vẫn do BLL kiểm tra; EF Core không tự thay thế phần kiểm tra nghiệp vụ của bài.

## Phạm vi bước này và bước Migration tiếp theo

Đã kiểm tra thực tế: 32 kiểm tra kết nối, tên bảng/cột, khóa chính, quan hệ 1-n, kiểu dữ liệu,
đọc dữ liệu EF khớp ADO.NET, tham số LINQ, shadow FK và factory. Công cụ EF nhận diện đúng
context, provider MySQL và database `quanlysinhvien`. Build không lỗi/cảnh báo.

Chưa tạo/applied migration, không chạy EnsureCreated, EnsureDeleted, Migrate hoặc SaveChanges
trong quá trình kiểm tra. Database hiện có được giữ nguyên.

Vì hai bảng đã tồn tại do bài ADO.NET tạo trước, bước Migration tiếp theo cần chuẩn bị baseline
cho schema hiện tại trước khi Update-Database, tránh chạy lại CREATE TABLE trên bảng đã có.
Khóa ngoại hiện có còn có ON UPDATE CASCADE; EF cấu hình OnDelete, còn quy tắc ON UPDATE này
cần được giữ rõ trong SQL migration khi thiết lập baseline. Không chạy Update-Database ở bước này.

Tài liệu: https://learn.microsoft.com/en-us/ef/core/cli/dbcontext-creation
và https://learn.microsoft.com/en-us/ef/core/managing-schemas/ensure-created

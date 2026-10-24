# QLTV — Quản lý thư viện WinForms và Class Library

Ngày làm: **24.10.2026**.

Solution dùng C#/.NET 10, MySQL và Entity Framework Core, gồm hai project:
- **QLTVApp:** Windows Forms App, có giao diện và chạy bằng F5.
- **QLTVDAO:** Class Library chứa dữ liệu/nghiệp vụ, tạo DLL được QLTVApp tham chiếu.

Giao diện giữ control và màu WinForms mặc định, gồm ba tab Đầu sách, Danh mục, Bản sách.
Mỗi tab có thêm/sửa/xóa/làm mới; chọn dòng để sửa, hỏi xác nhận trước sửa/xóa/đóng,
báo chưa thay đổi khi bấm Sửa mà dữ liệu vẫn như cũ. Tab Đầu sách tìm tên/tác giả và lọc danh mục;
tab Bản sách lọc theo đầu sách. Lỗi nhập hiển thị bằng ErrorProvider, lỗi BLL/DAL được View bắt và thông báo.

Tham khảo bài [24_10_2026_EFW của MKII3184/LTNC](https://github.com/MKII3184/LTNC/tree/main/24_10_2026_EFW),
gồm Book, Category, BookCopy, BookDbContext và migration InitialBookDB.
Code trong dự án này được viết lại, bổ sung validation, phân tầng và cấu hình riêng không lộ mật khẩu.

## Mô hình dữ liệu

```text
Category (1) ─── (n) Book (1) ─── (n) BookCopy
```

- **Category:** CategoryId tự tăng, Description.
- **Book:** BookId tự tăng, Title, Publisher, Price, TotalCopies, Author, PublicationYear, CategoryId.
- **BookCopy:** BookCopyId tự tăng, Status, Condition, BookId.

Book là đầu sách; BookCopy là một bản sách vật lý.
TotalCopies là tổng số bản khai báo của đầu sách như code tham khảo, không phải trường tự tính.
Ví dụ có thể khai báo 10 bản và mới đăng ký chi tiết một BookCopy. Các thao tác BookCopy
hiện không tự tăng/giảm TotalCopies. Chưa có chức năng quản lý bạn đọc và phiếu mượn/trả.

## Cấu trúc

```text
QLTV/
├── QLTV.slnx
├── mysql.example.json
├── .gitignore
├── README.md
├── QLTVApp/
│   ├── QLTVApp.csproj              # WinForms, project chạy bằng F5
│   ├── Program.cs
│   ├── Form1.cs                    # Sự kiện và xử lý tại View
│   └── Form1.Designer.cs           # Giao diện chỉnh được trong Designer
└── QLTVDAO/
    ├── QLTVDAO.csproj              # Class Library, xuất DLL
    ├── Entities/
    │   ├── Category.cs
    │   ├── Book.cs
    │   └── BookCopy.cs
    ├── DAO/                       # Truy vấn LINQ và SaveChanges qua EF Core
    ├── DAL/                       # Chuyển yêu cầu truy cập dữ liệu đến DAO
    ├── BLL/                       # Validation và quy tắc nghiệp vụ
    ├── Migrations/                # InitialBookDB và model snapshot
    ├── BookDbContext.cs
    ├── BookDbContextFactory.cs
    ├── DataHelper.cs
    └── GlobalUsings.cs
```

Luồng gọi: **QLTVApp (View) → BLL → DAL → DAO → DbContext → MySQL**.
Class Library này không phụ thuộc dự án Quản lý sinh viên.

## Mở trong Visual Studio

1. Chọn **File → Open → Project/Solution**, mở `QLTV.slnx` trong thư mục này.
2. Solution Explorer hiển thị `QLTVApp` và `QLTVDAO`. Nếu vừa thêm project khi solution đang mở,
   chấp nhận Reload hoặc đóng/mở lại solution để thấy đủ cả hai.
3. Chọn **Build → Build Solution** hoặc nhấn **Ctrl + Shift + B**.
4. Nhấp phải **QLTVApp → Set as Startup Project**, rồi nhấn **F5** hoặc **Ctrl + F5**.
   Không chọn QLTVDAO làm startup vì đó là thư viện DLL.
5. Khi database mới trống, form mở tab Danh mục: thêm danh mục trước, sau đó thêm Đầu sách,
   rồi thêm Bản sách. Mã tự tăng do MySQL cấp, người dùng không nhập mã mới.

Muốn chỉnh giao diện: trong QLTVApp, nhấp phải **Form1.cs → View Designer**.
Chạy từ PowerShell ở thư mục QLTV cũng được:

```powershell
dotnet run --project .\QLTVApp\QLTVApp.csproj
```

Nếu tự tạo project tương tự trong Visual Studio: **Create a new project → Class Library → C# → .NET 10**.
“Library” ở đây nghĩa là thư viện mã DLL; Visual Studio không tự tạo sẵn chức năng quản lý sách.

## Kết nối và Migration

Bộ package 10.0.9 khớp MySql.Data 26.7.0 đang dùng: EF Core, MySQL provider, Design và Tools.
File cấu hình QLTV riêng nằm cạnh solution. Sao chép `mysql.example.json` thành `mysql.local.json`,
điền thông tin MySQL của máy và giữ Database là `quanlythuvien` để tách khỏi `quanlysinhvien`.
Trên máy đang thực hiện bài, file riêng đã được chuẩn bị từ cấu hình MySQL của QLSV với tên database mới.

`mysql.local.json` đã được Git bỏ qua. Mật khẩu không nằm trong C#, migration hoặc file mẫu;
file cấu hình riêng không được đưa vào bản publish. Trên máy khác cần tạo lại file riêng trước khi build.

Trên máy hiện tại, database `quanlythuvien` đã được chuẩn bị bằng InitialBookDB để chạy WinForms.
Ba bảng đã sẵn sàng và chưa có dữ liệu; bắt đầu bằng cách thêm danh mục trên giao diện.
Không thêm dữ liệu mẫu tự động và không thay đổi database QLSV.

Sau khi build, mở **Tools → NuGet Package Manager → Package Manager Console**:

```powershell
Get-DbContext -Context BookDbContext -Project QLTVDAO -StartupProject QLTVApp
Get-Migration -Context BookDbContext -Project QLTVDAO -StartupProject QLTVApp
```

Đã có migration InitialBookDB; không cần chạy Add-Migration lần nữa cho cùng schema.
Khi muốn tạo database QLTV thật từ migration, chạy:

```powershell
Update-Database -Context BookDbContext -Project QLTVDAO -StartupProject QLTVApp
```

Lệnh trên dành cho database QLTV mới. Nếu `quanlythuvien` đã có bảng do chương trình khác tạo,
cần đối chiếu schema/baseline trước; không chạy lại CREATE TABLE hoặc xóa database để xử lý lỗi.
Không dùng EnsureCreated cùng Migrations. Constructor của DbContext không tự đổi schema hoặc thêm dữ liệu.

## Ví dụ gọi DLL từ một chương trình Console/WinForms khác

Thêm Project Reference tới `QLTVDAO.csproj`, đặt cấu hình riêng cạnh file EXE của chương trình gọi,
rồi sử dụng các hàm BLL:

```csharp
using QLTVDAO;
using QLTVDAO.BLL;
using QLTVDAO.Entities;

try
{
    var category = new Category { Description = "Công nghệ thông tin" };
    new CategoryBLL().Them(category); // CategoryId nhận mã tự tăng.

    var book = new Book
    {
        Title = "Lập trình C# và EF Core",
        Author = "Nguyễn Văn A",
        Publisher = "NXB Giáo Dục",
        Price = 125000,
        TotalCopies = 10,
        PublicationYear = 2026,
        CategoryId = category.CategoryId
    };
    var bookBLL = new BookBLL();
    bookBLL.Them(book);

    new BookCopyBLL().Them(new BookCopy
    {
        BookId = book.BookId, Status = "Có sẵn", Condition = "Mới 100%"
    });

    var sach = bookBLL.TimTheoMa(book.BookId);
    var ketQua = bookBLL.LayDanhSach("C#", category.CategoryId);
}
catch (Exception ex)
{
    Console.WriteLine(DataHelper.LayThongBaoLoi(ex));
}
```

Ví dụ thêm dữ liệu mỗi lần được chạy, không phải bước tự động khi build.
Chương trình gọi phải xác nhận với người dùng trước khi sửa/xóa. BLL/DAL/DAO không dùng MessageBox,
không nuốt ngoại lệ; lỗi nghiệp vụ dùng ValidationException, lỗi kỹ thuật truyền lên nơi gọi.
Các hàm có đủ danh sách, tìm mã, thêm, sửa, xóa. Book có tìm kiếm tiêu đề/tác giả và lọc danh mục;
BookCopy có lọc theo đầu sách. Mã mới do MySQL tự tạo, không nhập mã như bài sinh viên.

Khác với xóa dây chuyền trong repo tham khảo, QLTV này dùng Restrict: không xóa danh mục còn sách,
không xóa đầu sách còn bản sách. Khóa ngoại MySQL vẫn bảo vệ khi gọi thẳng DAL/DAO.
LINQ dùng tham số thay cho ghép dữ liệu vào SQL để chống SQL injection.

## Kiểm tra

Build: **0 lỗi, 0 cảnh báo**. Đã qua **29 kiểm tra tích hợp trên MySQL**, gồm migration tạo đủ ba bảng,
snapshot khớp model, CRUD cả ba entity, quan hệ, validation, FK, ngoại lệ và payload SQLi.
Các kiểm tra dùng database tạm `qltv_verify_<mã ngẫu nhiên>` và đã dọn sau khi chạy.
Phần WinForms đã qua thêm 33 kiểm tra luồng nút và MySQL trên database tạm: CRUD ba tab,
chọn dòng, ComboBox, validation, lọc, không thay đổi, hủy xác nhận và bắt lỗi tại View.
Kiểm tra bố cục bằng ảnh render của cả ba tab; mật khẩu và các file bin/obj không đưa lên Git.

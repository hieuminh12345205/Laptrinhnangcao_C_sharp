# QLTV — Quản lý thư viện WinForms và Class Library

Ngày làm: **24.10.2026**. Cập nhật giao diện đầu sách/bản sao: **31.10.2026**.

Solution dùng C#/.NET 10, MySQL và Entity Framework Core, gồm hai project:
- **QLTVApp:** Windows Forms App, có giao diện và chạy bằng F5.
- **QLTVDAO:** Class Library chứa dữ liệu/nghiệp vụ, tạo DLL được QLTVApp tham chiếu.

## Giao diện

Giao diện giữ control và màu WinForms mặc định, không đặt màu nền/chữ riêng cho nút.
Một màn hình duy nhất theo kiểu master–detail:

- **Thanh trên:** ô từ khóa (tìm theo mã sách, tên sách, tác giả), ComboBox lọc thể loại,
  nút Tìm kiếm và Làm mới.
- **Khung trái — Thông tin đầu sách:** mã sách, tên sách, tác giả, nhà xuất bản, thể loại,
  giá sách, năm xuất bản và số bản sao (ô chỉ đọc). Các nút Thêm đầu sách, Lưu chỉnh sửa,
  Xóa đầu sách, Làm trống và Nhập bổ sung bản sao.
- **Khung phải trên — Danh sách đầu sách:** chọn một dòng để đổ thông tin sang khung trái
  và nạp danh sách bản sao của đầu sách đó.
- **Khung phải dưới — Danh sách bản sao:** tiêu đề hiển thị mã sách đang chọn và số bản sao.
  Có ô trạng thái, tình trạng vật lý, số bản nhập thêm cùng ba nút Nhập thêm bản sao,
  Cập nhật bản sao, Xóa bản sao.
- **Thanh trạng thái dưới cùng:** báo số đầu sách đã tải và thời điểm tải.

Lỗi nhập hiển thị bằng ErrorProvider ngay tại ô sai; lỗi BLL/DAL được View bắt và thông báo.
Hỏi xác nhận trước khi sửa, xóa, nhập bổ sung và trước khi đóng; báo "chưa thay đổi" khi bấm
Lưu chỉnh sửa mà dữ liệu vẫn như cũ.

Màn hình này **không quản lý thể loại** — thể loại chỉ xuất hiện dưới dạng ComboBox.
Khi database chưa có thể loại nào, form báo ngay lúc mở và chưa thêm được đầu sách;
hãy thêm dòng vào bảng `Categories` trước (ví dụ bằng MySQL Workbench).
Các hàm `CategoryBLL` thêm/sửa/xóa thể loại vẫn còn đầy đủ trong Class Library.

## Mô hình dữ liệu

```text
Category (1) ─── (n) Book (1) ─── (n) BookCopy
```

- **Category:** CategoryId tự tăng, Description.
- **Book:** BookId tự tăng, **MaSach**, Title, Publisher, Price, Author, PublicationYear, CategoryId.
- **BookCopy:** BookCopyId tự tăng, **MaBanSao**, Status, Condition, BookId.

Book là đầu sách; BookCopy là một bản sao vật lý.

Mỗi entity có hai loại mã, đừng nhầm:

| | Khóa chính | Mã nghiệp vụ |
| --- | --- | --- |
| Book | `BookId` int tự tăng, MySQL cấp | `MaSach` ví dụ `B006`, người dùng đặt |
| BookCopy | `BookCopyId` int tự tăng | `MaBanSao` ví dụ `B006.001`, hệ thống sinh |

`MaSach` và `MaBanSao` đều có unique index nên không thể trùng, kể cả khi gọi thẳng DAO.
`MaBanSao` sinh theo mã đầu sách và số thứ tự nối tiếp; người dùng không tự đặt.

**Số bản sao không còn là cột trong database.** `Book.SoBanSao` là thuộc tính `[NotMapped]`
trả về `BookCopies.Count`, nên con số trên giao diện luôn bằng số dòng BookCopy thực tế và
không thể lệch. Thuộc tính này chỉ đúng khi truy vấn có `Include(x => x.BookCopies)`.
Cột `TotalCopies` khai báo tay ở bản trước đã bị bỏ.

Chưa có chức năng quản lý bạn đọc và phiếu mượn/trả.

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
    ├── Migrations/                # InitialBookDB, ThemMaSachVaMaBanSao và model snapshot
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
5. Thêm thể loại vào bảng `Categories` trước, sau đó thêm đầu sách trên giao diện,
   rồi dùng **Nhập bổ sung bản sao** để đăng ký các bản sao.

Muốn chỉnh giao diện: trong QLTVApp, nhấp phải **Form1.cs → View Designer**.
Chạy từ PowerShell ở thư mục QLTV cũng được:

```powershell
dotnet run --project .\QLTVApp\QLTVApp.csproj
```

## Kết nối và Migration

Bộ package 10.0.9 khớp MySql.Data 26.7.0 đang dùng: EF Core, MySQL provider, Design và Tools.
File cấu hình QLTV riêng nằm cạnh solution. Sao chép `mysql.example.json` thành `mysql.local.json`,
điền thông tin MySQL của máy và giữ Database là `quanlythuvien` để tách khỏi `quanlysinhvien`.

`mysql.local.json` đã được Git bỏ qua. Mật khẩu không nằm trong C#, migration hoặc file mẫu;
file cấu hình riêng không được đưa vào bản publish. Trên máy khác cần tạo lại file riêng trước khi build.

Có hai migration:

| Migration | Nội dung |
| --- | --- |
| `20261024090818_InitialBookDB` | Tạo ba bảng Categories, Books, BookCopies. |
| `20261031070000_ThemMaSachVaMaBanSao` | Thêm `MaSach`, `MaBanSao` và unique index; bỏ cột `TotalCopies` cùng check constraint của nó; đổi `Author` sang bắt buộc. |

Database đang có dữ liệu vẫn chạy được migration thứ hai: nó sinh sẵn `MaSach` dạng `B001`,
`B002`... theo BookId và `MaBanSao` dạng `B001.001` theo từng đầu sách trước khi tạo unique index,
đồng thời thay `Author` đang NULL bằng chuỗi rỗng. Sau khi chạy xong nên mở lại và đặt
mã sách đúng theo quy ước của mình.

Sau khi build, mở **Tools → NuGet Package Manager → Package Manager Console** rồi chạy:

```powershell
Update-Database -Context BookDbContext -Project QLTVDAO -StartupProject QLTVApp
```

Kiểm tra trạng thái:

```powershell
Get-DbContext -Context BookDbContext -Project QLTVDAO -StartupProject QLTVApp
Get-Migration -Context BookDbContext -Project QLTVDAO -StartupProject QLTVApp
```

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
        MaSach = "B020",
        Title = "Lập trình C# và EF Core",
        Author = "Nguyễn Văn A",
        Publisher = "NXB Giáo Dục",
        Price = 125000,
        PublicationYear = 2026,
        CategoryId = category.CategoryId
    };
    var bookBLL = new BookBLL();
    bookBLL.Them(book);

    // Sinh B020.001 ... B020.005 trong một lần lưu.
    var banSao = new BookCopyBLL().Them(book.BookId, 5, "Sẵn sàng", "Mới 100%");

    var sach = bookBLL.TimTheoMa(book.BookId);          // Theo khóa chính.
    var theoMa = bookBLL.TimTheoMaSach("B020");          // Theo mã nghiệp vụ.
    var ketQua = bookBLL.LayDanhSach("C#", category.CategoryId);
    int soBan = sach!.SoBanSao;                          // = 5
}
catch (Exception ex)
{
    Console.WriteLine(DataHelper.LayThongBaoLoi(ex));
}
```

Ví dụ thêm dữ liệu mỗi lần được chạy, không phải bước tự động khi build.
Chương trình gọi phải xác nhận với người dùng trước khi sửa/xóa. BLL/DAL/DAO không dùng MessageBox,
không nuốt ngoại lệ; lỗi nghiệp vụ dùng ValidationException, lỗi kỹ thuật truyền lên nơi gọi.

Khác với xóa dây chuyền trong repo tham khảo, QLTV này dùng Restrict: không xóa thể loại còn sách,
không xóa đầu sách còn bản sao. Khóa ngoại MySQL vẫn bảo vệ khi gọi thẳng DAL/DAO.
LINQ dùng tham số thay cho ghép dữ liệu vào SQL để chống SQL injection.

## Kiểm tra

**Bản 24.10.2026** (ba tab Đầu sách/Danh mục/Bản sách, cột `TotalCopies`) đã qua:
build 0 lỗi 0 cảnh báo, 29 kiểm tra tích hợp trên MySQL và 33 kiểm tra luồng nút WinForms,
chạy trên database tạm `qltv_verify_<mã ngẫu nhiên>` và đã dọn sau khi chạy.

**Bản cập nhật 31.10.2026** (giao diện một màn hình, `MaSach`/`MaBanSao`, bỏ `TotalCopies`)
**chưa được build và chưa chạy kiểm tra nào** — phần này soạn trong môi trường Linux không có
.NET SDK và không có MySQL, nên không build thử được. Trước khi tin tưởng bản này, hãy tự chạy
trên máy Windows: `Update-Database`, build solution, rồi thử lần lượt thêm/sửa/xóa đầu sách,
nhập bổ sung bản sao, cập nhật và xóa bản sao, tìm kiếm và lọc thể loại.

Mật khẩu và các file bin/obj không đưa lên Git.

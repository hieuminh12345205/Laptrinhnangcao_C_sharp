# QLTV — Quản lý thư viện WinForms và Class Library

Ngày làm: **24.10.2026**.

Solution dùng C#/.NET 10, MySQL và Entity Framework Core, gồm hai project:
- **QLTVApp:** Windows Forms App, có giao diện và chạy bằng F5.
- **QLTVDAO:** Class Library chứa dữ liệu/nghiệp vụ, tạo DLL được QLTVApp tham chiếu.

Giao diện cập nhật theo mẫu giáo viên ngày **31.10.2026**: tiêu đề nền xanh nhạt,
thanh tìm kiếm ở trên, thông tin đầu sách bên trái; danh sách đầu sách ở trên và bản sao
của đầu sách đang chọn ở dưới bên phải. Các nút thêm/lưu/xóa dùng màu như mẫu.
Chọn một đầu sách để xem bản sao; nút **Nhập bổ sung bản sao** mở tab chỉnh sửa đã chọn sẵn sách.
Chọn một bản sao rồi bấm **Cập nhật bản sao** hoặc nhấp đúp dòng để sửa; có thể xóa ngay từ bảng bên dưới.
Tab **Thể loại** và **Chỉnh sửa bản sao** giữ các chức năng quản lý chi tiết.

Mỗi phần có thêm/sửa/xóa/làm trống; hỏi xác nhận trước sửa/xóa/đóng, báo chưa thay đổi
khi bấm lưu mà dữ liệu vẫn như cũ. Tìm theo mã số, tên sách/tác giả và lọc thể loại.
Khi kết quả tìm không còn đầu sách đang chọn, giao diện xóa thông tin và bản sao cũ.
Lỗi nhập hiển thị bằng ErrorProvider, lỗi BLL/DAL được View bắt và thông báo.
Mã sách/bản sao vẫn là số tự tăng, không đổi sang mã B006/B006.001 hoặc sửa schema database.

## Mô hình dữ liệu

```text
Category (1) ─── (n) Book (1) ─── (n) BookCopy
```

- **Category:** CategoryId tự tăng, Description.
- **Book:** BookId tự tăng, Title, Publisher, Price, TotalCopies, Author, PublicationYear, CategoryId.
- **BookCopy:** BookCopyId tự tăng, Status, Condition, BookId.

Book là đầu sách; BookCopy là một bản sách vật lý.
Khi thêm đầu sách mới, TotalCopies (ô **Số bản sao**) quyết định số BookCopy được tạo ngay.
Ví dụ nhập 5 thì lưu một Book và 5 BookCopy, mặc định **Có sẵn**, tình trạng **Mới**.
BookDAO dùng một lần SaveChanges để lưu cả đầu sách và bản sao trong cùng transaction:
nếu lỗi thì hoàn tác toàn bộ, ngoại lệ truyền lên View để thông báo.
Với đầu sách đã có, dùng **Nhập bổ sung bản sao** để thêm từng cuốn và chỉnh trạng thái/tình trạng.
TotalCopies luôn theo số BookCopy thực tế: thêm/xóa bản sao hoặc chuyển bản sao sang đầu sách khác
đều cập nhật tổng trong cùng transaction. Xóa bản sao cuối cùng đưa tổng về 0 và giữ đầu sách.
Khi đang sửa đầu sách, ô số lượng chỉ để xem; muốn đổi số lượng thì thêm/xóa bản sao.
Migration **SyncBookCopyTotals** đồng bộ tổng của dữ liệu cũ theo BookCopy đã có,
không tự sinh bù bản sao. Bản cập nhật này cần chạy Update-Database trước khi dùng.
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
    ├── Migrations/                # InitialBookDB và model snapshot
    ├── BookDbContext.cs
    ├── BookDbContextFactory.cs
    ├── DataHelper.cs
    ├── DatabaseTransaction.cs      # Begin transaction, Commit, Rollback, Dispose
    ├── DuLieuMau.cs                # Tạo dữ liệu minh họa qua BLL, không thêm trùng
    └── GlobalUsings.cs
```

Luồng gọi: **QLTVApp (View) → BLL → DAL → DAO → DbContext → MySQL**.
Class Library này không phụ thuộc dự án Quản lý sinh viên.

### Thêm, sửa, xóa và ngoại lệ ở DAL

Ba lớp `BookDAL`, `BookCopyDAL`, `CategoryDAL` đều có `Add(đối tượng)`,
`Update(đối tượng)`, `Delete(int id)`. Các tên cũ `Them`, `Sua`, `Xoa` vẫn gọi về
ba hàm này; BLL đã dùng trực tiếp Add/Update/Delete.

Trong mỗi hàm, `try` chứa thao tác gọi DAO. `catch (ValidationException)` dùng `throw;`
để giữ nguyên lỗi nghiệp vụ. `catch (Exception ex)` ném `DataException` với mô tả
thao tác và giữ lỗi gốc ở `InnerException`. Không dùng `throw ex;` hoặc chỉ chép `ex.Message`.
BLL tiếp tục truyền lỗi lên View; View dùng `ThucHienAnToan` và `DataHelper.LayThongBaoLoi`
để hiện thông báo phù hợp, không in lỗi kỹ thuật/chuỗi kết nối ra giao diện.

DAO vẫn thực hiện EF Core `Add`, tìm bản ghi để sửa/xóa, rồi `SaveChanges`.
`using var db` giải phóng DbContext kể cả khi có ngoại lệ, không cần thêm `finally` để đóng tay.
Nếu lỗi, hàm ném ngoại lệ; nếu thành công, hàm trả về bình thường và mã tự tăng được gán lại cho đối tượng.
Việc kiểm tra dữ liệu và quy tắc không xóa đầu sách/thể loại còn dữ liệu con vẫn nằm ở BLL.

### DatabaseTransaction: Commit và Rollback

`DatabaseTransaction.cs` dùng chung cho các hàm thêm/sửa/xóa trong ba DAO.
Khởi tạo `new DatabaseTransaction(db)` bắt đầu giao dịch trên DbContext được truyền vào.

- `Commit()`: chấp nhận các thay đổi đã SaveChanges; không tự gọi SaveChanges.
- `Rollback()`: hoàn tác tất cả lệnh đã ghi trong giao dịch.
- `Dispose()`: giải phóng giao dịch; giao dịch chưa commit sẽ được hoàn tác, DbContext vẫn thuộc nơi gọi.
- `ThucHien(db, thaoTac)`: hàm dùng chung chứa try/catch; thành công thì Commit, lỗi thì Rollback và throw.

Ví dụ cách các DAO sử dụng:

```csharp
using var db = new BookDbContext();
DatabaseTransaction.ThucHien(db, () =>
{
    db.Categories.Add(new Category { Description = "Danh mục thử" });
    db.SaveChanges();
    // Có thể gọi thêm SaveChanges ở đây; tất cả vẫn thuộc cùng một giao dịch.
});
```

Các lệnh trong một giao dịch phải dùng cùng DbContext. BookCopyDAO giữ mức cô lập Serializable
để đồng bộ bản sao và tổng số khi có thao tác đồng thời. Chỉ trả mã mới cho nơi gọi sau khi commit thành công.
Nếu Rollback cũng lỗi vì mất kết nối, ngoại lệ ban đầu vẫn được ném lên; lỗi rollback được giữ trong
`ex.Data["RollbackException"]` để chẩn đoán, không hiển thị chi tiết kỹ thuật cho người dùng.
DAO tạo DbContext riêng cho mỗi thao tác và giải phóng sau khi dùng; không tái sử dụng các entity đã rollback.

### Business cho các nút đầu sách

| Nút trên form | Phương thức | Xử lý |
|---|---|---|
| Thêm đầu sách | `BookBLL.Them(book)` | Kiểm tra dữ liệu, thể loại, tạo đầu sách và đủ bản sao ban đầu. |
| Lưu chỉnh sửa | `BookBLL.CoThayDoi(book)` và `Sua(book)` | Phát hiện chưa sửa gì trước xác nhận; lưu thông tin, giữ tổng bản sao thực tế. |
| Xóa đầu sách | `BookBLL.Xoa(id)` | Chặn khi không tìm thấy sách hoặc còn bản sao. |
| Làm trống | `Form1.LamMoiSach()` ở View | Xóa ô nhập, đặt lại trạng thái nút; không thay đổi database. |
| Nhập bổ sung bản sao | `BookBLL.NhapBoSungBanSao(id, soLuong, trangThai, tinhTrang)` | Kiểm tra sách, số lượng và dữ liệu; lưu toàn bộ đợt nhập và tổng trong một transaction. |

Form hiện nhập bổ sung từng cuốn qua tab bản sao. Business cũng hỗ trợ nhập một đợt, ví dụ:

```csharp
var copies = new BookBLL().NhapBoSungBanSao(bookId, 3); // Thêm 3 cuốn Có sẵn / Mới.
```

Lỗi validation hoặc lỗi database được truyền lên View để thông báo bằng `ThucHienAnToan`.
BLL/DAL không mở MessageBox và không nuốt lỗi. LINQ/EF dùng tham số, không ghép dữ liệu người dùng vào SQL.

### Dữ liệu mẫu

`DuLieuMau.ThemNeuChuaCo()` tạo **8 đầu sách, 6 thể loại, 28 bản sao** trên database mới trống.
Tên tác giả, nhà xuất bản, giá và năm là dữ liệu minh họa để học, không phải thông tin xuất bản thực tế.
Hàm dùng Business để validation và tạo bản sao; bỏ qua đầu sách đã có cùng tên và tác giả,
tái sử dụng thể loại đã tồn tại. Không sửa/xóa các bản ghi cũ, không tự bù bản sao cho sách đã có.

Muốn tạo mẫu trên máy khác, cấu hình MySQL và chạy Update-Database trước, rồi chạy từ thư mục QLTV:

```powershell
dotnet run --project .\QLTVApp\QLTVApp.csproj -- --du-lieu-mau
```

Chạy lại lệnh không thêm trùng sách/bản sao mẫu. Mở bình thường bằng Ctrl + F5 không tự thêm mẫu.

## Mở trong Visual Studio

1. Chọn **File → Open → Project/Solution**, mở `QLTV.slnx` trong thư mục này.
2. Solution Explorer hiển thị `QLTVApp` và `QLTVDAO`. Nếu vừa thêm project khi solution đang mở,
   chấp nhận Reload hoặc đóng/mở lại solution để thấy đủ cả hai.
3. Chọn **Build → Build Solution** hoặc nhấn **Ctrl + Shift + B**.
4. Nhấp phải **QLTVApp → Set as Startup Project**, rồi nhấn **F5** hoặc **Ctrl + F5**.
   Không chọn QLTVDAO làm startup vì đó là thư viện DLL.
5. Khi database mới trống, form mở tab Thể loại: thêm thể loại trước, sau đó thêm Đầu sách
   cùng số bản sao ban đầu. Muốn nhập thêm thì chọn sách rồi bấm Nhập bổ sung bản sao.
   Mã tự tăng do MySQL cấp, người dùng không nhập mã mới.

Muốn chỉnh giao diện: trong QLTVApp, nhấp phải **Form1.cs → View Designer**.
Tên cột, tiêu đề và ánh xạ dữ liệu của bốn DataGridView được đặt trong
`Form1.CauHinhCotDuLieu()` ngay sau InitializeComponent. Cách này giữ cấu hình cột khi lưu Designer;
không dựa vào cột tự sinh BookId/BookCopyId, vì sự kiện chọn dòng dùng tên colBookId/colCopyId.
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

Trên máy hiện tại, database `quanlythuvien` đã áp dụng InitialBookDB và SyncBookCopyTotals để chạy WinForms.
Ba bảng đã sẵn sàng; dữ liệu mẫu được tạo riêng theo yêu cầu, không tự chạy mỗi lần mở ứng dụng.
Không thay đổi database QLSV.

Sau khi build, mở **Tools → NuGet Package Manager → Package Manager Console**:

```powershell
Get-DbContext -Context BookDbContext -Project QLTVDAO -StartupProject QLTVApp
Get-Migration -Context BookDbContext -Project QLTVDAO -StartupProject QLTVApp
```

Đã có hai migration InitialBookDB và SyncBookCopyTotals; không cần tạo lại.
Khi tạo database mới hoặc cập nhật bản cũ lên schema hiện tại, chạy:

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
    bookBLL.Them(book); // Tạo đồng thời 10 bản sao Có sẵn / Mới.

    new BookCopyBLL().Them(new BookCopy // Nhập bổ sung một cuốn: tổng tự tăng lên 11.
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

Đối chiếu đầy đủ với bộ code giáo viên tại [THAM_KHAO_GV.md](THAM_KHAO_GV.md).
Đã bổ sung `BookBLL.ImportBill(List<Book>)`: BookId = 0 tạo sách mới, mã đã có thì bổ sung;
TotalCopies ở mỗi dòng là số lượng nhập thêm. Toàn bộ phiếu dùng một DbContext và transaction,
chỉ trả mã mới sau commit, lỗi ở bất kỳ dòng nào thì rollback toàn phiếu.
Đây là hàm Business; giao diện hiện tại chưa thêm màn hình nhập phiếu/Excel.
Ba entity có `IsInValid()` trả danh sách lỗi (rỗng khi hợp lệ), và CategoryBLL có `LayTheoTen()`.

Build: **0 lỗi, 0 cảnh báo**. Đã qua **61 kiểm tra tầng dữ liệu và MySQL**, gồm migration tạo đủ ba bảng,
snapshot khớp model, CRUD cả ba entity, quan hệ, validation, FK, ngoại lệ và payload SQLi.
Hai kiểm tra ngoại lệ bổ sung bao phủ đủ chín hàm Add/Update/Delete của ba lớp DAL:
giữ nguyên lỗi nghiệp vụ, giữ nguyên nhân kỹ thuật ở InnerException và che nội dung nhạy cảm khi thông báo.
Các kiểm tra dùng database tạm `qltv_verify_<mã ngẫu nhiên>` và đã dọn sau khi chạy.
Đã kiểm tra thêm việc tạo đồng thời bản sao, đồng bộ tổng khi thêm/xóa/chuyển sách,
migration sửa tổng cũ, và hoàn tác khi lưu bản sao hoặc cập nhật tổng gặp lỗi.
Có kiểm tra Business phát hiện chưa thay đổi, nhập bổ sung nhiều cuốn trong một transaction,
hoàn tác khi lỗi giữa đợt nhập, tạo mẫu đủ 8 đầu sách/28 bản sao và chạy lại không trùng.
Lớp DatabaseTransaction được kiểm tra Commit, Rollback tường minh, tự rollback khi chưa commit,
giữ nguyên ngoại lệ và giải phóng transaction mà không đóng DbContext của nơi gọi.
Phần WinForms theo mẫu mới đã qua **51 kiểm tra** luồng nút và MySQL trên database tạm:
CRUD ba tab, bảng bản sao theo sách đang chọn, các nút nhập/sửa/xóa bản sao từ màn hình chính,
tìm mã sách, ComboBox, validation, lọc, không thay đổi, hủy xác nhận và bắt lỗi tại View.
Kiểm tra bố cục bằng ảnh render cả ba tab và cửa sổ lớn/nhỏ; các database kiểm tra được dọn sau khi chạy.
Mật khẩu và các file bin/obj không đưa lên Git.

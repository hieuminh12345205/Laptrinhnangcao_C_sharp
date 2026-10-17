# Quản lý sinh viên

## Chuẩn bị MySQL ngày 17.10.2026

`Data/DataHelper.cs` dùng gói MySql.Data để chạy SQL có tham số:

- `ExecuteNonQuery`: INSERT/UPDATE/DELETE, trả về số dòng bị ảnh hưởng.
- `ExecuteQuery`: SELECT, trả về DataTable.

Chuỗi kết nối được truyền vào constructor. Không đưa mật khẩu thật vào mã nguồn hoặc commit.
Đã tạo hai bảng `quanlysinhvien.lophoc` và `quanlysinhvien.sinhvien` trên MySQL ngày 17.10.2026.
Script lưu tại `Data/Sql/lophoc.sql` và `Data/Sql/sinhvien.sql` (chạy lophoc.sql trước). Khóa ngoại MaLop liên kết quan hệ 1-n,
chặn xóa lớp còn sinh viên. Điểm dùng DECIMAL(3,1), giới hạn từ 0 đến 10.
DAO đã đọc/ghi MySQL. Có ba lớp mẫu CSE0001, CSE0002, CSE0003 để chọn trên ComboBox.
Phần đọc cấu hình đã gộp vào `Data/DataHelper.cs`; constructor không tham số tự đọc mysql.local.json.
Các nút Thêm/Sửa/Xóa đã gọi SQL qua DataHelper. Dữ liệu đã lưu không mất khi đóng ứng dụng.

### Điền thông tin kết nối trên máy

1. Mở `mysql.local.json` trong project (đã tạo sẵn, được Git bỏ qua).
2. Giữ Server = 127.0.0.1, Port = 3306, Database = quanlysinhvien, User = root.
3. Điền mật khẩu MySQL đã đặt lúc cài vào Password, lưu file. Không gửi mật khẩu trong chat.
4. Kiểm tra bằng chương trình console riêng, không thêm nút vào form. Trong PowerShell chạy:

```powershell
cd 'D:\C#\ConsoleApp1'
dotnet run --project .\KiemTraMySql\KiemTraMySql.csproj
```

Chương trình dùng cùng DataHelper và mysql.local.json, chỉ đọc database, phiên bản và danh sách bảng.
Khi hiện `OK: kết nối và hai bảng đã sẵn sàng.` là kết nối thành công. Mỗi lần chạy tự build và sao chép cấu hình mới.

`mysql.example.json` là mẫu được commit cho người khác sử dụng. Mật khẩu thật chỉ nằm trong mysql.local.json
và bản sao trong thư mục bin đã được Git bỏ qua. File mật khẩu không được tự sao chép khi Publish.
Nếu bạn dùng mật khẩu có dấu nháy kép hoặc dấu gạch chéo ngược, hãy escape theo cú pháp JSON.
Build thành công không có nghĩa đăng nhập MySQL thành công; hãy xem kết quả của chương trình KiemTraMySql.
Đã kiểm tra đăng nhập thực tế bằng C# ngày 17.10.2026: kết nối thành công tới quanlysinhvien,
MySQL 8.4.11 và tìm thấy hai bảng lophoc, sinhvien.

Mở `Quanlysinhvien.slnx` trong Visual Studio và nhấn F5 để chạy.
Nếu Visual Studio hỏi nạp lại file đã thay đổi bên ngoài, chọn Reload.

## Các file chính

- `Data/Entity/LopHoc.cs`: thông tin lớp, danh sách sinh viên của lớp và validation.
- `Data/Entity/SinhVien.cs`: thông tin sinh viên, Data Annotations và phương thức `KiemTraHopLe`.
- `Data/DAO/LopHocDAO.cs`: lấy và thao tác dữ liệu lớp trong MySQL bằng SQL có tham số.
- `Data/DAL/LopHocDAL.cs`: cung cấp các hàm truy cập dữ liệu lớp cho BLL.
- `BLL/LopHocBLL.cs`: validation lớp, kiểm tra mã lớp trùng và chặn xóa lớp còn sinh viên.
- `Data/DAL/SinhVienDAL.cs`: cung cấp các hàm truy cập dữ liệu và chuyển yêu cầu từ BLL sang DAO.
- `Data/DAO/SinhVienDAO.cs`: đọc, tìm, lọc, thêm/sửa/xóa sinh viên trong MySQL.
- `BLL/SinhVienBLL.cs`: kiểm tra dữ liệu, mã trùng, làm tròn điểm và tìm kiếm, sau đó gọi DAL.
- `Views/frmQuanLySV.cs`: đọc các ô nhập, gọi BLL, hiển thị kết quả và hỏi xác nhận.
- `Views/frmQuanLySV.Designer.cs` và `.resx`: giao diện và tài nguyên của form.

Luồng xử lý của bài: **Views → BLL → DAL → DAO**. Các tầng dùng chung đối tượng trong **Data/Entity**.
DAO là lớp thao tác dữ liệu thuộc tầng truy cập dữ liệu. Bài tập tách DAL và DAO thành hai lớp để dễ theo dõi yêu cầu từ BLL đến nơi lưu dữ liệu.
BLL, DAL và DAO không gọi MessageBox; thông báo và xác nhận thuộc về View.

## Phần lớp học

LopHocBLL có các hàm `LayDanhSach`, `TimTheoMa`, `Them`, `Sua` và `Xoa`.
Thêm/sửa kiểm tra mã và tên lớp bằng Data Annotations. Mã lớp không được trùng, kể cả khác chữ hoa/thường.
Sửa giữ mã lớp và danh sách sinh viên, chỉ thay tên lớp. Xóa chỉ được phép khi lớp không có sinh viên.
Các hàm ghi tại BLL trả void khi thành công, ném ValidationException khi vi phạm nghiệp vụ.
Lỗi MySQL/cấu hình tự truyền lên View; không chuyển thành bool/out string.

Form tạo một LopHocBLL, dùng nó cho ComboBox và truyền nó vào SinhVienBLL.
Hai phần cùng đọc database; khóa ngoại đảm bảo sinh viên luôn thuộc lớp đang tồn tại.
DAO không tự thêm dữ liệu trong constructor. Chạy Data/Sql/dulieumau.sql để thêm ba lớp mẫu trên máy mới.
ComboBox đặt DisplayMember = TenLop và ValueMember = MaLop: hiển thị tên nhưng lấy mã bằng SelectedValue.
Thanh tìm kiếm lọc theo mã lớp, nên vẫn phân biệt được hai lớp có tên giống nhau.
Hiện chưa có form riêng để bấm thêm/sửa/xóa lớp; các chức năng này đã được xây dựng ở tầng dữ liệu và nghiệp vụ.

## Xem cấu trúc trong Visual Studio

1. Mở `D:\C#\ConsoleApp1\Quanlysinhvien\Quanlysinhvien.slnx`.
2. Nhấn Ctrl + Alt + L để mở Solution Explorer.
3. Nếu được hỏi nạp lại project hoặc file, chọn Reload.
4. Mở các thư mục BLL, Data và Views để xem các lớp tương ứng.
5. Nhấp phải `Views/frmQuanLySV.cs` rồi chọn View Designer, hoặc nhấn Shift + F7.

## Cách thử

1. Khi mở form: con trỏ ở mã sinh viên, nút Thêm bật, Sửa/Xóa tắt.
2. Nhập mã đã lưu trong database: hiện thông tin; Thêm tắt, Sửa/Xóa bật.
3. Nhập mã mới, ví dụ `SV002`: các ô còn lại được xóa để nhập sinh viên mới.
4. Điền thông tin, chọn dấu kiểm bên cạnh ngày sinh, chọn giới tính, lớp và trạng thái rồi bấm Thêm.
5. Bấm một dòng trên bảng để đưa dữ liệu lên các ô nhập. Thay đổi thông tin rồi bấm Sửa.
6. Sửa và Xóa có hộp thoại xác nhận. Chọn No để hủy.
7. Làm mới xóa thông tin nhập, bật Thêm, tắt Sửa/Xóa và đưa con trỏ về mã sinh viên.
8. Tìm kiếm lọc đồng thời theo từ khóa, lớp và điểm tối thiểu. Hiển thị tất cả xóa bộ lọc.

## Bổ sung ngày 10.10.2026

- Khi Thêm/Sửa có dữ liệu sai, ErrorProvider hiển thị lỗi cạnh từng ô. Rê chuột vào biểu tượng để đọc lỗi.
- Con trỏ chuyển đến ô sai đầu tiên. Chỉnh lại ô sẽ xóa biểu tượng lỗi của ô đó; Làm mới xóa tất cả lỗi.
- Validation trả đầy đủ lỗi thuộc tính và ngày sinh trong cùng lần kiểm tra.
- BLL chuẩn hóa họ tên bằng cách gộp nhiều khoảng trắng thành một khoảng trắng.
- Chọn lớp ở thanh tìm kiếm sẽ tự cập nhật bảng theo từ khóa, mã lớp và điểm tối thiểu hiện tại.
- Vào ô mã sinh viên sẽ chọn toàn bộ mã; Enter chuyển sang họ tên theo thứ tự Tab của hàng đầu tiên.

## Đối chiếu sáu yêu cầu bài tập

1. Giao diện: có các ô thông tin, giới tính, lớp, điểm, trạng thái, nút chức năng, thanh tìm kiếm và bảng.
2. LopHoc/SinhVien: quan hệ 1-n, Data Annotations và KiemTraHopLe; SinhVien có LayLoiValidation để báo lỗi theo ô.
3. FormLoad: Tab theo từng hàng từ trái sang phải rồi xuống hàng, tiêu điểm ở txtMaSV, nạp lớp/bảng và thiết lập nút.
4. Mã tồn tại: tự điền thông tin, tắt Thêm, bật Sửa/Xóa. Mã mới: giữ mã, xóa thông tin khác, bật Thêm, tắt Sửa/Xóa.
5. Làm mới: xóa thông tin và lỗi, bỏ chọn ngày sinh/giới tính/lớp/trạng thái, điểm về 0, tiêu điểm ở txtMaSV.
6. Sửa, Xóa và Đóng: hỏi Yes/No, mặc định No. Xóa lớp đang có sinh viên bị BLL từ chối.

Kiểm tra tự động ngày 10.10.2026: 61 kiểm tra dữ liệu, quan hệ lớp-sinh viên, CRUD qua BLL/DAL/DAO,
validation, ErrorProvider, thứ tự Tab, trạng thái nút, làm mới và lọc đã qua. Build không lỗi/cảnh báo.
Hộp thoại Yes/No được rà soát trong code; để thử thủ công, chọn Sửa/Xóa/Đóng rồi chọn No và kiểm tra dữ liệu giữ nguyên.

## Quy tắc dữ liệu

- Mã và họ tên bắt buộc, có giới hạn độ dài.
- Email phải hợp lệ; số điện thoại có 10 chữ số, bắt đầu bằng 0.
- Ngày sinh bắt buộc và không nằm trong tương lai.
- Giới tính phải là Nam hoặc Nữ; phải chọn lớp và trạng thái.
- Điểm từ 0 đến 10; khi đọc từ form, làm tròn một chữ số thập phân.
- Mỗi lớp có nhiều sinh viên, mỗi sinh viên thuộc một lớp.

## Phạm vi

Dữ liệu lưu trong MySQL; constructor không tự tạo sinh viên mẫu. Sinh viên đã lưu còn nguyên sau khi đóng ứng dụng.
Nút Đóng và dấu X đều hỏi xác nhận. Bài tập dùng xác nhận Yes/No cho thao tác nguy hiểm, chưa có đăng nhập.
Khi mã đã tồn tại, tắt nút Thêm nhưng vẫn cho sửa các ô thông tin để dùng nút Sửa.

## Hoàn thiện CRUD MySQL và chống SQLi — 17.10.2026

- BLL, DAL, DAO có AddSinhVien, UpdateSinhVien, DeleteSinhVien; form gọi các hàm này.
- INSERT/UPDATE/DELETE, tìm mã và lọc đều truyền dữ liệu bằng MySqlParameter có kiểu rõ ràng.
- Không chèn dữ liệu nhập bằng dấu $ hoặc phép nối chuỗi vào SQL. Tên bảng/cột và mẫu SQL cố định trong code.
- UPDATE và DELETE chỉ tác động bản ghi có MaSV trùng tham số, kiểm tra số dòng để báo đúng kết quả.
- Mã trùng và lớp không tồn tại được kiểm tra ở BLL, đồng thời bị khóa chính/khóa ngoại MySQL chặn.
- Thông báo lỗi không in câu SQL, chuỗi kết nối, mật khẩu hoặc dữ liệu người dùng.
- Form chọn lớp bằng SelectedValue = MaLop; không phụ thuộc việc hai đối tượng C# có cùng tham chiếu.
- Khác với trước đây, tìm kiếm được thực hiện trong MySQL. LOCATE tìm từ khóa nguyên văn, không coi %/_ là wildcard.

Kiểm tra thực tế: 34 kiểm tra MySQL/SQLi đã qua. Bao gồm thêm và đọc bằng BLL mới, sửa toàn bộ thuộc tính,
sửa không thay đổi nội dung, chuyển lớp, xóa, validation, khóa chính/khóa ngoại và luồng FormLoad/mã sinh viên/lọc/làm mới.
Payload được thử ở mã, họ tên và bộ lọc; bảng và bản ghi ngoài mục tiêu giữ nguyên. Chỉ các bản ghi thử mã ngẫu nhiên
được tạo và dọn sau kiểm tra. Ba lớp mẫu còn thiếu đã được bổ sung, không ghi đè lớp đã có.

## Xử lý ngoại lệ — 17.10.2026

- BLL ném ValidationException khi vi phạm nghiệp vụ; lỗi MySQL/cấu hình/quyền đọc file tự truyền qua DAO, DAL, BLL lên View.
- Form có try/catch cho các thao tác dữ liệu, kể cả chọn dòng trong bảng. Không hiển thị trực tiếp ex.Message vì có thể chứa thông tin nhạy cảm.
- DataHelper phân biệt lỗi đăng nhập, mất kết nối, mã trùng, khóa ngoại và dữ liệu sai. using tự đóng kết nối khi có ngoại lệ.
- Khi kết nối gián đoạn giữa lúc lưu, kiểm tra lại dữ liệu trong MySQL trước khi thử lại; không tự động chạy lại lệnh ghi.
- Khi điền dữ liệu, try/finally luôn trả cờ dangHienThi về false; ngày sinh và điểm được kiểm tra khoảng trước khi gán vào control.
- Trường chuỗi null được chuyển thành rỗng để Data Annotations báo lỗi thay vì NullReferenceException.

Đã qua 45 kiểm tra tích hợp, bổ sung các trường hợp mất file cấu hình, JSON sai, cổng sai, sai mật khẩu,
thông báo không lộ nội dung ngoại lệ và thao tác thất bại không thay đổi dữ liệu. Chỉ bản cấu hình trong thư mục kiểm tra
được sửa tạm rồi khôi phục; cấu hình của dự án được giữ nguyên.

## Đọc dữ liệu theo phương pháp hướng kết nối — 17.10.2026

DataHelper.ExecuteReader(sqlSelect, thamSo) trả MySqlDataReader. SinhVienDAO dùng phương thức này
cho LayDanhSach, TimTheoMa và TimKiem; DocDanhSach đọc từng dòng bằng while (reader.Read()).
Kết nối phải mở trong lúc đọc. Nơi gọi dùng using var reader để luôn giải phóng reader khi ra khỏi hàm,
kể cả có ngoại lệ. CommandBehavior.CloseConnection tự đóng kết nối khi reader được đóng.
Nếu mở kết nối hoặc chạy SELECT thất bại trước khi trả reader, DataHelper giải phóng kết nối rồi ném lại lỗi.
Không cần gọi CloseConnection thủ công sau vòng lặp. ExecuteQuery trả DataTable vẫn dùng cho DAO lớp học.
SQL có tham số, kiểu DateTime/decimal và quan hệ LopHoc-SinhVien được giữ nguyên.

Đã qua 50 kiểm tra tích hợp MySQL/SQLi, gồm đọc nhiều dòng, tham số, trả kết nối về pool khi đọc xong,
khi chuyển đổi dữ liệu gặp lỗi và khi SELECT sai; build không có lỗi hoặc cảnh báo.

## Đối chiếu code tham khảo của cô — 17.10.2026

Giữ cấu trúc Views → BLL → DAL → DAO → DataHelper. Code truy vấn nằm trong DAO;
DAL chuyển tiếp yêu cầu, BLL kiểm tra nghiệp vụ. Dự án dùng MySql.Data thay cho Microsoft.Data.SqlClient.

| Code tham khảo | Phương thức trong dự án |
| --- | --- |
| getAllSinhVien1 / GetAllSinhVien1 (Reader, cùng chức năng) | LayDanhSach() |
| GetAllSinhVien (DataTable, foreach) | LayDanhSachPhiKetNoi() |
| GetSinhVienByMaSV / GetSinhVienById | TimTheoMa(maSV) |
| GetSinhViensByMaLop | LayTheoMaLop(maLop) |
| AddSinhVien / UpdateSinhVien / DeleteSinhVien | Cùng tên trong DAO/DAL/BLL |
| ExcuteReader / GetDataTable | DataHelper.ExecuteReader / ExecuteQuery |

Ví dụ gọi từ BLL (hai hàm đầu trả cùng dữ liệu qua hai cách đọc):

```csharp
var bll = new SinhVienBLL();
var danhSach1 = bll.LayDanhSach();
var danhSach2 = bll.LayDanhSachPhiKetNoi();
var theoLop = bll.LayTheoMaLop("CSE0001");
var sinhVien = bll.TimTheoMa("SV0001"); // Không có thì trả null.
```

Form vẫn dùng Reader cho danh sách/tìm kiếm. Cách DataTable dùng để học và đối chiếu;
không truy vấn cả hai cách mỗi lần mở form. Cả hai đều lấy dữ liệu mới từ MySQL, không tìm trong List cũ.

Các điểm sửa so với code tham khảo:
- INSERT/UPDATE/DELETE và lọc mã lớp dùng tham số @, không ghép dữ liệu bằng $.
- UPDATE có thứ tự đúng: UPDATE ... SET ... WHERE MaSV = @MaSV.
- DeleteSinhVien chạy DELETE trong MySQL, không chỉ Remove khỏi List.
- NgaySinh là DateTime, Diem là decimal, SoDienThoai tương ứng SDT; LopHoc giữ quan hệ 1-n.
- Có TrangThai; SELECT chỉ rõ các cột và JOIN lớp để lấy tên lớp.
- Reader được đóng bằng using, kết nối được giải phóng cả khi có lỗi.
- Không dùng throw new Exception(ex.Message): DataHelper dùng throw để giữ lỗi gốc,
  View bắt ngoại lệ và chuyển thành thông báo an toàn, không báo thành công khi ghi thất bại.

Kiểm tra sau bổ sung: 54 kiểm tra tích hợp đã qua. Hai cách đọc cho cùng giá trị ở mọi cột và cùng thứ tự;
DataTable giữ quan hệ lớp-sinh viên; lọc mã lớp trả đúng bản ghi và chặn payload SQLi.
Build: 0 lỗi, 0 cảnh báo. Dữ liệu thử được dọn sau khi chạy.

## Yêu cầu cuối: ngoại lệ do View xử lý — 17.10.2026

- LopHocDAO và SinhVienDAO thao tác MySQL bằng ADO.NET, truyền tham số để chống SQLi.
- DAL/DAO không bắt lỗi rồi bỏ qua: ngoại lệ tự truyền lên tầng gọi.
- Các hàm ghi ở SinhVienBLL và LopHocBLL trả void khi thành công. Lỗi nghiệp vụ
  (dữ liệu sai, mã trùng, không tìm thấy bản ghi, lớp còn sinh viên) ném ValidationException.
- Lỗi MySQL/cấu hình không bị BLL đổi thành false/out string; giữ nguyên ngoại lệ để View bắt.
- View dùng ThucHienAnToan: catch ValidationException để báo nội dung nghiệp vụ,
  catch Exception để chuyển lỗi kỹ thuật thành thông báo an toàn qua DataHelper.LayThongBaoLoi.
- Các hàm kiểm tra dữ liệu KiemTra/LayLoiValidation vẫn trả kết quả kiểm tra để ErrorProvider
  đánh dấu từng ô nhập. Khi gọi trực tiếp hàm ghi BLL, dữ liệu sai vẫn bị ném ValidationException.
- DAO/DAL lớp học trả số dòng; BLL kiểm tra kết quả để tránh báo thành công nếu bản ghi vừa bị xóa.

Chữ ký mới: AddSinhVien(sv), UpdateSinhVien(sv), DeleteSinhVien(maSV),
LopHocBLL.Them(lop), Sua(lop), Xoa(maLop); không truyền out string loi vào các hàm ghi nữa.
Luồng ngoại lệ: DataHelper/DAO → DAL → BLL → catch tại View.

Đã qua 59 kiểm tra MySQL/SQLi, bao gồm ngoại lệ cấu hình qua cả hai DAL,
ngoại lệ MySQL qua mọi hàm ghi của hai BLL, validation/mã trùng/không tìm thấy,
CRUD, hai cách đọc, giải phóng kết nối và các luồng form. Build: 0 lỗi, 0 cảnh báo.

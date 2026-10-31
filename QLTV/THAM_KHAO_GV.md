# Đối chiếu code giáo viên và dự án QLTV

Đã đọc 10 file Entity, Business và Reposistory được cung cấp ngày 31.10.2026.
Giữ cấu trúc hiện có của bài: WinForms → BLL → DAL → DAO → DbContext → MySQL.

| Code tham khảo | Phần tương ứng trong dự án |
|---|---|
| Category, Book, BookCopy | Entities/Category.cs, Book.cs, BookCopy.cs |
| ManageBookBuss | BLL/BookBLL.cs |
| BookCopyBuss, CategoryBuss | BLL/BookCopyBLL.cs, CategoryBLL.cs |
| BookRepository, BookCopyRepository, CategoryRepository | DAO/BookDAO.cs, BookCopyDAO.cs, CategoryDAO.cs, qua các lớp DAL |
| DBReposistory | DatabaseTransaction.cs |

## Những phần đã áp dụng

- CRUD, tìm mã, tìm sách theo tên/tác giả và lọc thể loại.
- Tạo bản sao cùng đầu sách mới; bổ sung bản sao cho sách đã có.
- Đồng bộ TotalCopies theo số BookCopy thực tế, chặn xóa đầu sách/thể loại còn dữ liệu con.
- IsInValid() trên ba entity trả danh sách ValidationResult; danh sách rỗng nghĩa là hợp lệ.
- CategoryBLL.LayTheoTen() tìm tên thể loại qua truy vấn có tham số tại database.
- Business kiểm tra đối tượng rỗng trước khi đọc thuộc tính.
- BookBLL.ImportBill() nhập nhiều dòng trong một transaction dùng chung DbContext.

## Quy ước ImportBill

Mỗi Book đầu vào biểu diễn một dòng phiếu:

- BookId = 0: tạo đầu sách mới. Cần đủ thông tin hợp lệ và CategoryId đã tồn tại.
- BookId > 0: bổ sung cho sách đã có. Chỉ cần mã và TotalCopies; không ghi đè tên/tác giả/giá.
- TotalCopies trong dòng phiếu là số lượng nhập thêm, từ 1 đến 100000.
- Mỗi đầu sách tối đa 100000 bản sao; tổng được đếm lại từ BookCopy.
- Mọi dòng thành công mới commit; một dòng lỗi thì rollback toàn phiếu và ném ngoại lệ.
- Mã đầu sách mới chỉ được gán vào đầu vào sau khi toàn phiếu commit thành công.

Hàm này hiện là API Business, chưa thêm màn hình nhập phiếu hoặc đọc Excel.
Các nút nhập sách và nhập bổ sung hiện tại vẫn dùng được như trước.

## Điều chỉnh cho phù hợp dự án

1. Giữ mã int tự tăng của MySQL và trường TotalCopies, tương ứng NumberOfCopies trong mẫu.
   Không đổi khóa chính sang chuỗi hoặc sinh mã bằng cách sắp xếp chuỗi rồi lấy phần tử cuối.
2. Transaction và mọi lệnh ghi của một phiếu dùng cùng DbContext. Một transaction mở trên
   DbContext riêng không bao trùm các Repository tự tạo DbContext khác.
3. Catch phải ném lại lỗi sau rollback để View biết thao tác thất bại; dùng throw để giữ nguyên lỗi gốc.
4. Khi tạo bản sao, điền cả Status và Condition; mặc định Có sẵn / Mới.
5. IsInValid trả danh sách rỗng thay vì null; có thể kiểm tra bằng Count > 0.
6. Giữ giới hạn và quy tắc validation hiện tại để tương thích dữ liệu/database đang dùng.
   Không chép các kiểm tra đảo điều kiện tồn tại hoặc các lời gọi Add bị lặp trong bản tham khảo.
7. Tiếp tục dùng LINQ/EF có tham số và cấu hình MySQL riêng được Git bỏ qua.

Không sao chép cấu hình kết nối hoặc mật khẩu vào repo. Không thay đổi các file giáo viên gốc.

# Quản lý sinh viên

Mở `Quanlysinhvien.slnx` trong Visual Studio và nhấn F5 để chạy.
Nếu Visual Studio hỏi nạp lại file đã thay đổi bên ngoài, chọn Reload.

## Các file chính

- `Data/Entity/LopHoc.cs`: thông tin lớp, danh sách sinh viên của lớp và validation.
- `Data/Entity/SinhVien.cs`: thông tin sinh viên, Data Annotations và phương thức `KiemTraHopLe`.
- `Data/DAO/LopHocDAO.cs`: giữ danh sách lớp và thao tác trực tiếp với dữ liệu lớp.
- `Data/DAL/LopHocDAL.cs`: cung cấp các hàm truy cập dữ liệu lớp cho BLL.
- `BLL/LopHocBLL.cs`: validation lớp, kiểm tra mã lớp trùng và chặn xóa lớp còn sinh viên.
- `Data/DAL/SinhVienDAL.cs`: cung cấp các hàm truy cập dữ liệu và chuyển yêu cầu từ BLL sang DAO.
- `Data/DAO/SinhVienDAO.cs`: chứa dữ liệu mẫu, lấy danh sách và trực tiếp thực hiện thêm/sửa/xóa trong bộ nhớ.
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
Các hàm ghi trả về bool và thông báo lỗi qua `out string loi`, giống SinhVienBLL.

Form tạo một LopHocBLL, dùng nó cho ComboBox và truyền nó vào SinhVienBLL.
Nhờ vậy, hai phần dùng chung đối tượng lớp; sinh viên luôn tham chiếu đúng lớp đang có trong danh sách.
Constructor LopHocDAO tạo ba lớp mẫu CSE0001, CSE0002, CSE0003 với tên Khoa học máy tính 01, 02, 03.
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
2. Nhập `SV001`: hiện sinh viên mẫu; Thêm tắt, Sửa/Xóa bật.
3. Nhập mã mới, ví dụ `SV002`: các ô còn lại được xóa để nhập sinh viên mới.
4. Điền thông tin, chọn dấu kiểm bên cạnh ngày sinh, chọn giới tính, lớp và trạng thái rồi bấm Thêm.
5. Bấm một dòng trên bảng để đưa dữ liệu lên các ô nhập. Thay đổi thông tin rồi bấm Sửa.
6. Sửa và Xóa có hộp thoại xác nhận. Chọn No để hủy.
7. Làm mới xóa thông tin nhập, bật Thêm, tắt Sửa/Xóa và đưa con trỏ về mã sinh viên.
8. Tìm kiếm lọc đồng thời theo từ khóa, lớp và điểm tối thiểu. Hiển thị tất cả xóa bộ lọc.

## Quy tắc dữ liệu

- Mã và họ tên bắt buộc, có giới hạn độ dài.
- Email phải hợp lệ; số điện thoại có 10 chữ số, bắt đầu bằng 0.
- Ngày sinh bắt buộc và không nằm trong tương lai.
- Giới tính phải là Nam hoặc Nữ; phải chọn lớp và trạng thái.
- Điểm từ 0 đến 10; khi đọc từ form, làm tròn một chữ số thập phân.
- Mỗi lớp có nhiều sinh viên, mỗi sinh viên thuộc một lớp.

## Phạm vi

Dữ liệu mẫu gồm ba lớp và sinh viên SV001. Dữ liệu lưu trong bộ nhớ và mất khi đóng ứng dụng.
Nút Đóng và dấu X đều hỏi xác nhận. Bài tập dùng xác nhận Yes/No cho thao tác nguy hiểm, chưa có đăng nhập.
Khi mã đã tồn tại, tắt nút Thêm nhưng vẫn cho sửa các ô thông tin để dùng nút Sửa.

# Quản lý sinh viên

Mở `Quanlysinhvien.slnx` trong Visual Studio và nhấn F5 để chạy.
Nếu Visual Studio hỏi nạp lại file đã thay đổi bên ngoài, chọn Reload.

## Các file chính

- `LopHoc.cs`: thông tin lớp, danh sách sinh viên của lớp và validation.
- `SinhVien.cs`: thông tin sinh viên, Data Annotations và phương thức `KiemTraHopLe`.
- `Form1.Designer.cs`: giao diện WinForms, dùng màu mặc định.
- `Form1.cs`: nạp dữ liệu, bật/tắt nút, thêm/sửa/xóa, làm mới và tìm kiếm.

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

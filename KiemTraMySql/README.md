# Kiểm tra MySQL bằng console

Trong PowerShell chạy từ thư mục repo:

```powershell
dotnet run --project .\KiemTraMySql\KiemTraMySql.csproj
```

Chương trình dùng DataHelper và mysql.local.json của project Quanlysinhvien.
Chỉ chạy SELECT DATABASE(), VERSION() và SHOW TABLES; không mở form hay ghi dữ liệu.
Mật khẩu không được in ra màn hình. Các bản sao cấu hình trong bin được Git bỏ qua.

Kết quả thành công: `OK: kết nối và hai bảng đã sẵn sàng.`

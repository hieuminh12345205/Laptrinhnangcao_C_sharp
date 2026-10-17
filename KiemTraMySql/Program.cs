using MySql.Data.MySqlClient;
using Quanlysinhvien.Data;

// Chạy riêng từ PowerShell, không mở hoặc thay đổi giao diện WinForms.
// Dùng DataHelper và cùng mysql.local.json của project quản lý sinh viên.
try
{
    var helper = new DataHelper();
    Console.WriteLine(helper.KiemTraKetNoi());

    // Chỉ đọc danh sách bảng, không thêm/sửa/xóa dữ liệu.
    var bang = helper.ExecuteQuery("SHOW TABLES;");
    var tenBang = bang.Rows.Cast<System.Data.DataRow>()
        .Select(dong => dong[0].ToString()!).ToList();
    Console.WriteLine("Các bảng: " + string.Join(", ", tenBang));
    if (!tenBang.Contains("lophoc") || !tenBang.Contains("sinhvien"))
    {
        Console.WriteLine("Chưa đủ hai bảng lophoc và sinhvien. Hãy chạy các file Data/Sql.");
        return 2;
    }
    Console.WriteLine("OK: kết nối và hai bảng đã sẵn sàng.");
    return 0;
}
catch (MySqlException ex)
{
    // Không in chuỗi kết nối hoặc mật khẩu ra màn hình.
    Console.Error.WriteLine(ex.Number switch
    {
        1045 => "Sai tài khoản hoặc mật khẩu. Kiểm tra User và Password trong mysql.local.json.",
        1049 => "Không tìm thấy database. Kiểm tra Database trong mysql.local.json.",
        _ => "Không kết nối được MySQL. Kiểm tra dịch vụ MySQL84, Server và Port. Mã lỗi: " + ex.Number
    });
    return 1;
}
catch (InvalidOperationException ex)
{
    Console.Error.WriteLine(ex.Message);
    return 1;
}
catch (Exception)
{
    Console.Error.WriteLine("Không đọc được cấu hình hoặc không thực hiện được kiểm tra MySQL.");
    return 1;
}

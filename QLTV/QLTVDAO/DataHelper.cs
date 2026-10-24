using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace QLTVDAO;

public static class DataHelper
{
    // Dùng cấu hình riêng của QLTV; không chứa mật khẩu trong C# hay migration.
    public static string LayChuoiKetNoi(string? thuMuc = null)
    {
        string file = Path.Combine(thuMuc ?? AppContext.BaseDirectory, "mysql.local.json");
        if (!File.Exists(file))
            throw new InvalidOperationException("Chưa có mysql.local.json. Sao chép file mẫu và điền cấu hình QLTV.");
        ThongTinKetNoi config;
        try
        {
            config = JsonSerializer.Deserialize<ThongTinKetNoi>(File.ReadAllText(file))
                ?? throw new InvalidOperationException("Cấu hình MySQL trống.");
        }
        catch (JsonException)
        {
            throw new InvalidOperationException("mysql.local.json không đúng định dạng JSON.");
        }
        if (string.IsNullOrWhiteSpace(config.Server) || string.IsNullOrWhiteSpace(config.Database)
            || string.IsNullOrWhiteSpace(config.User) || string.IsNullOrEmpty(config.Password)
            || config.Port is 0 or > 65535)
            throw new InvalidOperationException("Hãy kiểm tra Server, Port, Database, User và Password trong mysql.local.json.");
        return new MySqlConnectionStringBuilder
        {
            Server = config.Server, Port = config.Port, Database = config.Database,
            UserID = config.User, Password = config.Password, ConnectionTimeout = 5
        }.ConnectionString;
    }

    // Nơi gọi (View/Console) dùng hàm này; không hiển thị lỗi kỹ thuật gốc có thể chứa bí mật.
    public static string LayThongBaoLoi(Exception ex)
    {
        if (ex is DbUpdateException && ex.InnerException != null) ex = ex.InnerException;
        return ex switch
        {
            System.ComponentModel.DataAnnotations.ValidationException => ex.Message,
            MySqlException { Number: 1451 } => "Không thể xóa vì dữ liệu đang được bản ghi khác sử dụng.",
            MySqlException { Number: 1452 } => "Danh mục hoặc đầu sách được chọn không còn tồn tại.",
            MySqlException { Number: 1062 } => "Dữ liệu đã tồn tại.",
            MySqlException { Number: 1045 } => "Sai tài khoản/mật khẩu hoặc chưa được cấp quyền MySQL.",
            MySqlException => "Không thực hiện được thao tác MySQL. Hãy kiểm tra kết nối và database QLTV.",
            InvalidOperationException or IOException or UnauthorizedAccessException => "Kiểm tra mysql.local.json và quyền truy cập file.",
            _ => "Thao tác không thành công. Hãy kiểm tra lại dữ liệu."
        };
    }

    private class ThongTinKetNoi
    {
        public ThongTinKetNoi() { }
        public string Server { get; set; } = "127.0.0.1";
        public uint Port { get; set; } = 3306;
        public string Database { get; set; } = "quanlythuvien";
        public string User { get; set; } = "root";
        public string Password { get; set; } = "";
    }
}

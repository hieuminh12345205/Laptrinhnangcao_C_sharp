using System.Data;
using System.Text.Json;
using MySql.Data.MySqlClient;

namespace Quanlysinhvien.Data;

// Dùng chung cho DAO sinh viên và lớp học khi chuyển sang MySQL.
// Chuỗi kết nối được truyền từ bên ngoài, không ghi mật khẩu vào mã nguồn.
public class DataHelper
{
    private readonly string chuoiKetNoi;

    // DAO có thể dùng constructor này để tự đọc cấu hình của máy.
    public DataHelper() : this(LayChuoiKetNoi()) { }

    public DataHelper(string chuoiKetNoi)
    {
        this.chuoiKetNoi = chuoiKetNoi;
    }

    // Thông báo ngắn, không lộ câu SQL, chuỗi kết nối hoặc nội dung nhập.
    public static string LayThongBaoLoi(MySqlException ex) => ex.Number switch
    {
        0 => "Kết nối hoặc truy vấn MySQL bị lỗi/hết thời gian chờ. Hãy kiểm tra dữ liệu đã lưu trước khi thử lại.",
        1042 or 2002 or 2003 => "Không kết nối được MySQL. Kiểm tra dịch vụ MySQL84, địa chỉ và cổng kết nối.",
        2006 or 2013 => "Kết nối MySQL bị gián đoạn. Hãy kiểm tra dữ liệu đã lưu trước khi thực hiện lại.",
        1205 or 1213 => "Dữ liệu đang được thao tác bởi kết nối khác. Hãy tải lại rồi thử lại.",
        1062 => "Mã đã tồn tại trong database.",
        1452 => "Lớp học không còn tồn tại. Hãy tải lại danh sách lớp.",
        1451 => "Không thể xóa lớp đang có sinh viên.",
        1045 => "Sai tài khoản/mật khẩu hoặc tài khoản chưa được cấp quyền.",
        1049 or 1146 => "Chưa có database hoặc bảng. Hãy chạy script tạo bảng.",
        3819 or 1406 or 1264 or 1292 => "Dữ liệu không thỏa điều kiện hoặc độ dài của bảng.",
        _ => "Không thực hiện được thao tác MySQL. Kiểm tra kết nối và quyền truy cập. Mã lỗi: " + ex.Number
    };

    // Chỉ chuyển loại lỗi thành thông báo an toàn; không hiển thị ex.Message.
    public static string LayThongBaoLoi(Exception ex) => ex switch
    {
        MySqlException mysql => LayThongBaoLoi(mysql),
        InvalidDataException or FormatException or InvalidCastException or OverflowException or ArgumentOutOfRangeException
            => "Dữ liệu đọc được không phù hợp. Hãy kiểm tra ngày sinh, điểm và các cột trong database.",
        IOException => "Không đọc được file cấu hình. Kiểm tra mysql.local.json rồi chạy lại chương trình.",
        UnauthorizedAccessException => "Không có quyền đọc file mysql.local.json. Hãy kiểm tra quyền truy cập file.",
        InvalidOperationException => "Kiểm tra nội dung mysql.local.json theo mysql.example.json, lưu và build lại chương trình.",
        _ => "Có lỗi khi thực hiện thao tác. Hãy kiểm tra dữ liệu đã lưu trước khi thử lại."
    };

    // Chỉ đọc để kiểm tra đăng nhập và database, không thay đổi dữ liệu.
    public string KiemTraKetNoi()
    {
        DataTable bang = ExecuteQuery("SELECT DATABASE() AS TenDatabase, VERSION() AS PhienBan;");
        return $"Kết nối thành công!\nDatabase: {bang.Rows[0]["TenDatabase"]}\nMySQL: {bang.Rows[0]["PhienBan"]}";
    }

    // INSERT, UPDATE, DELETE: trả về số dòng bị ảnh hưởng.
    // Dùng tham số @TenThamSo thay vì ghép trực tiếp dữ liệu người dùng vào SQL.
    public int ExecuteNonQuery(string sql, params MySqlParameter[] thamSo)
    {
        using var ketNoi = new MySqlConnection(chuoiKetNoi);
        using var lenh = new MySqlCommand(sql, ketNoi);
        lenh.Parameters.AddRange(thamSo);
        ketNoi.Open();
        return lenh.ExecuteNonQuery();
    }

    // Hướng kết nối: giữ kết nối mở để DAO đọc lần lượt bằng reader.Read().
    // Nơi gọi phải dùng using cho reader; đóng reader sẽ tự đóng kết nối.
    public MySqlDataReader ExecuteReader(string sqlSelect, params MySqlParameter[] thamSo)
    {
        var ketNoi = new MySqlConnection(chuoiKetNoi);
        try
        {
            using var lenh = new MySqlCommand(sqlSelect, ketNoi);
            lenh.Parameters.AddRange(thamSo); // Vẫn dùng tham số để chống SQL injection.
            ketNoi.Open();
            return lenh.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch
        {
            // Nếu chưa trả được reader thì phải tự giải phóng kết nối.
            ketNoi.Dispose();
            throw; // Để BLL/form xử lý lỗi, không trả dữ liệu rỗng giả thành công.
        }
    }

    // SELECT: trả kết quả thành DataTable để DAO đọc và chuyển thành đối tượng.
    public DataTable ExecuteQuery(string sql, params MySqlParameter[] thamSo)
    {
        using var ketNoi = new MySqlConnection(chuoiKetNoi);
        using var lenh = new MySqlCommand(sql, ketNoi);
        lenh.Parameters.AddRange(thamSo);
        using var adapter = new MySqlDataAdapter(lenh);
        var bang = new DataTable();
        ketNoi.Open();
        adapter.Fill(bang);
        return bang;
        // using tự đóng kết nối, kể cả khi câu SQL gặp lỗi.
    }

    // Dùng chung cấu hình riêng của máy cho ADO.NET và EF Core.
    public static string LayChuoiKetNoi(string? thuMucCauHinh = null)
    {
        string duongDan = Path.Combine(thuMucCauHinh ?? AppContext.BaseDirectory, "mysql.local.json");
        if (!File.Exists(duongDan))
            throw new InvalidOperationException("Chưa có mysql.local.json. Hãy sao chép mysql.example.json thành mysql.local.json và nhập thông tin kết nối.");

        ThongTinKetNoi cauHinh;
        try
        {
            cauHinh = JsonSerializer.Deserialize<ThongTinKetNoi>(File.ReadAllText(duongDan))
                ?? throw new InvalidOperationException("File mysql.local.json chưa có cấu hình.");
        }
        catch (JsonException)
        {
            throw new InvalidOperationException("File mysql.local.json không đúng định dạng JSON. Hãy kiểm tra theo mysql.example.json.");
        }

        if (string.IsNullOrWhiteSpace(cauHinh.Server) || string.IsNullOrWhiteSpace(cauHinh.User)
            || string.IsNullOrWhiteSpace(cauHinh.Database) || cauHinh.Port is 0 or > 65535)
            throw new InvalidOperationException("Hãy điền Server, User, Database và Port từ 1 đến 65535 trong mysql.local.json.");
        if (string.IsNullOrEmpty(cauHinh.Password))
            throw new InvalidOperationException("Hãy điền mật khẩu MySQL của bạn vào Password trong mysql.local.json rồi build lại project.");

        // Dùng builder để mật khẩu có dấu chấm phẩy hoặc ký tự đặc biệt vẫn hợp lệ.
        return new MySqlConnectionStringBuilder
        {
            Server = cauHinh.Server,
            Port = cauHinh.Port,
            Database = cauHinh.Database,
            UserID = cauHinh.User,
            Password = cauHinh.Password,
            // UPDATE không đổi nội dung vẫn báo tìm thấy một dòng.
            UseAffectedRows = false,
            ConnectionTimeout = 5
        }.ConnectionString;
    }

    // Mẫu dữ liệu để đọc JSON; đặt trong DataHelper để chỉ cần một file C#.
    private class ThongTinKetNoi
    {
        public ThongTinKetNoi() { }
        public string Server { get; set; } = "127.0.0.1";
        public uint Port { get; set; } = 3306;
        public string Database { get; set; } = "quanlysinhvien";
        public string User { get; set; } = "root";
        public string Password { get; set; } = "";
    }
}

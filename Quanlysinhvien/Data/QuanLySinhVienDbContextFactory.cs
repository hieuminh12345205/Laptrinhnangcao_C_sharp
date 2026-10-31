using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MySql.EntityFrameworkCore.Extensions;

namespace Quanlysinhvien.Data;

// Cho công cụ Migration tạo DbContext mà không khởi chạy giao diện WinForms.
public class QuanLySinhVienDbContextFactory : IDesignTimeDbContextFactory<QuanLySinhVienDbContext>
{
    public QuanLySinhVienDbContext CreateDbContext(string[] args)
    {
        // Đọc bản cấu hình cạnh DLL đã build, không phụ thuộc thư mục đang mở terminal.
        string thuMuc = Path.GetDirectoryName(typeof(QuanLySinhVienDbContext).Assembly.Location)!;
        var options = new DbContextOptionsBuilder<QuanLySinhVienDbContext>();
        options.UseMySQL(DataHelper.LayChuoiKetNoi(thuMuc));
        return new QuanLySinhVienDbContext(options.Options);
    }
}

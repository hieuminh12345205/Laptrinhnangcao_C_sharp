using QLTVDAO.BLL;
using QLTVDAO.Entities;

namespace QLTVDAO;

// Chỉ gọi khi muốn tạo dữ liệu để học/thử nghiệm, không tự chạy mỗi lần mở form.
// Các thông tin sách dưới đây là dữ liệu minh họa, không phải thông tin xuất bản thực tế.
public static class DuLieuMau
{
    public static int ThemNeuChuaCo(Func<BookDbContext>? taoContext = null)
    {
        var categoryBLL = new CategoryBLL(taoContext);
        var bookBLL = new BookBLL(taoContext);
        var categories = categoryBLL.LayDanhSach();
        var books = bookBLL.LayDanhSach();
        int soSachDaThem = 0;

        var mau = new[]
        {
            (Ten: "C# và lập trình hướng đối tượng", TacGia: "Nguyễn Minh An", TheLoai: "Công nghệ thông tin", Gia: 125000m, Nam: 2025, SoBan: 5),
            (Ten: "Cơ sở dữ liệu MySQL", TacGia: "Trần Hoàng Nam", TheLoai: "Công nghệ thông tin", Gia: 110000m, Nam: 2024, SoBan: 4),
            (Ten: "Nhập môn Entity Framework Core", TacGia: "Lê Thu Hà", TheLoai: "Công nghệ thông tin", Gia: 135000m, Nam: 2026, SoBan: 3),
            (Ten: "Truyện ngắn tuổi học trò", TacGia: "Phạm Ngọc Mai", TheLoai: "Văn học", Gia: 75000m, Nam: 2022, SoBan: 4),
            (Ten: "Lịch sử Việt Nam qua các thời kỳ", TacGia: "Đặng Quốc Huy", TheLoai: "Lịch sử", Gia: 145000m, Nam: 2023, SoBan: 2),
            (Ten: "Kỹ năng quản lý thời gian", TacGia: "Vũ Minh Đức", TheLoai: "Kỹ năng sống", Gia: 89000m, Nam: 2024, SoBan: 3),
            (Ten: "Toán ứng dụng cơ bản", TacGia: "Phan Văn Bình", TheLoai: "Khoa học", Gia: 120000m, Nam: 2021, SoBan: 3),
            (Ten: "Tiếng Anh giao tiếp hằng ngày", TacGia: "Lê Thị Thanh", TheLoai: "Ngoại ngữ", Gia: 99000m, Nam: 2025, SoBan: 4)
        };

        foreach (var item in mau)
        {
            // Chạy lại không thêm trùng và không ghi đè dữ liệu người dùng đã sửa.
            if (books.Any(x => string.Equals(x.Title, item.Ten, StringComparison.OrdinalIgnoreCase)
                && string.Equals(x.Author, item.TacGia, StringComparison.OrdinalIgnoreCase))) continue;

            var category = categories.FirstOrDefault(x =>
                string.Equals(x.Description, item.TheLoai, StringComparison.OrdinalIgnoreCase));
            if (category == null)
            {
                category = new Category { Description = item.TheLoai };
                categoryBLL.Them(category);
                categories.Add(category);
            }

            var book = new Book
            {
                Title = item.Ten, Author = item.TacGia, Publisher = "NXB Minh họa",
                Price = item.Gia, PublicationYear = item.Nam, CategoryId = category.CategoryId,
                TotalCopies = item.SoBan
            };
            bookBLL.Them(book); // Tự tạo luôn các bản sao Có sẵn / Mới.
            books.Add(book);
            soSachDaThem++;
        }
        // Không bắt lỗi ở đây: lỗi được truyền lên View/Console để hiển thị an toàn.
        return soSachDaThem;
    }
}

using System.ComponentModel.DataAnnotations;
using QLTVDAO.DAL;
using QLTVDAO.Entities;

namespace QLTVDAO.BLL;

public class BookBLL
{
    private readonly BookDAL dal;
    private readonly CategoryDAL categoryDAL;
    public BookBLL(Func<BookDbContext>? taoContext = null)
    {
        dal = new BookDAL(taoContext);
        categoryDAL = new CategoryDAL(taoContext);
    }
    public List<Book> LayDanhSach(string tuKhoa = "", int? categoryId = null)
        => dal.LayDanhSach((tuKhoa ?? "").Trim(), categoryId);
    public Book? TimTheoMa(int id) => dal.TimTheoMa(id);
    public Book? TimTheoMaSach(string maSach) => dal.TimTheoMaSach((maSach ?? "").Trim().ToUpperInvariant());
    public void Them(Book book)
    {
        KiemTra(book);
        if (book.BookId != 0) throw new ValidationException("Đầu sách này đã được lưu, hãy dùng chức năng sửa.");
        dal.Them(book);
    }
    public void Sua(Book book)
    {
        KiemTra(book);
        dal.Sua(book);
    }
    public void Xoa(int id)
    {
        var book = dal.TimTheoMa(id) ?? throw new ValidationException("Không tìm thấy đầu sách cần xóa.");
        if (book.BookCopies.Count > 0) throw new ValidationException("Hãy xóa hết bản sao trước khi xóa đầu sách.");
        dal.Xoa(id);
    }
    private void KiemTra(Book book)
    {
        book.MaSach = (book.MaSach ?? "").Trim().ToUpperInvariant();
        book.Title = (book.Title ?? "").Trim();
        book.Author = (book.Author ?? "").Trim();
        book.Publisher = book.Publisher?.Trim();
        KiemTraDuLieu.KiemTra(book);
        // Chặn trùng mã ở nghiệp vụ để báo lỗi rõ ràng; unique index vẫn là tuyến bảo vệ cuối.
        if (dal.MaSachDaDung(book.MaSach, book.BookId))
            throw new ValidationException($"Mã sách {book.MaSach} đã được dùng cho đầu sách khác.");
        if (categoryDAL.TimTheoMa(book.CategoryId) == null)
            throw new ValidationException("Thể loại không tồn tại.");
    }
}

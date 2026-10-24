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
    public void Them(Book book)
    {
        KiemTra(book);
        if (book.BookId != 0) throw new ValidationException("Mã đầu sách mới được MySQL tự tạo.");
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
        if (book.BookCopies.Count > 0) throw new ValidationException("Hãy xử lý các bản sách trước khi xóa đầu sách.");
        dal.Xoa(id);
    }
    private void KiemTra(Book book)
    {
        book.Title = (book.Title ?? "").Trim();
        book.Author = book.Author?.Trim();
        book.Publisher = book.Publisher?.Trim();
        KiemTraDuLieu.KiemTra(book);
        if (categoryDAL.TimTheoMa(book.CategoryId) == null)
            throw new ValidationException("Danh mục không tồn tại.");
    }
}

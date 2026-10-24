using System.ComponentModel.DataAnnotations;
using QLTVDAO.DAL;
using QLTVDAO.Entities;

namespace QLTVDAO.BLL;

public class BookCopyBLL
{
    private readonly BookCopyDAL dal;
    private readonly BookDAL bookDAL;
    public BookCopyBLL(Func<BookDbContext>? taoContext = null)
    {
        dal = new BookCopyDAL(taoContext);
        bookDAL = new BookDAL(taoContext);
    }
    public List<BookCopy> LayDanhSach(int? bookId = null) => dal.LayDanhSach(bookId);
    public BookCopy? TimTheoMa(int id) => dal.TimTheoMa(id);
    public void Them(BookCopy copy)
    {
        KiemTra(copy);
        if (copy.BookCopyId != 0) throw new ValidationException("Mã bản sách mới được MySQL tự tạo.");
        dal.Them(copy);
    }
    public void Sua(BookCopy copy)
    {
        KiemTra(copy);
        dal.Sua(copy);
    }
    public void Xoa(int id) => dal.Xoa(id);
    private void KiemTra(BookCopy copy)
    {
        copy.Status = (copy.Status ?? "").Trim();
        copy.Condition = (copy.Condition ?? "").Trim();
        KiemTraDuLieu.KiemTra(copy);
        if (bookDAL.TimTheoMa(copy.BookId) == null)
            throw new ValidationException("Đầu sách không tồn tại.");
    }
}

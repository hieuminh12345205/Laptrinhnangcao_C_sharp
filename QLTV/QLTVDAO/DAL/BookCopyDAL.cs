using QLTVDAO.DAO;
using QLTVDAO.Entities;

namespace QLTVDAO.DAL;

public class BookCopyDAL
{
    private readonly BookCopyDAO dao;
    public BookCopyDAL(Func<BookDbContext>? taoContext = null) => dao = new BookCopyDAO(taoContext);
    public List<BookCopy> LayDanhSach(int? bookId = null) => dao.LayDanhSach(bookId);
    public BookCopy? TimTheoMa(int id) => dao.TimTheoMa(id);
    public int DemTheoSach(int bookId) => dao.DemTheoSach(bookId);
    public List<BookCopy> ThemNhieu(int bookId, int soLuong, string status, string condition)
        => dao.ThemNhieu(bookId, soLuong, status, condition);
    public void Sua(BookCopy copy) => dao.Sua(copy);
    public void Xoa(int id) => dao.Xoa(id);
}

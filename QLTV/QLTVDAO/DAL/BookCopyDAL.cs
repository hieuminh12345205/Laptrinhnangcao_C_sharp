using QLTVDAO.DAO;
using QLTVDAO.Entities;

namespace QLTVDAO.DAL;

public class BookCopyDAL
{
    private readonly BookCopyDAO dao;
    public BookCopyDAL(Func<BookDbContext>? taoContext = null) => dao = new BookCopyDAO(taoContext);
    public List<BookCopy> LayDanhSach(int? bookId = null) => dao.LayDanhSach(bookId);
    public BookCopy? TimTheoMa(int id) => dao.TimTheoMa(id);
    public void Them(BookCopy copy) => dao.Them(copy);
    public void Sua(BookCopy copy) => dao.Sua(copy);
    public void Xoa(int id) => dao.Xoa(id);
}

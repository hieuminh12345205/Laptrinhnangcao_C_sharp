using QLTVDAO.DAO;
using QLTVDAO.Entities;

namespace QLTVDAO.DAL;

public class BookDAL
{
    private readonly BookDAO dao;
    public BookDAL(Func<BookDbContext>? taoContext = null) => dao = new BookDAO(taoContext);
    public List<Book> LayDanhSach(string tuKhoa = "", int? categoryId = null) => dao.LayDanhSach(tuKhoa, categoryId);
    public Book? TimTheoMa(int id) => dao.TimTheoMa(id);
    public Book? TimTheoMaSach(string maSach) => dao.TimTheoMaSach(maSach);
    public bool MaSachDaDung(string maSach, int boQuaId = 0) => dao.MaSachDaDung(maSach, boQuaId);
    public void Them(Book book) => dao.Them(book);
    public void Sua(Book book) => dao.Sua(book);
    public void Xoa(int id) => dao.Xoa(id);
}

using System.ComponentModel.DataAnnotations;
using System.Data;
using QLTVDAO.DAO;
using QLTVDAO.Entities;

namespace QLTVDAO.DAL;

public class BookDAL
{
    private readonly BookDAO dao;
    public BookDAL(Func<BookDbContext>? taoContext = null) => dao = new BookDAO(taoContext);
    public List<Book> LayDanhSach(string tuKhoa = "", int? categoryId = null) => dao.LayDanhSach(tuKhoa, categoryId);
    public Book? TimTheoMa(int id) => dao.TimTheoMa(id);

    public void Add(Book book)
    {
        try
        {
            dao.Them(book); // DAO gọi Add và SaveChanges của EF Core.
        }
        catch (ValidationException)
        {
            throw; // Giữ nguyên thông báo nghiệp vụ và vị trí phát sinh lỗi.
        }
        catch (Exception ex)
        {
            // InnerException giữ lỗi gốc để tầng View nhận biết nguyên nhân.
            throw new DataException("Không thể thêm đầu sách.", ex);
        }
    }

    public void Update(Book book)
    {
        try
        {
            dao.Sua(book);
        }
        catch (ValidationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException("Không thể cập nhật đầu sách.", ex);
        }
    }

    public void NhapPhieu(List<Book> books)
    {
        try { dao.NhapPhieu(books); }
        catch (ValidationException) { throw; }
        catch (Exception ex) { throw new DataException("Không thể lưu phiếu nhập sách.", ex); }
    }

    public void Delete(int id)
    {
        try
        {
            dao.Xoa(id);
        }
        catch (ValidationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException("Không thể xóa đầu sách.", ex);
        }
    }

    // Giữ các tên cũ để những đoạn code đã gọi DAL vẫn sử dụng được.
    public void Them(Book book) => Add(book);
    public void Sua(Book book) => Update(book);
    public void Xoa(int id) => Delete(id);
}

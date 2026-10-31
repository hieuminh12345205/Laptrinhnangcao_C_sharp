using System.ComponentModel.DataAnnotations;
using System.Data;
using QLTVDAO.DAO;
using QLTVDAO.Entities;

namespace QLTVDAO.DAL;

public class BookCopyDAL
{
    private readonly BookCopyDAO dao;
    public BookCopyDAL(Func<BookDbContext>? taoContext = null) => dao = new BookCopyDAO(taoContext);
    public List<BookCopy> LayDanhSach(int? bookId = null) => dao.LayDanhSach(bookId);
    public BookCopy? TimTheoMa(int id) => dao.TimTheoMa(id);

    public void Add(BookCopy copy)
    {
        try
        {
            dao.Them(copy); // Thêm một bản sách vật lý.
        }
        catch (ValidationException)
        {
            throw; // Chuyển nguyên lỗi nghiệp vụ lên nơi gọi.
        }
        catch (Exception ex)
        {
            throw new DataException("Không thể thêm bản sao.", ex);
        }
    }

    public void Update(BookCopy copy)
    {
        try
        {
            dao.Sua(copy);
        }
        catch (ValidationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException("Không thể cập nhật bản sao.", ex);
        }
    }

    public void AddNhieu(List<BookCopy> copies)
    {
        try
        {
            dao.ThemNhieu(copies);
        }
        catch (ValidationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException("Không thể nhập bổ sung bản sao.", ex);
        }
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
            throw new DataException("Không thể xóa bản sao.", ex);
        }
    }

    // Giữ các tên cũ để những đoạn code đã gọi DAL vẫn sử dụng được.
    public void Them(BookCopy copy) => Add(copy);
    public void Sua(BookCopy copy) => Update(copy);
    public void Xoa(int id) => Delete(id);
}

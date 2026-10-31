using System.ComponentModel.DataAnnotations;
using System.Data;
using QLTVDAO.DAO;
using QLTVDAO.Entities;

namespace QLTVDAO.DAL;

// DAL thêm ngữ cảnh cho lỗi kỹ thuật rồi ném tiếp lên BLL/View.
public class CategoryDAL
{
    private readonly CategoryDAO dao;
    public CategoryDAL(Func<BookDbContext>? taoContext = null) => dao = new CategoryDAO(taoContext);
    public List<Category> LayDanhSach(string tuKhoa = "") => dao.LayDanhSach(tuKhoa);
    public Category? TimTheoMa(int id) => dao.TimTheoMa(id);
    public bool CoSach(int id) => dao.CoSach(id);

    public void Add(Category category)
    {
        try
        {
            dao.Them(category); // DAO lưu thể loại và nhận mã tự tăng từ MySQL.
        }
        catch (ValidationException)
        {
            throw; // Không đổi lỗi nghiệp vụ thành lỗi truy cập database.
        }
        catch (Exception ex)
        {
            throw new DataException("Không thể thêm thể loại.", ex);
        }
    }

    public void Update(Category category)
    {
        try
        {
            dao.Sua(category);
        }
        catch (ValidationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new DataException("Không thể cập nhật thể loại.", ex);
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
            throw new DataException("Không thể xóa thể loại.", ex);
        }
    }

    // Giữ các tên cũ để những đoạn code đã gọi DAL vẫn sử dụng được.
    public void Them(Category category) => Add(category);
    public void Sua(Category category) => Update(category);
    public void Xoa(int id) => Delete(id);
}

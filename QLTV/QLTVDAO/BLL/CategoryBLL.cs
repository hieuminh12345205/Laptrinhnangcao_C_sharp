using System.ComponentModel.DataAnnotations;
using QLTVDAO.DAL;
using QLTVDAO.Entities;

namespace QLTVDAO.BLL;

public class CategoryBLL
{
    private readonly CategoryDAL dal;
    public CategoryBLL(Func<BookDbContext>? taoContext = null) => dal = new CategoryDAL(taoContext);
    public List<Category> LayDanhSach() => dal.LayDanhSach();
    public Category? TimTheoMa(int id) => dal.TimTheoMa(id);
    public void Them(Category category)
    {
        category.Description = (category.Description ?? "").Trim();
        KiemTraDuLieu.KiemTra(category);
        if (category.CategoryId != 0) throw new ValidationException("Mã danh mục mới được MySQL tự tạo.");
        dal.Them(category);
    }
    public void Sua(Category category)
    {
        category.Description = (category.Description ?? "").Trim();
        KiemTraDuLieu.KiemTra(category);
        dal.Sua(category);
    }
    public void Xoa(int id)
    {
        if (dal.CoSach(id)) throw new ValidationException("Không thể xóa danh mục đang có sách.");
        dal.Xoa(id);
    }
}

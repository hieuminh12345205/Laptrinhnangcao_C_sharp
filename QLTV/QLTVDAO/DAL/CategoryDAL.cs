using QLTVDAO.DAO;
using QLTVDAO.Entities;

namespace QLTVDAO.DAL;

// DAL không nuốt ngoại lệ: truyền nguyên lỗi DAO lên BLL và nơi gọi.
public class CategoryDAL
{
    private readonly CategoryDAO dao;
    public CategoryDAL(Func<BookDbContext>? taoContext = null) => dao = new CategoryDAO(taoContext);
    public List<Category> LayDanhSach() => dao.LayDanhSach();
    public Category? TimTheoMa(int id) => dao.TimTheoMa(id);
    public bool CoSach(int id) => dao.CoSach(id);
    public void Them(Category category) => dao.Them(category);
    public void Sua(Category category) => dao.Sua(category);
    public void Xoa(int id) => dao.Xoa(id);
}

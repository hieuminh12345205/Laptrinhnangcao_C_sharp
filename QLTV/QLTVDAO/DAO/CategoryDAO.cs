using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using QLTVDAO.Entities;

namespace QLTVDAO.DAO;

// Mỗi thao tác có DbContext riêng, using luôn giải phóng kết nối khi có lỗi.
public class CategoryDAO
{
    private readonly Func<BookDbContext> taoContext;
    public CategoryDAO(Func<BookDbContext>? taoContext = null)
        => this.taoContext = taoContext ?? (() => new BookDbContext());

    public List<Category> LayDanhSach(string tuKhoa = "")
    {
        using var db = taoContext();
        var query = db.Categories.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(tuKhoa))
            query = query.Where(x => x.Description.Contains(tuKhoa));
        return query.OrderBy(x => x.CategoryId).ToList();
    }
    public Category? TimTheoMa(int id)
    {
        using var db = taoContext();
        return db.Categories.AsNoTracking().SingleOrDefault(x => x.CategoryId == id);
    }
    public bool CoSach(int id)
    {
        using var db = taoContext();
        return db.Books.Any(x => x.CategoryId == id);
    }
    public void Them(Category category)
    {
        using var db = taoContext();
        var moi = new Category { Description = category.Description };
        DatabaseTransaction.ThucHien(db, () =>
        {
            db.Categories.Add(moi);
            db.SaveChanges();
        });
        category.CategoryId = moi.CategoryId; // Nhận mã tự tăng từ MySQL.
    }
    public void Sua(Category category)
    {
        using var db = taoContext();
        var cu = db.Categories.Find(category.CategoryId)
            ?? throw new ValidationException("Không tìm thấy danh mục cần sửa.");
        DatabaseTransaction.ThucHien(db, () =>
        {
            cu.Description = category.Description;
            db.SaveChanges();
        });
    }
    public void Xoa(int id)
    {
        using var db = taoContext();
        var cu = db.Categories.Find(id)
            ?? throw new ValidationException("Không tìm thấy danh mục cần xóa.");
        DatabaseTransaction.ThucHien(db, () =>
        {
            db.Categories.Remove(cu);
            db.SaveChanges();
        });
    }
}

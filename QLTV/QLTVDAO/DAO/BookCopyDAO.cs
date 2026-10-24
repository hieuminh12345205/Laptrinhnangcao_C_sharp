using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using QLTVDAO.Entities;

namespace QLTVDAO.DAO;

public class BookCopyDAO
{
    private readonly Func<BookDbContext> taoContext;
    public BookCopyDAO(Func<BookDbContext>? taoContext = null)
        => this.taoContext = taoContext ?? (() => new BookDbContext());

    public List<BookCopy> LayDanhSach(int? bookId = null)
    {
        using var db = taoContext();
        var query = db.BookCopies.AsNoTracking().Include(x => x.Book).AsQueryable();
        if (bookId.HasValue) query = query.Where(x => x.BookId == bookId.Value);
        return query.OrderBy(x => x.BookCopyId).ToList();
    }
    public BookCopy? TimTheoMa(int id)
    {
        using var db = taoContext();
        return db.BookCopies.AsNoTracking().Include(x => x.Book).SingleOrDefault(x => x.BookCopyId == id);
    }
    public void Them(BookCopy copy)
    {
        using var db = taoContext();
        var moi = new BookCopy { BookId = copy.BookId, Status = copy.Status, Condition = copy.Condition };
        db.BookCopies.Add(moi);
        db.SaveChanges();
        copy.BookCopyId = moi.BookCopyId;
    }
    public void Sua(BookCopy copy)
    {
        using var db = taoContext();
        var cu = db.BookCopies.Find(copy.BookCopyId)
            ?? throw new ValidationException("Không tìm thấy bản sách cần sửa.");
        cu.BookId = copy.BookId;
        cu.Status = copy.Status;
        cu.Condition = copy.Condition;
        db.SaveChanges();
    }
    public void Xoa(int id)
    {
        using var db = taoContext();
        var cu = db.BookCopies.Find(id)
            ?? throw new ValidationException("Không tìm thấy bản sách cần xóa.");
        db.BookCopies.Remove(cu);
        db.SaveChanges();
    }
}

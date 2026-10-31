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
        return query.OrderBy(x => x.MaBanSao).ToList();
    }
    public BookCopy? TimTheoMa(int id)
    {
        using var db = taoContext();
        return db.BookCopies.AsNoTracking().Include(x => x.Book).SingleOrDefault(x => x.BookCopyId == id);
    }
    public int DemTheoSach(int bookId)
    {
        using var db = taoContext();
        return db.BookCopies.Count(x => x.BookId == bookId);
    }

    // Sinh mã bản sao nối tiếp và lưu trong cùng một SaveChanges.
    public List<BookCopy> ThemNhieu(int bookId, int soLuong, string status, string condition)
    {
        using var db = taoContext();
        var book = db.Books.AsNoTracking().SingleOrDefault(x => x.BookId == bookId)
            ?? throw new ValidationException("Đầu sách không tồn tại.");
        string tienTo = book.MaSach + ".";
        var daCo = db.BookCopies.Where(x => x.BookId == bookId).Select(x => x.MaBanSao).ToList();
        int soLonNhat = 0;
        foreach (string ma in daCo)
            if (ma.StartsWith(tienTo, StringComparison.Ordinal)
                && int.TryParse(ma[tienTo.Length..], out int so) && so > soLonNhat)
                soLonNhat = so;
        var moi = new List<BookCopy>();
        for (int i = 1; i <= soLuong; i++)
            moi.Add(new BookCopy
            {
                BookId = bookId,
                MaBanSao = tienTo + (soLonNhat + i).ToString("000"),
                Status = status,
                Condition = condition
            });
        db.BookCopies.AddRange(moi);
        db.SaveChanges();
        return moi;
    }

    // Chỉ sửa trạng thái và tình trạng; mã bản sao và đầu sách giữ nguyên theo bản ghi đã lưu.
    public void Sua(BookCopy copy)
    {
        using var db = taoContext();
        var cu = db.BookCopies.Find(copy.BookCopyId)
            ?? throw new ValidationException("Không tìm thấy bản sao cần sửa.");
        cu.Status = copy.Status;
        cu.Condition = copy.Condition;
        db.SaveChanges();
    }
    public void Xoa(int id)
    {
        using var db = taoContext();
        var cu = db.BookCopies.Find(id)
            ?? throw new ValidationException("Không tìm thấy bản sao cần xóa.");
        db.BookCopies.Remove(cu);
        db.SaveChanges();
    }
}

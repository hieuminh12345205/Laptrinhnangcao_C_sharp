using System.ComponentModel.DataAnnotations;
using System.Data;
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
        => ThemNhieu(new List<BookCopy> { copy });

    public void ThemNhieu(List<BookCopy> copies)
    {
        if (copies.Count == 0) throw new ValidationException("Hãy nhập ít nhất một bản sao.");
        int bookId = copies[0].BookId;
        if (copies.Any(x => x.BookId != bookId))
            throw new ValidationException("Các bản sao bổ sung phải thuộc cùng một đầu sách.");
        using var db = taoContext();
        // Cùng transaction để bản sao và tổng số bản luôn được lưu/hoàn tác cùng nhau.
        // Serializable tránh hai người đồng thời ghi đè kết quả đếm của nhau.
        // Chỉ sao chép dữ liệu cần lưu, tránh thêm nhầm navigation do nơi gọi truyền vào.
        var moi = copies.Select(copy => new BookCopy
        {
            BookId = bookId, Status = copy.Status, Condition = copy.Condition
        }).ToList();
        DatabaseTransaction.ThucHien(db, () =>
        {
            var book = db.Books.Find(bookId)
                ?? throw new ValidationException("Đầu sách không tồn tại.");
            if (db.BookCopies.Count(x => x.BookId == bookId) + copies.Count > 100000)
                throw new ValidationException("Một đầu sách tối đa 100000 bản sao.");
            db.BookCopies.AddRange(moi);
            db.SaveChanges();
            CapNhatTongSoBan(db, book);
            db.SaveChanges();
        }, IsolationLevel.Serializable);
        // Chỉ trả mã sau khi cả bản sao và tổng số đã được lưu thành công.
        for (int i = 0; i < copies.Count; i++) copies[i].BookCopyId = moi[i].BookCopyId;
    }
    public void Sua(BookCopy copy)
    {
        using var db = taoContext();
        DatabaseTransaction.ThucHien(db, () =>
        {
            var cu = db.BookCopies.Find(copy.BookCopyId)
                ?? throw new ValidationException("Không tìm thấy bản sách cần sửa.");
            var sachCu = db.Books.Find(cu.BookId)
                ?? throw new ValidationException("Đầu sách cũ không tồn tại.");
            var sachMoi = db.Books.Find(copy.BookId)
                ?? throw new ValidationException("Đầu sách mới không tồn tại.");
            cu.BookId = copy.BookId;
            cu.Status = copy.Status;
            cu.Condition = copy.Condition;
            db.SaveChanges();
            // Nếu chuyển cuốn sách sang đầu sách khác, cập nhật cả hai bên.
            CapNhatTongSoBan(db, sachCu);
            if (sachMoi.BookId != sachCu.BookId) CapNhatTongSoBan(db, sachMoi);
            db.SaveChanges();
        }, IsolationLevel.Serializable);
    }
    public void Xoa(int id)
    {
        using var db = taoContext();
        DatabaseTransaction.ThucHien(db, () =>
        {
            var cu = db.BookCopies.Find(id)
                ?? throw new ValidationException("Không tìm thấy bản sách cần xóa.");
            var book = db.Books.Find(cu.BookId)
                ?? throw new ValidationException("Đầu sách không tồn tại.");
            db.BookCopies.Remove(cu);
            db.SaveChanges();
            CapNhatTongSoBan(db, book);
            db.SaveChanges();
        }, IsolationLevel.Serializable);
    }

    private static void CapNhatTongSoBan(BookDbContext db, Book book)
    {
        // Đếm từ database, không cộng với số khai báo có thể đã sai từ dữ liệu cũ.
        book.TotalCopies = db.BookCopies.Count(x => x.BookId == book.BookId);
    }
}

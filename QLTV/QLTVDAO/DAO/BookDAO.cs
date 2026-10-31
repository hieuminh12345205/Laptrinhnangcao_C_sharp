using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using QLTVDAO.Entities;

namespace QLTVDAO.DAO;

public class BookDAO
{
    private readonly Func<BookDbContext> taoContext;
    public BookDAO(Func<BookDbContext>? taoContext = null)
        => this.taoContext = taoContext ?? (() => new BookDbContext());

    public List<Book> LayDanhSach(string tuKhoa = "", int? categoryId = null)
    {
        using var db = taoContext();
        var query = db.Books.AsNoTrackingWithIdentityResolution()
            .Include(x => x.Category).Include(x => x.BookCopies).AsQueryable();
        if (!string.IsNullOrWhiteSpace(tuKhoa))
            query = query.Where(x => x.MaSach.Contains(tuKhoa) || x.Title.Contains(tuKhoa) || x.Author.Contains(tuKhoa));
        if (categoryId.HasValue) query = query.Where(x => x.CategoryId == categoryId.Value);
        // EF chuyển các giá trị LINQ thành tham số; không ghép chuỗi SQL.
        return query.OrderBy(x => x.MaSach).ToList();
    }
    public Book? TimTheoMa(int id)
    {
        using var db = taoContext();
        return db.Books.AsNoTrackingWithIdentityResolution().Include(x => x.Category)
            .Include(x => x.BookCopies).SingleOrDefault(x => x.BookId == id);
    }
    public Book? TimTheoMaSach(string maSach)
    {
        using var db = taoContext();
        return db.Books.AsNoTrackingWithIdentityResolution().Include(x => x.Category)
            .Include(x => x.BookCopies).SingleOrDefault(x => x.MaSach == maSach);
    }
    // boQuaId: bản ghi đang sửa, không tính là trùng với chính nó.
    public bool MaSachDaDung(string maSach, int boQuaId = 0)
    {
        using var db = taoContext();
        return db.Books.Any(x => x.MaSach == maSach && x.BookId != boQuaId);
    }
    public void Them(Book book)
    {
        using var db = taoContext();
        var moi = new Book();
        GanThuocTinh(moi, book); // Chỉ lưu thuộc tính, không vô tình thêm cả graph navigation.
        db.Books.Add(moi);
        db.SaveChanges();
        book.BookId = moi.BookId;
    }
    public void Sua(Book book)
    {
        using var db = taoContext();
        var cu = db.Books.Find(book.BookId)
            ?? throw new ValidationException("Không tìm thấy đầu sách cần sửa.");
        GanThuocTinh(cu, book);
        db.SaveChanges();
    }
    public void Xoa(int id)
    {
        using var db = taoContext();
        var cu = db.Books.Find(id)
            ?? throw new ValidationException("Không tìm thấy đầu sách cần xóa.");
        db.Books.Remove(cu);
        db.SaveChanges();
    }
    private static void GanThuocTinh(Book dich, Book nguon)
    {
        dich.MaSach = nguon.MaSach;
        dich.Title = nguon.Title;
        dich.Author = nguon.Author;
        dich.Publisher = nguon.Publisher;
        dich.Price = nguon.Price;
        dich.PublicationYear = nguon.PublicationYear;
        dich.CategoryId = nguon.CategoryId;
    }
}

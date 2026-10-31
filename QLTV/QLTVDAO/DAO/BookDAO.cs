using System.ComponentModel.DataAnnotations;
using System.Data;
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
        {
            // Mã tự tăng là số; các từ khóa khác tìm trong tên sách và tác giả.
            bool laMaSach = int.TryParse(tuKhoa, out int maSach);
            query = query.Where(x => (laMaSach && x.BookId == maSach)
                || x.Title.Contains(tuKhoa) || (x.Author != null && x.Author.Contains(tuKhoa)));
        }
        if (categoryId.HasValue) query = query.Where(x => x.CategoryId == categoryId.Value);
        // EF chuyển các giá trị LINQ thành tham số; không ghép chuỗi SQL.
        return query.OrderBy(x => x.BookId).ToList();
    }
    public Book? TimTheoMa(int id)
    {
        using var db = taoContext();
        return db.Books.AsNoTrackingWithIdentityResolution().Include(x => x.Category)
            .Include(x => x.BookCopies).SingleOrDefault(x => x.BookId == id);
    }
    public void Them(Book book)
    {
        using var db = taoContext();
        if (book.TotalCopies < 1 || book.TotalCopies > 100000)
            throw new ValidationException("Số bản sao ban đầu phải từ 1 đến 100000.");
        var moi = TaoSachMoi(book);
        DatabaseTransaction.ThucHien(db, () =>
        {
            db.Books.Add(moi);
            db.SaveChanges(); // Đầu sách và bản sao dùng cùng giao dịch.
        });
        book.BookId = moi.BookId;
    }

    private static Book TaoSachMoi(Book book)
    {
        var moi = new Book();
        GanThuocTinh(moi, book); // Không thêm nhầm navigation do nơi gọi truyền vào.
        moi.TotalCopies = book.TotalCopies;

        // Thêm đầu sách mới thì tạo đủ bản sao theo số lượng nhập trên form.
        // Dùng quan hệ navigation: EF tự gán BookId sau khi MySQL cấp mã sách.
        for (int i = 0; i < book.TotalCopies; i++)
        {
            moi.BookCopies.Add(new BookCopy
            {
                Status = "Có sẵn",
                Condition = "Mới"
            });
        }
        return moi;
    }

    public void NhapPhieu(List<Book> books)
    {
        using var db = taoContext();
        var sachMoi = new List<(Book DongNhap, Book DaLuu)>();
        DatabaseTransaction.ThucHien(db, () =>
        {
            // Mọi dòng dùng cùng db và transaction, không gọi DAO tự mở kết nối khác.
            foreach (var dong in books)
            {
                if (dong.TotalCopies < 1 || dong.TotalCopies > 100000)
                    throw new ValidationException("Số lượng mỗi dòng nhập phải từ 1 đến 100000.");
                if (dong.BookId == 0)
                {
                    if (!db.Categories.Any(x => x.CategoryId == dong.CategoryId))
                        throw new ValidationException("Thể loại của đầu sách mới không tồn tại.");
                    var moi = TaoSachMoi(dong);
                    db.Books.Add(moi);
                    db.SaveChanges();
                    sachMoi.Add((dong, moi));
                }
                else
                {
                    var cu = db.Books.Find(dong.BookId)
                        ?? throw new ValidationException("Không tìm thấy đầu sách cần nhập bổ sung.");
                    int soBanCu = db.BookCopies.Count(x => x.BookId == cu.BookId);
                    if (soBanCu + dong.TotalCopies > 100000)
                        throw new ValidationException("Một đầu sách tối đa 100000 bản sao.");
                    for (int i = 0; i < dong.TotalCopies; i++)
                        db.BookCopies.Add(new BookCopy { BookId = cu.BookId, Status = "Có sẵn", Condition = "Mới" });
                    db.SaveChanges();
                    cu.TotalCopies = db.BookCopies.Count(x => x.BookId == cu.BookId);
                    db.SaveChanges();
                }
            }
        }, IsolationLevel.Serializable);

        // Chỉ gán mã cho dữ liệu đầu vào khi TOÀN BỘ phiếu đã commit.
        foreach (var item in sachMoi) item.DongNhap.BookId = item.DaLuu.BookId;
    }
    public void Sua(Book book)
    {
        using var db = taoContext();
        var cu = db.Books.Find(book.BookId)
            ?? throw new ValidationException("Không tìm thấy đầu sách cần sửa.");
        DatabaseTransaction.ThucHien(db, () =>
        {
            GanThuocTinh(cu, book);
            db.SaveChanges();
        });
        book.TotalCopies = cu.TotalCopies; // Sửa thông tin sách không thay đổi số bản thực tế.
    }
    public void Xoa(int id)
    {
        using var db = taoContext();
        var cu = db.Books.Find(id)
            ?? throw new ValidationException("Không tìm thấy đầu sách cần xóa.");
        DatabaseTransaction.ThucHien(db, () =>
        {
            db.Books.Remove(cu);
            db.SaveChanges();
        });
    }
    private static void GanThuocTinh(Book dich, Book nguon)
    {
        dich.Title = nguon.Title;
        dich.Author = nguon.Author;
        dich.Publisher = nguon.Publisher;
        dich.Price = nguon.Price;
        dich.PublicationYear = nguon.PublicationYear;
        dich.CategoryId = nguon.CategoryId;
    }
}

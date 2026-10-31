using System.ComponentModel.DataAnnotations;
using QLTVDAO.DAL;
using QLTVDAO.Entities;

namespace QLTVDAO.BLL;

public class BookBLL
{
    private readonly BookDAL dal;
    private readonly CategoryDAL categoryDAL;
    private readonly BookCopyBLL copyBLL;
    public BookBLL(Func<BookDbContext>? taoContext = null)
    {
        dal = new BookDAL(taoContext);
        categoryDAL = new CategoryDAL(taoContext);
        copyBLL = new BookCopyBLL(taoContext);
    }
    public List<Book> LayDanhSach(string tuKhoa = "", int? categoryId = null)
        => dal.LayDanhSach((tuKhoa ?? "").Trim(), categoryId);
    public Book? TimTheoMa(int id) => dal.TimTheoMa(id);
    public void Them(Book book)
    {
        KiemTra(book);
        if (book.BookId != 0) throw new ValidationException("Mã đầu sách mới được MySQL tự tạo.");
        if (book.TotalCopies < 1) throw new ValidationException("Đầu sách mới phải có ít nhất một bản sao.");
        dal.Add(book);
    }
    public void Sua(Book book)
    {
        KiemTra(book);
        dal.Update(book);
    }

    // Tổng số bản trên mỗi dòng là SỐ LƯỢNG NHẬP THÊM, không phải tổng hiện có.
    // BookId = 0: sách mới; BookId > 0: chỉ bổ sung bản sao, giữ thông tin sách cũ.
    public void ImportBill(List<Book> books)
    {
        if (books == null || books.Count == 0)
            throw new ValidationException("Phiếu nhập phải có ít nhất một đầu sách.");
        foreach (var dong in books)
        {
            if (dong == null) throw new ValidationException("Dòng nhập sách không được rỗng.");
            if (dong.TotalCopies < 1 || dong.TotalCopies > 100000)
                throw new ValidationException("Số lượng mỗi dòng nhập phải từ 1 đến 100000.");
            if (dong.BookId == 0) KiemTra(dong);
            else if (dong.BookId < 0 || dal.TimTheoMa(dong.BookId) == null)
                throw new ValidationException("Không tìm thấy đầu sách cần nhập bổ sung.");
        }
        dal.NhapPhieu(books);
    }

    public bool CoThayDoi(Book book)
    {
        KiemTra(book);
        var cu = dal.TimTheoMa(book.BookId)
            ?? throw new ValidationException("Không tìm thấy đầu sách cần sửa.");
        // Tổng bản sao do DAO tính, không phải thông tin người dùng sửa trực tiếp.
        return cu.Title != book.Title || (cu.Author ?? "") != (book.Author ?? "")
            || (cu.Publisher ?? "") != (book.Publisher ?? "") || cu.Price != book.Price
            || cu.PublicationYear != book.PublicationYear || cu.CategoryId != book.CategoryId;
    }

    public List<BookCopy> NhapBoSungBanSao(int bookId, int soLuong = 1,
        string trangThai = "Có sẵn", string tinhTrang = "Mới")
        => copyBLL.NhapBoSung(bookId, soLuong, trangThai, tinhTrang);
    public void Xoa(int id)
    {
        var book = dal.TimTheoMa(id) ?? throw new ValidationException("Không tìm thấy đầu sách cần xóa.");
        if (book.BookCopies.Count > 0) throw new ValidationException("Hãy xử lý các bản sách trước khi xóa đầu sách.");
        dal.Delete(id);
    }
    private void KiemTra(Book book)
    {
        if (book == null) throw new ValidationException("Thông tin đầu sách không được rỗng.");
        book.Title = (book.Title ?? "").Trim();
        book.Author = book.Author?.Trim();
        book.Publisher = book.Publisher?.Trim();
        KiemTraDuLieu.KiemTra(book);
        if (categoryDAL.TimTheoMa(book.CategoryId) == null)
            throw new ValidationException("Danh mục không tồn tại.");
    }
}

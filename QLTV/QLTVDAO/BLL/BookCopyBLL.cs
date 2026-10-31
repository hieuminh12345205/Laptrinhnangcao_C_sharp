using System.ComponentModel.DataAnnotations;
using QLTVDAO.DAL;
using QLTVDAO.Entities;

namespace QLTVDAO.BLL;

public class BookCopyBLL
{
    private readonly BookCopyDAL dal;
    private readonly BookDAL bookDAL;
    public BookCopyBLL(Func<BookDbContext>? taoContext = null)
    {
        dal = new BookCopyDAL(taoContext);
        bookDAL = new BookDAL(taoContext);
    }
    public List<BookCopy> LayDanhSach(int? bookId = null) => dal.LayDanhSach(bookId);
    public BookCopy? TimTheoMa(int id) => dal.TimTheoMa(id);
    public void Them(BookCopy copy)
    {
        KiemTra(copy);
        if (copy.BookCopyId != 0) throw new ValidationException("Mã bản sách mới được MySQL tự tạo.");
        dal.Add(copy);
    }
    public void Sua(BookCopy copy)
    {
        KiemTra(copy);
        dal.Update(copy);
    }

    // Cả đợt nhập được lưu trong một transaction, không gọi Them lặp nhiều lần.
    public List<BookCopy> NhapBoSung(int bookId, int soLuong, string trangThai = "Có sẵn", string tinhTrang = "Mới")
    {
        if (soLuong < 1 || soLuong > 100000)
            throw new ValidationException("Số lượng bổ sung phải từ 1 đến 100000.");
        var mau = new BookCopy { BookId = bookId, Status = trangThai, Condition = tinhTrang };
        var book = KiemTra(mau); // Kiểm tra đầu sách, trạng thái và tình trạng trước khi ghi.
        if (book.BookCopies.Count + soLuong > 100000)
            throw new ValidationException("Một đầu sách tối đa 100000 bản sao.");

        var copies = new List<BookCopy>();
        for (int i = 0; i < soLuong; i++)
            copies.Add(new BookCopy { BookId = bookId, Status = mau.Status, Condition = mau.Condition });
        dal.AddNhieu(copies);
        return copies;
    }
    public void Xoa(int id) => dal.Delete(id);
    private Book KiemTra(BookCopy copy)
    {
        if (copy == null) throw new ValidationException("Thông tin bản sao không được rỗng.");
        copy.Status = (copy.Status ?? "").Trim();
        copy.Condition = (copy.Condition ?? "").Trim();
        KiemTraDuLieu.KiemTra(copy);
        return bookDAL.TimTheoMa(copy.BookId)
            ?? throw new ValidationException("Đầu sách không tồn tại.");
    }
}

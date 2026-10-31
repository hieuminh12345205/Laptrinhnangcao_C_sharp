using System.ComponentModel.DataAnnotations;
using QLTVDAO.DAL;
using QLTVDAO.Entities;

namespace QLTVDAO.BLL;

public class BookCopyBLL
{
    public const int SoBanToiDaMoiLan = 100;

    private readonly BookCopyDAL dal;
    private readonly BookDAL bookDAL;
    public BookCopyBLL(Func<BookDbContext>? taoContext = null)
    {
        dal = new BookCopyDAL(taoContext);
        bookDAL = new BookDAL(taoContext);
    }
    public List<BookCopy> LayDanhSach(int? bookId = null) => dal.LayDanhSach(bookId);
    public BookCopy? TimTheoMa(int id) => dal.TimTheoMa(id);
    public int DemTheoSach(int bookId) => dal.DemTheoSach(bookId);

    // Nhập bổ sung: mã bản sao do hệ thống sinh theo mã đầu sách, người dùng không tự đặt.
    public List<BookCopy> Them(int bookId, int soLuong, string status, string condition)
    {
        if (soLuong < 1 || soLuong > SoBanToiDaMoiLan)
            throw new ValidationException($"Số bản nhập thêm phải từ 1 đến {SoBanToiDaMoiLan}.");
        if (bookDAL.TimTheoMa(bookId) == null)
            throw new ValidationException("Đầu sách không tồn tại.");
        var mau = new BookCopy
        {
            BookId = bookId,
            MaBanSao = "TAM", // Giá trị tạm để qua Required; DAO sinh mã thật trước khi lưu.
            Status = (status ?? "").Trim(),
            Condition = (condition ?? "").Trim()
        };
        KiemTraDuLieu.KiemTra(mau);
        return dal.ThemNhieu(bookId, soLuong, mau.Status, mau.Condition);
    }

    public void Sua(BookCopy copy)
    {
        var cu = dal.TimTheoMa(copy.BookCopyId)
            ?? throw new ValidationException("Không tìm thấy bản sao cần sửa.");
        copy.MaBanSao = cu.MaBanSao;
        copy.BookId = cu.BookId;
        copy.Status = (copy.Status ?? "").Trim();
        copy.Condition = (copy.Condition ?? "").Trim();
        KiemTraDuLieu.KiemTra(copy);
        dal.Sua(copy);
    }
    public void Xoa(int id) => dal.Xoa(id);
}

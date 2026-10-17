using Quanlysinhvien.Data.DAL;
using Quanlysinhvien.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Quanlysinhvien.BLL;

// BLL ném lỗi nghiệp vụ; không bắt lỗi MySQL/cấu hình để View xử lý.
public class LopHocBLL
{
    private readonly LopHocDAL dal = new();

    public List<LopHoc> LayDanhSach() => dal.LayDanhSach();
    public LopHoc? TimTheoMa(string maLop) => dal.TimTheoMa(maLop.Trim());

    public bool KiemTra(LopHoc lop, out string loi)
    {
        lop.MaLop = (lop.MaLop ?? "").Trim();
        lop.TenLop = (lop.TenLop ?? "").Trim();
        return lop.KiemTraHopLe(out loi);
    }

    public void Them(LopHoc lop)
    {
        if (!KiemTra(lop, out string loi)) throw new ValidationException(loi);
        if (TimTheoMa(lop.MaLop) != null)
            throw new ValidationException("Mã lớp đã tồn tại.");
        if (dal.Them(lop) != 1)
            throw new ValidationException("Không thêm được lớp học.");
    }

    public void Sua(LopHoc lop)
    {
        if (!KiemTra(lop, out string loi)) throw new ValidationException(loi);
        LopHoc? cu = TimTheoMa(lop.MaLop);
        if (cu == null)
            throw new ValidationException("Không tìm thấy lớp cần sửa.");
        // Mã lớp dùng để tìm; thao tác sửa chỉ thay đổi tên lớp.
        if (dal.Sua(cu, lop) != 1)
            throw new ValidationException("Không tìm thấy lớp cần sửa. Dữ liệu có thể vừa bị xóa.");
    }

    public void Xoa(string maLop)
    {
        LopHoc? lop = TimTheoMa(maLop);
        if (lop == null)
            throw new ValidationException("Không tìm thấy lớp cần xóa.");
        if (dal.CoSinhVien(lop.MaLop))
            throw new ValidationException("Không thể xóa lớp đang có sinh viên. Hãy chuyển hoặc xóa các sinh viên trước.");
        // Khóa ngoại MySQL chặn trường hợp có sinh viên được thêm đồng thời.
        if (dal.Xoa(lop) != 1)
            throw new ValidationException("Không tìm thấy lớp cần xóa. Dữ liệu có thể vừa bị xóa.");
    }
}

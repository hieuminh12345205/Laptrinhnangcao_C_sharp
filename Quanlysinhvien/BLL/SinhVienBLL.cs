using Quanlysinhvien.Data.DAL;
using Quanlysinhvien.Data.Entity;

namespace Quanlysinhvien.BLL;

// BLL kiểm tra quy tắc nghiệp vụ trước khi gọi DAL.
// Form chỉ gọi BLL, không tự sửa các danh sách dữ liệu.
public class SinhVienBLL
{
    private readonly SinhVienDAL dal;
    private readonly LopHocBLL lopHocBLL;

    public SinhVienBLL() : this(new LopHocBLL()) { }

    public SinhVienBLL(LopHocBLL lopHocBLL)
    {
        // Dùng cùng phần lớp học với form để dữ liệu hai bên luôn khớp nhau.
        this.lopHocBLL = lopHocBLL;
        dal = new SinhVienDAL(lopHocBLL.TimTheoMa("CSE0001")
            ?? throw new InvalidOperationException("Không tìm thấy lớp cho sinh viên mẫu."));
    }

    public List<LopHoc> LayDanhSachLop() => lopHocBLL.LayDanhSach();
    public List<SinhVien> LayDanhSach() => dal.LayDanhSach();
    public SinhVien? TimTheoMa(string maSV) => dal.TimTheoMa(maSV.Trim());

    public bool KiemTra(SinhVien sv, out string loi)
    {
        // Chuẩn hóa trước khi dùng Data Annotations kiểm tra đối tượng.
        sv.MaSV = sv.MaSV.Trim();
        sv.HoTen = sv.HoTen.Trim();
        sv.Email = sv.Email.Trim();
        sv.SDT = sv.SDT.Trim();
        // Kiểm tra khoảng trước khi làm tròn, tránh biến 10.04 thành 10.0.
        if (sv.Diem < 0 || sv.Diem > 10)
        {
            loi = "Điểm phải từ 0 đến 10.";
            return false;
        }
        sv.Diem = Math.Round(sv.Diem, 1, MidpointRounding.AwayFromZero);
        if (!sv.KiemTraHopLe(out loi)) return false;

        // Chỉ nhận lớp có trong danh sách; dùng cùng đối tượng để giữ quan hệ 1-n.
        LopHoc? lop = lopHocBLL.TimTheoMa(sv.LopHoc!.MaLop);
        if (lop == null)
        {
            loi = "Lớp học không tồn tại.";
            return false;
        }
        sv.LopHoc = lop;
        return true;
    }

    public bool Them(SinhVien sv, out string loi)
    {
        if (!KiemTra(sv, out loi)) return false;
        if (TimTheoMa(sv.MaSV) != null)
        {
            loi = "Mã sinh viên đã tồn tại.";
            return false;
        }
        dal.Them(sv);
        return true;
    }

    public bool Sua(SinhVien sv, out string loi)
    {
        if (!KiemTra(sv, out loi)) return false;
        SinhVien? cu = TimTheoMa(sv.MaSV);
        if (cu == null)
        {
            loi = "Không tìm thấy sinh viên cần sửa.";
            return false;
        }
        dal.Sua(cu, sv);
        return true;
    }

    public bool Xoa(string maSV, out string loi)
    {
        SinhVien? sv = TimTheoMa(maSV);
        if (sv == null)
        {
            loi = "Không tìm thấy sinh viên cần xóa.";
            return false;
        }
        dal.Xoa(sv);
        loi = "";
        return true;
    }

    public List<SinhVien> TimKiem(string tuKhoa, string maLop, decimal diemTu)
    {
        tuKhoa = tuKhoa.Trim();
        // Các điều kiện được kết hợp: từ khóa, lớp và điểm tối thiểu.
        return dal.LayDanhSach().Where(sv =>
            (sv.MaSV.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)
             || sv.HoTen.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)
             || sv.Email.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)
             || sv.SDT.Contains(tuKhoa))
            && (maLop == "" || string.Equals(sv.LopHoc?.MaLop, maLop, StringComparison.OrdinalIgnoreCase))
            && sv.Diem >= diemTu).ToList();
    }
}

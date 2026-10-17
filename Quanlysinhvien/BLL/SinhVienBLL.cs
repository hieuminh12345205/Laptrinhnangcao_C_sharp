using Quanlysinhvien.Data.DAL;
using Quanlysinhvien.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Quanlysinhvien.BLL;

// BLL kiểm tra quy tắc nghiệp vụ trước khi gọi DAL.
// Form chỉ gọi BLL, không tự sửa các danh sách dữ liệu.
public class SinhVienBLL
{
    private readonly SinhVienDAL dal = new();
    private readonly LopHocBLL lopHocBLL;

    public SinhVienBLL() : this(new LopHocBLL()) { }

    public SinhVienBLL(LopHocBLL lopHocBLL)
    {
        this.lopHocBLL = lopHocBLL;
    }

    public List<LopHoc> LayDanhSachLop() => lopHocBLL.LayDanhSach();
    public List<SinhVien> LayDanhSach() => dal.LayDanhSach();
    // Hai cách đọc có cùng kiểu kết quả, có thể dùng để đối chiếu khi học.
    public List<SinhVien> LayDanhSachPhiKetNoi() => dal.LayDanhSachPhiKetNoi();
    public List<SinhVien> LayTheoMaLop(string maLop) => dal.LayTheoMaLop(maLop);
    public SinhVien? TimTheoMa(string maSV) => dal.TimTheoMa(maSV.Trim());

    public List<ValidationResult> LayLoiValidation(SinhVien sv)
    {
        // Chuẩn hóa trước khi dùng Data Annotations kiểm tra đối tượng.
        sv.MaSV = (sv.MaSV ?? "").Trim();
        sv.HoTen = Regex.Replace((sv.HoTen ?? "").Trim(), @"\s+", " ");
        sv.Email = (sv.Email ?? "").Trim();
        sv.SDT = (sv.SDT ?? "").Trim();
        // Kiểm tra khoảng trước khi làm tròn, tránh biến 10.04 thành 10.0.
        if (sv.Diem >= 0 && sv.Diem <= 10)
            sv.Diem = Math.Round(sv.Diem, 1, MidpointRounding.AwayFromZero);
        var ketQua = sv.LayLoiValidation();

        // Chỉ nhận lớp còn tồn tại trong database. Khóa ngoại bảo vệ quan hệ 1-n.
        if (sv.LopHoc != null)
        {
            LopHoc? lop = lopHocBLL.TimTheoMa(sv.LopHoc.MaLop);
            if (lop == null)
                ketQua.Add(new ValidationResult("Lớp học không tồn tại.", new[] { nameof(SinhVien.LopHoc) }));
            else
                sv.LopHoc = lop;
        }
        return ketQua;
    }

    public bool KiemTra(SinhVien sv, out string loi)
    {
        var ketQua = LayLoiValidation(sv);
        loi = string.Join(Environment.NewLine, ketQua.Select(x => x.ErrorMessage));
        return ketQua.Count == 0;
    }

    // Lỗi nghiệp vụ dùng ValidationException; lỗi MySQL/cấu hình tự truyền lên View.
    public void AddSinhVien(SinhVien sv)
    {
        if (!KiemTra(sv, out string loi)) throw new ValidationException(loi);
        if (TimTheoMa(sv.MaSV) != null)
            throw new ValidationException("Mã sinh viên đã tồn tại.");
        // Khóa chính MySQL vẫn chặn trùng nếu hai người thêm đồng thời.
        if (dal.AddSinhVien(sv) != 1)
            throw new ValidationException("Không thêm được sinh viên.");
    }

    public void UpdateSinhVien(SinhVien sv)
    {
        if (!KiemTra(sv, out string loi)) throw new ValidationException(loi);
        if (dal.UpdateSinhVien(sv) != 1)
            throw new ValidationException("Không tìm thấy sinh viên cần sửa. Dữ liệu có thể vừa bị xóa.");
    }

    public void DeleteSinhVien(string maSV)
    {
        if (string.IsNullOrWhiteSpace(maSV))
            throw new ValidationException("Hãy nhập mã sinh viên cần xóa.");
        if (dal.DeleteSinhVien(maSV) != 1)
            throw new ValidationException("Không tìm thấy sinh viên cần xóa.");
    }
    public List<SinhVien> TimKiem(string tuKhoa, string maLop, decimal diemTu)
    {
        return dal.TimKiem(tuKhoa.Trim(), maLop, diemTu);
    }
}

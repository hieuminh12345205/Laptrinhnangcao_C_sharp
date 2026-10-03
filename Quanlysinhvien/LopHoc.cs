using System.ComponentModel.DataAnnotations;

namespace Quanlysinhvien;

public class LopHoc
{
    // Data Annotations: quy định dữ liệu bắt buộc và độ dài tối đa.
    [Required(ErrorMessage = "Mã lớp không được để trống.")]
    [StringLength(20, ErrorMessage = "Mã lớp tối đa 20 ký tự.")]
    public string MaLop { get; set; } = "";

    [Required(ErrorMessage = "Tên lớp không được để trống.")]
    [StringLength(100, ErrorMessage = "Tên lớp tối đa 100 ký tự.")]
    public string TenLop { get; set; } = "";

    // Quan hệ 1-n: một lớp có nhiều sinh viên.
    public List<SinhVien> SinhViens { get; set; } = new();

    public bool KiemTraHopLe(out string loi)
    {
        // Kiểm tra tất cả thuộc tính có gắn Data Annotations.
        // Trả về true nếu hợp lệ; biến loi chứa các thông báo nếu sai.
        var ketQua = new List<ValidationResult>();
        bool hopLe = Validator.TryValidateObject(this, new ValidationContext(this), ketQua, true);
        loi = string.Join(Environment.NewLine, ketQua.Select(x => x.ErrorMessage));
        return hopLe;
    }
}

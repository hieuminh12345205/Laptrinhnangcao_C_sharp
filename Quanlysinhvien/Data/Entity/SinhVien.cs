using System.ComponentModel.DataAnnotations;

namespace Quanlysinhvien.Data.Entity;

public class SinhVien : IValidatableObject
{
    // Required: bắt buộc nhập. StringLength: giới hạn số ký tự.
    [Required(ErrorMessage = "Mã sinh viên không được để trống.")]
    [StringLength(20, ErrorMessage = "Mã sinh viên tối đa 20 ký tự.")]
    public string MaSV { get; set; } = "";

    [Required(ErrorMessage = "Họ tên không được để trống.")]
    [StringLength(100, ErrorMessage = "Họ tên tối đa 100 ký tự.")]
    public string HoTen { get; set; } = "";

    public DateTime NgaySinh { get; set; }

    [Required(ErrorMessage = "Hãy chọn giới tính.")]
    [RegularExpression("^(Nam|Nữ)$", ErrorMessage = "Giới tính phải là Nam hoặc Nữ.")]
    public string GioiTinh { get; set; } = "";

    [Required(ErrorMessage = "Email không được để trống.")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Số điện thoại không được để trống.")]
    [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại gồm 10 chữ số và bắt đầu bằng 0.")]
    public string SDT { get; set; } = "";

    [Required(ErrorMessage = "Hãy chọn lớp học.")]
    public LopHoc? LopHoc { get; set; }

    // Mỗi sinh viên thuộc một lớp; dùng TenLop để hiển thị trên bảng.
    public string TenLop => LopHoc?.TenLop ?? "";

    [Range(typeof(decimal), "0", "10", ErrorMessage = "Điểm phải từ 0 đến 10.")]
    public decimal Diem { get; set; }

    [Required(ErrorMessage = "Hãy chọn trạng thái.")]
    [RegularExpression("^(Đang học|Bảo lưu|Đã tốt nghiệp)$", ErrorMessage = "Trạng thái không hợp lệ.")]
    public string TrangThai { get; set; } = "";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // Các quy tắc bổ sung mà Required hoặc Range không kiểm tra được.
        if (NgaySinh.Date > DateTime.Today || NgaySinh == default)
            yield return new ValidationResult("Ngày sinh không hợp lệ hoặc nằm trong tương lai.", new[] { nameof(NgaySinh) });
        if (Diem != Math.Round(Diem, 1))
            yield return new ValidationResult("Điểm chỉ được có một chữ số thập phân.", new[] { nameof(Diem) });
        if (LopHoc != null && !LopHoc.KiemTraHopLe(out string loi))
            yield return new ValidationResult(loi, new[] { nameof(LopHoc) });
    }

    public bool KiemTraHopLe(out string loi)
    {
        // validateAllProperties = true: kiểm tra mọi Data Annotation.
        // Validator cũng gọi Validate để kiểm tra ngày sinh và số lẻ của điểm.
        var ketQua = new List<ValidationResult>();
        bool hopLe = Validator.TryValidateObject(this, new ValidationContext(this), ketQua, true);
        loi = string.Join(Environment.NewLine, ketQua.Select(x => x.ErrorMessage));
        return hopLe;
    }
}


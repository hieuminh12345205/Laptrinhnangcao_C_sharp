using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTVDAO.Entities;

public class Book : IValidatableObject
{
    [Key]
    public int BookId { get; set; }

    // Mã nghiệp vụ do người dùng đặt (B006). Khác BookId là khóa chính tự tăng của MySQL.
    [Required(ErrorMessage = "Hãy nhập mã sách.")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Mã sách dài từ 2 đến 20 ký tự.")]
    [RegularExpression("^[A-Z0-9][A-Z0-9_-]*$", ErrorMessage = "Mã sách chỉ gồm chữ in hoa, số, dấu _ và -.")]
    public string MaSach { get; set; } = "";

    [Required(ErrorMessage = "Hãy nhập tiêu đề sách.")]
    [StringLength(200)]
    public string Title { get; set; } = "";

    [StringLength(150)]
    public string? Publisher { get; set; }

    [Range(typeof(decimal), "0", "999999999", ErrorMessage = "Giá sách phải từ 0 đến 999999999.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Hãy nhập tác giả.")]
    [StringLength(100)]
    public string Author { get; set; } = "";

    public int PublicationYear { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Hãy chọn thể loại cho sách.")]
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    // Một đầu sách có nhiều bản sách vật lý.
    public ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();

    // Số bản sao luôn bằng số BookCopy đã đăng ký; không lưu thành cột riêng để tránh lệch số liệu.
    // Chỉ đúng khi truy vấn có Include(x => x.BookCopies).
    [NotMapped]
    public int SoBanSao => BookCopies.Count;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (PublicationYear < 1000 || PublicationYear > DateTime.Today.Year)
            yield return new ValidationResult("Năm xuất bản phải từ 1000 đến năm hiện tại.", new[] { nameof(PublicationYear) });
        if (Price != Math.Round(Price, 2))
            yield return new ValidationResult("Giá sách tối đa hai chữ số thập phân.", new[] { nameof(Price) });
    }
}

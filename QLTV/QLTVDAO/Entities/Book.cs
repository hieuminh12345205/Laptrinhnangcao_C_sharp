using System.ComponentModel.DataAnnotations;

namespace QLTVDAO.Entities;

public class Book : IValidatableObject
{
    [Key]
    public int BookId { get; set; }

    [Required(ErrorMessage = "Hãy nhập tiêu đề sách.")]
    [StringLength(200)]
    public string Title { get; set; } = "";

    [StringLength(150)]
    public string? Publisher { get; set; }

    [Range(typeof(decimal), "0", "999999999", ErrorMessage = "Giá sách phải từ 0 đến 999999999.")]
    public decimal Price { get; set; }

    // Số bản khai báo của đầu sách; BookCopy ghi chi tiết từng bản đã đăng ký.
    [Range(1, 100000, ErrorMessage = "Tổng số bản phải từ 1 đến 100000.")]
    public int TotalCopies { get; set; }

    [StringLength(100)]
    public string? Author { get; set; }

    public int PublicationYear { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Hãy chọn danh mục cho sách.")]
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    // Một đầu sách có nhiều bản sách vật lý.
    public ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (PublicationYear < 1000 || PublicationYear > DateTime.Today.Year)
            yield return new ValidationResult("Năm xuất bản phải từ 1000 đến năm hiện tại.", new[] { nameof(PublicationYear) });
        if (Price != Math.Round(Price, 2))
            yield return new ValidationResult("Giá sách tối đa hai chữ số thập phân.", new[] { nameof(Price) });
    }
}

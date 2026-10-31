using System.ComponentModel.DataAnnotations;

namespace QLTVDAO.Entities;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Hãy nhập mô tả danh mục.")]
    [StringLength(100, ErrorMessage = "Mô tả danh mục tối đa 100 ký tự.")]
    public string Description { get; set; } = "";

    // Một danh mục có nhiều đầu sách.
    public ICollection<Book> Books { get; set; } = new List<Book>();

    public List<ValidationResult> IsInValid()
    {
        var loi = new List<ValidationResult>();
        Validator.TryValidateObject(this, new ValidationContext(this), loi, true);
        return loi;
    }
}

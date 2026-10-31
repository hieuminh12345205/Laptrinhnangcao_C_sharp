using System.ComponentModel.DataAnnotations;

namespace QLTVDAO.Entities;

public class BookCopy
{
    [Key]
    public int BookCopyId { get; set; }

    [Required(ErrorMessage = "Hãy nhập trạng thái bản sách.")]
    [StringLength(50)]
    public string Status { get; set; } = "Có sẵn";

    [Required(ErrorMessage = "Hãy nhập tình trạng bản sách.")]
    [StringLength(100)]
    public string Condition { get; set; } = "Mới";

    [Range(1, int.MaxValue, ErrorMessage = "Hãy chọn đầu sách.")]
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public List<ValidationResult> IsInValid()
    {
        var loi = new List<ValidationResult>();
        Validator.TryValidateObject(this, new ValidationContext(this), loi, true);
        return loi;
    }
}

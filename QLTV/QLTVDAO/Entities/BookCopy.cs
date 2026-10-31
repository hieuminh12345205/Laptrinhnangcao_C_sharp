using System.ComponentModel.DataAnnotations;

namespace QLTVDAO.Entities;

public class BookCopy
{
    [Key]
    public int BookCopyId { get; set; }

    // Mã bản sao sinh tự động theo mã đầu sách: B006.001, B006.002...
    [Required(ErrorMessage = "Thiếu mã bản sao.")]
    [StringLength(30)]
    public string MaBanSao { get; set; } = "";

    [Required(ErrorMessage = "Hãy chọn trạng thái bản sao.")]
    [StringLength(50)]
    public string Status { get; set; } = "Sẵn sàng";

    [Required(ErrorMessage = "Hãy nhập tình trạng vật lý của bản sao.")]
    [StringLength(100)]
    public string Condition { get; set; } = "Mới";

    [Range(1, int.MaxValue, ErrorMessage = "Hãy chọn đầu sách.")]
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;

namespace QLTVDAO.BLL;

public static class KiemTraDuLieu
{
    public static void KiemTra(object doiTuong)
    {
        ArgumentNullException.ThrowIfNull(doiTuong);
        var loi = new List<ValidationResult>();
        Validator.TryValidateObject(doiTuong, new ValidationContext(doiTuong), loi, true);
        if (loi.Count > 0)
            throw new ValidationException(string.Join(Environment.NewLine, loi.Select(x => x.ErrorMessage)));
    }
}

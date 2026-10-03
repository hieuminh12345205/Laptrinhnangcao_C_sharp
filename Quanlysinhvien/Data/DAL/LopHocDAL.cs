using Quanlysinhvien.Data.DAO;
using Quanlysinhvien.Data.Entity;

namespace Quanlysinhvien.Data.DAL;

// DAL chuyển các yêu cầu truy cập lớp học sang DAO.
public class LopHocDAL
{
    private readonly LopHocDAO dao = new();

    // Hàm lấy danh sách lớp để BLL cung cấp cho ComboBox trên form.
    public List<LopHoc> LayDanhSach()
    {
        return dao.LayDanhSach();
    }
    public LopHoc? TimTheoMa(string maLop) => dao.TimTheoMa(maLop.Trim());
    public void Them(LopHoc lop) => dao.Them(lop);
    public void Sua(LopHoc cu, LopHoc moi) => dao.Sua(cu, moi);
    public void Xoa(LopHoc lop) => dao.Xoa(lop);
}

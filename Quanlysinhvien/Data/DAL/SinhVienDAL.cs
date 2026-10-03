using Quanlysinhvien.Data.DAO;
using Quanlysinhvien.Data.Entity;

namespace Quanlysinhvien.Data.DAL;

// DAL cung cấp các hàm truy cập dữ liệu cho BLL.
// Trong bài này, DAL chuyển yêu cầu cho DAO xử lý với danh sách trong bộ nhớ.
public class SinhVienDAL
{
    // Tạo một DAO và dùng lại, tránh khởi tạo lại dữ liệu sau mỗi lần gọi hàm.
    private readonly SinhVienDAO dao;

    public SinhVienDAL(LopHoc lopMau)
    {
        dao = new SinhVienDAO(lopMau);
    }

    // Lấy danh sách sinh viên để hiển thị DataGridView.
    public List<SinhVien> LayDanhSach()
    {
        return dao.LayDanhSach();
    }

    public SinhVien? TimTheoMa(string maSV)
    {
        return dao.TimTheoMa(maSV.Trim());
    }

    // BLL kiểm tra dữ liệu và mã trùng trước khi gọi các hàm ghi dữ liệu.
    public void Them(SinhVien sv)
    {
        dao.Them(sv);
    }

    public void Sua(SinhVien cu, SinhVien moi)
    {
        dao.Sua(cu, moi);
    }

    public void Xoa(SinhVien sv)
    {
        dao.Xoa(sv);
    }
}

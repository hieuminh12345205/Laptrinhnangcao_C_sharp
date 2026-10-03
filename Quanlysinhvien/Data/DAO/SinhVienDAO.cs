using Quanlysinhvien.Data.Entity;

namespace Quanlysinhvien.Data.DAO;

// DAO trực tiếp giữ danh sách và thực hiện các thao tác với dữ liệu.
// Hiện dùng bộ nhớ; sau này có thể thay bằng đọc file hoặc cơ sở dữ liệu.
public class SinhVienDAO
{
    private readonly List<SinhVien> danhSachSV = new();

    public SinhVienDAO(LopHoc lopMau)
    {
        // Nạp dữ liệu mẫu một lần khi tạo DAO.
        // Nhận lớp từ phần lớp học, không tạo một danh sách lớp riêng.
        Them(new SinhVien
        {
            MaSV = "SV001", HoTen = "Nguyễn Văn An", NgaySinh = new DateTime(2005, 5, 12),
            GioiTinh = "Nam", Email = "an@example.com", SDT = "0912345678",
            LopHoc = lopMau, Diem = 8.5m, TrangThai = "Đang học"
        });
    }

    // Trả danh sách mới để bên ngoài không thêm/xóa trực tiếp vào danh sách gốc.
    // Các đối tượng bên trong vẫn dùng chung để duy trì quan hệ lớp - sinh viên.
    public List<SinhVien> LayDanhSach() => danhSachSV.ToList();

    public SinhVien? TimTheoMa(string maSV)
    {
        return danhSachSV.Find(sv => sv.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));
    }

    public void Them(SinhVien sv)
    {
        // Thêm vào danh sách chung và danh sách sinh viên của lớp.
        danhSachSV.Add(sv);
        sv.LopHoc!.SinhViens.Add(sv);
    }

    public void Sua(SinhVien cu, SinhVien moi)
    {
        // Khi đổi lớp, xóa sinh viên khỏi lớp cũ rồi đưa vào lớp mới.
        int viTri = danhSachSV.IndexOf(cu);
        cu.LopHoc!.SinhViens.Remove(cu);
        danhSachSV[viTri] = moi;
        moi.LopHoc!.SinhViens.Add(moi);
    }

    public void Xoa(SinhVien sv)
    {
        // Xóa ở cả hai danh sách để không còn tham chiếu sinh viên trong lớp.
        danhSachSV.Remove(sv);
        sv.LopHoc!.SinhViens.Remove(sv);
    }
}


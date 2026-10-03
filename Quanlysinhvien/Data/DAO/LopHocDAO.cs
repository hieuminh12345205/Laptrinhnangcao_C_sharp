using Quanlysinhvien.Data.Entity;

namespace Quanlysinhvien.Data.DAO;

// DAO lớp học trực tiếp giữ và thao tác danh sách trong bộ nhớ.
public class LopHocDAO
{
    private readonly List<LopHoc> danhSachLop;

    public LopHocDAO()
    {
        // Tạo danh sách và thêm dữ liệu mẫu, tương tự constructor trong bài GV.
        danhSachLop = new List<LopHoc>();
        danhSachLop.Add(new LopHoc
        {
            MaLop = "CSE0001",
            TenLop = "Khoa học máy tính 01"
        });
        danhSachLop.Add(new LopHoc
        {
            MaLop = "CSE0002",
            TenLop = "Khoa học máy tính 02"
        });
        danhSachLop.Add(new LopHoc
        {
            MaLop = "CSE0003",
            TenLop = "Khoa học máy tính 03"
        });
    }

    public List<LopHoc> LayDanhSach()
    {
        // Sao chép danh sách để bên gọi không thêm/xóa trực tiếp danh sách gốc.
        return danhSachLop.ToList();
    }

    public LopHoc? TimTheoMa(string maLop)
    {
        return danhSachLop.Find(lop => lop.MaLop.Equals(maLop, StringComparison.OrdinalIgnoreCase));
    }

    public void Them(LopHoc lop)
    {
        // Lớp mới bắt đầu với danh sách sinh viên trống.
        danhSachLop.Add(new LopHoc { MaLop = lop.MaLop, TenLop = lop.TenLop });
    }

    public void Sua(LopHoc cu, LopHoc moi)
    {
        // Giữ đối tượng cũ và danh sách sinh viên để bảo toàn quan hệ 1-n.
        cu.TenLop = moi.TenLop;
    }

    public void Xoa(LopHoc lop) => danhSachLop.Remove(lop);
}

using Quanlysinhvien.Data.DAO;
using Quanlysinhvien.Data.Entity;

namespace Quanlysinhvien.Data.DAL;

// DAL chuyển yêu cầu từ BLL đến DAO chạy SQL.
public class SinhVienDAL
{
    private readonly SinhVienDAO dao = new();

    public List<SinhVien> LayDanhSach() => dao.LayDanhSach();
    public List<SinhVien> LayDanhSachPhiKetNoi() => dao.LayDanhSachPhiKetNoi();
    public List<SinhVien> LayTheoMaLop(string maLop) => dao.LayTheoMaLop(maLop.Trim());
    public SinhVien? TimTheoMa(string maSV) => dao.TimTheoMa(maSV.Trim());
    public List<SinhVien> TimKiem(string tuKhoa, string maLop, decimal diemTu)
        => dao.TimKiem(tuKhoa, maLop, diemTu);
    public int AddSinhVien(SinhVien sv) => dao.AddSinhVien(sv);
    public int UpdateSinhVien(SinhVien sv) => dao.UpdateSinhVien(sv);
    public int DeleteSinhVien(string maSV) => dao.DeleteSinhVien(maSV.Trim());
}

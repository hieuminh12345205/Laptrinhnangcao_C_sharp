using System.Data;
using MySql.Data.MySqlClient;
using Quanlysinhvien.Data.Entity;

namespace Quanlysinhvien.Data.DAO;

public class LopHocDAO
{
    private DataHelper Helper => new DataHelper();

    public List<LopHoc> LayDanhSach()
    {
        var bang = Helper.ExecuteQuery("SELECT MaLop, TenLop FROM lophoc ORDER BY MaLop");
        return bang.Rows.Cast<DataRow>().Select(dong => new LopHoc
        {
            MaLop = (string)dong["MaLop"], TenLop = (string)dong["TenLop"]
        }).ToList();
    }

    public LopHoc? TimTheoMa(string maLop)
    {
        var bang = Helper.ExecuteQuery("SELECT MaLop, TenLop FROM lophoc WHERE MaLop = @MaLop",
            new MySqlParameter("@MaLop", MySqlDbType.VarChar) { Value = maLop });
        if (bang.Rows.Count == 0) return null;
        return new LopHoc { MaLop = (string)bang.Rows[0]["MaLop"], TenLop = (string)bang.Rows[0]["TenLop"] };
    }

    public bool CoSinhVien(string maLop)
    {
        // Kiểm tra database hiện tại, không dựa vào một danh sách cũ trong bộ nhớ.
        var bang = Helper.ExecuteQuery("SELECT MaSV FROM sinhvien WHERE MaLop = @MaLop LIMIT 1",
            new MySqlParameter("@MaLop", MySqlDbType.VarChar) { Value = maLop });
        return bang.Rows.Count > 0;
    }

    public int Them(LopHoc lop)
    {
        return Helper.ExecuteNonQuery("INSERT INTO lophoc (MaLop, TenLop) VALUES (@MaLop, @TenLop)",
            new MySqlParameter("@MaLop", MySqlDbType.VarChar) { Value = lop.MaLop },
            new MySqlParameter("@TenLop", MySqlDbType.VarChar) { Value = lop.TenLop });
    }

    public int Sua(LopHoc cu, LopHoc moi)
    {
        return Helper.ExecuteNonQuery("UPDATE lophoc SET TenLop = @TenLop WHERE MaLop = @MaLop",
            new MySqlParameter("@TenLop", MySqlDbType.VarChar) { Value = moi.TenLop },
            new MySqlParameter("@MaLop", MySqlDbType.VarChar) { Value = cu.MaLop });
    }

    public int Xoa(LopHoc lop)
    {
        return Helper.ExecuteNonQuery("DELETE FROM lophoc WHERE MaLop = @MaLop",
            new MySqlParameter("@MaLop", MySqlDbType.VarChar) { Value = lop.MaLop });
    }
}

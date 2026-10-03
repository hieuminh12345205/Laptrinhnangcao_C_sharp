namespace Quanlysinhvien;

public partial class Form1 : Form
{
    // Dữ liệu bài tập lưu trong bộ nhớ, chưa dùng cơ sở dữ liệu.
    private readonly List<LopHoc> danhSachLop = new();
    private readonly List<SinhVien> danhSachSV = new();
    private bool dangHienThi; // Tránh TextChanged chạy khi code điền thông tin.

    public Form1()
    {
        InitializeComponent();
        // Load đã nối trong Designer; các sự kiện còn lại nối tại đây.
        txtMaSV.TextChanged += TxtMaSV_TextChanged;
        btnThem.Click += BtnThem_Click;
        btnSua.Click += BtnSua_Click;
        btnXoa.Click += BtnXoa_Click;
        btnLamMoi.Click += (_, _) => LamMoi();
        btnDong.Click += (_, _) => Close();
        btnTimKiem.Click += (_, _) => HienThiDanhSach();
        btnHienThiTatCa.Click += (_, _) => HienThiTatCa();
        dgvSinhVien.CellClick += DgvSinhVien_CellClick;
        FormClosing += Form1_FormClosing;
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        // Tab theo từng hàng: từ trái qua phải, sau đó xuống hàng tiếp.
        Control[] thuTuTab = { txtMaSV, txtHoTen, cboLop, dtpNgaySinh,
            rdoNam, rdoNu, nudDiem, txtEmail, txtSDT, cboTrangThai,
            btnThem, btnSua, btnXoa, btnLamMoi, btnDong, pnlTimKiem, dgvSinhVien };
        for (int i = 0; i < thuTuTab.Length; i++)
            thuTuTab[i].TabIndex = i;
        pnlTimKiem.TabStop = false;

        dtpNgaySinh.MaxDate = DateTime.Today;
        dtpNgaySinh.ShowCheckBox = true; // Bỏ dấu chọn nghĩa là chưa nhập ngày sinh.
        cboLop.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLocLop.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThai.Items.AddRange(new object[] { "Đang học", "Bảo lưu", "Đã tốt nghiệp" });

        TaoDuLieuMau();
        cboLop.DisplayMember = nameof(LopHoc.TenLop);
        cboLop.DataSource = danhSachLop;
        cboLocLop.Items.Add("Tất cả lớp");
        foreach (LopHoc lop in danhSachLop)
            cboLocLop.Items.Add(lop.TenLop);
        cboLocLop.SelectedIndex = 0;

        HienThiDanhSach();
        LamMoi();
        // Đợi form hiện ra rồi đặt con trỏ vào mã sinh viên.
        BeginInvoke(new Action(() => txtMaSV.Focus()));
    }

    private void TaoDuLieuMau()
    {
        danhSachLop.Add(new LopHoc { MaLop = "L01", TenLop = "CNTT01" });
        danhSachLop.Add(new LopHoc { MaLop = "L02", TenLop = "CNTT02" });
        danhSachLop.Add(new LopHoc { MaLop = "L03", TenLop = "CNTT03" });
        var sv = new SinhVien
        {
            MaSV = "SV001", HoTen = "Nguyễn Văn An", NgaySinh = new DateTime(2005, 5, 12),
            GioiTinh = "Nam", Email = "an@example.com", SDT = "0912345678",
            LopHoc = danhSachLop[0], Diem = 8.5m, TrangThai = "Đang học"
        };
        danhSachSV.Add(sv);
        danhSachLop[0].SinhViens.Add(sv);
    }

    private SinhVien? TimSinhVien()
    {
        // Bỏ khoảng trắng hai đầu và không phân biệt hoa/thường.
        return danhSachSV.Find(sv => sv.MaSV.Equals(
            txtMaSV.Text.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    private void TxtMaSV_TextChanged(object? sender, EventArgs e)
    {
        if (dangHienThi) return;
        SinhVien? sv = TimSinhVien();
        if (sv != null) DienThongTin(sv);
        else XoaThongTin(); // Giữ mã vừa nhập, xóa các ô còn lại.
        CapNhatNut(sv != null);
    }

    private void CapNhatNut(bool daTonTai)
    {
        // Tắt chức năng nhập mới là tắt nút Thêm; vẫn cho sửa thông tin.
        btnThem.Enabled = !daTonTai;
        btnSua.Enabled = daTonTai;
        btnXoa.Enabled = daTonTai;
    }

    private void XoaThongTin()
    {
        txtHoTen.Clear();
        txtEmail.Clear();
        txtSDT.Clear();
        rdoNam.Checked = false;
        rdoNu.Checked = false;
        dtpNgaySinh.Value = DateTime.Today;
        dtpNgaySinh.Checked = false;
        cboLop.SelectedIndex = -1;
        cboTrangThai.SelectedIndex = -1;
        nudDiem.Value = 0;
    }

    private void LamMoi()
    {
        dangHienThi = true;
        txtMaSV.Clear();
        XoaThongTin();
        dangHienThi = false;
        CapNhatNut(false);
        dgvSinhVien.ClearSelection();
        txtMaSV.Focus();
    }

    private void DienThongTin(SinhVien sv)
    {
        dangHienThi = true;
        txtMaSV.Text = sv.MaSV;
        txtHoTen.Text = sv.HoTen;
        txtEmail.Text = sv.Email;
        txtSDT.Text = sv.SDT;
        dtpNgaySinh.Value = sv.NgaySinh;
        dtpNgaySinh.Checked = true;
        rdoNam.Checked = sv.GioiTinh == "Nam";
        rdoNu.Checked = sv.GioiTinh == "Nữ";
        cboLop.SelectedItem = sv.LopHoc;
        cboTrangThai.SelectedItem = sv.TrangThai;
        nudDiem.Value = sv.Diem;
        dangHienThi = false;
        CapNhatNut(true);
    }

    private SinhVien DocThongTin()
    {
        // Tạo đối tượng mới để kiểm tra trước khi thay đổi danh sách.
        return new SinhVien
        {
            MaSV = txtMaSV.Text.Trim(), HoTen = txtHoTen.Text.Trim(),
            Email = txtEmail.Text.Trim(), SDT = txtSDT.Text.Trim(),
            NgaySinh = dtpNgaySinh.Checked ? dtpNgaySinh.Value.Date : default,
            GioiTinh = rdoNam.Checked ? "Nam" : rdoNu.Checked ? "Nữ" : "",
            LopHoc = cboLop.SelectedItem as LopHoc,
            Diem = Math.Round(nudDiem.Value, 1, MidpointRounding.AwayFromZero),
            TrangThai = cboTrangThai.Text
        };
    }

    private bool KiemTra(SinhVien sv)
    {
        if (sv.KiemTraHopLe(out string loi)) return true;
        MessageBox.Show(loi, "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
    }

    private void BtnThem_Click(object? sender, EventArgs e)
    {
        if (TimSinhVien() != null)
        {
            MessageBox.Show("Mã sinh viên đã tồn tại.");
            return;
        }
        SinhVien sv = DocThongTin();
        if (!KiemTra(sv)) return;
        danhSachSV.Add(sv);
        sv.LopHoc!.SinhViens.Add(sv);
        HienThiDanhSach();
        CapNhatNut(true);
        MessageBox.Show("Đã thêm sinh viên.");
    }

    private void BtnSua_Click(object? sender, EventArgs e)
    {
        SinhVien? cu = TimSinhVien();
        if (cu == null) return;
        SinhVien moi = DocThongTin();
        if (!KiemTra(moi)) return;
        if (!XacNhan($"Bạn có muốn lưu thay đổi sinh viên {cu.MaSV}?")) return;
        // Thay đối tượng cũ, đồng thời cập nhật quan hệ lớp - sinh viên.
        int viTri = danhSachSV.IndexOf(cu);
        cu.LopHoc!.SinhViens.Remove(cu);
        danhSachSV[viTri] = moi;
        moi.LopHoc!.SinhViens.Add(moi);
        HienThiDanhSach();
        MessageBox.Show("Đã sửa sinh viên.");
    }

    private void BtnXoa_Click(object? sender, EventArgs e)
    {
        SinhVien? sv = TimSinhVien();
        if (sv == null) return;
        if (!XacNhan($"Bạn có chắc muốn xóa sinh viên {sv.MaSV} - {sv.HoTen}?")) return;
        danhSachSV.Remove(sv);
        sv.LopHoc!.SinhViens.Remove(sv);
        HienThiDanhSach();
        LamMoi();
    }

    private bool XacNhan(string noiDung)
    {
        // Mặc định chọn No để tránh vô tình thực hiện thao tác nguy hiểm.
        return MessageBox.Show(noiDung, "Xác nhận", MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
    }

    private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (!XacNhan("Đóng ứng dụng? Dữ liệu đang lưu trong bộ nhớ sẽ mất."))
            e.Cancel = true;
    }

    private void HienThiDanhSach()
    {
        string tuKhoa = txtTuKhoa.Text.Trim();
        string lop = cboLocLop.SelectedIndex > 0 ? cboLocLop.Text : "";
        // Lọc đồng thời theo từ khóa, lớp và điểm tối thiểu.
        var ketQua = danhSachSV.Where(sv =>
            (sv.MaSV.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)
             || sv.HoTen.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)
             || sv.Email.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)
             || sv.SDT.Contains(tuKhoa))
            && (lop == "" || sv.TenLop == lop)
            && sv.Diem >= nudDiemTu.Value).ToList();
        dgvSinhVien.DataSource = null;
        dgvSinhVien.DataSource = ketQua;
        dgvSinhVien.ClearSelection();
        lblTongSo.Text = $"Hiển thị: {ketQua.Count} / Tổng số: {danhSachSV.Count} sinh viên";
    }

    private void HienThiTatCa()
    {
        txtTuKhoa.Clear();
        cboLocLop.SelectedIndex = 0;
        nudDiemTu.Value = 0;
        HienThiDanhSach();
    }

    private void DgvSinhVien_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        // Bỏ qua lần bấm vào tiêu đề cột.
        if (e.RowIndex < 0) return;
        if (dgvSinhVien.Rows[e.RowIndex].DataBoundItem is SinhVien sv)
            DienThongTin(sv);
    }
}

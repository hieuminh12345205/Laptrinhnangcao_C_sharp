using Quanlysinhvien.BLL;
using Quanlysinhvien.Data;
using Quanlysinhvien.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Quanlysinhvien.Views
{
    public partial class frmQuanLySV : Form
    {
        // View nhận dữ liệu và gọi BLL; không trực tiếp thêm/sửa/xóa danh sách.
        private readonly LopHocBLL lopHocBLL = new();
        private readonly SinhVienBLL sinhVienBLL;
        private bool dangHienThi; // Tránh TextChanged chạy khi code điền thông tin.
        private bool daTaiDuLieu;

        public frmQuanLySV()
        {
            sinhVienBLL = new SinhVienBLL(lopHocBLL);
            InitializeComponent();
            // Load đã nối trong Designer; các sự kiện còn lại nối tại đây.
            txtMaSV.TextChanged += TxtMaSV_TextChanged;
            txtMaSV.Enter += (_, _) => txtMaSV.SelectAll();
            txtMaSV.KeyDown += (_, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                txtHoTen.Focus();
                e.SuppressKeyPress = true;
            };
            // Xóa biểu tượng lỗi của ô đang được người dùng chỉnh lại.
            txtHoTen.TextChanged += (_, _) => errorProvider.SetError(txtHoTen, "");
            txtEmail.TextChanged += (_, _) => errorProvider.SetError(txtEmail, "");
            txtSDT.TextChanged += (_, _) => errorProvider.SetError(txtSDT, "");
            dtpNgaySinh.ValueChanged += (_, _) => errorProvider.SetError(dtpNgaySinh, "");
            rdoNam.CheckedChanged += (_, _) => errorProvider.SetError(rdoNu, "");
            rdoNu.CheckedChanged += (_, _) => errorProvider.SetError(rdoNu, "");
            cboLop.SelectedIndexChanged += (_, _) => errorProvider.SetError(cboLop, "");
            cboTrangThai.SelectedIndexChanged += (_, _) => errorProvider.SetError(cboTrangThai, "");
            nudDiem.ValueChanged += (_, _) => errorProvider.SetError(nudDiem, "");
            cboLocLop.SelectedValueChanged += (_, _) =>
            {
                if (daTaiDuLieu) HienThiDanhSach();
            };
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += (_, _) => LamMoi();
            btnDong.Click += (_, _) => Close();
            btnTimKiem.Click += (_, _) => HienThiDanhSach();
            btnHienThiTatCa.Click += (_, _) => HienThiTatCa();
            dgvSinhVien.CellClick += DgvSinhVien_CellClick;
            FormClosing += frmQuanLySV_FormClosing;
        }

        private void frmQuanLySV_Load(object? sender, EventArgs e)
        {
            ThucHienAnToan(() =>
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

                List<LopHoc> danhSachLop = lopHocBLL.LayDanhSach();
                cboLop.DisplayMember = nameof(LopHoc.TenLop);
                cboLop.ValueMember = nameof(LopHoc.MaLop);
                cboLop.DataSource = danhSachLop;
                // Hiển thị tên dễ đọc, nhưng dùng mã lớp để lọc đúng cả khi tên trùng nhau.
                var danhSachLoc = new List<LopHoc>
                {
                    new LopHoc { MaLop = "", TenLop = "Tất cả lớp" }
                };
                danhSachLoc.AddRange(danhSachLop);
                cboLocLop.DisplayMember = nameof(LopHoc.TenLop);
                cboLocLop.ValueMember = nameof(LopHoc.MaLop);
                cboLocLop.DataSource = danhSachLoc;
                cboLocLop.SelectedIndex = 0;

                daTaiDuLieu = true;
                HienThiDanhSach();
                LamMoi();
                // Đợi form hiện ra rồi đặt con trỏ vào mã sinh viên.
                BeginInvoke(new Action(() => txtMaSV.Focus()));
            });
        }

        private SinhVien? TimSinhVien()
        {
            return sinhVienBLL.TimTheoMa(txtMaSV.Text);
        }

        private void TxtMaSV_TextChanged(object? sender, EventArgs e)
        {
            ThucHienAnToan(() =>
            {
                if (dangHienThi) return;
                errorProvider.Clear();
                SinhVien? sv = TimSinhVien();
                if (sv != null) DienThongTin(sv);
                else XoaThongTin(); // Giữ mã vừa nhập, xóa các ô còn lại.
                CapNhatNut(sv != null);
            });
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
            errorProvider.Clear();
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
            // Kiểm tra trước khi gán để tránh điền dở form nếu dữ liệu ngoài khoảng.
            if (sv.NgaySinh < dtpNgaySinh.MinDate || sv.NgaySinh > dtpNgaySinh.MaxDate
                || sv.Diem < nudDiem.Minimum || sv.Diem > nudDiem.Maximum || sv.LopHoc == null)
                throw new InvalidDataException();
            errorProvider.Clear();
            dangHienThi = true;
            try
            {
                txtMaSV.Text = sv.MaSV;
                txtHoTen.Text = sv.HoTen;
                txtEmail.Text = sv.Email;
                txtSDT.Text = sv.SDT;
                dtpNgaySinh.Value = sv.NgaySinh;
                dtpNgaySinh.Checked = true;
                rdoNam.Checked = sv.GioiTinh == "Nam";
                rdoNu.Checked = sv.GioiTinh == "Nữ";
                cboLop.SelectedValue = sv.LopHoc!.MaLop;
                cboTrangThai.SelectedItem = sv.TrangThai;
                nudDiem.Value = sv.Diem;
            }
            finally
            {
                // Luôn khôi phục cờ, kể cả khi điền dữ liệu gặp lỗi.
                dangHienThi = false;
            }
            CapNhatNut(true);
        }

        private SinhVien DocThongTin()
        {
            // Tạo đối tượng mới để kiểm tra trước khi thay đổi danh sách.
            return new SinhVien
            {
                MaSV = txtMaSV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                SDT = txtSDT.Text.Trim(),
                NgaySinh = dtpNgaySinh.Checked ? dtpNgaySinh.Value.Date : default,
                GioiTinh = rdoNam.Checked ? "Nam" : rdoNu.Checked ? "Nữ" : "",
                LopHoc = cboLop.SelectedItem as LopHoc,
                Diem = nudDiem.Value, // BLL chịu trách nhiệm làm tròn điểm.
                TrangThai = cboTrangThai.Text
            };
        }

        private bool KiemTra(SinhVien sv)
        {
            errorProvider.Clear();
            var loi = sinhVienBLL.LayLoiValidation(sv);
            Control? oDauTien = null;
            foreach (var ketQua in loi)
            {
                foreach (string tenThuocTinh in ketQua.MemberNames)
                {
                    Control? oNhap = tenThuocTinh switch
                    {
                        nameof(SinhVien.MaSV) => txtMaSV,
                        nameof(SinhVien.HoTen) => txtHoTen,
                        nameof(SinhVien.Email) => txtEmail,
                        nameof(SinhVien.SDT) => txtSDT,
                        nameof(SinhVien.NgaySinh) => dtpNgaySinh,
                        nameof(SinhVien.GioiTinh) => rdoNu,
                        nameof(SinhVien.LopHoc) => cboLop,
                        nameof(SinhVien.Diem) => nudDiem,
                        nameof(SinhVien.TrangThai) => cboTrangThai,
                        _ => null
                    };
                    if (oNhap == null) continue;
                    string loiCu = errorProvider.GetError(oNhap);
                    errorProvider.SetError(oNhap, loiCu == "" ? ketQua.ErrorMessage
                        : loiCu + Environment.NewLine + ketQua.ErrorMessage);
                    oDauTien ??= oNhap;
                }
            }
            // Đặt con trỏ vào ô sai đầu tiên, không đổi tiêu điểm liên tục trong vòng lặp.
            oDauTien?.Focus();
            return loi.Count == 0;
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            ThucHienAnToan(() =>
            {
                SinhVien sv = DocThongTin();
                if (!KiemTra(sv)) return;
                sinhVienBLL.AddSinhVien(sv); // Nếu lỗi, chuyển đến catch của View.
                HienThiDanhSach();
                DienThongTin(sv); // Hiển thị lại họ tên và điểm sau khi BLL chuẩn hóa.
                MessageBox.Show("Đã thêm sinh viên.");
            });
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            ThucHienAnToan(() =>
            {
                SinhVien? cu = TimSinhVien();
                if (cu == null) throw new ValidationException("Không tìm thấy sinh viên cần sửa.");
                SinhVien moi = DocThongTin();
                if (!KiemTra(moi)) return;
                // So sánh giá trị sau khi chuẩn hóa; so mã lớp, không so đối tượng lớp.
                bool khongThayDoi = moi.HoTen == cu.HoTen
                    && moi.NgaySinh.Date == cu.NgaySinh.Date
                    && moi.GioiTinh == cu.GioiTinh
                    && moi.Email == cu.Email
                    && moi.SDT == cu.SDT
                    && moi.LopHoc?.MaLop == cu.LopHoc?.MaLop
                    && moi.Diem == cu.Diem
                    && moi.TrangThai == cu.TrangThai;
                if (khongThayDoi)
                {
                    MessageBox.Show("Bạn chưa thay đổi thông tin sinh viên.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Không hỏi xác nhận và không chạy UPDATE xuống MySQL.
                }
                if (!XacNhan($"Bạn có muốn lưu thay đổi sinh viên {cu.MaSV}?")) return;
                sinhVienBLL.UpdateSinhVien(moi);
                HienThiDanhSach();
                DienThongTin(moi);
                MessageBox.Show("Đã sửa sinh viên.");
            });
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            ThucHienAnToan(() =>
            {
                errorProvider.Clear();
                SinhVien? sv = TimSinhVien();
                if (sv == null) throw new ValidationException("Không tìm thấy sinh viên cần xóa.");
                if (!XacNhan($"Bạn có chắc muốn xóa sinh viên {sv.MaSV} - {sv.HoTen}?")) return;
                sinhVienBLL.DeleteSinhVien(sv.MaSV);
                HienThiDanhSach();
                LamMoi();
            });
        }

        // Chỉ View hiển thị MessageBox. Không đưa SQL hoặc mật khẩu vào thông báo lỗi.
        private void ThucHienAnToan(Action thaoTac)
        {
            try { thaoTac(); }
            catch (ValidationException ex)
            {
                // Chỉ thông báo nghiệp vụ do BLL tạo mới được hiển thị trực tiếp.
                MessageBox.Show(ex.Message, "Dữ liệu không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Chặn lỗi ở biên giao diện; không coi thao tác thất bại là thành công.
                MessageBox.Show(DataHelper.LayThongBaoLoi(ex), "Không thực hiện được thao tác",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool XacNhan(string noiDung)
        {
            // Mặc định chọn No để tránh vô tình thực hiện thao tác nguy hiểm.
            return MessageBox.Show(noiDung, "Xác nhận", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        private void frmQuanLySV_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!XacNhan("Bạn có muốn đóng ứng dụng? Thông tin chưa lưu trên các ô nhập sẽ mất."))
                e.Cancel = true;
        }

        private void HienThiDanhSach()
        {
            ThucHienAnToan(() =>
            {
                string tuKhoa = txtTuKhoa.Text.Trim();
                string maLop = cboLocLop.SelectedValue as string ?? "";
                List<SinhVien> ketQua = sinhVienBLL.TimKiem(tuKhoa, maLop, nudDiemTu.Value);
                dgvSinhVien.DataSource = null;
                dgvSinhVien.DataSource = ketQua;
                dgvSinhVien.ClearSelection();
                lblTongSo.Text = $"Hiển thị: {ketQua.Count} / Tổng số: {sinhVienBLL.LayDanhSach().Count} sinh viên";
            });
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
                ThucHienAnToan(() => DienThongTin(sv));
        }

        private void rdoNu_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

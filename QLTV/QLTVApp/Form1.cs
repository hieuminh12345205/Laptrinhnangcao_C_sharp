using System.ComponentModel.DataAnnotations;
using QLTVDAO;
using QLTVDAO.BLL;
using QLTVDAO.Entities;

namespace QLTVApp
{
    public partial class Form1 : Form
    {
        private readonly CategoryBLL categoryBLL;
        private readonly BookBLL bookBLL;
        private readonly BookCopyBLL copyBLL;
        private int bookId;  // 0: đang nhập đầu sách mới; > 0: đang chọn một đầu sách.
        private int copyId;  // 0: chưa chọn bản sao nào.
        private Book? sachDangChon;
        // Nạp lại lưới làm SelectionChanged bắn liên tục; cờ này chặn xử lý trong lúc nạp.
        private bool dangTaiLuoi;

        private static readonly string[] DanhSachTrangThai = { "Sẵn sàng", "Đang mượn", "Bảo trì", "Thất lạc" };

        public Form1() : this(null) { }

        // Cho bộ kiểm tra truyền database tạm; khi chạy bình thường dùng mysql.local.json.
        public Form1(Func<BookDbContext>? taoContext)
        {
            categoryBLL = new CategoryBLL(taoContext);
            bookBLL = new BookBLL(taoContext);
            copyBLL = new BookCopyBLL(taoContext);
            InitializeComponent();
            cboCopyStatus.Items.AddRange(DanhSachTrangThai);
            cboCopyStatus.SelectedIndex = 0;
            nudYear.Maximum = DateTime.Today.Year;
            nudYear.Value = DateTime.Today.Year;

            Load += (_, _) => ThucHienAnToan(() =>
            {
                // Mở View Designer không truy cập database.
                if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
                TaiDanhMuc();
                HienThiSach();
                LamTrong();
                // Màn hình này không quản lý thể loại; database trống thì chưa thêm được đầu sách.
                if (cboCategory.Items.Count == 0)
                    HienThongBao("Chưa có thể loại nào trong database nên chưa thêm được đầu sách.\n"
                        + "Hãy thêm thể loại vào bảng Categories trước, ví dụ bằng MySQL Workbench.", true);
                BeginInvoke(new Action(() => txtMaSach.Focus()));
            });

            btnSearch.Click += (_, _) => ThucHienAnToan(HienThiSach);
            txtSearch.KeyDown += (_, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                ThucHienAnToan(HienThiSach);
            };
            btnReload.Click += (_, _) => ThucHienAnToan(() =>
            {
                txtSearch.Clear();
                if (cboFilter.Items.Count > 0) cboFilter.SelectedIndex = 0;
                TaiDanhMuc();
                HienThiSach();
                LamTrong();
            });

            dgvBooks.SelectionChanged += (_, _) => ThucHienAnToan(() =>
            {
                if (dangTaiLuoi) return;
                if (dgvBooks.CurrentRow?.Tag is not Book book) return;
                ChonSach(book);
            });
            dgvCopies.SelectionChanged += (_, _) =>
            {
                if (dangTaiLuoi) return;
                if (dgvCopies.CurrentRow?.Tag is not BookCopy copy) { copyId = 0; CapNhatTrangThaiNut(); return; }
                copyId = copy.BookCopyId;
                ChonTrangThai(copy.Status);
                txtCopyCondition.Text = copy.Condition;
                CapNhatTrangThaiNut();
            };

            btnBookAdd.Click += (_, _) => ThucHienAnToan(ThemSach);
            btnBookSave.Click += (_, _) => ThucHienAnToan(SuaSach);
            btnBookDelete.Click += (_, _) => ThucHienAnToan(XoaSach);
            btnBookClear.Click += (_, _) => LamTrong();

            btnNhapBoSung.Click += (_, _) => ThucHienAnToan(NhapBoSungBanSao);
            btnCopyAdd.Click += (_, _) => ThucHienAnToan(NhapBoSungBanSao);
            btnCopySave.Click += (_, _) => ThucHienAnToan(CapNhatBanSao);
            btnCopyDelete.Click += (_, _) => ThucHienAnToan(XoaBanSao);

            // Đổi nội dung ô nhập thì xóa dấu báo lỗi của ô đó.
            foreach (Control control in new Control[] { txtMaSach, txtTitle, txtAuthor, txtPublisher, txtCopyCondition })
                control.TextChanged += (_, _) => errorProvider.SetError(control, "");
            cboCategory.SelectedIndexChanged += (_, _) => errorProvider.SetError(cboCategory, "");
            foreach (NumericUpDown control in new[] { nudPrice, nudYear })
                control.ValueChanged += (_, _) => errorProvider.SetError(control, "");

            FormClosing += (_, e) =>
            {
                if (e.CloseReason == CloseReason.WindowsShutDown) return;
                if (!XacNhan("Bạn có muốn đóng ứng dụng? Thông tin đang nhập chưa lưu sẽ mất.")) e.Cancel = true;
            };
        }

        // ---------- Nạp dữ liệu ----------

        private void TaiDanhMuc()
        {
            var danhSach = categoryBLL.LayDanhSach();

            object? locDangChon = cboFilter.SelectedValue;
            var choLoc = new List<Category> { new() { CategoryId = 0, Description = "-- Tất cả thể loại --" } };
            choLoc.AddRange(danhSach);
            cboFilter.DataSource = choLoc;
            cboFilter.DisplayMember = nameof(Category.Description);
            cboFilter.ValueMember = nameof(Category.CategoryId);
            if (locDangChon != null && choLoc.Any(x => x.CategoryId == Convert.ToInt32(locDangChon)))
                cboFilter.SelectedValue = locDangChon;
            else cboFilter.SelectedIndex = 0;

            // Danh sách riêng cho ô chọn thể loại: hai ComboBox không dùng chung một nguồn dữ liệu.
            object? sachDangChonLoai = cboCategory.SelectedValue;
            cboCategory.DataSource = new List<Category>(danhSach);
            cboCategory.DisplayMember = nameof(Category.Description);
            cboCategory.ValueMember = nameof(Category.CategoryId);
            if (sachDangChonLoai != null && danhSach.Any(x => x.CategoryId == Convert.ToInt32(sachDangChonLoai)))
                cboCategory.SelectedValue = sachDangChonLoai;
            else if (danhSach.Count > 0) cboCategory.SelectedIndex = 0;
            else cboCategory.SelectedIndex = -1;
        }

        private void HienThiSach()
        {
            int loc = cboFilter.SelectedValue == null ? 0 : Convert.ToInt32(cboFilter.SelectedValue);
            var danhSach = bookBLL.LayDanhSach(txtSearch.Text.Trim(), loc == 0 ? null : loc);

            dangTaiLuoi = true;
            try
            {
                dgvBooks.Rows.Clear();
                foreach (var book in danhSach)
                {
                    int dong = dgvBooks.Rows.Add(book.MaSach, book.Title, book.Category?.Description,
                        book.Author, book.Publisher, book.Price, book.PublicationYear, book.SoBanSao);
                    dgvBooks.Rows[dong].Tag = book;
                }
                dgvBooks.ClearSelection();
                dgvBooks.CurrentCell = null;
            }
            finally { dangTaiLuoi = false; }
            lblTrangThai.Text = $"Đã tải {danhSach.Count} đầu sách lúc {DateTime.Now:HH:mm:ss}.";
        }

        private void HienThiBanSao()
        {
            copyId = 0;
            if (bookId == 0)
            {
                dangTaiLuoi = true;
                try { dgvCopies.Rows.Clear(); } finally { dangTaiLuoi = false; }
                grpCopies.Text = "Danh sách bản sao";
                txtSoBanSao.Text = "0";
                CapNhatTrangThaiNut();
                return;
            }
            var danhSach = copyBLL.LayDanhSach(bookId);
            dangTaiLuoi = true;
            try
            {
                dgvCopies.Rows.Clear();
                foreach (var copy in danhSach)
                {
                    int dong = dgvCopies.Rows.Add(copy.MaBanSao, copy.Status, copy.Condition);
                    dgvCopies.Rows[dong].Tag = copy;
                }
                dgvCopies.ClearSelection();
                dgvCopies.CurrentCell = null;
            }
            finally { dangTaiLuoi = false; }
            txtSoBanSao.Text = danhSach.Count.ToString();
            grpCopies.Text = $"Danh sách bản sao của sách {sachDangChon?.MaSach} ({danhSach.Count} bản sao)";
            CapNhatTrangThaiNut();
        }

        // ---------- Đầu sách ----------

        private void ChonSach(Book book)
        {
            errorProvider.Clear();
            sachDangChon = book;
            bookId = book.BookId;
            txtMaSach.Text = book.MaSach;
            txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author;
            txtPublisher.Text = book.Publisher ?? "";
            if (cboCategory.Items.Count > 0) cboCategory.SelectedValue = book.CategoryId;
            nudPrice.Value = Math.Clamp(book.Price, nudPrice.Minimum, nudPrice.Maximum);
            nudYear.Value = Math.Clamp(book.PublicationYear, (int)nudYear.Minimum, (int)nudYear.Maximum);
            HienThiBanSao();
        }

        private void LamTrong()
        {
            errorProvider.Clear();
            bookId = 0; copyId = 0; sachDangChon = null;
            txtMaSach.Clear(); txtTitle.Clear(); txtAuthor.Clear(); txtPublisher.Clear();
            if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;
            nudPrice.Value = 0;
            nudYear.Value = DateTime.Today.Year;
            txtCopyCondition.Text = "Mới";
            cboCopyStatus.SelectedIndex = 0;
            nudSoBanNhap.Value = 1;
            dangTaiLuoi = true;
            try { dgvBooks.ClearSelection(); dgvBooks.CurrentCell = null; }
            finally { dangTaiLuoi = false; }
            HienThiBanSao();
            txtMaSach.Focus();
        }

        private Book DocSach() => new()
        {
            BookId = bookId,
            MaSach = txtMaSach.Text.Trim().ToUpperInvariant(),
            Title = txtTitle.Text.Trim(),
            Author = txtAuthor.Text.Trim(),
            Publisher = txtPublisher.Text.Trim(),
            Price = nudPrice.Value,
            PublicationYear = (int)nudYear.Value,
            CategoryId = cboCategory.SelectedValue == null ? 0 : Convert.ToInt32(cboCategory.SelectedValue)
        };

        private bool KiemTraSach(Book book) => KiemTraNhap(book, new()
        {
            [nameof(Book.MaSach)] = txtMaSach,
            [nameof(Book.Title)] = txtTitle,
            [nameof(Book.Author)] = txtAuthor,
            [nameof(Book.Publisher)] = txtPublisher,
            [nameof(Book.Price)] = nudPrice,
            [nameof(Book.PublicationYear)] = nudYear,
            [nameof(Book.CategoryId)] = cboCategory
        });

        private void ThemSach()
        {
            if (bookId != 0) { HienThongBao("Đang chọn một đầu sách. Bấm Làm trống trước khi thêm mới.", true); return; }
            var book = DocSach(); if (!KiemTraSach(book)) return;
            bookBLL.Them(book);
            HoanTatLuu($"Đã thêm đầu sách {book.MaSach}.", () => ChonTheoMa(book.BookId));
        }

        private void SuaSach()
        {
            var cu = bookBLL.TimTheoMa(bookId) ?? throw new ValidationException("Không tìm thấy đầu sách cần sửa.");
            var moi = DocSach(); if (!KiemTraSach(moi)) return;
            if (cu.MaSach == moi.MaSach && cu.Title == moi.Title && cu.Author == moi.Author
                && (cu.Publisher ?? "") == moi.Publisher && cu.Price == moi.Price
                && cu.PublicationYear == moi.PublicationYear && cu.CategoryId == moi.CategoryId)
            { ThongBaoChuaDoi(); return; }
            if (!XacNhan($"Bạn có muốn lưu thay đổi của đầu sách {cu.MaSach}?")) return;
            bookBLL.Sua(moi);
            HoanTatLuu($"Đã lưu chỉnh sửa đầu sách {moi.MaSach}.", () => ChonTheoMa(moi.BookId));
        }

        private void XoaSach()
        {
            var book = bookBLL.TimTheoMa(bookId) ?? throw new ValidationException("Không tìm thấy đầu sách cần xóa.");
            if (!XacNhan($"Bạn có chắc muốn xóa đầu sách {book.MaSach} - {book.Title}?")) return;
            bookBLL.Xoa(book.BookId);
            HoanTatLuu($"Đã xóa đầu sách {book.MaSach}.", LamTrong);
        }

        // Sau khi tải lại danh sách, chọn lại đúng dòng vừa thao tác.
        private void ChonTheoMa(int id)
        {
            foreach (DataGridViewRow dong in dgvBooks.Rows)
                if (dong.Tag is Book book && book.BookId == id)
                {
                    dangTaiLuoi = true;
                    try { dong.Selected = true; dgvBooks.CurrentCell = dong.Cells[0]; }
                    finally { dangTaiLuoi = false; }
                    ChonSach(book);
                    return;
                }
            LamTrong();
        }

        // ---------- Bản sao ----------

        private void NhapBoSungBanSao()
        {
            if (bookId == 0) { HienThongBao("Hãy chọn một đầu sách trước khi nhập bản sao.", true); return; }
            int soLuong = (int)nudSoBanNhap.Value;
            string trangThai = cboCopyStatus.Text.Trim();
            string tinhTrang = txtCopyCondition.Text.Trim();
            if (tinhTrang.Length == 0)
            {
                errorProvider.SetError(txtCopyCondition, "Hãy nhập tình trạng vật lý của bản sao.");
                txtCopyCondition.Focus();
                return;
            }
            if (!XacNhan($"Nhập thêm {soLuong} bản sao cho đầu sách {sachDangChon?.MaSach}?")) return;
            var moi = copyBLL.Them(bookId, soLuong, trangThai, tinhTrang);
            HoanTatLuu($"Đã nhập thêm {moi.Count} bản sao ({moi[0].MaBanSao} ... {moi[^1].MaBanSao}).",
                () => ChonTheoMa(bookId));
        }

        private void CapNhatBanSao()
        {
            if (copyId == 0) { HienThongBao("Hãy chọn một bản sao trong danh sách.", true); return; }
            var cu = copyBLL.TimTheoMa(copyId) ?? throw new ValidationException("Không tìm thấy bản sao cần sửa.");
            string trangThai = cboCopyStatus.Text.Trim();
            string tinhTrang = txtCopyCondition.Text.Trim();
            if (cu.Status == trangThai && cu.Condition == tinhTrang) { ThongBaoChuaDoi(); return; }
            if (!XacNhan($"Bạn có muốn cập nhật bản sao {cu.MaBanSao}?")) return;
            copyBLL.Sua(new BookCopy { BookCopyId = copyId, Status = trangThai, Condition = tinhTrang });
            HoanTatLuu($"Đã cập nhật bản sao {cu.MaBanSao}.", () => ChonTheoMa(bookId));
        }

        private void XoaBanSao()
        {
            if (copyId == 0) { HienThongBao("Hãy chọn một bản sao trong danh sách.", true); return; }
            var cu = copyBLL.TimTheoMa(copyId) ?? throw new ValidationException("Không tìm thấy bản sao cần xóa.");
            if (!XacNhan($"Bạn có chắc muốn xóa bản sao {cu.MaBanSao}?")) return;
            copyBLL.Xoa(copyId);
            HoanTatLuu($"Đã xóa bản sao {cu.MaBanSao}.", () => ChonTheoMa(bookId));
        }

        // ---------- Dùng chung ----------

        // ComboBox dạng DropDownList chỉ chọn được giá trị có sẵn; thêm vào danh sách nếu DB có trạng thái khác.
        private void ChonTrangThai(string trangThai)
        {
            if (!cboCopyStatus.Items.Contains(trangThai)) cboCopyStatus.Items.Add(trangThai);
            cboCopyStatus.SelectedItem = trangThai;
        }

        private void CapNhatTrangThaiNut()
        {
            btnBookAdd.Enabled = bookId == 0;
            btnBookSave.Enabled = btnBookDelete.Enabled = bookId > 0;
            btnNhapBoSung.Enabled = btnCopyAdd.Enabled = bookId > 0;
            btnCopySave.Enabled = btnCopyDelete.Enabled = copyId > 0;
        }

        private bool KiemTraNhap(object doiTuong, Dictionary<string, Control> oNhap)
        {
            errorProvider.Clear();
            var loi = new List<ValidationResult>();
            Validator.TryValidateObject(doiTuong, new ValidationContext(doiTuong), loi, true);
            Control? oDauTien = null;
            foreach (var ketQua in loi)
                foreach (string ten in ketQua.MemberNames)
                    if (oNhap.TryGetValue(ten, out Control? control))
                    {
                        errorProvider.SetError(control, ketQua.ErrorMessage);
                        oDauTien ??= control;
                    }
            oDauTien?.Focus();
            return loi.Count == 0;
        }

        private void HoanTatLuu(string thongBao, Action dienLai)
        {
            // Lệnh ghi đã thành công. Nếu tải lại lỗi thì không báo nhầm là ghi thất bại.
            try { TaiDanhMuc(); HienThiSach(); dienLai(); }
            catch (Exception ex)
            {
                HienThongBao(thongBao + "\nChưa tải lại được giao diện. " + DataHelper.LayThongBaoLoi(ex), true);
                return;
            }
            HienThongBao(thongBao);
        }

        private void ThongBaoChuaDoi() => HienThongBao("Bạn chưa thay đổi thông tin.");
        protected virtual void HienThongBao(string noiDung, bool canhBao = false) => MessageBox.Show(noiDung,
            "Thông báo", MessageBoxButtons.OK, canhBao ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        protected virtual bool XacNhan(string noiDung) => MessageBox.Show(noiDung, "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        private void ThucHienAnToan(Action thaoTac)
        {
            try { thaoTac(); }
            catch (Exception ex)
            {
                // View bắt ngoại lệ do Class Library ném lên; không in chuỗi kết nối/mật khẩu.
                HienThongBao(DataHelper.LayThongBaoLoi(ex), true);
            }
        }
    }
}

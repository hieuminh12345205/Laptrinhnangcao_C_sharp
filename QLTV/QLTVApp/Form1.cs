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
        private int bookId, categoryId, copyId; // 0: thêm mới; > 0: đang chọn bản ghi.

        public Form1() : this(null) { }

        // Cho bộ kiểm tra truyền database tạm; khi chạy bình thường dùng mysql.local.json.
        public Form1(Func<BookDbContext>? taoContext)
        {
            categoryBLL = new CategoryBLL(taoContext);
            bookBLL = new BookBLL(taoContext);
            copyBLL = new BookCopyBLL(taoContext);
            InitializeComponent();
            cboCopyStatus.Items.AddRange(new object[] { "Có sẵn", "Đang mượn", "Bảo trì", "Thất lạc" });
            nudYear.Maximum = DateTime.Today.Year;

            Load += (_, _) => ThucHienAnToan(() =>
            {
                // Mở View Designer không truy cập database.
                if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
                TaiLaiDuLieu();
                LamMoiSach(); LamMoiDanhMuc(); LamMoiBanSach();
                // Database mới chưa có danh mục: bắt đầu ở tab cần nhập trước.
                if (cboCategory.Items.Count == 0) tabsMain.SelectedTab = tabCategories;
                BeginInvoke(new Action(() =>
                {
                    if (tabsMain.SelectedTab == tabCategories) txtDescription.Focus();
                    else txtTitle.Focus();
                }));
            });
            btnReload.Click += (_, _) => ThucHienAnToan(TaiLaiDuLieu);
            btnClose.Click += (_, _) => Close();
            FormClosing += (_, e) =>
            {
                if (!XacNhan("Bạn có muốn đóng ứng dụng? Thông tin đang nhập chưa lưu sẽ mất.")) e.Cancel = true;
            };
            tabsMain.SelectedIndexChanged += (_, _) =>
            {
                if (tabsMain.SelectedTab == tabBooks) txtTitle.Focus();
                else if (tabsMain.SelectedTab == tabCategories) txtDescription.Focus();
                else cboCopyBook.Focus();
            };

            btnBookAdd.Click += (_, _) => ThucHienAnToan(ThemSach);
            btnBookSave.Click += (_, _) => ThucHienAnToan(SuaSach);
            btnBookDelete.Click += (_, _) => ThucHienAnToan(XoaSach);
            btnBookNew.Click += (_, _) => LamMoiSach();
            btnBookSearch.Click += (_, _) => ThucHienAnToan(HienThiSach);
            btnBookAll.Click += (_, _) => ThucHienAnToan(() =>
            {
                txtBookSearch.Clear(); cboBookFilter.SelectedValue = 0; HienThiSach();
            });
            txtBookSearch.KeyDown += (_, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                ThucHienAnToan(HienThiSach);
            };
            dgvBooks.CellClick += (_, e) => ThucHienAnToan(() =>
            {
                if (e.RowIndex < 0) return;
                int id = Convert.ToInt32(dgvBooks.Rows[e.RowIndex].Cells["colBookId"].Value);
                DienSach(bookBLL.TimTheoMa(id) ?? throw new ValidationException("Đầu sách này không còn tồn tại."));
            });

            btnCategoryAdd.Click += (_, _) => ThucHienAnToan(ThemDanhMuc);
            btnCategorySave.Click += (_, _) => ThucHienAnToan(SuaDanhMuc);
            btnCategoryDelete.Click += (_, _) => ThucHienAnToan(XoaDanhMuc);
            btnCategoryNew.Click += (_, _) => LamMoiDanhMuc();
            dgvCategories.CellClick += (_, e) => ThucHienAnToan(() =>
            {
                if (e.RowIndex < 0) return;
                int id = Convert.ToInt32(dgvCategories.Rows[e.RowIndex].Cells["colCategoryId"].Value);
                DienDanhMuc(categoryBLL.TimTheoMa(id) ?? throw new ValidationException("Danh mục này không còn tồn tại."));
            });

            btnCopyAdd.Click += (_, _) => ThucHienAnToan(ThemBanSach);
            btnCopySave.Click += (_, _) => ThucHienAnToan(SuaBanSach);
            btnCopyDelete.Click += (_, _) => ThucHienAnToan(XoaBanSach);
            btnCopyNew.Click += (_, _) => LamMoiBanSach();
            btnCopyFilter.Click += (_, _) => ThucHienAnToan(HienThiBanSach);
            btnCopyAll.Click += (_, _) => ThucHienAnToan(() =>
            {
                cboCopyFilter.SelectedValue = 0; HienThiBanSach();
            });
            dgvCopies.CellClick += (_, e) => ThucHienAnToan(() =>
            {
                if (e.RowIndex < 0) return;
                int id = Convert.ToInt32(dgvCopies.Rows[e.RowIndex].Cells["colCopyId"].Value);
                DienBanSach(copyBLL.TimTheoMa(id) ?? throw new ValidationException("Bản sách này không còn tồn tại."));
            });

            // Đổi nội dung ô nhập thì xóa dấu báo lỗi của ô đó.
            foreach (Control control in new Control[] { txtTitle, txtAuthor, txtPublisher, txtDescription, txtCondition })
                control.TextChanged += (_, _) => errorProvider.SetError(control, "");
            foreach (ComboBox control in new[] { cboCategory, cboCopyBook, cboCopyStatus })
                control.SelectedIndexChanged += (_, _) => errorProvider.SetError(control, "");
            foreach (NumericUpDown control in new[] { nudPrice, nudYear, nudTotalCopies })
                control.ValueChanged += (_, _) => errorProvider.SetError(control, "");
            LamMoiSach(); LamMoiDanhMuc(); LamMoiBanSach();
        }

        private static int? MaLoc(ComboBox combo)
            => combo.SelectedValue is int id && id > 0 ? id : null;

        private static void ChonMa(ComboBox combo, int? id)
        {
            combo.SelectedIndex = -1;
            if (id.HasValue) combo.SelectedValue = id.Value;
        }

        private void TaiLaiDuLieu()
        {
            // Mỗi ComboBox dùng một List riêng để không tự thay đổi lựa chọn của nhau.
            int? danhMuc = (cboCategory.SelectedItem as Category)?.CategoryId;
            int? dauSach = (cboCopyBook.SelectedItem as Book)?.BookId;
            int locDanhMuc = MaLoc(cboBookFilter) ?? 0;
            int locDauSach = MaLoc(cboCopyFilter) ?? 0;
            var categories = categoryBLL.LayDanhSach();
            var books = bookBLL.LayDanhSach();

            cboCategory.DisplayMember = nameof(Category.Description);
            cboCategory.ValueMember = nameof(Category.CategoryId);
            cboCategory.DataSource = categories.ToList();
            ChonMa(cboCategory, danhMuc);

            var categoriesFilter = new List<Category> { new() { CategoryId = 0, Description = "Tất cả danh mục" } };
            categoriesFilter.AddRange(categories);
            cboBookFilter.DisplayMember = nameof(Category.Description);
            cboBookFilter.ValueMember = nameof(Category.CategoryId);
            cboBookFilter.DataSource = categoriesFilter;
            cboBookFilter.SelectedValue = categories.Any(x => x.CategoryId == locDanhMuc) ? locDanhMuc : 0;

            cboCopyBook.DisplayMember = nameof(Book.Title);
            cboCopyBook.ValueMember = nameof(Book.BookId);
            cboCopyBook.DataSource = books.ToList();
            ChonMa(cboCopyBook, dauSach);

            var booksFilter = new List<Book> { new() { BookId = 0, Title = "Tất cả đầu sách" } };
            booksFilter.AddRange(books);
            cboCopyFilter.DisplayMember = nameof(Book.Title);
            cboCopyFilter.ValueMember = nameof(Book.BookId);
            cboCopyFilter.DataSource = booksFilter;
            cboCopyFilter.SelectedValue = books.Any(x => x.BookId == locDauSach) ? locDauSach : 0;

            dgvCategories.DataSource = categories.Select(x => new { x.CategoryId, x.Description }).ToList();
            dgvCategories.ClearSelection();
            lblCategoryCount.Text = $"Số danh mục: {categories.Count}";
            HienThiSach(); HienThiBanSach();
            if (bookId > 0) lblRegisteredCount.Text = (books.FirstOrDefault(x => x.BookId == bookId)?.BookCopies.Count ?? 0).ToString();
        }

        private void HienThiSach()
        {
            var books = bookBLL.LayDanhSach(txtBookSearch.Text, MaLoc(cboBookFilter));
            dgvBooks.DataSource = books.Select(x => new
            {
                x.BookId, x.Title, x.Author, x.Publisher, x.PublicationYear, x.Price, x.TotalCopies,
                RegisteredCopies = x.BookCopies.Count, CategoryDescription = x.Category.Description
            }).ToList();
            dgvBooks.ClearSelection();
            lblBookCount.Text = $"Số đầu sách hiển thị: {books.Count}";
        }

        private void HienThiBanSach()
        {
            var copies = copyBLL.LayDanhSach(MaLoc(cboCopyFilter));
            dgvCopies.DataSource = copies.Select(x => new { x.BookCopyId, x.BookId, Title = x.Book.Title, x.Status, x.Condition }).ToList();
            dgvCopies.ClearSelection();
            lblCopyCount.Text = $"Số bản sách hiển thị: {copies.Count}";
        }

        private Book DocSach() => new()
        {
            BookId = bookId, Title = txtTitle.Text.Trim(), Author = txtAuthor.Text.Trim(),
            Publisher = txtPublisher.Text.Trim(), Price = nudPrice.Value,
            PublicationYear = (int)nudYear.Value, TotalCopies = (int)nudTotalCopies.Value,
            CategoryId = (cboCategory.SelectedItem as Category)?.CategoryId ?? 0
        };

        private void DienSach(Book book)
        {
            KiemTraDuLieu.KiemTra(book); // Kiểm tra trước khi gán giá trị vào NumericUpDown.
            errorProvider.Clear(); bookId = book.BookId;
            txtBookId.Text = book.BookId.ToString(); txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author ?? ""; txtPublisher.Text = book.Publisher ?? "";
            nudPrice.Value = book.Price; nudYear.Value = book.PublicationYear; nudTotalCopies.Value = book.TotalCopies;
            ChonMa(cboCategory, book.CategoryId);
            lblRegisteredCount.Text = book.BookCopies.Count.ToString();
            TrangThaiNut(btnBookAdd, btnBookSave, btnBookDelete, bookId);
        }

        private void LamMoiSach()
        {
            bookId = 0; errorProvider.Clear(); txtBookId.Clear(); txtTitle.Clear(); txtAuthor.Clear(); txtPublisher.Clear();
            nudPrice.Value = 0; nudYear.Value = DateTime.Today.Year; nudTotalCopies.Value = 1;
            cboCategory.SelectedIndex = -1; lblRegisteredCount.Text = "0";
            TrangThaiNut(btnBookAdd, btnBookSave, btnBookDelete, 0);
            dgvBooks.ClearSelection(); txtTitle.Focus();
        }

        private bool KiemTraSach(Book book) => KiemTraNhap(book, new()
        {
            [nameof(Book.Title)] = txtTitle, [nameof(Book.Author)] = txtAuthor,
            [nameof(Book.Publisher)] = txtPublisher, [nameof(Book.Price)] = nudPrice,
            [nameof(Book.PublicationYear)] = nudYear, [nameof(Book.TotalCopies)] = nudTotalCopies,
            [nameof(Book.CategoryId)] = cboCategory
        });

        private void ThemSach()
        {
            var book = DocSach(); if (!KiemTraSach(book)) return;
            bookBLL.Them(book);
            HoanTatLuu("Đã thêm đầu sách.", () => { LamMoiSach(); DienSach(bookBLL.TimTheoMa(book.BookId)!); });
        }

        private void SuaSach()
        {
            var cu = bookBLL.TimTheoMa(bookId) ?? throw new ValidationException("Không tìm thấy đầu sách cần sửa.");
            var moi = DocSach(); if (!KiemTraSach(moi)) return;
            if (cu.Title == moi.Title && (cu.Author ?? "") == moi.Author && (cu.Publisher ?? "") == moi.Publisher
                && cu.Price == moi.Price && cu.TotalCopies == moi.TotalCopies
                && cu.PublicationYear == moi.PublicationYear && cu.CategoryId == moi.CategoryId)
            { ThongBaoChuaDoi(); return; }
            if (!XacNhan($"Bạn có muốn lưu thay đổi đầu sách {bookId}?")) return;
            bookBLL.Sua(moi);
            HoanTatLuu("Đã sửa đầu sách.", () => DienSach(bookBLL.TimTheoMa(bookId)!));
        }

        private void XoaSach()
        {
            var book = bookBLL.TimTheoMa(bookId) ?? throw new ValidationException("Không tìm thấy đầu sách cần xóa.");
            if (!XacNhan($"Bạn có chắc muốn xóa đầu sách {book.BookId} - {book.Title}?")) return;
            bookBLL.Xoa(book.BookId);
            HoanTatLuu("Đã xóa đầu sách.", LamMoiSach);
        }

        private void DienDanhMuc(Category category)
        {
            errorProvider.Clear(); categoryId = category.CategoryId;
            txtCategoryId.Text = categoryId.ToString(); txtDescription.Text = category.Description;
            TrangThaiNut(btnCategoryAdd, btnCategorySave, btnCategoryDelete, categoryId);
        }
        private void LamMoiDanhMuc()
        {
            categoryId = 0; errorProvider.Clear(); txtCategoryId.Clear(); txtDescription.Clear();
            TrangThaiNut(btnCategoryAdd, btnCategorySave, btnCategoryDelete, 0);
            dgvCategories.ClearSelection(); txtDescription.Focus();
        }
        private Category DocDanhMuc() => new() { CategoryId = categoryId, Description = txtDescription.Text.Trim() };
        private bool KiemTraDanhMuc(Category category)
            => KiemTraNhap(category, new() { [nameof(Category.Description)] = txtDescription });
        private void ThemDanhMuc()
        {
            var category = DocDanhMuc(); if (!KiemTraDanhMuc(category)) return;
            categoryBLL.Them(category);
            HoanTatLuu("Đã thêm danh mục.", () => { LamMoiDanhMuc(); DienDanhMuc(category); });
        }
        private void SuaDanhMuc()
        {
            var cu = categoryBLL.TimTheoMa(categoryId) ?? throw new ValidationException("Không tìm thấy danh mục cần sửa.");
            var moi = DocDanhMuc(); if (!KiemTraDanhMuc(moi)) return;
            if (cu.Description == moi.Description) { ThongBaoChuaDoi(); return; }
            if (!XacNhan($"Bạn có muốn sửa danh mục {categoryId}?")) return;
            categoryBLL.Sua(moi);
            HoanTatLuu("Đã sửa danh mục.", () => DienDanhMuc(moi));
        }
        private void XoaDanhMuc()
        {
            if (!XacNhan($"Bạn có chắc muốn xóa danh mục {categoryId}?")) return;
            categoryBLL.Xoa(categoryId);
            HoanTatLuu("Đã xóa danh mục.", LamMoiDanhMuc);
        }

        private BookCopy DocBanSach() => new()
        {
            BookCopyId = copyId, BookId = (cboCopyBook.SelectedItem as Book)?.BookId ?? 0,
            Status = cboCopyStatus.Text, Condition = txtCondition.Text.Trim()
        };
        private bool KiemTraBanSach(BookCopy copy) => KiemTraNhap(copy, new()
        {
            [nameof(BookCopy.BookId)] = cboCopyBook, [nameof(BookCopy.Status)] = cboCopyStatus,
            [nameof(BookCopy.Condition)] = txtCondition
        });
        private void DienBanSach(BookCopy copy)
        {
            errorProvider.Clear(); copyId = copy.BookCopyId; txtCopyId.Text = copyId.ToString();
            ChonMa(cboCopyBook, copy.BookId);
            // Giữ được cả trạng thái cũ đã được lưu bằng chương trình khác.
            if (!cboCopyStatus.Items.Contains(copy.Status)) cboCopyStatus.Items.Add(copy.Status);
            cboCopyStatus.SelectedItem = copy.Status; txtCondition.Text = copy.Condition;
            TrangThaiNut(btnCopyAdd, btnCopySave, btnCopyDelete, copyId);
        }
        private void LamMoiBanSach()
        {
            copyId = 0; errorProvider.Clear(); txtCopyId.Clear(); cboCopyBook.SelectedIndex = -1;
            cboCopyStatus.SelectedIndex = -1; txtCondition.Clear();
            TrangThaiNut(btnCopyAdd, btnCopySave, btnCopyDelete, 0);
            dgvCopies.ClearSelection(); cboCopyBook.Focus();
        }
        private void ThemBanSach()
        {
            var copy = DocBanSach(); if (!KiemTraBanSach(copy)) return;
            copyBLL.Them(copy);
            HoanTatLuu("Đã thêm bản sách.", () => { LamMoiBanSach(); DienBanSach(copy); });
        }
        private void SuaBanSach()
        {
            var cu = copyBLL.TimTheoMa(copyId) ?? throw new ValidationException("Không tìm thấy bản sách cần sửa.");
            var moi = DocBanSach(); if (!KiemTraBanSach(moi)) return;
            if (cu.BookId == moi.BookId && cu.Status == moi.Status && cu.Condition == moi.Condition)
            { ThongBaoChuaDoi(); return; }
            if (!XacNhan($"Bạn có muốn sửa bản sách {copyId}?")) return;
            copyBLL.Sua(moi);
            HoanTatLuu("Đã sửa bản sách.", () => DienBanSach(moi));
        }
        private void XoaBanSach()
        {
            if (!XacNhan($"Bạn có chắc muốn xóa bản sách {copyId}?")) return;
            copyBLL.Xoa(copyId);
            HoanTatLuu("Đã xóa bản sách.", LamMoiBanSach);
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

        private static void TrangThaiNut(Button them, Button sua, Button xoa, int id)
        { them.Enabled = id == 0; sua.Enabled = xoa.Enabled = id > 0; }

        private void HoanTatLuu(string thongBao, Action dienLai)
        {
            // Lệnh ghi đã thành công. Nếu tải lại lỗi thì không báo nhầm là ghi thất bại.
            try { TaiLaiDuLieu(); dienLai(); }
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

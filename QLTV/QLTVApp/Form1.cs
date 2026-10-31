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
        private int banSaoDangChon; // Dòng được chọn ở bảng bản sao dưới danh sách đầu sách.

        public Form1() : this(null) { }

        // Cho bộ kiểm tra truyền database tạm; khi chạy bình thường dùng mysql.local.json.
        public Form1(Func<BookDbContext>? taoContext)
        {
            categoryBLL = new CategoryBLL(taoContext);
            bookBLL = new BookBLL(taoContext);
            copyBLL = new BookCopyBLL(taoContext);
            InitializeComponent();
            CauHinhCotDuLieu();
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
            // Hai nút nhập bản sao cùng mở màn hình nhập, đã chọn sẵn đầu sách.
            btnBookCopies.Click += (_, _) => ThucHienAnToan(MoNhapBanSao);
            btnDetailCopyAdd.Click += (_, _) => ThucHienAnToan(MoNhapBanSao);
            btnDetailCopyEdit.Click += (_, _) => ThucHienAnToan(MoSuaBanSao);
            btnDetailCopyDelete.Click += (_, _) => ThucHienAnToan(() =>
            {
                DienBanSach(LayBanSaoDangChon());
                XoaBanSach();
            });
            btnBackToBooks.Click += (_, _) => tabsMain.SelectedTab = tabBooks;
            dgvBookCopies.CellClick += (_, e) =>
            {
                if (e.RowIndex < 0) return;
                banSaoDangChon = Convert.ToInt32(dgvBookCopies.Rows[e.RowIndex].Cells["colDetailCopyId"].Value);
                CapNhatNutBanSao();
            };
            dgvBookCopies.CellDoubleClick += (_, e) =>
            {
                if (e.RowIndex < 0) return;
                banSaoDangChon = Convert.ToInt32(dgvBookCopies.Rows[e.RowIndex].Cells["colDetailCopyId"].Value);
                ThucHienAnToan(MoSuaBanSao);
            };
            dgvBookCopies.KeyUp += (_, e) =>
            {
                if (dgvBookCopies.CurrentRow == null) return;
                banSaoDangChon = Convert.ToInt32(dgvBookCopies.CurrentRow.Cells["colDetailCopyId"].Value);
                CapNhatNutBanSao();
            };
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
            dgvBooks.KeyUp += (_, e) => ThucHienAnToan(() =>
            {
                if (e.KeyCode is not (Keys.Up or Keys.Down or Keys.Home or Keys.End or Keys.PageUp or Keys.PageDown)) return;
                if (dgvBooks.CurrentRow == null) return;
                int id = Convert.ToInt32(dgvBooks.CurrentRow.Cells["colBookId"].Value);
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

        private void CauHinhCotDuLieu()
        {
            // Đặt cấu hình tại đây để thao tác lưu Designer không làm mất tên cột.
            // Name dùng để tìm ô khi chọn dòng; DataPropertyName dùng để lấy dữ liệu.
            foreach (var grid in new[] { dgvBooks, dgvBookCopies, dgvCategories, dgvCopies })
            {
                grid.AutoGenerateColumns = false;
                grid.Columns.Clear();
                grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(194, 223, 237);
                grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(25, 57, 73);
                grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 195);
                grid.DefaultCellStyle.SelectionForeColor = Color.White;
            }

            ThemCot(dgvBooks, "colBookId", "BookId", "Mã sách", 75);
            ThemCot(dgvBooks, "colTitle", "Title", "Tên đầu sách", 220);
            ThemCot(dgvBooks, "colCategory", "CategoryDescription", "Thể loại", 140);
            ThemCot(dgvBooks, "colAuthor", "Author", "Tác giả", 150);
            ThemCot(dgvBooks, "colPublisher", "Publisher", "Nhà xuất bản", 150);
            ThemCot(dgvBooks, "colPrice", "Price", "Giá sách", 115, dinhDang: "N2");
            ThemCot(dgvBooks, "colYear", "PublicationYear", "Năm XB", 85);
            ThemCot(dgvBooks, "colTotalCopies", "TotalCopies", "Số bản sao", 90);
            ThemCot(dgvBooks, "colRegistered", "RegisteredCopies", "Bản sao đã có", 115);

            ThemCot(dgvBookCopies, "colDetailCopyId", "BookCopyId", "Mã bản sao", 150);
            ThemCot(dgvBookCopies, "colDetailStatus", "Status", "Trạng thái", 200, lapDay: true);
            ThemCot(dgvBookCopies, "colDetailCondition", "Condition", "Tình trạng vật lý", 200, lapDay: true);

            ThemCot(dgvCategories, "colCategoryId", "CategoryId", "Mã thể loại", 150);
            ThemCot(dgvCategories, "colDescription", "Description", "Thể loại", 400, lapDay: true);

            ThemCot(dgvCopies, "colCopyId", "BookCopyId", "Mã bản sao", 120);
            ThemCot(dgvCopies, "colCopyBookId", "BookId", "Mã đầu sách", 120);
            ThemCot(dgvCopies, "colCopyTitle", "Title", "Đầu sách", 350);
            ThemCot(dgvCopies, "colCopyStatus", "Status", "Trạng thái", 180);
            ThemCot(dgvCopies, "colCopyCondition", "Condition", "Tình trạng vật lý", 230);
        }

        private static void ThemCot(DataGridView grid, string tenCot, string thuocTinh,
            string tieuDe, int chieuRong, bool lapDay = false, string dinhDang = "")
        {
            var cot = new DataGridViewTextBoxColumn
            {
                Name = tenCot, DataPropertyName = thuocTinh, HeaderText = tieuDe,
                Width = chieuRong, MinimumWidth = 60, SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = lapDay ? DataGridViewAutoSizeColumnMode.Fill : DataGridViewAutoSizeColumnMode.None
            };
            cot.DefaultCellStyle.Format = dinhDang;
            grid.Columns.Add(cot);
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

            var categoriesFilter = new List<Category> { new() { CategoryId = 0, Description = "-- Tất cả thể loại --" } };
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
            lblCategoryCount.Text = $"Số thể loại: {categories.Count}";
            HienThiSach(); HienThiBanSach();
            HienThiBanSaoCuaSach();
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
            lblBookCount.Text = $"Đã tải {books.Count} đầu sách lúc {DateTime.Now:HH:mm:ss}";
            // Khi kết quả lọc không còn sách đang chọn, bỏ cả thông tin và bản sao cũ.
            if (bookId > 0 && !books.Any(x => x.BookId == bookId)) LamMoiSach();
            foreach (DataGridViewRow row in dgvBooks.Rows)
                if (Convert.ToInt32(row.Cells["colBookId"].Value) == bookId) row.Selected = true;
        }

        private void HienThiBanSach()
        {
            var copies = copyBLL.LayDanhSach(MaLoc(cboCopyFilter));
            dgvCopies.DataSource = copies.Select(x => new { x.BookCopyId, x.BookId, Title = x.Book.Title, x.Status, x.Condition }).ToList();
            dgvCopies.ClearSelection();
            lblCopyCount.Text = $"Số bản sao hiển thị: {copies.Count}";
        }

        private void HienThiBanSaoCuaSach()
        {
            // Không truyền null khi chưa chọn sách: null nghĩa là lấy TẤT CẢ bản sao.
            var copies = bookId > 0 ? copyBLL.LayDanhSach(bookId) : new List<BookCopy>();
            dgvBookCopies.DataSource = copies.Select(x => new { x.BookCopyId, x.Status, x.Condition }).ToList();
            dgvBookCopies.ClearSelection();
            if (!copies.Any(x => x.BookCopyId == banSaoDangChon)) banSaoDangChon = 0;
            foreach (DataGridViewRow row in dgvBookCopies.Rows)
                if (Convert.ToInt32(row.Cells["colDetailCopyId"].Value) == banSaoDangChon) row.Selected = true;
            lblRegisteredCount.Text = copies.Count.ToString();
            if (bookId > 0) nudTotalCopies.Value = copies.Count;
            grpBookCopies.Text = bookId > 0
                ? $"Danh sách bản sao của sách {bookId} ({copies.Count} bản sao)"
                : "Danh sách bản sao — chọn một đầu sách";
            CapNhatNutBanSao();
        }

        private void CapNhatNutBanSao()
        {
            btnBookCopies.Enabled = btnDetailCopyAdd.Enabled = bookId > 0;
            btnDetailCopyEdit.Enabled = btnDetailCopyDelete.Enabled = bookId > 0 && banSaoDangChon > 0;
        }

        private void MoNhapBanSao()
        {
            if (bookId == 0) throw new ValidationException("Hãy chọn đầu sách trước khi nhập bản sao.");
            if (bookBLL.TimTheoMa(bookId) == null) throw new ValidationException("Đầu sách này không còn tồn tại.");
            LamMoiBanSach();
            ChonMa(cboCopyBook, bookId);
            ChonMa(cboCopyFilter, bookId);
            cboCopyStatus.SelectedItem = "Có sẵn";
            txtCondition.Text = "Mới";
            HienThiBanSach();
            tabsMain.SelectedTab = tabCopies;
            cboCopyStatus.Focus();
        }

        private BookCopy LayBanSaoDangChon()
        {
            var copy = copyBLL.TimTheoMa(banSaoDangChon);
            if (copy == null || copy.BookId != bookId)
                throw new ValidationException("Hãy chọn một bản sao của đầu sách hiện tại.");
            return copy;
        }

        private void MoSuaBanSao()
        {
            DienBanSach(LayBanSaoDangChon());
            ChonMa(cboCopyFilter, bookId);
            HienThiBanSach();
            tabsMain.SelectedTab = tabCopies;
            cboCopyStatus.Focus();
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
            nudTotalCopies.Minimum = 0;
            nudTotalCopies.Enabled = false; // Thay đổi số lượng bằng thêm/xóa bản sao.
            txtBookId.Text = book.BookId.ToString(); txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author ?? ""; txtPublisher.Text = book.Publisher ?? "";
            nudPrice.Value = book.Price; nudYear.Value = book.PublicationYear; nudTotalCopies.Value = book.TotalCopies;
            ChonMa(cboCategory, book.CategoryId);
            lblRegisteredCount.Text = book.BookCopies.Count.ToString();
            TrangThaiNut(btnBookAdd, btnBookSave, btnBookDelete, bookId);
            HienThiBanSaoCuaSach();
        }

        private void LamMoiSach()
        {
            bookId = 0; errorProvider.Clear(); txtBookId.Clear(); txtTitle.Clear(); txtAuthor.Clear(); txtPublisher.Clear();
            nudTotalCopies.Minimum = 1;
            nudTotalCopies.Enabled = true;
            nudPrice.Value = 0; nudYear.Value = DateTime.Today.Year; nudTotalCopies.Value = 1;
            cboCategory.SelectedIndex = -1; lblRegisteredCount.Text = "0";
            TrangThaiNut(btnBookAdd, btnBookSave, btnBookDelete, 0);
            banSaoDangChon = 0;
            HienThiBanSaoCuaSach();
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
            HoanTatLuu($"Đã thêm đầu sách và {book.TotalCopies} bản sao.", () => { LamMoiSach(); DienSach(bookBLL.TimTheoMa(book.BookId)!); });
        }

        private void SuaSach()
        {
            var moi = DocSach(); if (!KiemTraSach(moi)) return;
            if (!bookBLL.CoThayDoi(moi))
            { ThongBaoChuaDoi(); return; }
            if (!XacNhan($"Bạn có muốn lưu thay đổi đầu sách {bookId}?")) return;
            bookBLL.Sua(moi);
            HoanTatLuu("Đã sửa đầu sách.", () =>
            {
                // Đổi tên/thể loại có thể khiến sách không còn khớp bộ lọc hiện tại.
                // Khi đó HienThiSach đã làm trống, không tải lại bằng mã 0.
                if (bookId == moi.BookId) DienSach(bookBLL.TimTheoMa(moi.BookId)!);
            });
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
            // Form hiện nhập từng cuốn; Business cũng hỗ trợ nhập cả đợt khi cần.
            copy = bookBLL.NhapBoSungBanSao(copy.BookId, 1, copy.Status, copy.Condition).Single();
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

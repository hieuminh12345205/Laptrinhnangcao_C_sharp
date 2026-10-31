#nullable enable
namespace QLTVApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer? components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            errorProvider = new ErrorProvider(components);
            layoutRoot = new TableLayoutPanel();
            pnlHeader = new Panel();
            lblHeading = new Label();
            lblSubtitle = new Label();
            tabsMain = new TabControl();
            tabBooks = new TabPage();
            layoutBooks = new TableLayoutPanel();
            layoutSearch = new TableLayoutPanel();
            lblBookSearch = new Label();
            txtBookSearch = new TextBox();
            lblBookFilter = new Label();
            cboBookFilter = new ComboBox();
            btnBookSearch = new Button();
            btnBookAll = new Button();
            layoutMaster = new TableLayoutPanel();
            grpBookInfo = new GroupBox();
            layoutBookInfo = new TableLayoutPanel();
            layoutFields = new TableLayoutPanel();
            lblBookId = new Label();
            lblTitle = new Label();
            lblAuthor = new Label();
            lblPublisher = new Label();
            lblCategory = new Label();
            lblPrice = new Label();
            lblYear = new Label();
            lblTotalCopies = new Label();
            lblRegisteredTitle = new Label();
            txtBookId = new TextBox();
            txtTitle = new TextBox();
            txtAuthor = new TextBox();
            txtPublisher = new TextBox();
            cboCategory = new ComboBox();
            nudPriceHost = new Panel();
            nudPrice = new NumericUpDown();
            nudYearHost = new Panel();
            nudYear = new NumericUpDown();
            nudTotalCopiesHost = new Panel();
            nudTotalCopies = new NumericUpDown();
            lblRegisteredCount = new Label();
            layoutBookActions = new TableLayoutPanel();
            btnBookCopies = new Button();
            btnBookAdd = new Button();
            btnBookSave = new Button();
            btnBookDelete = new Button();
            btnBookNew = new Button();
            layoutLists = new TableLayoutPanel();
            grpBooks = new GroupBox();
            dgvBooks = new DataGridView();
            grpBookCopies = new GroupBox();
            layoutBookCopies = new TableLayoutPanel();
            dgvBookCopies = new DataGridView();
            lblCopyHint = new Label();
            layoutDetailActions = new TableLayoutPanel();
            btnDetailCopyAdd = new Button();
            btnDetailCopyEdit = new Button();
            btnDetailCopyDelete = new Button();
            tabCategories = new TabPage();
            layoutCategories = new TableLayoutPanel();
            layoutCategoryFields = new TableLayoutPanel();
            lblCategoryId = new Label();
            txtCategoryId = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            layoutCategoryActions = new TableLayoutPanel();
            btnCategoryAdd = new Button();
            btnCategorySave = new Button();
            btnCategoryDelete = new Button();
            btnCategoryNew = new Button();
            dgvCategories = new DataGridView();
            lblCategoryCount = new Label();
            tabCopies = new TabPage();
            layoutCopies = new TableLayoutPanel();
            layoutCopyFields = new TableLayoutPanel();
            lblCopyId = new Label();
            txtCopyId = new TextBox();
            lblCopyBook = new Label();
            cboCopyBook = new ComboBox();
            lblCopyStatus = new Label();
            cboCopyStatus = new ComboBox();
            lblCondition = new Label();
            txtCondition = new TextBox();
            layoutCopyActions = new TableLayoutPanel();
            btnBackToBooks = new Button();
            btnCopyAdd = new Button();
            btnCopySave = new Button();
            btnCopyDelete = new Button();
            btnCopyNew = new Button();
            layoutCopyFilter = new TableLayoutPanel();
            lblCopyFilter = new Label();
            cboCopyFilter = new ComboBox();
            btnCopyFilter = new Button();
            btnCopyAll = new Button();
            dgvCopies = new DataGridView();
            lblCopyCount = new Label();
            layoutFooter = new TableLayoutPanel();
            lblBookCount = new Label();
            btnReload = new Button();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            layoutRoot.SuspendLayout();
            pnlHeader.SuspendLayout();
            tabsMain.SuspendLayout();
            tabBooks.SuspendLayout();
            layoutBooks.SuspendLayout();
            layoutSearch.SuspendLayout();
            layoutMaster.SuspendLayout();
            grpBookInfo.SuspendLayout();
            layoutBookInfo.SuspendLayout();
            layoutFields.SuspendLayout();
            nudPriceHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPrice).BeginInit();
            nudYearHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudYear).BeginInit();
            nudTotalCopiesHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTotalCopies).BeginInit();
            layoutBookActions.SuspendLayout();
            layoutLists.SuspendLayout();
            grpBooks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            grpBookCopies.SuspendLayout();
            layoutBookCopies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBookCopies).BeginInit();
            layoutDetailActions.SuspendLayout();
            tabCategories.SuspendLayout();
            layoutCategories.SuspendLayout();
            layoutCategoryFields.SuspendLayout();
            layoutCategoryActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            tabCopies.SuspendLayout();
            layoutCopies.SuspendLayout();
            layoutCopyFields.SuspendLayout();
            layoutCopyActions.SuspendLayout();
            layoutCopyFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCopies).BeginInit();
            layoutFooter.SuspendLayout();
            SuspendLayout();
            //
            // errorProvider
            //
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            //
            // layoutRoot
            //
            layoutRoot.ColumnCount = 1;
            layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutRoot.Controls.Add(pnlHeader, 0, 0);
            layoutRoot.Controls.Add(tabsMain, 0, 1);
            layoutRoot.Controls.Add(layoutFooter, 0, 2);
            layoutRoot.Dock = DockStyle.Fill;
            layoutRoot.Location = new Point(0, 0);
            layoutRoot.Margin = new Padding(0);
            layoutRoot.Name = "layoutRoot";
            layoutRoot.RowCount = 3;
            layoutRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 88F));
            layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            layoutRoot.Size = new Size(1240, 790);
            layoutRoot.TabIndex = 0;
            //
            // pnlHeader
            //
            pnlHeader.BackColor = Color.FromArgb(223, 241, 252);
            pnlHeader.Controls.Add(lblHeading);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1240, 88);
            pnlHeader.TabIndex = 1;
            //
            // lblHeading
            //
            lblHeading.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblHeading.ForeColor = Color.FromArgb(17, 80, 105);
            lblHeading.Location = new Point(22, 12);
            lblHeading.Name = "lblHeading";
            lblHeading.Size = new Size(1000, 40);
            lblHeading.TabIndex = 2;
            lblHeading.Text = "QUẢN LÝ ĐẦU SÁCH VÀ BẢN SAO";
            //
            // lblSubtitle
            //
            lblSubtitle.ForeColor = Color.FromArgb(85, 101, 111);
            lblSubtitle.Location = new Point(24, 55);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(1000, 27);
            lblSubtitle.TabIndex = 3;
            lblSubtitle.Text = "Tra cứu, cập nhật thông tin đầu sách và quản lý bản sao";
            //
            // tabsMain
            //
            tabsMain.Controls.Add(tabBooks);
            tabsMain.Controls.Add(tabCategories);
            tabsMain.Controls.Add(tabCopies);
            tabsMain.Dock = DockStyle.Fill;
            tabsMain.Location = new Point(0, 88);
            tabsMain.Margin = new Padding(0);
            tabsMain.Name = "tabsMain";
            tabsMain.SelectedIndex = 0;
            tabsMain.Size = new Size(1240, 670);
            tabsMain.TabIndex = 4;
            //
            // tabBooks
            //
            tabBooks.BackColor = Color.FromArgb(248, 249, 252);
            tabBooks.Controls.Add(layoutBooks);
            tabBooks.Location = new Point(4, 32);
            tabBooks.Name = "tabBooks";
            tabBooks.Padding = new Padding(5);
            tabBooks.Size = new Size(1232, 634);
            tabBooks.TabIndex = 5;
            tabBooks.Text = "Đầu sách và bản sao";
            //
            // layoutBooks
            //
            layoutBooks.ColumnCount = 1;
            layoutBooks.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBooks.Controls.Add(layoutSearch, 0, 0);
            layoutBooks.Controls.Add(layoutMaster, 0, 1);
            layoutBooks.Dock = DockStyle.Fill;
            layoutBooks.Location = new Point(5, 5);
            layoutBooks.Margin = new Padding(0);
            layoutBooks.Name = "layoutBooks";
            layoutBooks.RowCount = 2;
            layoutBooks.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            layoutBooks.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBooks.Size = new Size(1222, 624);
            layoutBooks.TabIndex = 12;
            //
            // layoutSearch
            //
            layoutSearch.BackColor = Color.FromArgb(239, 246, 250);
            layoutSearch.ColumnCount = 6;
            layoutSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 114F));
            layoutSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            layoutSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75F));
            layoutSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            layoutSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108F));
            layoutSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108F));
            layoutSearch.Controls.Add(lblBookSearch, 0, 0);
            layoutSearch.Controls.Add(txtBookSearch, 1, 0);
            layoutSearch.Controls.Add(lblBookFilter, 2, 0);
            layoutSearch.Controls.Add(cboBookFilter, 3, 0);
            layoutSearch.Controls.Add(btnBookSearch, 4, 0);
            layoutSearch.Controls.Add(btnBookAll, 5, 0);
            layoutSearch.Dock = DockStyle.Fill;
            layoutSearch.Location = new Point(0, 0);
            layoutSearch.Margin = new Padding(0);
            layoutSearch.Name = "layoutSearch";
            layoutSearch.Padding = new Padding(7, 5, 4, 7);
            layoutSearch.RowCount = 1;
            layoutSearch.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutSearch.Size = new Size(1222, 58);
            layoutSearch.TabIndex = 13;
            //
            // lblBookSearch
            //
            lblBookSearch.Dock = DockStyle.Fill;
            lblBookSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBookSearch.Location = new Point(10, 5);
            lblBookSearch.Name = "lblBookSearch";
            lblBookSearch.Size = new Size(108, 46);
            lblBookSearch.TabIndex = 14;
            lblBookSearch.Text = "Từ khóa tra";
            lblBookSearch.TextAlign = ContentAlignment.MiddleLeft;
            //
            // txtBookSearch
            //
            txtBookSearch.Dock = DockStyle.Fill;
            txtBookSearch.Location = new Point(125, 10);
            txtBookSearch.Margin = new Padding(4, 5, 9, 4);
            txtBookSearch.Name = "txtBookSearch";
            txtBookSearch.PlaceholderText = "Mã sách, tên sách, tác giả...";
            txtBookSearch.Size = new Size(430, 30);
            txtBookSearch.TabIndex = 15;
            //
            // lblBookFilter
            //
            lblBookFilter.Dock = DockStyle.Fill;
            lblBookFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBookFilter.Location = new Point(567, 5);
            lblBookFilter.Name = "lblBookFilter";
            lblBookFilter.Size = new Size(69, 46);
            lblBookFilter.TabIndex = 16;
            lblBookFilter.Text = "Thể loại";
            lblBookFilter.TextAlign = ContentAlignment.MiddleLeft;
            //
            // cboBookFilter
            //
            cboBookFilter.Dock = DockStyle.Fill;
            cboBookFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBookFilter.Location = new Point(643, 10);
            cboBookFilter.Margin = new Padding(4, 5, 9, 4);
            cboBookFilter.Name = "cboBookFilter";
            cboBookFilter.Size = new Size(349, 31);
            cboBookFilter.TabIndex = 17;
            //
            // btnBookSearch
            //
            btnBookSearch.BackColor = Color.FromArgb(0, 137, 186);
            btnBookSearch.Dock = DockStyle.Fill;
            btnBookSearch.FlatAppearance.BorderSize = 0;
            btnBookSearch.FlatStyle = FlatStyle.Flat;
            btnBookSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBookSearch.ForeColor = Color.White;
            btnBookSearch.Location = new Point(1004, 8);
            btnBookSearch.MinimumSize = new Size(0, 32);
            btnBookSearch.Name = "btnBookSearch";
            btnBookSearch.Size = new Size(102, 40);
            btnBookSearch.TabIndex = 18;
            btnBookSearch.Text = "Tìm kiếm";
            btnBookSearch.UseVisualStyleBackColor = false;
            //
            // btnBookAll
            //
            btnBookAll.Dock = DockStyle.Fill;
            btnBookAll.Location = new Point(1112, 8);
            btnBookAll.MinimumSize = new Size(0, 32);
            btnBookAll.Name = "btnBookAll";
            btnBookAll.Size = new Size(103, 40);
            btnBookAll.TabIndex = 19;
            btnBookAll.Text = "Làm mới";
            btnBookAll.UseVisualStyleBackColor = true;
            //
            // layoutMaster
            //
            layoutMaster.ColumnCount = 2;
            layoutMaster.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 380F));
            layoutMaster.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMaster.Controls.Add(grpBookInfo, 0, 0);
            layoutMaster.Controls.Add(layoutLists, 1, 0);
            layoutMaster.Dock = DockStyle.Fill;
            layoutMaster.Location = new Point(0, 58);
            layoutMaster.Margin = new Padding(0);
            layoutMaster.Name = "layoutMaster";
            layoutMaster.RowCount = 1;
            layoutMaster.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutMaster.Size = new Size(1222, 566);
            layoutMaster.TabIndex = 20;
            //
            // grpBookInfo
            //
            grpBookInfo.Controls.Add(layoutBookInfo);
            grpBookInfo.Dock = DockStyle.Fill;
            grpBookInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpBookInfo.Location = new Point(5, 5);
            grpBookInfo.Margin = new Padding(5);
            grpBookInfo.Name = "grpBookInfo";
            grpBookInfo.Padding = new Padding(9);
            grpBookInfo.Size = new Size(370, 556);
            grpBookInfo.TabIndex = 21;
            grpBookInfo.TabStop = false;
            grpBookInfo.Text = "Thông tin đầu sách";
            //
            // layoutBookInfo
            //
            layoutBookInfo.ColumnCount = 1;
            layoutBookInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBookInfo.Controls.Add(layoutFields, 0, 0);
            layoutBookInfo.Controls.Add(layoutBookActions, 0, 2);
            layoutBookInfo.Dock = DockStyle.Fill;
            layoutBookInfo.Font = new Font("Segoe UI", 10F);
            layoutBookInfo.Location = new Point(9, 32);
            layoutBookInfo.Margin = new Padding(0);
            layoutBookInfo.Name = "layoutBookInfo";
            layoutBookInfo.RowCount = 3;
            layoutBookInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 350F));
            layoutBookInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBookInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 136F));
            layoutBookInfo.Size = new Size(352, 515);
            layoutBookInfo.TabIndex = 22;
            //
            // layoutFields
            //
            layoutFields.ColumnCount = 2;
            layoutFields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 134F));
            layoutFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutFields.Controls.Add(lblBookId, 0, 0);
            layoutFields.Controls.Add(lblTitle, 0, 1);
            layoutFields.Controls.Add(lblAuthor, 0, 2);
            layoutFields.Controls.Add(lblPublisher, 0, 3);
            layoutFields.Controls.Add(lblCategory, 0, 4);
            layoutFields.Controls.Add(lblPrice, 0, 5);
            layoutFields.Controls.Add(lblYear, 0, 6);
            layoutFields.Controls.Add(lblTotalCopies, 0, 7);
            layoutFields.Controls.Add(lblRegisteredTitle, 0, 8);
            layoutFields.Controls.Add(txtBookId, 1, 0);
            layoutFields.Controls.Add(txtTitle, 1, 1);
            layoutFields.Controls.Add(txtAuthor, 1, 2);
            layoutFields.Controls.Add(txtPublisher, 1, 3);
            layoutFields.Controls.Add(cboCategory, 1, 4);
            layoutFields.Controls.Add(nudPriceHost, 1, 5);
            layoutFields.Controls.Add(nudYearHost, 1, 6);
            layoutFields.Controls.Add(nudTotalCopiesHost, 1, 7);
            layoutFields.Controls.Add(lblRegisteredCount, 1, 8);
            layoutFields.Dock = DockStyle.Fill;
            layoutFields.Location = new Point(0, 0);
            layoutFields.Margin = new Padding(0);
            layoutFields.Name = "layoutFields";
            layoutFields.RowCount = 9;
            layoutFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutFields.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutFields.Size = new Size(352, 350);
            layoutFields.TabIndex = 23;
            //
            // lblBookId
            //
            lblBookId.Dock = DockStyle.Fill;
            lblBookId.Location = new Point(3, 0);
            lblBookId.Name = "lblBookId";
            lblBookId.Size = new Size(128, 38);
            lblBookId.TabIndex = 24;
            lblBookId.Text = "Mã sách:";
            lblBookId.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblTitle
            //
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Location = new Point(3, 38);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(128, 38);
            lblTitle.TabIndex = 25;
            lblTitle.Text = "Tên sách (*):";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblAuthor
            //
            lblAuthor.Dock = DockStyle.Fill;
            lblAuthor.Location = new Point(3, 76);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new Size(128, 38);
            lblAuthor.TabIndex = 26;
            lblAuthor.Text = "Tác giả:";
            lblAuthor.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblPublisher
            //
            lblPublisher.Dock = DockStyle.Fill;
            lblPublisher.Location = new Point(3, 114);
            lblPublisher.Name = "lblPublisher";
            lblPublisher.Size = new Size(128, 38);
            lblPublisher.TabIndex = 27;
            lblPublisher.Text = "Nhà xuất bản:";
            lblPublisher.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblCategory
            //
            lblCategory.Dock = DockStyle.Fill;
            lblCategory.Location = new Point(3, 152);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(128, 38);
            lblCategory.TabIndex = 28;
            lblCategory.Text = "Thể loại (*):";
            lblCategory.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblPrice
            //
            lblPrice.Dock = DockStyle.Fill;
            lblPrice.Location = new Point(3, 190);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(128, 38);
            lblPrice.TabIndex = 29;
            lblPrice.Text = "Giá sách:";
            lblPrice.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblYear
            //
            lblYear.Dock = DockStyle.Fill;
            lblYear.Location = new Point(3, 228);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(128, 38);
            lblYear.TabIndex = 30;
            lblYear.Text = "Năm xuất bản:";
            lblYear.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblTotalCopies
            //
            lblTotalCopies.Dock = DockStyle.Fill;
            lblTotalCopies.Location = new Point(3, 266);
            lblTotalCopies.Name = "lblTotalCopies";
            lblTotalCopies.Size = new Size(128, 38);
            lblTotalCopies.TabIndex = 31;
            lblTotalCopies.Text = "Số bản khai báo:";
            lblTotalCopies.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblRegisteredTitle
            //
            lblRegisteredTitle.Dock = DockStyle.Fill;
            lblRegisteredTitle.Location = new Point(3, 304);
            lblRegisteredTitle.Name = "lblRegisteredTitle";
            lblRegisteredTitle.Size = new Size(128, 46);
            lblRegisteredTitle.TabIndex = 32;
            lblRegisteredTitle.Text = "Bản sao đã có:";
            lblRegisteredTitle.TextAlign = ContentAlignment.MiddleLeft;
            //
            // txtBookId
            //
            txtBookId.Dock = DockStyle.Fill;
            txtBookId.Location = new Point(138, 5);
            txtBookId.Margin = new Padding(4, 5, 9, 4);
            txtBookId.Name = "txtBookId";
            txtBookId.PlaceholderText = "Tự tăng";
            txtBookId.ReadOnly = true;
            txtBookId.Size = new Size(205, 30);
            txtBookId.TabIndex = 33;
            txtBookId.TabStop = false;
            //
            // txtTitle
            //
            txtTitle.Dock = DockStyle.Fill;
            txtTitle.Location = new Point(138, 43);
            txtTitle.Margin = new Padding(4, 5, 9, 4);
            txtTitle.MaxLength = 200;
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(205, 30);
            txtTitle.TabIndex = 34;
            //
            // txtAuthor
            //
            txtAuthor.Dock = DockStyle.Fill;
            txtAuthor.Location = new Point(138, 81);
            txtAuthor.Margin = new Padding(4, 5, 9, 4);
            txtAuthor.MaxLength = 100;
            txtAuthor.Name = "txtAuthor";
            txtAuthor.Size = new Size(205, 30);
            txtAuthor.TabIndex = 35;
            //
            // txtPublisher
            //
            txtPublisher.Dock = DockStyle.Fill;
            txtPublisher.Location = new Point(138, 119);
            txtPublisher.Margin = new Padding(4, 5, 9, 4);
            txtPublisher.MaxLength = 150;
            txtPublisher.Name = "txtPublisher";
            txtPublisher.Size = new Size(205, 30);
            txtPublisher.TabIndex = 36;
            //
            // cboCategory
            //
            cboCategory.Dock = DockStyle.Fill;
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Location = new Point(138, 157);
            cboCategory.Margin = new Padding(4, 5, 9, 4);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(205, 31);
            cboCategory.TabIndex = 37;
            //
            // nudPriceHost
            //
            nudPriceHost.Controls.Add(nudPrice);
            nudPriceHost.Dock = DockStyle.Fill;
            nudPriceHost.Location = new Point(134, 190);
            nudPriceHost.Margin = new Padding(0);
            nudPriceHost.Name = "nudPriceHost";
            nudPriceHost.Padding = new Padding(4, 5, 9, 4);
            nudPriceHost.Size = new Size(218, 38);
            nudPriceHost.TabIndex = 38;
            //
            // nudPrice
            //
            nudPrice.DecimalPlaces = 2;
            nudPrice.Dock = DockStyle.Top;
            nudPrice.Font = new Font("Segoe UI", 10F);
            nudPrice.Location = new Point(4, 5);
            nudPrice.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            nudPrice.Name = "nudPrice";
            nudPrice.Size = new Size(205, 30);
            nudPrice.TabIndex = 39;
            nudPrice.TextAlign = HorizontalAlignment.Right;
            nudPrice.ThousandsSeparator = true;
            //
            // nudYearHost
            //
            nudYearHost.Controls.Add(nudYear);
            nudYearHost.Dock = DockStyle.Fill;
            nudYearHost.Location = new Point(134, 228);
            nudYearHost.Margin = new Padding(0);
            nudYearHost.Name = "nudYearHost";
            nudYearHost.Padding = new Padding(4, 5, 9, 4);
            nudYearHost.Size = new Size(218, 38);
            nudYearHost.TabIndex = 40;
            //
            // nudYear
            //
            nudYear.Dock = DockStyle.Top;
            nudYear.Font = new Font("Segoe UI", 10F);
            nudYear.Location = new Point(4, 5);
            nudYear.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            nudYear.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudYear.Name = "nudYear";
            nudYear.Size = new Size(205, 30);
            nudYear.TabIndex = 41;
            nudYear.TextAlign = HorizontalAlignment.Right;
            nudYear.Value = new decimal(new int[] { 2026, 0, 0, 0 });
            //
            // nudTotalCopiesHost
            //
            nudTotalCopiesHost.Controls.Add(nudTotalCopies);
            nudTotalCopiesHost.Dock = DockStyle.Fill;
            nudTotalCopiesHost.Location = new Point(134, 266);
            nudTotalCopiesHost.Margin = new Padding(0);
            nudTotalCopiesHost.Name = "nudTotalCopiesHost";
            nudTotalCopiesHost.Padding = new Padding(4, 5, 9, 4);
            nudTotalCopiesHost.Size = new Size(218, 38);
            nudTotalCopiesHost.TabIndex = 42;
            //
            // nudTotalCopies
            //
            nudTotalCopies.Dock = DockStyle.Top;
            nudTotalCopies.Font = new Font("Segoe UI", 10F);
            nudTotalCopies.Location = new Point(4, 5);
            nudTotalCopies.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudTotalCopies.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTotalCopies.Name = "nudTotalCopies";
            nudTotalCopies.Size = new Size(205, 30);
            nudTotalCopies.TabIndex = 43;
            nudTotalCopies.TextAlign = HorizontalAlignment.Right;
            nudTotalCopies.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // lblRegisteredCount
            //
            lblRegisteredCount.Dock = DockStyle.Fill;
            lblRegisteredCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRegisteredCount.ForeColor = Color.FromArgb(0, 120, 180);
            lblRegisteredCount.Location = new Point(137, 304);
            lblRegisteredCount.Name = "lblRegisteredCount";
            lblRegisteredCount.Padding = new Padding(0, 0, 12, 0);
            lblRegisteredCount.Size = new Size(212, 46);
            lblRegisteredCount.TabIndex = 44;
            lblRegisteredCount.Text = "0";
            lblRegisteredCount.TextAlign = ContentAlignment.MiddleRight;
            //
            // layoutBookActions
            //
            layoutBookActions.ColumnCount = 2;
            layoutBookActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBookActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutBookActions.Controls.Add(btnBookAdd, 0, 0);
            layoutBookActions.Controls.Add(btnBookSave, 1, 0);
            layoutBookActions.Controls.Add(btnBookDelete, 0, 1);
            layoutBookActions.Controls.Add(btnBookNew, 1, 1);
            layoutBookActions.Controls.Add(btnBookCopies, 0, 2);
            layoutBookActions.Dock = DockStyle.Fill;
            layoutBookActions.Location = new Point(0, 379);
            layoutBookActions.Margin = new Padding(0);
            layoutBookActions.Name = "layoutBookActions";
            layoutBookActions.RowCount = 3;
            layoutBookActions.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            layoutBookActions.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            layoutBookActions.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            layoutBookActions.Size = new Size(352, 136);
            layoutBookActions.TabIndex = 45;
            //
            // btnBookCopies
            //
            btnBookCopies.BackColor = Color.FromArgb(0, 137, 186);
            layoutBookActions.SetColumnSpan(btnBookCopies, 2);
            btnBookCopies.Dock = DockStyle.Fill;
            btnBookCopies.FlatAppearance.BorderSize = 0;
            btnBookCopies.FlatStyle = FlatStyle.Flat;
            btnBookCopies.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBookCopies.ForeColor = Color.White;
            btnBookCopies.Location = new Point(3, 91);
            btnBookCopies.MinimumSize = new Size(0, 32);
            btnBookCopies.Name = "btnBookCopies";
            btnBookCopies.Size = new Size(346, 42);
            btnBookCopies.TabIndex = 50;
            btnBookCopies.Text = "NHẬP BỔ SUNG BẢN SAO";
            btnBookCopies.UseVisualStyleBackColor = false;
            //
            // btnBookAdd
            //
            btnBookAdd.BackColor = Color.FromArgb(20, 161, 78);
            btnBookAdd.Dock = DockStyle.Fill;
            btnBookAdd.FlatAppearance.BorderSize = 0;
            btnBookAdd.FlatStyle = FlatStyle.Flat;
            btnBookAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBookAdd.ForeColor = Color.White;
            btnBookAdd.Location = new Point(3, 3);
            btnBookAdd.MinimumSize = new Size(0, 32);
            btnBookAdd.Name = "btnBookAdd";
            btnBookAdd.Size = new Size(170, 38);
            btnBookAdd.TabIndex = 46;
            btnBookAdd.Text = "Thêm đầu sách";
            btnBookAdd.UseVisualStyleBackColor = false;
            //
            // btnBookSave
            //
            btnBookSave.BackColor = Color.FromArgb(225, 133, 17);
            btnBookSave.Dock = DockStyle.Fill;
            btnBookSave.FlatAppearance.BorderSize = 0;
            btnBookSave.FlatStyle = FlatStyle.Flat;
            btnBookSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBookSave.ForeColor = Color.White;
            btnBookSave.Location = new Point(179, 3);
            btnBookSave.MinimumSize = new Size(0, 32);
            btnBookSave.Name = "btnBookSave";
            btnBookSave.Size = new Size(170, 38);
            btnBookSave.TabIndex = 47;
            btnBookSave.Text = "Lưu chỉnh sửa";
            btnBookSave.UseVisualStyleBackColor = false;
            //
            // btnBookDelete
            //
            btnBookDelete.BackColor = Color.FromArgb(213, 45, 48);
            btnBookDelete.Dock = DockStyle.Fill;
            btnBookDelete.FlatAppearance.BorderSize = 0;
            btnBookDelete.FlatStyle = FlatStyle.Flat;
            btnBookDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBookDelete.ForeColor = Color.White;
            btnBookDelete.Location = new Point(3, 47);
            btnBookDelete.MinimumSize = new Size(0, 32);
            btnBookDelete.Name = "btnBookDelete";
            btnBookDelete.Size = new Size(170, 38);
            btnBookDelete.TabIndex = 48;
            btnBookDelete.Text = "Xóa đầu sách";
            btnBookDelete.UseVisualStyleBackColor = false;
            //
            // btnBookNew
            //
            btnBookNew.Dock = DockStyle.Fill;
            btnBookNew.Location = new Point(179, 47);
            btnBookNew.MinimumSize = new Size(0, 32);
            btnBookNew.Name = "btnBookNew";
            btnBookNew.Size = new Size(170, 38);
            btnBookNew.TabIndex = 49;
            btnBookNew.Text = "Làm trống";
            btnBookNew.UseVisualStyleBackColor = true;
            //
            // layoutLists
            //
            layoutLists.ColumnCount = 1;
            layoutLists.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutLists.Controls.Add(grpBooks, 0, 0);
            layoutLists.Controls.Add(grpBookCopies, 0, 1);
            layoutLists.Dock = DockStyle.Fill;
            layoutLists.Location = new Point(380, 0);
            layoutLists.Margin = new Padding(0);
            layoutLists.Name = "layoutLists";
            layoutLists.RowCount = 2;
            layoutLists.RowStyles.Add(new RowStyle(SizeType.Percent, 51F));
            layoutLists.RowStyles.Add(new RowStyle(SizeType.Percent, 49F));
            layoutLists.Size = new Size(842, 566);
            layoutLists.TabIndex = 51;
            //
            // grpBooks
            //
            grpBooks.Controls.Add(dgvBooks);
            grpBooks.Dock = DockStyle.Fill;
            grpBooks.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpBooks.Location = new Point(5, 5);
            grpBooks.Margin = new Padding(5);
            grpBooks.Name = "grpBooks";
            grpBooks.Padding = new Padding(9);
            grpBooks.Size = new Size(832, 278);
            grpBooks.TabIndex = 52;
            grpBooks.TabStop = false;
            grpBooks.Text = "Danh sách đầu sách";
            //
            // dgvBooks
            //
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(245, 249, 252);
            dgvBooks.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvBooks.BackgroundColor = Color.White;
            dgvBooks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBooks.Dock = DockStyle.Fill;
            dgvBooks.EnableHeadersVisualStyles = false;
            dgvBooks.Font = new Font("Segoe UI", 10F);
            dgvBooks.Location = new Point(9, 32);
            dgvBooks.MultiSelect = false;
            dgvBooks.Name = "dgvBooks";
            dgvBooks.ReadOnly = true;
            dgvBooks.RowHeadersVisible = false;
            dgvBooks.RowHeadersWidth = 51;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.Size = new Size(814, 237);
            dgvBooks.TabIndex = 53;
            //
            // grpBookCopies
            //
            grpBookCopies.Controls.Add(layoutBookCopies);
            grpBookCopies.Dock = DockStyle.Fill;
            grpBookCopies.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpBookCopies.Location = new Point(5, 293);
            grpBookCopies.Margin = new Padding(5);
            grpBookCopies.Name = "grpBookCopies";
            grpBookCopies.Padding = new Padding(9);
            grpBookCopies.Size = new Size(832, 268);
            grpBookCopies.TabIndex = 63;
            grpBookCopies.TabStop = false;
            grpBookCopies.Text = "Danh sách bản sao — chọn một đầu sách";
            //
            // layoutBookCopies
            //
            layoutBookCopies.ColumnCount = 1;
            layoutBookCopies.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBookCopies.Controls.Add(dgvBookCopies, 0, 0);
            layoutBookCopies.Controls.Add(lblCopyHint, 0, 1);
            layoutBookCopies.Controls.Add(layoutDetailActions, 0, 2);
            layoutBookCopies.Dock = DockStyle.Fill;
            layoutBookCopies.Font = new Font("Segoe UI", 10F);
            layoutBookCopies.Location = new Point(9, 32);
            layoutBookCopies.Margin = new Padding(0);
            layoutBookCopies.Name = "layoutBookCopies";
            layoutBookCopies.RowCount = 3;
            layoutBookCopies.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutBookCopies.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            layoutBookCopies.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutBookCopies.Size = new Size(814, 227);
            layoutBookCopies.TabIndex = 64;
            //
            // dgvBookCopies
            //
            dgvBookCopies.AllowUserToAddRows = false;
            dgvBookCopies.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(245, 249, 252);
            dgvBookCopies.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            dgvBookCopies.BackgroundColor = Color.White;
            dgvBookCopies.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvBookCopies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBookCopies.Dock = DockStyle.Fill;
            dgvBookCopies.EnableHeadersVisualStyles = false;
            dgvBookCopies.Font = new Font("Segoe UI", 10F);
            dgvBookCopies.Location = new Point(3, 3);
            dgvBookCopies.MultiSelect = false;
            dgvBookCopies.Name = "dgvBookCopies";
            dgvBookCopies.ReadOnly = true;
            dgvBookCopies.RowHeadersVisible = false;
            dgvBookCopies.RowHeadersWidth = 51;
            dgvBookCopies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBookCopies.Size = new Size(808, 153);
            dgvBookCopies.TabIndex = 65;
            //
            // lblCopyHint
            //
            lblCopyHint.Dock = DockStyle.Fill;
            lblCopyHint.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblCopyHint.ForeColor = Color.DimGray;
            lblCopyHint.Location = new Point(3, 159);
            lblCopyHint.Name = "lblCopyHint";
            lblCopyHint.Size = new Size(808, 28);
            lblCopyHint.TabIndex = 69;
            lblCopyHint.Text = "Chọn một bản sao để cập nhật trạng thái, tình trạng hoặc xóa.";
            lblCopyHint.TextAlign = ContentAlignment.MiddleLeft;
            //
            // layoutDetailActions
            //
            layoutDetailActions.ColumnCount = 4;
            layoutDetailActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 167F));
            layoutDetailActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 166F));
            layoutDetailActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutDetailActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDetailActions.Controls.Add(btnDetailCopyAdd, 0, 0);
            layoutDetailActions.Controls.Add(btnDetailCopyEdit, 1, 0);
            layoutDetailActions.Controls.Add(btnDetailCopyDelete, 2, 0);
            layoutDetailActions.Dock = DockStyle.Fill;
            layoutDetailActions.Location = new Point(0, 187);
            layoutDetailActions.Margin = new Padding(0);
            layoutDetailActions.Name = "layoutDetailActions";
            layoutDetailActions.RowCount = 1;
            layoutDetailActions.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDetailActions.Size = new Size(814, 40);
            layoutDetailActions.TabIndex = 70;
            //
            // btnDetailCopyAdd
            //
            btnDetailCopyAdd.Dock = DockStyle.Fill;
            btnDetailCopyAdd.ForeColor = Color.SeaGreen;
            btnDetailCopyAdd.Location = new Point(3, 3);
            btnDetailCopyAdd.MinimumSize = new Size(0, 32);
            btnDetailCopyAdd.Name = "btnDetailCopyAdd";
            btnDetailCopyAdd.Size = new Size(161, 34);
            btnDetailCopyAdd.TabIndex = 71;
            btnDetailCopyAdd.Text = "Nhập thêm bản sao";
            btnDetailCopyAdd.UseVisualStyleBackColor = true;
            //
            // btnDetailCopyEdit
            //
            btnDetailCopyEdit.Dock = DockStyle.Fill;
            btnDetailCopyEdit.Location = new Point(170, 3);
            btnDetailCopyEdit.MinimumSize = new Size(0, 32);
            btnDetailCopyEdit.Name = "btnDetailCopyEdit";
            btnDetailCopyEdit.Size = new Size(160, 34);
            btnDetailCopyEdit.TabIndex = 72;
            btnDetailCopyEdit.Text = "Cập nhật bản sao";
            btnDetailCopyEdit.UseVisualStyleBackColor = true;
            //
            // btnDetailCopyDelete
            //
            btnDetailCopyDelete.Dock = DockStyle.Fill;
            btnDetailCopyDelete.ForeColor = Color.Firebrick;
            btnDetailCopyDelete.Location = new Point(336, 3);
            btnDetailCopyDelete.MinimumSize = new Size(0, 32);
            btnDetailCopyDelete.Name = "btnDetailCopyDelete";
            btnDetailCopyDelete.Size = new Size(134, 34);
            btnDetailCopyDelete.TabIndex = 73;
            btnDetailCopyDelete.Text = "Xóa bản sao";
            btnDetailCopyDelete.UseVisualStyleBackColor = true;
            //
            // tabCategories
            //
            tabCategories.BackColor = Color.FromArgb(248, 249, 252);
            tabCategories.Controls.Add(layoutCategories);
            tabCategories.Location = new Point(4, 29);
            tabCategories.Name = "tabCategories";
            tabCategories.Padding = new Padding(5);
            tabCategories.Size = new Size(192, 0);
            tabCategories.TabIndex = 6;
            tabCategories.Text = "Thể loại";
            //
            // layoutCategories
            //
            layoutCategories.ColumnCount = 1;
            layoutCategories.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCategories.Controls.Add(layoutCategoryFields, 0, 0);
            layoutCategories.Controls.Add(layoutCategoryActions, 0, 1);
            layoutCategories.Controls.Add(dgvCategories, 0, 2);
            layoutCategories.Controls.Add(lblCategoryCount, 0, 3);
            layoutCategories.Dock = DockStyle.Fill;
            layoutCategories.Location = new Point(5, 5);
            layoutCategories.Margin = new Padding(0);
            layoutCategories.Name = "layoutCategories";
            layoutCategories.RowCount = 4;
            layoutCategories.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            layoutCategories.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            layoutCategories.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutCategories.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutCategories.Size = new Size(182, 0);
            layoutCategories.TabIndex = 74;
            //
            // layoutCategoryFields
            //
            layoutCategoryFields.ColumnCount = 4;
            layoutCategoryFields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F));
            layoutCategoryFields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutCategoryFields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            layoutCategoryFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCategoryFields.Controls.Add(lblCategoryId, 0, 0);
            layoutCategoryFields.Controls.Add(txtCategoryId, 1, 0);
            layoutCategoryFields.Controls.Add(lblDescription, 2, 0);
            layoutCategoryFields.Controls.Add(txtDescription, 3, 0);
            layoutCategoryFields.Dock = DockStyle.Fill;
            layoutCategoryFields.Location = new Point(0, 0);
            layoutCategoryFields.Margin = new Padding(0);
            layoutCategoryFields.Name = "layoutCategoryFields";
            layoutCategoryFields.Padding = new Padding(12, 8, 12, 8);
            layoutCategoryFields.RowCount = 1;
            layoutCategoryFields.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutCategoryFields.Size = new Size(182, 64);
            layoutCategoryFields.TabIndex = 75;
            //
            // lblCategoryId
            //
            lblCategoryId.Dock = DockStyle.Fill;
            lblCategoryId.Location = new Point(15, 8);
            lblCategoryId.Name = "lblCategoryId";
            lblCategoryId.Size = new Size(109, 48);
            lblCategoryId.TabIndex = 76;
            lblCategoryId.Text = "Mã thể loại";
            lblCategoryId.TextAlign = ContentAlignment.MiddleLeft;
            //
            // txtCategoryId
            //
            txtCategoryId.Dock = DockStyle.Fill;
            txtCategoryId.Location = new Point(131, 13);
            txtCategoryId.Margin = new Padding(4, 5, 9, 4);
            txtCategoryId.Name = "txtCategoryId";
            txtCategoryId.PlaceholderText = "Tự tăng";
            txtCategoryId.ReadOnly = true;
            txtCategoryId.Size = new Size(97, 30);
            txtCategoryId.TabIndex = 77;
            txtCategoryId.TabStop = false;
            //
            // lblDescription
            //
            lblDescription.Dock = DockStyle.Fill;
            lblDescription.Location = new Point(240, 8);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(94, 48);
            lblDescription.TabIndex = 78;
            lblDescription.Text = "Thể loại (*)";
            lblDescription.TextAlign = ContentAlignment.MiddleLeft;
            //
            // txtDescription
            //
            txtDescription.Dock = DockStyle.Fill;
            txtDescription.Location = new Point(341, 13);
            txtDescription.Margin = new Padding(4, 5, 9, 4);
            txtDescription.MaxLength = 100;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(1, 30);
            txtDescription.TabIndex = 79;
            //
            // layoutCategoryActions
            //
            layoutCategoryActions.ColumnCount = 5;
            layoutCategoryActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCategoryActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutCategoryActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutCategoryActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutCategoryActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutCategoryActions.Controls.Add(btnCategoryAdd, 1, 0);
            layoutCategoryActions.Controls.Add(btnCategorySave, 2, 0);
            layoutCategoryActions.Controls.Add(btnCategoryDelete, 3, 0);
            layoutCategoryActions.Controls.Add(btnCategoryNew, 4, 0);
            layoutCategoryActions.Dock = DockStyle.Fill;
            layoutCategoryActions.Location = new Point(0, 64);
            layoutCategoryActions.Margin = new Padding(0);
            layoutCategoryActions.Name = "layoutCategoryActions";
            layoutCategoryActions.RowCount = 1;
            layoutCategoryActions.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutCategoryActions.Size = new Size(182, 46);
            layoutCategoryActions.TabIndex = 80;
            //
            // btnCategoryAdd
            //
            btnCategoryAdd.BackColor = Color.FromArgb(20, 161, 78);
            btnCategoryAdd.Dock = DockStyle.Fill;
            btnCategoryAdd.FlatAppearance.BorderSize = 0;
            btnCategoryAdd.FlatStyle = FlatStyle.Flat;
            btnCategoryAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCategoryAdd.ForeColor = Color.White;
            btnCategoryAdd.Location = new Point(-375, 3);
            btnCategoryAdd.MinimumSize = new Size(0, 32);
            btnCategoryAdd.Name = "btnCategoryAdd";
            btnCategoryAdd.Size = new Size(134, 40);
            btnCategoryAdd.TabIndex = 81;
            btnCategoryAdd.Text = "Thêm thể loại";
            btnCategoryAdd.UseVisualStyleBackColor = false;
            //
            // btnCategorySave
            //
            btnCategorySave.BackColor = Color.FromArgb(225, 133, 17);
            btnCategorySave.Dock = DockStyle.Fill;
            btnCategorySave.FlatAppearance.BorderSize = 0;
            btnCategorySave.FlatStyle = FlatStyle.Flat;
            btnCategorySave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCategorySave.ForeColor = Color.White;
            btnCategorySave.Location = new Point(-235, 3);
            btnCategorySave.MinimumSize = new Size(0, 32);
            btnCategorySave.Name = "btnCategorySave";
            btnCategorySave.Size = new Size(134, 40);
            btnCategorySave.TabIndex = 82;
            btnCategorySave.Text = "Lưu chỉnh sửa";
            btnCategorySave.UseVisualStyleBackColor = false;
            //
            // btnCategoryDelete
            //
            btnCategoryDelete.BackColor = Color.FromArgb(213, 45, 48);
            btnCategoryDelete.Dock = DockStyle.Fill;
            btnCategoryDelete.FlatAppearance.BorderSize = 0;
            btnCategoryDelete.FlatStyle = FlatStyle.Flat;
            btnCategoryDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCategoryDelete.ForeColor = Color.White;
            btnCategoryDelete.Location = new Point(-95, 3);
            btnCategoryDelete.MinimumSize = new Size(0, 32);
            btnCategoryDelete.Name = "btnCategoryDelete";
            btnCategoryDelete.Size = new Size(134, 40);
            btnCategoryDelete.TabIndex = 83;
            btnCategoryDelete.Text = "Xóa thể loại";
            btnCategoryDelete.UseVisualStyleBackColor = false;
            //
            // btnCategoryNew
            //
            btnCategoryNew.Dock = DockStyle.Fill;
            btnCategoryNew.Location = new Point(45, 3);
            btnCategoryNew.MinimumSize = new Size(0, 32);
            btnCategoryNew.Name = "btnCategoryNew";
            btnCategoryNew.Size = new Size(134, 40);
            btnCategoryNew.TabIndex = 84;
            btnCategoryNew.Text = "Làm trống";
            btnCategoryNew.UseVisualStyleBackColor = true;
            //
            // dgvCategories
            //
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(245, 249, 252);
            dgvCategories.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            dgvCategories.BackgroundColor = Color.White;
            dgvCategories.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.Dock = DockStyle.Fill;
            dgvCategories.EnableHeadersVisualStyles = false;
            dgvCategories.Font = new Font("Segoe UI", 10F);
            dgvCategories.Location = new Point(3, 113);
            dgvCategories.MultiSelect = false;
            dgvCategories.Name = "dgvCategories";
            dgvCategories.ReadOnly = true;
            dgvCategories.RowHeadersVisible = false;
            dgvCategories.RowHeadersWidth = 51;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.Size = new Size(176, 1);
            dgvCategories.TabIndex = 85;
            //
            // lblCategoryCount
            //
            lblCategoryCount.Dock = DockStyle.Fill;
            lblCategoryCount.Location = new Point(3, -29);
            lblCategoryCount.Name = "lblCategoryCount";
            lblCategoryCount.Size = new Size(176, 30);
            lblCategoryCount.TabIndex = 88;
            lblCategoryCount.Text = "Số thể loại: 0";
            lblCategoryCount.TextAlign = ContentAlignment.MiddleLeft;
            //
            // tabCopies
            //
            tabCopies.BackColor = Color.FromArgb(248, 249, 252);
            tabCopies.Controls.Add(layoutCopies);
            tabCopies.Location = new Point(4, 29);
            tabCopies.Name = "tabCopies";
            tabCopies.Padding = new Padding(5);
            tabCopies.Size = new Size(192, 0);
            tabCopies.TabIndex = 7;
            tabCopies.Text = "Chỉnh sửa bản sao";
            //
            // layoutCopies
            //
            layoutCopies.ColumnCount = 1;
            layoutCopies.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCopies.Controls.Add(layoutCopyFields, 0, 0);
            layoutCopies.Controls.Add(layoutCopyActions, 0, 1);
            layoutCopies.Controls.Add(layoutCopyFilter, 0, 2);
            layoutCopies.Controls.Add(dgvCopies, 0, 3);
            layoutCopies.Controls.Add(lblCopyCount, 0, 4);
            layoutCopies.Dock = DockStyle.Fill;
            layoutCopies.Location = new Point(5, 5);
            layoutCopies.Margin = new Padding(0);
            layoutCopies.Name = "layoutCopies";
            layoutCopies.RowCount = 5;
            layoutCopies.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            layoutCopies.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            layoutCopies.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            layoutCopies.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutCopies.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layoutCopies.Size = new Size(182, 0);
            layoutCopies.TabIndex = 89;
            //
            // layoutCopyFields
            //
            layoutCopyFields.ColumnCount = 4;
            layoutCopyFields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F));
            layoutCopyFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            layoutCopyFields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutCopyFields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            layoutCopyFields.Controls.Add(lblCopyId, 0, 0);
            layoutCopyFields.Controls.Add(txtCopyId, 1, 0);
            layoutCopyFields.Controls.Add(lblCopyBook, 2, 0);
            layoutCopyFields.Controls.Add(cboCopyBook, 3, 0);
            layoutCopyFields.Controls.Add(lblCopyStatus, 0, 1);
            layoutCopyFields.Controls.Add(cboCopyStatus, 1, 1);
            layoutCopyFields.Controls.Add(lblCondition, 2, 1);
            layoutCopyFields.Controls.Add(txtCondition, 3, 1);
            layoutCopyFields.Dock = DockStyle.Fill;
            layoutCopyFields.Location = new Point(0, 0);
            layoutCopyFields.Margin = new Padding(0);
            layoutCopyFields.Name = "layoutCopyFields";
            layoutCopyFields.Padding = new Padding(12, 8, 12, 4);
            layoutCopyFields.RowCount = 2;
            layoutCopyFields.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutCopyFields.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutCopyFields.Size = new Size(182, 100);
            layoutCopyFields.TabIndex = 90;
            //
            // lblCopyId
            //
            lblCopyId.Dock = DockStyle.Fill;
            lblCopyId.Location = new Point(15, 8);
            lblCopyId.Name = "lblCopyId";
            lblCopyId.Size = new Size(109, 44);
            lblCopyId.TabIndex = 91;
            lblCopyId.Text = "Mã bản sao";
            lblCopyId.TextAlign = ContentAlignment.MiddleLeft;
            //
            // txtCopyId
            //
            txtCopyId.Dock = DockStyle.Fill;
            txtCopyId.Location = new Point(131, 13);
            txtCopyId.Margin = new Padding(4, 5, 9, 4);
            txtCopyId.Name = "txtCopyId";
            txtCopyId.PlaceholderText = "Tự tăng";
            txtCopyId.ReadOnly = true;
            txtCopyId.Size = new Size(1, 30);
            txtCopyId.TabIndex = 92;
            txtCopyId.TabStop = false;
            //
            // lblCopyBook
            //
            lblCopyBook.Dock = DockStyle.Fill;
            lblCopyBook.Location = new Point(107, 8);
            lblCopyBook.Name = "lblCopyBook";
            lblCopyBook.Size = new Size(104, 44);
            lblCopyBook.TabIndex = 93;
            lblCopyBook.Text = "Đầu sách (*)";
            lblCopyBook.TextAlign = ContentAlignment.MiddleLeft;
            //
            // cboCopyBook
            //
            cboCopyBook.Dock = DockStyle.Fill;
            cboCopyBook.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCopyBook.Location = new Point(218, 13);
            cboCopyBook.Margin = new Padding(4, 5, 9, 4);
            cboCopyBook.Name = "cboCopyBook";
            cboCopyBook.Size = new Size(1, 31);
            cboCopyBook.TabIndex = 94;
            //
            // lblCopyStatus
            //
            lblCopyStatus.Dock = DockStyle.Fill;
            lblCopyStatus.Location = new Point(15, 52);
            lblCopyStatus.Name = "lblCopyStatus";
            lblCopyStatus.Size = new Size(109, 44);
            lblCopyStatus.TabIndex = 95;
            lblCopyStatus.Text = "Trạng thái (*)";
            lblCopyStatus.TextAlign = ContentAlignment.MiddleLeft;
            //
            // cboCopyStatus
            //
            cboCopyStatus.Dock = DockStyle.Fill;
            cboCopyStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCopyStatus.Location = new Point(131, 57);
            cboCopyStatus.Margin = new Padding(4, 5, 9, 4);
            cboCopyStatus.Name = "cboCopyStatus";
            cboCopyStatus.Size = new Size(1, 31);
            cboCopyStatus.TabIndex = 96;
            //
            // lblCondition
            //
            lblCondition.Dock = DockStyle.Fill;
            lblCondition.Location = new Point(107, 52);
            lblCondition.Name = "lblCondition";
            lblCondition.Size = new Size(104, 44);
            lblCondition.TabIndex = 97;
            lblCondition.Text = "Tình trạng (*)";
            lblCondition.TextAlign = ContentAlignment.MiddleLeft;
            //
            // txtCondition
            //
            txtCondition.Dock = DockStyle.Fill;
            txtCondition.Location = new Point(218, 57);
            txtCondition.Margin = new Padding(4, 5, 9, 4);
            txtCondition.MaxLength = 100;
            txtCondition.Name = "txtCondition";
            txtCondition.Size = new Size(1, 30);
            txtCondition.TabIndex = 98;
            //
            // layoutCopyActions
            //
            layoutCopyActions.ColumnCount = 6;
            layoutCopyActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            layoutCopyActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCopyActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutCopyActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutCopyActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutCopyActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            layoutCopyActions.Controls.Add(btnBackToBooks, 0, 0);
            layoutCopyActions.Controls.Add(btnCopyAdd, 2, 0);
            layoutCopyActions.Controls.Add(btnCopySave, 3, 0);
            layoutCopyActions.Controls.Add(btnCopyDelete, 4, 0);
            layoutCopyActions.Controls.Add(btnCopyNew, 5, 0);
            layoutCopyActions.Dock = DockStyle.Fill;
            layoutCopyActions.Location = new Point(0, 100);
            layoutCopyActions.Margin = new Padding(0);
            layoutCopyActions.Name = "layoutCopyActions";
            layoutCopyActions.RowCount = 1;
            layoutCopyActions.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutCopyActions.Size = new Size(182, 46);
            layoutCopyActions.TabIndex = 99;
            //
            // btnBackToBooks
            //
            btnBackToBooks.Dock = DockStyle.Fill;
            btnBackToBooks.Location = new Point(3, 3);
            btnBackToBooks.MinimumSize = new Size(0, 32);
            btnBackToBooks.Name = "btnBackToBooks";
            btnBackToBooks.Size = new Size(164, 40);
            btnBackToBooks.TabIndex = 100;
            btnBackToBooks.Text = "← Về danh sách sách";
            btnBackToBooks.UseVisualStyleBackColor = true;
            //
            // btnCopyAdd
            //
            btnCopyAdd.BackColor = Color.FromArgb(20, 161, 78);
            btnCopyAdd.Dock = DockStyle.Fill;
            btnCopyAdd.FlatAppearance.BorderSize = 0;
            btnCopyAdd.FlatStyle = FlatStyle.Flat;
            btnCopyAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCopyAdd.ForeColor = Color.White;
            btnCopyAdd.Location = new Point(-375, 3);
            btnCopyAdd.MinimumSize = new Size(0, 32);
            btnCopyAdd.Name = "btnCopyAdd";
            btnCopyAdd.Size = new Size(134, 40);
            btnCopyAdd.TabIndex = 101;
            btnCopyAdd.Text = "Thêm bản sao";
            btnCopyAdd.UseVisualStyleBackColor = false;
            //
            // btnCopySave
            //
            btnCopySave.BackColor = Color.FromArgb(225, 133, 17);
            btnCopySave.Dock = DockStyle.Fill;
            btnCopySave.FlatAppearance.BorderSize = 0;
            btnCopySave.FlatStyle = FlatStyle.Flat;
            btnCopySave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCopySave.ForeColor = Color.White;
            btnCopySave.Location = new Point(-235, 3);
            btnCopySave.MinimumSize = new Size(0, 32);
            btnCopySave.Name = "btnCopySave";
            btnCopySave.Size = new Size(134, 40);
            btnCopySave.TabIndex = 102;
            btnCopySave.Text = "Lưu chỉnh sửa";
            btnCopySave.UseVisualStyleBackColor = false;
            //
            // btnCopyDelete
            //
            btnCopyDelete.BackColor = Color.FromArgb(213, 45, 48);
            btnCopyDelete.Dock = DockStyle.Fill;
            btnCopyDelete.FlatAppearance.BorderSize = 0;
            btnCopyDelete.FlatStyle = FlatStyle.Flat;
            btnCopyDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCopyDelete.ForeColor = Color.White;
            btnCopyDelete.Location = new Point(-95, 3);
            btnCopyDelete.MinimumSize = new Size(0, 32);
            btnCopyDelete.Name = "btnCopyDelete";
            btnCopyDelete.Size = new Size(134, 40);
            btnCopyDelete.TabIndex = 103;
            btnCopyDelete.Text = "Xóa bản sao";
            btnCopyDelete.UseVisualStyleBackColor = false;
            //
            // btnCopyNew
            //
            btnCopyNew.Dock = DockStyle.Fill;
            btnCopyNew.Location = new Point(45, 3);
            btnCopyNew.MinimumSize = new Size(0, 32);
            btnCopyNew.Name = "btnCopyNew";
            btnCopyNew.Size = new Size(134, 40);
            btnCopyNew.TabIndex = 104;
            btnCopyNew.Text = "Làm trống";
            btnCopyNew.UseVisualStyleBackColor = true;
            //
            // layoutCopyFilter
            //
            layoutCopyFilter.ColumnCount = 4;
            layoutCopyFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            layoutCopyFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCopyFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            layoutCopyFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            layoutCopyFilter.Controls.Add(lblCopyFilter, 0, 0);
            layoutCopyFilter.Controls.Add(cboCopyFilter, 1, 0);
            layoutCopyFilter.Controls.Add(btnCopyFilter, 2, 0);
            layoutCopyFilter.Controls.Add(btnCopyAll, 3, 0);
            layoutCopyFilter.Dock = DockStyle.Fill;
            layoutCopyFilter.Location = new Point(0, 146);
            layoutCopyFilter.Margin = new Padding(0);
            layoutCopyFilter.Name = "layoutCopyFilter";
            layoutCopyFilter.Padding = new Padding(12, 4, 12, 4);
            layoutCopyFilter.RowCount = 1;
            layoutCopyFilter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutCopyFilter.Size = new Size(182, 52);
            layoutCopyFilter.TabIndex = 105;
            //
            // lblCopyFilter
            //
            lblCopyFilter.Dock = DockStyle.Fill;
            lblCopyFilter.Location = new Point(15, 4);
            lblCopyFilter.Name = "lblCopyFilter";
            lblCopyFilter.Size = new Size(114, 44);
            lblCopyFilter.TabIndex = 106;
            lblCopyFilter.Text = "Lọc đầu sách";
            lblCopyFilter.TextAlign = ContentAlignment.MiddleLeft;
            //
            // cboCopyFilter
            //
            cboCopyFilter.Dock = DockStyle.Fill;
            cboCopyFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCopyFilter.Location = new Point(136, 9);
            cboCopyFilter.Margin = new Padding(4, 5, 9, 4);
            cboCopyFilter.Name = "cboCopyFilter";
            cboCopyFilter.Size = new Size(1, 31);
            cboCopyFilter.TabIndex = 107;
            //
            // btnCopyFilter
            //
            btnCopyFilter.Dock = DockStyle.Fill;
            btnCopyFilter.Location = new Point(-87, 7);
            btnCopyFilter.MinimumSize = new Size(0, 32);
            btnCopyFilter.Name = "btnCopyFilter";
            btnCopyFilter.Size = new Size(124, 38);
            btnCopyFilter.TabIndex = 108;
            btnCopyFilter.Text = "Lọc";
            btnCopyFilter.UseVisualStyleBackColor = true;
            //
            // btnCopyAll
            //
            btnCopyAll.Dock = DockStyle.Fill;
            btnCopyAll.Location = new Point(43, 7);
            btnCopyAll.MinimumSize = new Size(0, 32);
            btnCopyAll.Name = "btnCopyAll";
            btnCopyAll.Size = new Size(124, 38);
            btnCopyAll.TabIndex = 109;
            btnCopyAll.Text = "Tất cả";
            btnCopyAll.UseVisualStyleBackColor = true;
            //
            // dgvCopies
            //
            dgvCopies.AllowUserToAddRows = false;
            dgvCopies.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(245, 249, 252);
            dgvCopies.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            dgvCopies.BackgroundColor = Color.White;
            dgvCopies.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dgvCopies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCopies.Dock = DockStyle.Fill;
            dgvCopies.EnableHeadersVisualStyles = false;
            dgvCopies.Font = new Font("Segoe UI", 10F);
            dgvCopies.Location = new Point(3, 201);
            dgvCopies.MultiSelect = false;
            dgvCopies.Name = "dgvCopies";
            dgvCopies.ReadOnly = true;
            dgvCopies.RowHeadersVisible = false;
            dgvCopies.RowHeadersWidth = 51;
            dgvCopies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCopies.Size = new Size(176, 1);
            dgvCopies.TabIndex = 110;
            //
            // lblCopyCount
            //
            lblCopyCount.Dock = DockStyle.Fill;
            lblCopyCount.Location = new Point(3, -29);
            lblCopyCount.Name = "lblCopyCount";
            lblCopyCount.Size = new Size(176, 30);
            lblCopyCount.TabIndex = 116;
            lblCopyCount.Text = "Số bản sao: 0";
            lblCopyCount.TextAlign = ContentAlignment.MiddleLeft;
            //
            // layoutFooter
            //
            layoutFooter.ColumnCount = 3;
            layoutFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 135F));
            layoutFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85F));
            layoutFooter.Controls.Add(lblBookCount, 0, 0);
            layoutFooter.Controls.Add(btnReload, 1, 0);
            layoutFooter.Controls.Add(btnClose, 2, 0);
            layoutFooter.Dock = DockStyle.Fill;
            layoutFooter.Location = new Point(0, 758);
            layoutFooter.Margin = new Padding(0);
            layoutFooter.Name = "layoutFooter";
            layoutFooter.RowCount = 1;
            layoutFooter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutFooter.Size = new Size(1240, 32);
            layoutFooter.TabIndex = 8;
            //
            // lblBookCount
            //
            lblBookCount.Dock = DockStyle.Fill;
            lblBookCount.ForeColor = Color.DimGray;
            lblBookCount.Location = new Point(3, 0);
            lblBookCount.Name = "lblBookCount";
            lblBookCount.Size = new Size(1014, 32);
            lblBookCount.TabIndex = 9;
            lblBookCount.Text = "Chưa tải dữ liệu";
            lblBookCount.TextAlign = ContentAlignment.MiddleLeft;
            //
            // btnReload
            //
            btnReload.Dock = DockStyle.Fill;
            btnReload.Location = new Point(1023, 3);
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(129, 26);
            btnReload.TabIndex = 10;
            btnReload.Text = "Tải lại dữ liệu";
            btnReload.UseVisualStyleBackColor = true;
            //
            // btnClose
            //
            btnClose.Dock = DockStyle.Fill;
            btnClose.Location = new Point(1158, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(79, 26);
            btnClose.TabIndex = 11;
            btnClose.Text = "Đóng";
            btnClose.UseVisualStyleBackColor = true;
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 252);
            ClientSize = new Size(1240, 790);
            Controls.Add(layoutRoot);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1140, 800);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý đầu sách và bản sao";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            layoutRoot.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            tabsMain.ResumeLayout(false);
            tabBooks.ResumeLayout(false);
            layoutBooks.ResumeLayout(false);
            layoutSearch.ResumeLayout(false);
            layoutSearch.PerformLayout();
            layoutMaster.ResumeLayout(false);
            grpBookInfo.ResumeLayout(false);
            layoutBookInfo.ResumeLayout(false);
            layoutFields.ResumeLayout(false);
            layoutFields.PerformLayout();
            nudPriceHost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudPrice).EndInit();
            nudYearHost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudYear).EndInit();
            nudTotalCopiesHost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudTotalCopies).EndInit();
            layoutBookActions.ResumeLayout(false);
            layoutLists.ResumeLayout(false);
            grpBooks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            grpBookCopies.ResumeLayout(false);
            layoutBookCopies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBookCopies).EndInit();
            layoutDetailActions.ResumeLayout(false);
            tabCategories.ResumeLayout(false);
            layoutCategories.ResumeLayout(false);
            layoutCategoryFields.ResumeLayout(false);
            layoutCategoryFields.PerformLayout();
            layoutCategoryActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            tabCopies.ResumeLayout(false);
            layoutCopies.ResumeLayout(false);
            layoutCopyFields.ResumeLayout(false);
            layoutCopyFields.PerformLayout();
            layoutCopyActions.ResumeLayout(false);
            layoutCopyFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCopies).EndInit();
            layoutFooter.ResumeLayout(false);
            ResumeLayout(false);
        }
        private TableLayoutPanel layoutRoot = null!;
        private Panel pnlHeader = null!;
        private Label lblHeading = null!;
        private Label lblSubtitle = null!;
        private TabControl tabsMain = null!;
        private TabPage tabBooks = null!;
        private TabPage tabCategories = null!;
        private TabPage tabCopies = null!;
        private TableLayoutPanel layoutFooter = null!;
        private Label lblBookCount = null!;
        private Button btnReload = null!;
        private Button btnClose = null!;
        private TableLayoutPanel layoutBooks = null!;
        private TableLayoutPanel layoutSearch = null!;
        private Label lblBookSearch = null!;
        private TextBox txtBookSearch = null!;
        private Label lblBookFilter = null!;
        private ComboBox cboBookFilter = null!;
        private Button btnBookSearch = null!;
        private Button btnBookAll = null!;
        private TableLayoutPanel layoutMaster = null!;
        private GroupBox grpBookInfo = null!;
        private TableLayoutPanel layoutBookInfo = null!;
        private TableLayoutPanel layoutFields = null!;
        private Label lblBookId = null!;
        private Label lblTitle = null!;
        private Label lblAuthor = null!;
        private Label lblPublisher = null!;
        private Label lblCategory = null!;
        private Label lblPrice = null!;
        private Label lblYear = null!;
        private Label lblTotalCopies = null!;
        private Label lblRegisteredTitle = null!;
        private TextBox txtBookId = null!;
        private TextBox txtTitle = null!;
        private TextBox txtAuthor = null!;
        private TextBox txtPublisher = null!;
        private ComboBox cboCategory = null!;
        private Panel nudPriceHost = null!;
        private NumericUpDown nudPrice = null!;
        private Panel nudYearHost = null!;
        private NumericUpDown nudYear = null!;
        private Panel nudTotalCopiesHost = null!;
        private NumericUpDown nudTotalCopies = null!;
        private Label lblRegisteredCount = null!;
        private TableLayoutPanel layoutBookActions = null!;
        private Button btnBookAdd = null!;
        private Button btnBookSave = null!;
        private Button btnBookDelete = null!;
        private Button btnBookNew = null!;
        private Button btnBookCopies = null!;
        private TableLayoutPanel layoutLists = null!;
        private GroupBox grpBooks = null!;
        private DataGridView dgvBooks = null!;
        private GroupBox grpBookCopies = null!;
        private TableLayoutPanel layoutBookCopies = null!;
        private DataGridView dgvBookCopies = null!;
        private Label lblCopyHint = null!;
        private TableLayoutPanel layoutDetailActions = null!;
        private Button btnDetailCopyAdd = null!;
        private Button btnDetailCopyEdit = null!;
        private Button btnDetailCopyDelete = null!;
        private TableLayoutPanel layoutCategories = null!;
        private TableLayoutPanel layoutCategoryFields = null!;
        private Label lblCategoryId = null!;
        private TextBox txtCategoryId = null!;
        private Label lblDescription = null!;
        private TextBox txtDescription = null!;
        private TableLayoutPanel layoutCategoryActions = null!;
        private Button btnCategoryAdd = null!;
        private Button btnCategorySave = null!;
        private Button btnCategoryDelete = null!;
        private Button btnCategoryNew = null!;
        private DataGridView dgvCategories = null!;
        private Label lblCategoryCount = null!;
        private TableLayoutPanel layoutCopies = null!;
        private TableLayoutPanel layoutCopyFields = null!;
        private Label lblCopyId = null!;
        private TextBox txtCopyId = null!;
        private Label lblCopyBook = null!;
        private ComboBox cboCopyBook = null!;
        private Label lblCopyStatus = null!;
        private ComboBox cboCopyStatus = null!;
        private Label lblCondition = null!;
        private TextBox txtCondition = null!;
        private TableLayoutPanel layoutCopyActions = null!;
        private Button btnBackToBooks = null!;
        private Button btnCopyAdd = null!;
        private Button btnCopySave = null!;
        private Button btnCopyDelete = null!;
        private Button btnCopyNew = null!;
        private TableLayoutPanel layoutCopyFilter = null!;
        private Label lblCopyFilter = null!;
        private ComboBox cboCopyFilter = null!;
        private Button btnCopyFilter = null!;
        private Button btnCopyAll = null!;
        private DataGridView dgvCopies = null!;
        private Label lblCopyCount = null!;
        private ErrorProvider errorProvider = null!;
    }
}

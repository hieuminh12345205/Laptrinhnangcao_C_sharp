namespace Quanlysinhvien.Views
{
    partial class frmQuanLySV
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            errorProvider = new ErrorProvider(components);
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtMaSV = new TextBox();
            txtEmail = new TextBox();
            dtpNgaySinh = new DateTimePicker();
            txtHoTen = new TextBox();
            label5 = new Label();
            txtSDT = new TextBox();
            label6 = new Label();
            label7 = new Label();
            rdoNam = new RadioButton();
            label8 = new Label();
            rdoNu = new RadioButton();
            label9 = new Label();
            cboLop = new ComboBox();
            cboTrangThai = new ComboBox();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            nudDiem = new NumericUpDown();
            btnSua = new Button();
            btnThem = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnDong = new Button();
            pnlTimKiem = new Panel();
            lblTuKhoa = new Label();
            txtTuKhoa = new TextBox();
            lblLocLop = new Label();
            cboLocLop = new ComboBox();
            lblDiemTu = new Label();
            nudDiemTu = new NumericUpDown();
            btnTimKiem = new Button();
            btnHienThiTatCa = new Button();
            lblDanhSach = new Label();
            lblTongSo = new Label();
            dgvSinhVien = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDiem).BeginInit();
            pnlTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudDiemTu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).BeginInit();
            SuspendLayout();
            // 
            // errorProvider
            // 
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 22);
            label1.Name = "label1";
            label1.Size = new Size(133, 20);
            label1.TabIndex = 0;
            label1.Text = "Thông tin sinh viên";
            // 
            // label2
            // 
            label2.Location = new Point(35, 58);
            label2.Name = "label2";
            label2.Size = new Size(85, 27);
            label2.TabIndex = 1;
            label2.Text = "Mã SV*";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new Point(35, 150);
            label3.Name = "label3";
            label3.Size = new Size(85, 27);
            label3.TabIndex = 2;
            label3.Text = "Email*";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.Location = new Point(35, 104);
            label4.Name = "label4";
            label4.Size = new Size(85, 27);
            label4.TabIndex = 3;
            label4.Text = "Ngày sinh*";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtMaSV
            // 
            txtMaSV.Location = new Point(130, 58);
            txtMaSV.Name = "txtMaSV";
            txtMaSV.Size = new Size(210, 27);
            txtMaSV.TabIndex = 4;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(130, 150);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(210, 27);
            txtEmail.TabIndex = 5;
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.Location = new Point(130, 104);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(210, 27);
            dtpNgaySinh.TabIndex = 6;
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(450, 58);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(210, 27);
            txtHoTen.TabIndex = 8;
            // 
            // label5
            // 
            label5.Location = new Point(355, 58);
            label5.Name = "label5";
            label5.Size = new Size(85, 27);
            label5.TabIndex = 7;
            label5.Text = "Họ và tên*";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtSDT
            // 
            txtSDT.Location = new Point(450, 150);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(210, 27);
            txtSDT.TabIndex = 10;
            // 
            // label6
            // 
            label6.Location = new Point(355, 150);
            label6.Name = "label6";
            label6.Size = new Size(85, 27);
            label6.TabIndex = 9;
            label6.Text = "SĐT*";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            label7.Location = new Point(355, 104);
            label7.Name = "label7";
            label7.Size = new Size(85, 27);
            label7.TabIndex = 11;
            label7.Text = "Giới tính*";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // rdoNam
            // 
            rdoNam.AutoSize = true;
            rdoNam.Location = new Point(450, 105);
            rdoNam.Name = "rdoNam";
            rdoNam.Size = new Size(62, 24);
            rdoNam.TabIndex = 12;
            rdoNam.TabStop = true;
            rdoNam.Text = "Nam";
            rdoNam.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(647, 204);
            label8.Name = "label8";
            label8.Size = new Size(0, 20);
            label8.TabIndex = 13;
            // 
            // rdoNu
            // 
            rdoNu.AutoSize = true;
            rdoNu.Location = new Point(530, 105);
            rdoNu.Name = "rdoNu";
            rdoNu.Size = new Size(50, 24);
            rdoNu.TabIndex = 14;
            rdoNu.TabStop = true;
            rdoNu.Text = "Nữ";
            rdoNu.UseVisualStyleBackColor = true;
            rdoNu.CheckedChanged += rdoNu_CheckedChanged;
            // 
            // label9
            // 
            label9.Location = new Point(675, 58);
            label9.Name = "label9";
            label9.Size = new Size(85, 27);
            label9.TabIndex = 15;
            label9.Text = "Lớp học";
            label9.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cboLop
            // 
            cboLop.FormattingEnabled = true;
            cboLop.Location = new Point(770, 58);
            cboLop.Name = "cboLop";
            cboLop.Size = new Size(210, 28);
            cboLop.TabIndex = 16;
            // 
            // cboTrangThai
            // 
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Location = new Point(770, 150);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(210, 28);
            cboTrangThai.TabIndex = 18;
            // 
            // label10
            // 
            label10.Location = new Point(675, 150);
            label10.Name = "label10";
            label10.Size = new Size(85, 27);
            label10.TabIndex = 17;
            label10.Text = "Trạng thái";
            label10.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            label11.Location = new Point(675, 104);
            label11.Name = "label11";
            label11.Size = new Size(85, 27);
            label11.TabIndex = 19;
            label11.Text = "Điểm";
            label11.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(525, 315);
            label12.Name = "label12";
            label12.Size = new Size(0, 20);
            label12.TabIndex = 20;
            // 
            // nudDiem
            // 
            nudDiem.DecimalPlaces = 1;
            nudDiem.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudDiem.Location = new Point(770, 104);
            nudDiem.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudDiem.Name = "nudDiem";
            nudDiem.Size = new Size(210, 27);
            nudDiem.TabIndex = 21;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(532, 200);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(100, 32);
            btnSua.TabIndex = 22;
            btnSua.Text = "&Sửa";
            btnSua.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(416, 200);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(100, 32);
            btnThem.TabIndex = 23;
            btnThem.Text = "&Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(648, 200);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(100, 32);
            btnXoa.TabIndex = 24;
            btnXoa.Text = "&Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(764, 200);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(100, 32);
            btnLamMoi.TabIndex = 25;
            btnLamMoi.Text = "&Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // btnDong
            // 
            btnDong.Location = new Point(880, 200);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(100, 32);
            btnDong.TabIndex = 26;
            btnDong.Text = "&Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // pnlTimKiem
            // 
            pnlTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlTimKiem.Controls.Add(lblTuKhoa);
            pnlTimKiem.Controls.Add(txtTuKhoa);
            pnlTimKiem.Controls.Add(lblLocLop);
            pnlTimKiem.Controls.Add(cboLocLop);
            pnlTimKiem.Controls.Add(lblDiemTu);
            pnlTimKiem.Controls.Add(nudDiemTu);
            pnlTimKiem.Controls.Add(btnTimKiem);
            pnlTimKiem.Controls.Add(btnHienThiTatCa);
            pnlTimKiem.Location = new Point(35, 248);
            pnlTimKiem.Name = "pnlTimKiem";
            pnlTimKiem.Size = new Size(952, 60);
            pnlTimKiem.TabIndex = 27;
            // 
            // lblTuKhoa
            // 
            lblTuKhoa.AutoSize = true;
            lblTuKhoa.Location = new Point(8, 20);
            lblTuKhoa.Name = "lblTuKhoa";
            lblTuKhoa.Size = new Size(62, 20);
            lblTuKhoa.TabIndex = 0;
            lblTuKhoa.Text = "Từ khóa";
            // 
            // txtTuKhoa
            // 
            txtTuKhoa.Location = new Point(72, 16);
            txtTuKhoa.Name = "txtTuKhoa";
            txtTuKhoa.PlaceholderText = "Mã, họ tên, email hoặc điện thoại";
            txtTuKhoa.Size = new Size(245, 27);
            txtTuKhoa.TabIndex = 0;
            // 
            // lblLocLop
            // 
            lblLocLop.AutoSize = true;
            lblLocLop.Location = new Point(329, 20);
            lblLocLop.Name = "lblLocLop";
            lblLocLop.Size = new Size(34, 20);
            lblLocLop.TabIndex = 1;
            lblLocLop.Text = "Lớp";
            // 
            // cboLocLop
            // 
            cboLocLop.Location = new Point(366, 16);
            cboLocLop.Name = "cboLocLop";
            cboLocLop.Size = new Size(150, 28);
            cboLocLop.TabIndex = 1;
            // 
            // lblDiemTu
            // 
            lblDiemTu.AutoSize = true;
            lblDiemTu.Location = new Point(529, 20);
            lblDiemTu.Name = "lblDiemTu";
            lblDiemTu.Size = new Size(63, 20);
            lblDiemTu.TabIndex = 2;
            lblDiemTu.Text = "Điểm từ";
            // 
            // nudDiemTu
            // 
            nudDiemTu.DecimalPlaces = 1;
            nudDiemTu.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudDiemTu.Location = new Point(592, 16);
            nudDiemTu.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudDiemTu.Name = "nudDiemTu";
            nudDiemTu.Size = new Size(70, 27);
            nudDiemTu.TabIndex = 2;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(678, 14);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(105, 32);
            btnTimKiem.TabIndex = 3;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // btnHienThiTatCa
            // 
            btnHienThiTatCa.Location = new Point(794, 14);
            btnHienThiTatCa.Name = "btnHienThiTatCa";
            btnHienThiTatCa.Size = new Size(145, 32);
            btnHienThiTatCa.TabIndex = 4;
            btnHienThiTatCa.Text = "Hiển thị tất cả";
            btnHienThiTatCa.UseVisualStyleBackColor = true;
            // 
            // lblDanhSach
            // 
            lblDanhSach.AutoSize = true;
            lblDanhSach.Location = new Point(35, 324);
            lblDanhSach.Name = "lblDanhSach";
            lblDanhSach.Size = new Size(138, 20);
            lblDanhSach.TabIndex = 1;
            lblDanhSach.Text = "Danh sách sinh viên";
            // 
            // lblTongSo
            // 
            lblTongSo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTongSo.AutoSize = true;
            lblTongSo.Location = new Point(600, 324);
            lblTongSo.Name = "lblTongSo";
            lblTongSo.Size = new Size(138, 20);
            lblTongSo.TabIndex = 2;
            lblTongSo.Text = "Tổng số: 0 sinh viên";
            // 
            // dgvSinhVien
            // 
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AllowUserToDeleteRows = false;
            dgvSinhVien.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.ColumnHeadersHeight = 29;
            dgvSinhVien.Location = new Point(35, 356);
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.Name = "dgvSinhVien";
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.RowHeadersVisible = false;
            dgvSinhVien.RowHeadersWidth = 51;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.Size = new Size(952, 220);
            dgvSinhVien.TabIndex = 0;
            // 
            // frmQuanLySV
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1023, 600);
            Controls.Add(dgvSinhVien);
            Controls.Add(lblDanhSach);
            Controls.Add(lblTongSo);
            Controls.Add(pnlTimKiem);
            Controls.Add(btnDong);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnThem);
            Controls.Add(btnSua);
            Controls.Add(nudDiem);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(cboTrangThai);
            Controls.Add(label10);
            Controls.Add(cboLop);
            Controls.Add(label9);
            Controls.Add(rdoNu);
            Controls.Add(label8);
            Controls.Add(rdoNam);
            Controls.Add(label7);
            Controls.Add(txtSDT);
            Controls.Add(label6);
            Controls.Add(txtHoTen);
            Controls.Add(label5);
            Controls.Add(dtpNgaySinh);
            Controls.Add(txtEmail);
            Controls.Add(txtMaSV);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            MinimumSize = new Size(1041, 647);
            Name = "frmQuanLySV";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý sinh viên";
            Load += frmQuanLySV_Load;
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDiem).EndInit();
            pnlTimKiem.ResumeLayout(false);
            pnlTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudDiemTu).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtMaSV;
        private TextBox txtEmail;
        private DateTimePicker dtpNgaySinh;
        private TextBox txtHoTen;
        private Label label5;
        private TextBox txtSDT;
        private Label label6;
        private Label label7;
        private RadioButton rdoNam;
        private Label label8;
        private RadioButton rdoNu;
        private Label label9;
        private ComboBox cboLop;
        private ComboBox cboTrangThai;
        private Label label10;
        private Label label11;
        private Label label12;
        private NumericUpDown nudDiem;
        private Button btnSua;
        private Button btnThem;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnDong;
        private Panel pnlTimKiem;
        private Label lblTuKhoa;
        private TextBox txtTuKhoa;
        private Label lblLocLop;
        private ComboBox cboLocLop;
        private Label lblDiemTu;
        private NumericUpDown nudDiemTu;
        private Button btnTimKiem;
        private Button btnHienThiTatCa;
        private Label lblDanhSach;
        private Label lblTongSo;
        private DataGridView dgvSinhVien;
        private ErrorProvider errorProvider;
    }
}





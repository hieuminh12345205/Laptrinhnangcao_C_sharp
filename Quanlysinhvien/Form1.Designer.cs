namespace Quanlysinhvien
{
    partial class Form1
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
            colMaSV = new DataGridViewTextBoxColumn();
            colHoTen = new DataGridViewTextBoxColumn();
            colNgaySinh = new DataGridViewTextBoxColumn();
            colGioiTinh = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colSDT = new DataGridViewTextBoxColumn();
            colLop = new DataGridViewTextBoxColumn();
            colDiem = new DataGridViewTextBoxColumn();
            colTrangThai = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)nudDiemTu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDiem).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 22);
            label1.Name = "label1";
            label1.Size = new Size(133, 20);
            label1.TabIndex = 0;
            label1.Text = "Thông tin sinh viên";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 58);
            label2.Name = "label2";
            label2.Size = new Size(57, 20);
            label2.TabIndex = 1;
            label2.Text = "Mã SV*";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(26, 145);
            label3.Name = "label3";
            label3.Size = new Size(52, 20);
            label3.TabIndex = 2;
            label3.Text = "Email*";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(26, 100);
            label4.Name = "label4";
            label4.Size = new Size(80, 20);
            label4.TabIndex = 3;
            label4.Text = "Ngày sinh*";
            // 
            // txtMaSV
            // 
            txtMaSV.Location = new Point(89, 58);
            txtMaSV.Name = "txtMaSV";
            txtMaSV.Size = new Size(125, 27);
            txtMaSV.TabIndex = 4;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(89, 142);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(125, 27);
            txtEmail.TabIndex = 5;
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.Location = new Point(112, 100);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(135, 27);
            dtpNgaySinh.TabIndex = 6;
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(357, 58);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(291, 27);
            txtHoTen.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(272, 61);
            label5.Name = "label5";
            label5.Size = new Size(79, 20);
            label5.TabIndex = 7;
            label5.Text = "Họ và tên*";
            // 
            // txtSDT
            // 
            txtSDT.Location = new Point(357, 145);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(125, 27);
            txtSDT.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(294, 148);
            label6.Name = "label6";
            label6.Size = new Size(41, 20);
            label6.TabIndex = 9;
            label6.Text = "SDT*";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(272, 107);
            label7.Name = "label7";
            label7.Size = new Size(71, 20);
            label7.TabIndex = 11;
            label7.Text = "Giới tính*";
            // 
            // rdoNam
            // 
            rdoNam.AutoSize = true;
            rdoNam.Location = new Point(357, 103);
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
            rdoNu.Location = new Point(432, 103);
            rdoNu.Name = "rdoNu";
            rdoNu.Size = new Size(50, 24);
            rdoNu.TabIndex = 14;
            rdoNu.TabStop = true;
            rdoNu.Text = "Nữ";
            rdoNu.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(690, 61);
            label9.Name = "label9";
            label9.Size = new Size(62, 20);
            label9.TabIndex = 15;
            label9.Text = "Lớp học";
            // 
            // cboLop
            // 
            cboLop.FormattingEnabled = true;
            cboLop.Location = new Point(758, 57);
            cboLop.Name = "cboLop";
            cboLop.Size = new Size(201, 28);
            cboLop.TabIndex = 16;
            // 
            // cboTrangThai
            // 
            cboTrangThai.FormattingEnabled = true;
            cboTrangThai.Location = new Point(758, 148);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(201, 28);
            cboTrangThai.TabIndex = 18;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(677, 151);
            label10.Name = "label10";
            label10.Size = new Size(75, 20);
            label10.TabIndex = 17;
            label10.Text = "Trạng thái";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(707, 107);
            label11.Name = "label11";
            label11.Size = new Size(45, 20);
            label11.TabIndex = 19;
            label11.Text = "Điểm";
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
            nudDiem.Location = new Point(758, 105);
            nudDiem.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudDiem.Name = "nudDiem";
            nudDiem.Size = new Size(150, 27);
            nudDiem.TabIndex = 21;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(488, 200);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 29);
            btnSua.TabIndex = 22;
            btnSua.Text = "&Sửa";
            btnSua.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(376, 200);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 23;
            btnThem.Text = "&Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(607, 200);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 24;
            btnXoa.Text = "&Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(733, 200);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(94, 29);
            btnLamMoi.TabIndex = 25;
            btnLamMoi.Text = "&Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // btnDong
            // 
            btnDong.Location = new Point(865, 200);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(94, 29);
            btnDong.TabIndex = 26;
            btnDong.Text = "&Đóng";
            btnDong.UseVisualStyleBackColor = true;
            // 
            // pnlTimKiem
            // 
            pnlTimKiem.Location = new Point(35, 248);
            pnlTimKiem.Name = "pnlTimKiem";
            pnlTimKiem.Size = new Size(952, 60);
            pnlTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlTimKiem.Controls.Add(lblTuKhoa);
            pnlTimKiem.Controls.Add(txtTuKhoa);
            pnlTimKiem.Controls.Add(lblLocLop);
            pnlTimKiem.Controls.Add(cboLocLop);
            pnlTimKiem.Controls.Add(lblDiemTu);
            pnlTimKiem.Controls.Add(nudDiemTu);
            pnlTimKiem.Controls.Add(btnTimKiem);
            pnlTimKiem.Controls.Add(btnHienThiTatCa);
            pnlTimKiem.TabIndex = 27;
            // Thanh tìm kiếm: các control nằm trong cùng Panel.
            lblTuKhoa.Text = "Từ khóa";
            lblTuKhoa.AutoSize = true;
            lblTuKhoa.Location = new Point(8, 20);
            txtTuKhoa.Name = "txtTuKhoa";
            txtTuKhoa.Location = new Point(72, 16);
            txtTuKhoa.Size = new Size(245, 27);
            txtTuKhoa.PlaceholderText = "Mã, họ tên, email hoặc điện thoại";
            txtTuKhoa.TabIndex = 0;
            lblLocLop.Text = "Lớp";
            lblLocLop.AutoSize = true;
            lblLocLop.Location = new Point(329, 20);
            cboLocLop.Name = "cboLocLop";
            cboLocLop.Location = new Point(366, 16);
            cboLocLop.Size = new Size(150, 28);
            cboLocLop.TabIndex = 1;
            lblDiemTu.Text = "Điểm từ";
            lblDiemTu.AutoSize = true;
            lblDiemTu.Location = new Point(529, 20);
            nudDiemTu.Name = "nudDiemTu";
            nudDiemTu.Location = new Point(592, 16);
            nudDiemTu.Size = new Size(70, 27);
            nudDiemTu.Minimum = 0;
            nudDiemTu.Maximum = 10;
            nudDiemTu.DecimalPlaces = 1;
            nudDiemTu.Increment = 0.1m;
            nudDiemTu.TabIndex = 2;
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.Location = new Point(678, 14);
            btnTimKiem.Size = new Size(105, 32);
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.TabIndex = 3;
            btnHienThiTatCa.Name = "btnHienThiTatCa";
            btnHienThiTatCa.Text = "Hiển thị tất cả";
            btnHienThiTatCa.Location = new Point(794, 14);
            btnHienThiTatCa.Size = new Size(145, 32);
            btnHienThiTatCa.TabIndex = 4;
            btnHienThiTatCa.UseVisualStyleBackColor = true;
            lblDanhSach.Text = "Danh sách sinh viên";
            lblDanhSach.AutoSize = true;
            lblDanhSach.Location = new Point(35, 324);
            lblTongSo.Name = "lblTongSo";
            lblTongSo.Text = "Tổng số: 0 sinh viên";
            lblTongSo.AutoSize = true;
            lblTongSo.Location = new Point(600, 324);
            lblTongSo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            // Các cột dùng DataPropertyName để lấy thuộc tính của SinhVien.
            colMaSV.HeaderText = "Mã SV";
            colMaSV.DataPropertyName = "MaSV";
            colHoTen.HeaderText = "Họ tên";
            colHoTen.DataPropertyName = "HoTen";
            colHoTen.FillWeight = 150;
            colNgaySinh.HeaderText = "Ngày sinh";
            colNgaySinh.DataPropertyName = "NgaySinh";
            colNgaySinh.DefaultCellStyle.Format = "dd/MM/yyyy";
            colGioiTinh.HeaderText = "Giới tính";
            colGioiTinh.DataPropertyName = "GioiTinh";
            colEmail.HeaderText = "Email";
            colEmail.DataPropertyName = "Email";
            colEmail.FillWeight = 160;
            colSDT.HeaderText = "Điện thoại";
            colSDT.DataPropertyName = "SDT";
            colLop.HeaderText = "Lớp";
            colLop.DataPropertyName = "TenLop";
            colDiem.HeaderText = "Điểm";
            colDiem.DataPropertyName = "Diem";
            colDiem.DefaultCellStyle.Format = "N1";
            colTrangThai.HeaderText = "Trạng thái";
            colTrangThai.DataPropertyName = "TrangThai";
            dgvSinhVien.Name = "dgvSinhVien";
            dgvSinhVien.Location = new Point(35, 356);
            dgvSinhVien.Size = new Size(952, 220);
            dgvSinhVien.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSinhVien.AutoGenerateColumns = false;
            dgvSinhVien.Columns.AddRange(colMaSV, colHoTen, colNgaySinh, colGioiTinh,
                colEmail, colSDT, colLop, colDiem, colTrangThai);
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AllowUserToDeleteRows = false;
            dgvSinhVien.RowHeadersVisible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1023, 600);
            MinimumSize = new Size(1041, 647);
            Text = "Quản lý sinh viên";
            StartPosition = FormStartPosition.CenterScreen;
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
            Name = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)nudDiem).EndInit();
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
        private DataGridViewTextBoxColumn colMaSV;
        private DataGridViewTextBoxColumn colHoTen;
        private DataGridViewTextBoxColumn colNgaySinh;
        private DataGridViewTextBoxColumn colGioiTinh;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colSDT;
        private DataGridViewTextBoxColumn colLop;
        private DataGridViewTextBoxColumn colDiem;
        private DataGridViewTextBoxColumn colTrangThai;
    }
}


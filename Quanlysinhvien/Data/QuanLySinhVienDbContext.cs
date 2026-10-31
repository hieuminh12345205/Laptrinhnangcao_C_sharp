using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using Quanlysinhvien.Data.Entity;

namespace Quanlysinhvien.Data;

// DbContext là cầu nối giữa các đối tượng C# và các bảng MySQL.
public class QuanLySinhVienDbContext : DbContext
{
    public DbSet<LopHoc> LopHocs => Set<LopHoc>();
    public DbSet<SinhVien> SinhViens => Set<SinhVien>();

    public QuanLySinhVienDbContext() { }

    public QuanLySinhVienDbContext(DbContextOptions<QuanLySinhVienDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Cùng file mysql.local.json với ADO.NET; không ghi mật khẩu trong code.
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseMySQL(DataHelper.LayChuoiKetNoi());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasCharSet("utf8mb4");
        MySQLModelBuilderExtensions.UseCollation(modelBuilder, "utf8mb4_unicode_ci");

        modelBuilder.Entity<LopHoc>(lop =>
        {
            lop.ToTable("lophoc", bang =>
            {
                bang.HasCheckConstraint("chk_lophoc_malop", "CHAR_LENGTH(TRIM(MaLop)) > 0");
                bang.HasCheckConstraint("chk_lophoc_tenlop", "CHAR_LENGTH(TRIM(TenLop)) > 0");
            });
            // MaLop không theo quy ước Id của EF nên phải chỉ rõ khóa chính.
            lop.HasKey(x => x.MaLop).HasName("PRIMARY");
            lop.Property(x => x.MaLop).HasColumnType("varchar(20)").ValueGeneratedNever();
            lop.Property(x => x.TenLop).HasColumnType("varchar(100)").IsRequired();
        });

        modelBuilder.Entity<SinhVien>(sv =>
        {
            sv.ToTable("sinhvien", bang =>
            {
                bang.HasCheckConstraint("chk_sinhvien_masv", "CHAR_LENGTH(TRIM(MaSV)) > 0");
                bang.HasCheckConstraint("chk_sinhvien_hoten", "CHAR_LENGTH(TRIM(HoTen)) > 0");
                bang.HasCheckConstraint("chk_sinhvien_email", "CHAR_LENGTH(TRIM(Email)) > 0");
                bang.HasCheckConstraint("chk_sinhvien_sdt", "SDT REGEXP '^0[0-9]{9}$'");
                bang.HasCheckConstraint("chk_sinhvien_gioitinh", "GioiTinh IN ('Nam', 'Nữ')");
                bang.HasCheckConstraint("chk_sinhvien_diem", "Diem BETWEEN 0 AND 10");
                bang.HasCheckConstraint("chk_sinhvien_trangthai", "TrangThai IN ('Đang học', 'Bảo lưu', 'Đã tốt nghiệp')");
            });
            sv.HasKey(x => x.MaSV).HasName("PRIMARY");
            sv.Property(x => x.MaSV).HasColumnType("varchar(20)").ValueGeneratedNever();
            sv.Property(x => x.HoTen).HasColumnType("varchar(100)").IsRequired();
            sv.Property(x => x.NgaySinh).HasColumnType("date").IsRequired();
            sv.Property(x => x.GioiTinh).HasColumnType("varchar(3)").IsRequired();
            sv.Property(x => x.Email).HasColumnType("varchar(254)").IsRequired();
            sv.Property(x => x.SDT).HasColumnType("varchar(10)").IsRequired();
            sv.Property(x => x.Diem).HasColumnType("decimal(3,1)").HasDefaultValue(0m);
            sv.Property(x => x.TrangThai).HasColumnType("varchar(30)")
                .IsRequired().HasDefaultValue("Đang học");

            // MaLop là khóa ngoại ẩn: EF quản lý nó qua thuộc tính LopHoc.
            // Cách này giữ nguyên entity và các control của bài ADO.NET.
            sv.Property<string>("MaLop").HasColumnType("varchar(20)").IsRequired();
            sv.HasOne(x => x.LopHoc).WithMany(x => x.SinhViens)
                .HasForeignKey("MaLop").IsRequired()
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_sinhvien_lophoc");
            sv.HasIndex("MaLop").HasDatabaseName("fk_sinhvien_lophoc");

            // TenLop chỉ dùng để hiển thị trên form, không phải cột của sinhvien.
            sv.Ignore(x => x.TenLop);
        });
    }
}

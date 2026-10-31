using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using QLTVDAO.Entities;

namespace QLTVDAO;

public class BookDbContext : DbContext
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<BookCopy> BookCopies => Set<BookCopy>();

    public BookDbContext() { }
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseMySQL(DataHelper.LayChuoiKetNoi());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasCharSet("utf8mb4");
        MySQLModelBuilderExtensions.UseCollation(modelBuilder, "utf8mb4_unicode_ci");

        modelBuilder.Entity<Category>(category =>
        {
            category.ToTable("Categories");
            category.Property(x => x.Description).IsRequired().HasMaxLength(100);
        });
        modelBuilder.Entity<Book>(book =>
        {
            book.ToTable("Books", table =>
            {
                table.HasCheckConstraint("CK_Books_Price", "Price >= 0 AND Price <= 999999999");
                table.HasCheckConstraint("CK_Books_TotalCopies", "TotalCopies BETWEEN 0 AND 100000");
                table.HasCheckConstraint("CK_Books_Year", "PublicationYear BETWEEN 1000 AND 9999");
            });
            book.Property(x => x.Price).HasPrecision(18, 2);
            // Restrict tránh xóa danh mục rồi mất toàn bộ đầu sách ngoài ý muốn.
            book.HasOne(x => x.Category).WithMany(x => x.Books)
                .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<BookCopy>(copy =>
        {
            copy.ToTable("BookCopies");
            copy.HasOne(x => x.Book).WithMany(x => x.BookCopies)
                .HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}

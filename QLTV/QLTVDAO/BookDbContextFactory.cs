using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MySql.EntityFrameworkCore.Extensions;

namespace QLTVDAO;

public class BookDbContextFactory : IDesignTimeDbContextFactory<BookDbContext>
{
    public BookDbContext CreateDbContext(string[] args)
    {
        string thuMuc = Path.GetDirectoryName(typeof(BookDbContext).Assembly.Location)!;
        var options = new DbContextOptionsBuilder<BookDbContext>();
        options.UseMySQL(DataHelper.LayChuoiKetNoi(thuMuc));
        return new BookDbContext(options.Options);
    }
}

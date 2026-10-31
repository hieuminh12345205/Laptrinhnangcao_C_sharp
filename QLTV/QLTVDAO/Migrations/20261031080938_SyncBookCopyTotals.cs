using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLTVDAO.Migrations
{
    /// <inheritdoc />
    public partial class SyncBookCopyTotals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Books_TotalCopies",
                table: "Books");

            // Đồng bộ dữ liệu cũ, kể cả đầu sách chưa có bản sao nào (tổng bằng 0).
            migrationBuilder.Sql(@"
                UPDATE Books b
                LEFT JOIN (SELECT BookId, COUNT(*) AS SoBan FROM BookCopies GROUP BY BookId) c
                    ON b.BookId = c.BookId
                SET b.TotalCopies = COALESCE(c.SoBan, 0);");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Books_TotalCopies",
                table: "Books",
                sql: "TotalCopies BETWEEN 0 AND 100000");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Books_TotalCopies",
                table: "Books");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Books_TotalCopies",
                table: "Books",
                sql: "TotalCopies BETWEEN 1 AND 100000");
        }
    }
}

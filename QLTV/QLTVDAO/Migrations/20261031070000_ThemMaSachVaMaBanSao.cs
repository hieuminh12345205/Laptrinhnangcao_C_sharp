using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLTVDAO.Migrations
{
    /// <inheritdoc />
    public partial class ThemMaSachVaMaBanSao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Số bản sao nay tính từ số dòng BookCopies nên cột khai báo tay không còn dùng.
            // Phải bỏ check constraint trước vì MySQL không cho xóa cột đang bị ràng buộc tham chiếu.
            migrationBuilder.DropCheckConstraint(
                name: "CK_Books_TotalCopies",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "TotalCopies",
                table: "Books");

            // Author trở thành bắt buộc: dọn các dòng cũ đang NULL trước khi đổi sang NOT NULL.
            migrationBuilder.Sql("UPDATE `Books` SET `Author` = '' WHERE `Author` IS NULL;");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Books",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaSach",
                table: "Books",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            // Dữ liệu cũ chưa có mã: sinh B001, B002... theo BookId để unique index không báo trùng.
            migrationBuilder.Sql(
                "UPDATE `Books` SET `MaSach` = CONCAT('B', LPAD(`BookId`, 3, '0')) WHERE `MaSach` = '';");

            migrationBuilder.AddColumn<string>(
                name: "MaBanSao",
                table: "BookCopies",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            // Đánh số bản sao cũ theo từng đầu sách: B001.001, B001.002...
            migrationBuilder.Sql(@"
UPDATE `BookCopies` c
JOIN (
    SELECT c2.`BookCopyId` AS id,
           CONCAT(b2.`MaSach`, '.', LPAD(
               ROW_NUMBER() OVER (PARTITION BY c2.`BookId` ORDER BY c2.`BookCopyId`), 3, '0')) AS ma
    FROM `BookCopies` c2
    JOIN `Books` b2 ON b2.`BookId` = c2.`BookId`
) t ON t.id = c.`BookCopyId`
SET c.`MaBanSao` = t.ma
WHERE c.`MaBanSao` = '';");

            migrationBuilder.CreateIndex(
                name: "IX_Books_MaSach",
                table: "Books",
                column: "MaSach",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_MaBanSao",
                table: "BookCopies",
                column: "MaBanSao",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookCopies_MaBanSao",
                table: "BookCopies");

            migrationBuilder.DropIndex(
                name: "IX_Books_MaSach",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "MaBanSao",
                table: "BookCopies");

            migrationBuilder.DropColumn(
                name: "MaSach",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Books",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            // Khôi phục cột cũ với giá trị 1 để không vi phạm check constraint BETWEEN 1 AND 100000.
            migrationBuilder.AddColumn<int>(
                name: "TotalCopies",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Books_TotalCopies",
                table: "Books",
                sql: "TotalCopies BETWEEN 1 AND 100000");
        }
    }
}

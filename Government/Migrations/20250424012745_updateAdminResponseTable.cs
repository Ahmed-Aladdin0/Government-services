using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Government.Migrations
{
    /// <inheritdoc />
    public partial class updateAdminResponseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminResponses_AspNetUsers_userId",
                table: "AdminResponses");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "AdminResponses");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "AdminResponses",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AdminResponses_userId",
                table: "AdminResponses",
                newName: "IX_AdminResponses_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AdminResponses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01954439-8011-7cca-9a77-c5c56990be36",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBOhysGiUjpTUh36kMdkny0USXGHYTdtAacMTMSSDirSHvXUG+i1ommjcENJPzch9A==");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminResponses_AspNetUsers_UserId",
                table: "AdminResponses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminResponses_AspNetUsers_UserId",
                table: "AdminResponses");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AdminResponses",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_AdminResponses_UserId",
                table: "AdminResponses",
                newName: "IX_AdminResponses_userId");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "AdminResponses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "AdminResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01954439-8011-7cca-9a77-c5c56990be36",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEC7LyvqGKdurQQrI0/iRjUaHIhlUUtiT/O6gaxPpgehES7mhXDXAXuVSdg7nQp9qSA==");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminResponses_AspNetUsers_userId",
                table: "AdminResponses",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

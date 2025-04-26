using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Government.Migrations
{
    /// <inheritdoc />
    public partial class initailcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01954439-8011-7cca-9a77-c5c56990be36",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFk4rzQzImUy/ybn31pr22R6L1EukxEHj+gAQSrIBkMpCLP/EbrEn8vUfrFq5ZIZYg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01954439-8011-7cca-9a77-c5c56990be36",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAz9zwUGZ4tP3n92Euf9UY0ZAUcu2ag63RGgpSfZlTx/n//TO8Ve7jqxjYcEDGJzbw==");
        }
    }
}

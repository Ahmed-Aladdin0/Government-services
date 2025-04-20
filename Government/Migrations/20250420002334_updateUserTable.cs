using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Government.Migrations
{
    /// <inheritdoc />
    public partial class updateUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01954439-8011-7cca-9a77-c5c56990be36",
                columns: new[] { "IsDeleted", "PasswordHash" },
                values: new object[] { false, "AQAAAAIAAYagAAAAEM+n3jCYYJBC0myHsoyV/PORYghabi/FMTd+XL6qG6yKe7Ec8zNVkv6IIfJpZ5TEVw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01954439-8011-7cca-9a77-c5c56990be36",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMAAAm5j8vUo112zWQjBQETUHX1NZ5gB17emHo8m/sx8WUjlbB45V3idqsevRyihFg==");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Government.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "Account_Mangment", "01954439-8011-7cca-9a77-c5bf8fae0bae" },
                    { 2, "permissions", "admin.create_response", "01954439-8011-7cca-9a77-c5bf8fae0bae" },
                    { 3, "permissions", "auth.admin.login", "01954439-8011-7cca-9a77-c5bf8fae0bae" },
                    { 4, "permissions", "auth.admin.register", "01954439-8011-7cca-9a77-c5bf8fae0bae" },
                    { 5, "permissions", "auth.admin.resend_confirm_email", "01954439-8011-7cca-9a77-c5bf8fae0bae" },
                    { 6, "permissions", "services.view_all", "01954439-8011-7cca-9a77-c5bf8fae0bae" },
                    { 7, "permissions", "services.create", "01954439-8011-7cca-9a77-c5bf8fae0bae" },
                    { 8, "permissions", "services.update", "01954439-8011-7cca-9a77-c5bf8fae0bae" },
                    { 9, "permissions", "services.toggle_availability", "01954439-8011-7cca-9a77-c5bf8fae0bae" },
                    { 10, "permissions", "users:read", "01954439-8011-7cca-9a77-c5bf8fae0bae" },
                    { 11, "permissions", "users:add", "01954439-8011-7cca-9a77-c5bf8fae0bae" },
                    { 12, "permissions", "users:update", "01954439-8011-7cca-9a77-c5bf8fae0bae" },
                    { 13, "permissions", "roles:read", "01954439-8011-7cca-9a77-c5bf8fae0bae" },
                    { 14, "permissions", "roles:add", "01954439-8011-7cca-9a77-c5bf8fae0bae" },
                    { 15, "permissions", "roles:update", "01954439-8011-7cca-9a77-c5bf8fae0bae" },
                    { 16, "permissions", "results:read", "01954439-8011-7cca-9a77-c5bf8fae0bae" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "01954439-8011-7cca-9a77-c5bf8fae0bae", "01954439-8011-7cca-9a77-c5c56990be36" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01954439-8011-7cca-9a77-c5c56990be36",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMAAAm5j8vUo112zWQjBQETUHX1NZ5gB17emHo8m/sx8WUjlbB45V3idqsevRyihFg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "01954439-8011-7cca-9a77-c5bf8fae0bae", "01954439-8011-7cca-9a77-c5c56990be36" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01954439-8011-7cca-9a77-c5c56990be36",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEP+M3uC58jh0jOAcKXvKgA365gL148XNSwYfwMW1t1Gp4VZ1Ro2UjrF+RU/9QbYVOQ==");
        }
    }
}

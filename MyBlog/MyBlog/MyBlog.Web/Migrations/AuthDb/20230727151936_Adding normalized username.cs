using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Addingnormalizedusername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad085342-f8e6-411e-8c9b-3e41dfb4a1a2",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29fda475-c84a-4541-bdce-70771aa95f28", "SUPERADMIN@MYBLOG.COM", "SUPERADMIN@MYBLOG.COM", "AQAAAAEAACcQAAAAEJak+WXTq6hKW1CSODMu3VKcYwKRipVARq2gBif/66L4elcNEyf/aythY/dryhufrQ==", "2d726c1b-5151-4fc7-b45c-b27e7c13974e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad085342-f8e6-411e-8c9b-3e41dfb4a1a2",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70e4be0e-cbb8-4ebd-a73f-1bbf222aeb39", null, null, "AQAAAAEAACcQAAAAEAJXpx8AJGUWhwGXMSl68/+BGOOPn5IxBCotLzppbrp7NvtNHvBa5NrDvFw7CHz0pA==", "610e295c-7a5c-4447-ab22-732b00cf3458" });
        }
    }
}

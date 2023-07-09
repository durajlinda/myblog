using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class NewMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad085342-f8e6-411e-8c9b-3e41dfb4a1a2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70e4be0e-cbb8-4ebd-a73f-1bbf222aeb39", "AQAAAAEAACcQAAAAEAJXpx8AJGUWhwGXMSl68/+BGOOPn5IxBCotLzppbrp7NvtNHvBa5NrDvFw7CHz0pA==", "610e295c-7a5c-4447-ab22-732b00cf3458" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad085342-f8e6-411e-8c9b-3e41dfb4a1a2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a5d9803-d006-4e5d-9f66-9c79e1c8b6a7", "AQAAAAEAACcQAAAAEDq7xW2HIvraC4tjBOCmyVjKjyf0WJyBNsc8/g3uYBiC94xaGxspziVgsG8VUmcstg==", "7f2216a1-35e9-4135-bcd3-d5ecdcca9ced" });
        }
    }
}

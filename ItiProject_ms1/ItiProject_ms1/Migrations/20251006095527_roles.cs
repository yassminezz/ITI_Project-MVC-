using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ItiProject_ms1.Migrations
{
    /// <inheritdoc />
    public partial class roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11adb370-9ce4-43b5-ab6a-e83b49e175a2", "1cf1f7e7-2466-4441-abb1-90d3166374aa", "Instructor", "instructor" },
                    { "12b0ec3e-292c-4364-88a8-8dae4989776c", "37a00841-707c-422a-ba34-09ca9658d9ea", "Hr", "hr" },
                    { "5abda937-47a2-4ee4-9aaa-bb863284b154", "d5586244-93c1-4f50-8eb2-000397d3ac4d", "Admin", "admin" },
                    { "f7170d2c-a630-4771-89b2-d545ff361131", "4cc30ff5-6c8e-4512-a703-232e6db4369f", "Student", "student" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11adb370-9ce4-43b5-ab6a-e83b49e175a2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12b0ec3e-292c-4364-88a8-8dae4989776c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5abda937-47a2-4ee4-9aaa-bb863284b154");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7170d2c-a630-4771-89b2-d545ff361131");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ItiProject_ms1.Migrations
{
    /// <inheritdoc />
    public partial class linkingUserWithOthers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Instructors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "17bd22f6-46bb-4ef4-ac54-8e99b7df8167", "b84097b8-7bce-475f-a4cf-a901d6150d1e", "Hr", "hr" },
                    { "1d89d953-7843-444a-99da-f6011753f70f", "380247b6-23e1-46cc-a6fe-1a1846fcdf24", "Admin", "admin" },
                    { "7544430b-fecb-4e0c-9bff-e6cbe464b8cd", "38b5752e-25f7-43e8-94fa-5563d8c8564c", "Instructor", "instructor" },
                    { "c566d0b9-3e92-4dce-9317-c75a83f6bd0a", "f453729b-1f94-4cb2-b92a-e298ba137798", "Student", "student" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_UserId",
                table: "Instructors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_AspNetUsers_UserId",
                table: "Instructors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_AspNetUsers_UserId",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_UserId",
                table: "Instructors");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17bd22f6-46bb-4ef4-ac54-8e99b7df8167");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d89d953-7843-444a-99da-f6011753f70f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7544430b-fecb-4e0c-9bff-e6cbe464b8cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c566d0b9-3e92-4dce-9317-c75a83f6bd0a");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Instructors");

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
    }
}

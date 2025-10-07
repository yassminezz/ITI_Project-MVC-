using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItiProject_ms1.Migrations
{
    /// <inheritdoc />
    public partial class correctCorseInstRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_instructors_InstructorId",
                table: "courses");

            migrationBuilder.AlterColumn<int>(
                name: "InstructorId",
                table: "courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_courses_instructors_InstructorId",
                table: "courses",
                column: "InstructorId",
                principalTable: "instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_instructors_InstructorId",
                table: "courses");

            migrationBuilder.AlterColumn<int>(
                name: "InstructorId",
                table: "courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_instructors_InstructorId",
                table: "courses",
                column: "InstructorId",
                principalTable: "instructors",
                principalColumn: "Id");
        }
    }
}

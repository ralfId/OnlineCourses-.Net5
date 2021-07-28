using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ttrytofix_onetoone_pricescourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Courses_PriceId",
                table: "Prices");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_CourseId",
                table: "Prices",
                column: "CourseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Courses_CourseId",
                table: "Prices",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Courses_CourseId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_CourseId",
                table: "Prices");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Courses_PriceId",
                table: "Prices",
                column: "PriceId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

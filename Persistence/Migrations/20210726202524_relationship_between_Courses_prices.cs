using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class relationship_between_Courses_prices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseInstructor_Courses_CourseId",
                table: "CourseInstructor");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseInstructor_Instructors_InstructorId",
                table: "CourseInstructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Courses_CourseId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_CourseId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_CourseInstructor_CourseId",
                table: "CourseInstructor");

            migrationBuilder.AddColumn<Guid>(
                name: "CoursesCourseId",
                table: "CourseInstructor",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InstructorsInstructorId",
                table: "CourseInstructor",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseInstructor_CoursesCourseId",
                table: "CourseInstructor",
                column: "CoursesCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseInstructor_InstructorsInstructorId",
                table: "CourseInstructor",
                column: "InstructorsInstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInstructor_Courses_CoursesCourseId",
                table: "CourseInstructor",
                column: "CoursesCourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInstructor_Instructors_InstructorsInstructorId",
                table: "CourseInstructor",
                column: "InstructorsInstructorId",
                principalTable: "Instructors",
                principalColumn: "InstructorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Courses_PriceId",
                table: "Prices",
                column: "PriceId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseInstructor_Courses_CoursesCourseId",
                table: "CourseInstructor");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseInstructor_Instructors_InstructorsInstructorId",
                table: "CourseInstructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Courses_PriceId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_CourseInstructor_CoursesCourseId",
                table: "CourseInstructor");

            migrationBuilder.DropIndex(
                name: "IX_CourseInstructor_InstructorsInstructorId",
                table: "CourseInstructor");

            migrationBuilder.DropColumn(
                name: "CoursesCourseId",
                table: "CourseInstructor");

            migrationBuilder.DropColumn(
                name: "InstructorsInstructorId",
                table: "CourseInstructor");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_CourseId",
                table: "Prices",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseInstructor_CourseId",
                table: "CourseInstructor",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInstructor_Courses_CourseId",
                table: "CourseInstructor",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInstructor_Instructors_InstructorId",
                table: "CourseInstructor",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "InstructorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Courses_CourseId",
                table: "Prices",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

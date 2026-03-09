using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeTrainerAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_AspNetUsers_MentorId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_MentorId",
                table: "Quizzes");

            migrationBuilder.AlterColumn<string>(
                name: "MentorId",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MentorId",
                table: "Quizzes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_MentorId",
                table: "Quizzes",
                column: "MentorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_AspNetUsers_MentorId",
                table: "Quizzes",
                column: "MentorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

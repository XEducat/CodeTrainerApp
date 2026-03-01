using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeTrainerAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizTitle",
                table: "UserHistories");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserHistories");

            migrationBuilder.RenameColumn(
                name: "TotalQuestions",
                table: "UserHistories",
                newName: "MaxScore");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserHistories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_QuizId",
                table: "UserHistories",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_UserId",
                table: "UserHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserHistories_AspNetUsers_UserId",
                table: "UserHistories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHistories_Quizzes_QuizId",
                table: "UserHistories",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHistories_AspNetUsers_UserId",
                table: "UserHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_UserHistories_Quizzes_QuizId",
                table: "UserHistories");

            migrationBuilder.DropIndex(
                name: "IX_UserHistories_QuizId",
                table: "UserHistories");

            migrationBuilder.DropIndex(
                name: "IX_UserHistories_UserId",
                table: "UserHistories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserHistories");

            migrationBuilder.RenameColumn(
                name: "MaxScore",
                table: "UserHistories",
                newName: "TotalQuestions");

            migrationBuilder.AddColumn<string>(
                name: "QuizTitle",
                table: "UserHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "UserHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

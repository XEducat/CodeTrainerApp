using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeTrainerAPI.Migrations
{
    /// <inheritdoc />
    public partial class EditQuizes1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgrammingTasks_Quizzes_QuizId",
                table: "ProgrammingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TestCases_ProgrammingTasks_ProgrammingTaskId",
                table: "TestCases");

            migrationBuilder.AlterColumn<int>(
                name: "ProgrammingTaskId",
                table: "TestCases",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "QuizId",
                table: "ProgrammingTasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgrammingTasks_Quizzes_QuizId",
                table: "ProgrammingTasks",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestCases_ProgrammingTasks_ProgrammingTaskId",
                table: "TestCases",
                column: "ProgrammingTaskId",
                principalTable: "ProgrammingTasks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgrammingTasks_Quizzes_QuizId",
                table: "ProgrammingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TestCases_ProgrammingTasks_ProgrammingTaskId",
                table: "TestCases");

            migrationBuilder.AlterColumn<int>(
                name: "ProgrammingTaskId",
                table: "TestCases",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuizId",
                table: "ProgrammingTasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgrammingTasks_Quizzes_QuizId",
                table: "ProgrammingTasks",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestCases_ProgrammingTasks_ProgrammingTaskId",
                table: "TestCases",
                column: "ProgrammingTaskId",
                principalTable: "ProgrammingTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

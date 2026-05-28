using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeTrainerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAnswersToHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAnswersJson",
                table: "UserHistories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAnswersJson",
                table: "UserHistories");
        }
    }
}

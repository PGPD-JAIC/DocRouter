using Microsoft.EntityFrameworkCore.Migrations;

namespace DocRouter.Persistence.Migrations
{
    public partial class addFolderIdToSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "Submissions");

            migrationBuilder.AddColumn<string>(
                name: "FolderId",
                table: "Submissions",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListId",
                table: "Submissions",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FolderId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "ListId",
                table: "Submissions");

            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "Submissions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}

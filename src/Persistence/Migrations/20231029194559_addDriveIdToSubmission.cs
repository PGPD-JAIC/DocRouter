using Microsoft.EntityFrameworkCore.Migrations;

namespace DocRouter.Persistence.Migrations
{
    public partial class addDriveIdToSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriveId",
                table: "Submissions",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriveId",
                table: "Submissions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DocRouter.Persistence.Migrations
{
    public partial class addDriveIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriveId",
                table: "Items",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriveId",
                table: "Items");
        }
    }
}

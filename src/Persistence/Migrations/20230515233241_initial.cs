using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DocRouter.Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    Edited = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    FolderUri = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    Edited = table.Column<DateTime>(nullable: true),
                    ItemName = table.Column<string>(maxLength: 100, nullable: false),
                    ItemUri = table.Column<string>(maxLength: 1000, nullable: false),
                    SubmissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    Edited = table.Column<DateTime>(nullable: true),
                    SubmissionId = table.Column<int>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    RoutedTo = table.Column<string>(maxLength: 100, nullable: false),
                    Comments = table.Column<string>(maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_SubmissionId",
                table: "Items",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SubmissionId",
                table: "Transactions",
                column: "SubmissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Submissions");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Finapp.Database.Migrations.Migrations
{
    public partial class InitialModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fip");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "fip",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "FinancialStatuses",
                schema: "fip",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Source = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    UserName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialStatuses_Users_UserName",
                        column: x => x.UserName,
                        principalSchema: "fip",
                        principalTable: "Users",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialStatuses_Source",
                schema: "fip",
                table: "FinancialStatuses",
                column: "Source");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialStatuses_UserName",
                schema: "fip",
                table: "FinancialStatuses",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialStatuses",
                schema: "fip");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "fip");
        }
    }
}

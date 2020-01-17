using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kanayri.Application.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("e93eb40d-62bc-4541-bca3-d3e3c382f66b"), "iPhone 6 Plus" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("27077b96-c6d9-4703-9e8f-acfc59b75566"), "iPhone 7 Plus" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kanayri.Persistence.Migrations
{
    public partial class AddedAggregates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1e258593-ba30-4202-aaf4-74b514631ee1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("84a5fd20-b59f-407e-949b-7704d0586ef0"));

            migrationBuilder.CreateTable(
                name: "Aggregate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TotalEvents = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Version = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aggregate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    AggregateId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Aggregate_AggregateId",
                        column: x => x.AggregateId,
                        principalTable: "Aggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { new Guid("9d6f6d5c-ce5d-4bee-958a-692230fb625f"), "iPhone 6 Plus", 600m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { new Guid("249f6399-b601-4c67-a81c-66978c190f52"), "iPhone 7 Plus", 700m });

            migrationBuilder.CreateIndex(
                name: "IX_Events_AggregateId",
                table: "Events",
                column: "AggregateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Aggregate");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("249f6399-b601-4c67-a81c-66978c190f52"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9d6f6d5c-ce5d-4bee-958a-692230fb625f"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { new Guid("84a5fd20-b59f-407e-949b-7704d0586ef0"), "iPhone 6 Plus", 600m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { new Guid("1e258593-ba30-4202-aaf4-74b514631ee1"), "iPhone 7 Plus", 700m });
        }
    }
}

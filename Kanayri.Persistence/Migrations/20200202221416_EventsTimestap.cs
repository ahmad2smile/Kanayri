using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kanayri.Persistence.Migrations
{
    public partial class EventsTimestap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("249f6399-b601-4c67-a81c-66978c190f52"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9d6f6d5c-ce5d-4bee-958a-692230fb625f"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { new Guid("fa180cfd-12a8-4dfc-baa4-994169b432a8"), "iPhone 6 Plus", 600m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { new Guid("663462f7-e699-4373-afa9-17351d45ccb1"), "iPhone 7 Plus", 700m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("663462f7-e699-4373-afa9-17351d45ccb1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fa180cfd-12a8-4dfc-baa4-994169b432a8"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Events");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { new Guid("9d6f6d5c-ce5d-4bee-958a-692230fb625f"), "iPhone 6 Plus", 600m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { new Guid("249f6399-b601-4c67-a81c-66978c190f52"), "iPhone 7 Plus", 700m });
        }
    }
}

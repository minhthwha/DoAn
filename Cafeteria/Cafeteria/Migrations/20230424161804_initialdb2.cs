using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafeteria.Migrations
{
    public partial class initialdb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuestAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuestName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuestPhoneNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "GuestName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "GuestPhoneNumber",
                table: "Orders");
        }
    }
}

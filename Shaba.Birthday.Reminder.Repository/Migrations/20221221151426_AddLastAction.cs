using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shaba.Birthday.Reminder.Repository.Migrations
{
    public partial class AddLastAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastAction",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastAction",
                table: "Users");
        }
    }
}

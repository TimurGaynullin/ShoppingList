using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingList.DataBase.Migrations
{
    public partial class Initial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAlive",
                table: "Password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAlive",
                table: "Password",
                nullable: false,
                defaultValue: false);
        }
    }
}

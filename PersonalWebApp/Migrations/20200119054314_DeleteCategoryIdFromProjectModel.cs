using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalWebApp.Migrations
{
    public partial class DeleteCategoryIdFromProjectModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Projects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Projects",
                nullable: false,
                defaultValue: 0);
        }
    }
}

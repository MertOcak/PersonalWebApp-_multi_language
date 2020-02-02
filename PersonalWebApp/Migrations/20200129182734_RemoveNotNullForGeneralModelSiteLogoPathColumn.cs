using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalWebApp.Migrations
{
    public partial class RemoveNotNullForGeneralModelSiteLogoPathColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SiteLogoPath",
                table: "Generals",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SiteLogoPath",
                table: "Generals",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalWebApp.Migrations
{
    public partial class CreatedAboutSkillSkillExperienceBlogBlogCategoriesContactRequestAndGeneralModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    BlogId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.BlogId);
                });

            migrationBuilder.CreateTable(
                name: "ContactRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExperienceName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Duration = table.Column<string>(nullable: false),
                    Color = table.Column<string>(nullable: false),
                    Icon = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SiteColor = table.Column<string>(nullable: false),
                    SiteLogoPath = table.Column<string>(nullable: false),
                    SiteOwnerJob = table.Column<string>(nullable: false),
                    SiteOwnerName = table.Column<string>(nullable: false),
                    SiteOwnerEmail = table.Column<string>(nullable: false),
                    SiteOwnerCountry = table.Column<string>(nullable: false),
                    SiteOwnerAddress = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(nullable: false),
                    BlogId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories", x => new { x.BlogId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_BlogCategories_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_CategoryId",
                table: "BlogCategories",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogCategories");

            migrationBuilder.DropTable(
                name: "ContactRequests");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Generals");

            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}

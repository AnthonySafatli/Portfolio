using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Migrations
{
    /// <inheritdoc />
    public partial class AddedTechStackItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechStackItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PathToImage = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechStackItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTechStack",
                columns: table => new
                {
                    ProjectsId = table.Column<string>(type: "TEXT", nullable: false),
                    TechStackItemsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTechStack", x => new { x.ProjectsId, x.TechStackItemsId });
                    table.ForeignKey(
                        name: "FK_ProjectTechStack_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTechStack_TechStackItems_TechStackItemsId",
                        column: x => x.TechStackItemsId,
                        principalTable: "TechStackItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTechStack_TechStackItemsId",
                table: "ProjectTechStack",
                column: "TechStackItemsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectTechStack");

            migrationBuilder.DropTable(
                name: "TechStackItems");
        }
    }
}

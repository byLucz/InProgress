using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XKANBAN.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnRelationToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_TableColumns_SColumnId",
                table: "Carts");

            migrationBuilder.DropTable(
                name: "TableColumns");

            migrationBuilder.CreateTable(
                name: "CustomColumns",
                columns: table => new
                {
                    ColumnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomColumns", x => x.ColumnId);
                    table.ForeignKey(
                        name: "FK_CustomColumns_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomColumns_ProjectId",
                table: "CustomColumns",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_CustomColumns_SColumnId",
                table: "Carts",
                column: "SColumnId",
                principalTable: "CustomColumns",
                principalColumn: "ColumnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_CustomColumns_SColumnId",
                table: "Carts");

            migrationBuilder.DropTable(
                name: "CustomColumns");

            migrationBuilder.CreateTable(
                name: "TableColumns",
                columns: table => new
                {
                    ColumnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableColumns", x => x.ColumnId);
                    table.ForeignKey(
                        name: "FK_TableColumns_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableColumns_ProjectId",
                table: "TableColumns",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_TableColumns_SColumnId",
                table: "Carts",
                column: "SColumnId",
                principalTable: "TableColumns",
                principalColumn: "ColumnId");
        }
    }
}

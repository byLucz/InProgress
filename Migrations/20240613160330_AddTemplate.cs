using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XKANBAN.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CartName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CartDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CartColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CartDeadLineDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CartIsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ColumnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnSlotNumber = table.Column<int>(type: "int", nullable: false),
                    ColumnIsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Templates");
        }
    }
}

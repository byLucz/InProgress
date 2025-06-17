using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XKANBAN.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartColor",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "CartDeadLineDate",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "CartDescription",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "CartIsDelete",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "CartName",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "ColumnIsActive",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "ColumnName",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "ColumnSlotNumber",
                table: "Templates");

            migrationBuilder.CreateTable(
                name: "CartTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeadLineDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartTemplate_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColumnTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlotNumber = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColumnTemplate_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartTemplate_TemplateId",
                table: "CartTemplate",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ColumnTemplate_TemplateId",
                table: "ColumnTemplate",
                column: "TemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartTemplate");

            migrationBuilder.DropTable(
                name: "ColumnTemplate");

            migrationBuilder.AddColumn<string>(
                name: "CartColor",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CartDeadLineDate",
                table: "Templates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CartDescription",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CartIsDelete",
                table: "Templates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CartName",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ColumnIsActive",
                table: "Templates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ColumnName",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColumnSlotNumber",
                table: "Templates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

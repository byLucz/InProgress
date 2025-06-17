using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XKANBAN.Migrations
{
    /// <inheritdoc />
    public partial class AddSlotNumberToColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SlotNumber",
                table: "CustomColumns",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlotNumber",
                table: "CustomColumns");
        }
    }
}

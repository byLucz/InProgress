using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XKANBAN.Migrations
{
    /// <inheritdoc />
    public partial class CleanUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssingId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Announcement");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssingId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Announcement",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

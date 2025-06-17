using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XKANBAN.Migrations
{
    /// <inheritdoc />
    public partial class fileuploaded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachedFile",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "AttachedFileName",
                table: "Carts");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "ChatMessages");

            migrationBuilder.AddColumn<byte[]>(
                name: "AttachedFile",
                table: "Carts",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttachedFileName",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

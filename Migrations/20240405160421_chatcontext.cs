using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XKANBAN.Migrations
{
    /// <inheritdoc />
    public partial class chatcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_Carts_CardId",
                table: "ChatMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMessage",
                table: "ChatMessage");

            migrationBuilder.RenameTable(
                name: "ChatMessage",
                newName: "ChatMessages");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessage_CardId",
                table: "ChatMessages",
                newName: "IX_ChatMessages_CardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages",
                column: "ChatMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Carts_CardId",
                table: "ChatMessages",
                column: "CardId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Carts_CardId",
                table: "ChatMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages");

            migrationBuilder.RenameTable(
                name: "ChatMessages",
                newName: "ChatMessage");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessages_CardId",
                table: "ChatMessage",
                newName: "IX_ChatMessage_CardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMessage",
                table: "ChatMessage",
                column: "ChatMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_Carts_CardId",
                table: "ChatMessage",
                column: "CardId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

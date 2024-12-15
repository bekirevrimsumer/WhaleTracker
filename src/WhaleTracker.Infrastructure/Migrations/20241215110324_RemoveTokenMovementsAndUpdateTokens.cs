using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhaleTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTokenMovementsAndUpdateTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TokenMovements");

            migrationBuilder.RenameColumn(
                name: "Symbol",
                table: "Tokens",
                newName: "TokenSymbol");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tokens",
                newName: "TokenIcon");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Tokens",
                newName: "TokenName");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Tokens",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Tokens",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TokenAddress",
                table: "Tokens",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "WalletId",
                table: "Tokens",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_WalletId",
                table: "Tokens",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Wallets_WalletId",
                table: "Tokens",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Wallets_WalletId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_WalletId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "TokenAddress",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Tokens");

            migrationBuilder.RenameColumn(
                name: "TokenSymbol",
                table: "Tokens",
                newName: "Symbol");

            migrationBuilder.RenameColumn(
                name: "TokenName",
                table: "Tokens",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "TokenIcon",
                table: "Tokens",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "TokenMovements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TokenId = table.Column<Guid>(type: "uuid", nullable: false),
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TransactionHash = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenMovements_Tokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Tokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TokenMovements_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TokenMovements_TokenId",
                table: "TokenMovements",
                column: "TokenId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenMovements_WalletId",
                table: "TokenMovements",
                column: "WalletId");
        }
    }
}

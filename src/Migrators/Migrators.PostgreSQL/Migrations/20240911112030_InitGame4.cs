using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitGame4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                schema: "Identity",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                schema: "Catalog",
                table: "Players",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameId",
                schema: "Catalog",
                table: "Players",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Games_GameId",
                schema: "Catalog",
                table: "Players",
                column: "GameId",
                principalSchema: "Catalog",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Games_GameId",
                schema: "Catalog",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_GameId",
                schema: "Catalog",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GameId",
                schema: "Catalog",
                table: "Players");
        }
    }
}

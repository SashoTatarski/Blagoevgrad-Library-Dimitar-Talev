using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Database.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservedBooks_Books_BookId1",
                table: "ReservedBooks");

            migrationBuilder.DropIndex(
                name: "IX_ReservedBooks_BookId1",
                table: "ReservedBooks");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "ReservedBooks");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedBooks_Books_BookId",
                table: "ReservedBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservedBooks_Books_BookId",
                table: "ReservedBooks");

            migrationBuilder.AddColumn<Guid>(
                name: "BookId1",
                table: "ReservedBooks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservedBooks_BookId1",
                table: "ReservedBooks",
                column: "BookId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedBooks_Books_BookId1",
                table: "ReservedBooks",
                column: "BookId1",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

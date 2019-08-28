using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Database.Migrations
{
    public partial class RemoveGenresFromBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Books_BookId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_BookId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Genres");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Genres",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_BookId",
                table: "Genres",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Books_BookId",
                table: "Genres",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

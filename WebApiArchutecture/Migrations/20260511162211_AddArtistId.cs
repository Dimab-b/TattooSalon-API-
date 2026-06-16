using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiArchutecture.Migrations
{
    /// <inheritdoc />
    public partial class AddArtistId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_signs_artists_artistId",
                table: "signs");

            migrationBuilder.RenameColumn(
                name: "artistId",
                table: "signs",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_signs_artistId",
                table: "signs",
                newName: "IX_signs_ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_signs_artists_ArtistId",
                table: "signs",
                column: "ArtistId",
                principalTable: "artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_signs_artists_ArtistId",
                table: "signs");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "signs",
                newName: "artistId");

            migrationBuilder.RenameIndex(
                name: "IX_signs_ArtistId",
                table: "signs",
                newName: "IX_signs_artistId");

            migrationBuilder.AddForeignKey(
                name: "FK_signs_artists_artistId",
                table: "signs",
                column: "artistId",
                principalTable: "artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

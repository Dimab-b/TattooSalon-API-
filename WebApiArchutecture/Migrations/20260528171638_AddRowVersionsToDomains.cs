using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiArchutecture.Migrations
{
    /// <inheritdoc />
    public partial class AddRowVersionsToDomains : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_signs_artists_ArtistId",
                table: "signs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tattoos",
                table: "tattoos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_artists",
                table: "artists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_signs",
                table: "signs");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "tattoos",
                newName: "Tattoos");

            migrationBuilder.RenameTable(
                name: "artists",
                newName: "Artists");

            migrationBuilder.RenameTable(
                name: "signs",
                newName: "SignUps");

            migrationBuilder.RenameIndex(
                name: "IX_signs_ArtistId",
                table: "SignUps",
                newName: "IX_SignUps_ArtistId");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Tattoos",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Artists",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "SignUps",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tattoos",
                table: "Tattoos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artists",
                table: "Artists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SignUps",
                table: "SignUps",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SignUps_Artists_ArtistId",
                table: "SignUps",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignUps_Artists_ArtistId",
                table: "SignUps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tattoos",
                table: "Tattoos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artists",
                table: "Artists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SignUps",
                table: "SignUps");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Tattoos");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "SignUps");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Tattoos",
                newName: "tattoos");

            migrationBuilder.RenameTable(
                name: "Artists",
                newName: "artists");

            migrationBuilder.RenameTable(
                name: "SignUps",
                newName: "signs");

            migrationBuilder.RenameIndex(
                name: "IX_SignUps_ArtistId",
                table: "signs",
                newName: "IX_signs_ArtistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tattoos",
                table: "tattoos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_artists",
                table: "artists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_signs",
                table: "signs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_signs_artists_ArtistId",
                table: "signs",
                column: "ArtistId",
                principalTable: "artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

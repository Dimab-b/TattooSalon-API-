using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiArchutecture.Migrations
{
    /// <inheritdoc />
    public partial class AddIsRevokedInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRevoked",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRevoked",
                table: "users");
        }
    }
}

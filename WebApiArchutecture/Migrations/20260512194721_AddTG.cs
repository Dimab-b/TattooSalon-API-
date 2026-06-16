using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiArchutecture.Migrations
{
    /// <inheritdoc />
    public partial class AddTG : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telegram_Tag",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telegram_Tag",
                table: "users");
        }
    }
}

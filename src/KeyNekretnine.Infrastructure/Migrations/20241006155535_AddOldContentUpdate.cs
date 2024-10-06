using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOldContentUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "content",
                table: "advert_updates",
                newName: "old_content");

            migrationBuilder.AddColumn<string>(
                name: "new_content",
                table: "advert_updates",
                type: "json",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "new_content",
                table: "advert_updates");

            migrationBuilder.RenameColumn(
                name: "old_content",
                table: "advert_updates",
                newName: "content");
        }
    }
}

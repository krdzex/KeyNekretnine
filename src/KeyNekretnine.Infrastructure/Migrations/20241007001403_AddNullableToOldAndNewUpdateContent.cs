using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableToOldAndNewUpdateContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "old_content",
                table: "advert_updates",
                type: "json",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "json");

            migrationBuilder.AlterColumn<string>(
                name: "new_content",
                table: "advert_updates",
                type: "json",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "json");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "old_content",
                table: "advert_updates",
                type: "json",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "json",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "new_content",
                table: "advert_updates",
                type: "json",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "json",
                oldNullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description_value",
                table: "adverts",
                newName: "description_sr");

            migrationBuilder.AddColumn<string>(
                name: "description_en",
                table: "adverts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description_en",
                table: "adverts");

            migrationBuilder.RenameColumn(
                name: "description_sr",
                table: "adverts",
                newName: "description_value");
        }
    }
}

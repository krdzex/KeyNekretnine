using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugsForCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "cities",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1,
                column: "slug",
                value: "andrijevica");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2,
                column: "slug",
                value: "bar");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3,
                column: "slug",
                value: "zabljak");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4,
                column: "slug",
                value: "savnik");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 5,
                column: "slug",
                value: "berane");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 6,
                column: "slug",
                value: "cetinje");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 7,
                column: "slug",
                value: "danilovgrad");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 8,
                column: "slug",
                value: "herceg-novi");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 9,
                column: "slug",
                value: "kolasin");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 10,
                column: "slug",
                value: "kotor");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 11,
                column: "slug",
                value: "mojkovac");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 12,
                column: "slug",
                value: "niksic");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 13,
                column: "slug",
                value: "plav");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 14,
                column: "slug",
                value: "pljevlja");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 15,
                column: "slug",
                value: "pluzine");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 16,
                column: "slug",
                value: "podgorica");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 17,
                column: "slug",
                value: "rozaje");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 18,
                column: "slug",
                value: "tivat");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 19,
                column: "slug",
                value: "ulcinj");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 20,
                column: "slug",
                value: "bijelo-polje");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 21,
                column: "slug",
                value: "budva");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 22,
                column: "slug",
                value: "tuzi");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 23,
                column: "slug",
                value: "petnjica");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 24,
                column: "slug",
                value: "gusinje");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "slug",
                table: "cities");
        }
    }
}

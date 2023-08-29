
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class ChangeCitiesId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "c29f1834-d40a-4511-8871-c6b2f1a2220b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "8fb31b92-23be-490b-8b1f-4b99608333c1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "da03954f-acab-4c5f-a1a4-2c37a03634f6");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1,
                column: "geo_id",
                value: "2319358");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2,
                column: "geo_id",
                value: "2319526");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3,
                column: "geo_id",
                value: "2319540");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4,
                column: "geo_id",
                value: "2319539");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 5,
                column: "geo_id",
                value: "2319359");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 6,
                column: "geo_id",
                value: "2319529");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 7,
                column: "geo_id",
                value: "2319530");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 8,
                column: "geo_id",
                value: "2187901");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 9,
                column: "geo_id",
                value: "2319531");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 10,
                column: "geo_id",
                value: "2319532");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 11,
                column: "geo_id",
                value: "2319533");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 12,
                column: "geo_id",
                value: "2319533");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 13,
                column: "geo_id",
                value: "2317882");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 14,
                column: "geo_id",
                value: "2319535");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 15,
                column: "geo_id",
                value: "2319536");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 16,
                column: "geo_id",
                value: "2319360");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 17,
                column: "geo_id",
                value: "2317936");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 18,
                column: "geo_id",
                value: "2319537");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 19,
                column: "geo_id",
                value: "2319538");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 20,
                column: "geo_id",
                value: "2319527");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 21,
                column: "geo_id",
                value: "2319528");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 22,
                column: "geo_id",
                value: "10141812");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 23,
                column: "geo_id",
                value: "7463938");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 24,
                column: "geo_id",
                value: "7460668");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "2e1c17f9-9317-4910-bff0-a6c57773d49c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "2afd5471-278d-4bf8-afd1-aec15c37f777");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "716fd211-dc76-42be-a914-62ad13e80792");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1,
                column: "geo_id",
                value: "297983629");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2,
                column: "geo_id",
                value: "298324414");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3,
                column: "geo_id",
                value: "298076995");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4,
                column: "geo_id",
                value: "299079819");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 5,
                column: "geo_id",
                value: "297983360");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 6,
                column: "geo_id",
                value: "298008175");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 7,
                column: "geo_id",
                value: "298134912");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 8,
                column: "geo_id",
                value: "298246430");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 9,
                column: "geo_id",
                value: "298271503");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 10,
                column: "geo_id",
                value: "297988513");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 11,
                column: "geo_id",
                value: "298230379");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 12,
                column: "geo_id",
                value: "297979150");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 13,
                column: "geo_id",
                value: "297986966");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 14,
                column: "geo_id",
                value: "298438579");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 15,
                column: "geo_id",
                value: "298163670");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 16,
                column: "geo_id",
                value: "298233944");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 17,
                column: "geo_id",
                value: "297978984");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 18,
                column: "geo_id",
                value: "298016342");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 19,
                column: "geo_id",
                value: "298023651");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 20,
                column: "geo_id",
                value: "298265596");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 21,
                column: "geo_id",
                value: "297988603");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 22,
                column: "geo_id",
                value: "298871101");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 23,
                column: "geo_id",
                value: "298605656");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 24,
                column: "geo_id",
                value: "299016015");
        }
    }
}

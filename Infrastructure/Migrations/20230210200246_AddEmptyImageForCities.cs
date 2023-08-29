using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class AddEmptyImageForCities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "12699fb8-6850-4b0b-9cb5-da46c209f271");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "90fcd409-22c8-4344-843a-db24121e8450");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "9811a514-7cb7-4c1b-bd4c-fe57d62998e9");

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "cities",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "007dd351-0f5b-4ea3-b83b-11d530bc37a5", "2306fd87-1855-46f9-97e5-951eee91406d", "User", "USER" },
                    { "5633f6db-66b0-4c20-ba14-1130d12a635d", "80f40a74-9d3c-459d-afb4-26762232af2a", "Administrator", "ADMINISTRATOR" },
                    { "71fb4e29-1775-41f6-b63d-97c7529dc285", "606cf4bd-aa78-4c45-bbd7-fa8193247dc6", "Moderator", "MODERATOR" }
                });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 5,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 6,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 7,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 8,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 9,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 10,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 11,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 12,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 13,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 14,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 15,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 16,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 17,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 18,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 19,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 20,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 21,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 22,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 23,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 24,
                column: "image_url",
                value: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "007dd351-0f5b-4ea3-b83b-11d530bc37a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "5633f6db-66b0-4c20-ba14-1130d12a635d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "71fb4e29-1775-41f6-b63d-97c7529dc285");

            migrationBuilder.DropColumn(
                name: "image_url",
                table: "cities");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "12699fb8-6850-4b0b-9cb5-da46c209f271", "05fee4be-963e-42ac-a1b3-25853c950fbe", "Administrator", "ADMINISTRATOR" },
                    { "90fcd409-22c8-4344-843a-db24121e8450", "f4f88101-2fd7-4d3d-aea8-aefd9cbd8083", "Moderator", "MODERATOR" },
                    { "9811a514-7cb7-4c1b-bd4c-fe57d62998e9", "399edbf5-8844-4dc6-a347-a95d23424286", "User", "USER" }
                });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class RenamingSomeFieldsAndAddingEnglish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "2b8a5b77-33da-49c3-a4ca-268b4ff5b990");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "5596677e-8128-4257-9724-e6232c4b4fd5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "67d39b13-c288-4227-b987-b27052c18616");

            migrationBuilder.RenameColumn(
                name: "no_of_badrooms",
                table: "adverts",
                newName: "no_of_bedrooms");

            migrationBuilder.AddColumn<string>(
                name: "description_en",
                table: "adverts",
                type: "character varying(10000)",
                maxLength: 10000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name_en",
                table: "advert_types",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name_en",
                table: "advert_statuses",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name_en",
                table: "advert_purposes",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "12699fb8-6850-4b0b-9cb5-da46c209f271", "05fee4be-963e-42ac-a1b3-25853c950fbe", "Administrator", "ADMINISTRATOR" },
                    { "90fcd409-22c8-4344-843a-db24121e8450", "f4f88101-2fd7-4d3d-aea8-aefd9cbd8083", "Moderator", "MODERATOR" },
                    { "9811a514-7cb7-4c1b-bd4c-fe57d62998e9", "399edbf5-8844-4dc6-a347-a95d23424286", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "advert_purposes",
                keyColumn: "id",
                keyValue: 1,
                column: "name_en",
                value: "Rent");

            migrationBuilder.UpdateData(
                table: "advert_purposes",
                keyColumn: "id",
                keyValue: 2,
                column: "name_en",
                value: "Sale");

            migrationBuilder.UpdateData(
                table: "advert_purposes",
                keyColumn: "id",
                keyValue: 3,
                column: "name_en",
                value: "Short term rent");

            migrationBuilder.UpdateData(
                table: "advert_statuses",
                keyColumn: "id",
                keyValue: 1,
                column: "name_en",
                value: "Accepted");

            migrationBuilder.UpdateData(
                table: "advert_statuses",
                keyColumn: "id",
                keyValue: 2,
                column: "name_en",
                value: "On Waiting");

            migrationBuilder.UpdateData(
                table: "advert_statuses",
                keyColumn: "id",
                keyValue: 3,
                column: "name_en",
                value: "Declined");

            migrationBuilder.UpdateData(
                table: "advert_statuses",
                keyColumn: "id",
                keyValue: 4,
                column: "name_en",
                value: "Uploading");

            migrationBuilder.UpdateData(
                table: "advert_types",
                keyColumn: "id",
                keyValue: 1,
                column: "name_en",
                value: "House");

            migrationBuilder.UpdateData(
                table: "advert_types",
                keyColumn: "id",
                keyValue: 2,
                column: "name_en",
                value: "Apartman");

            migrationBuilder.UpdateData(
                table: "advert_types",
                keyColumn: "id",
                keyValue: 3,
                column: "name_en",
                value: "Office space");

            migrationBuilder.UpdateData(
                table: "advert_types",
                keyColumn: "id",
                keyValue: 4,
                column: "name_en",
                value: "Villa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "description_en",
                table: "adverts");

            migrationBuilder.DropColumn(
                name: "name_en",
                table: "advert_types");

            migrationBuilder.DropColumn(
                name: "name_en",
                table: "advert_statuses");

            migrationBuilder.DropColumn(
                name: "name_en",
                table: "advert_purposes");

            migrationBuilder.RenameColumn(
                name: "no_of_bedrooms",
                table: "adverts",
                newName: "no_of_badrooms");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "2b8a5b77-33da-49c3-a4ca-268b4ff5b990", "96a0a063-031a-4c3c-b7b6-dc745c489131", "Moderator", "MODERATOR" },
                    { "5596677e-8128-4257-9724-e6232c4b4fd5", "6ab093ef-8c30-4c04-8405-1e2013aba4d1", "Administrator", "ADMINISTRATOR" },
                    { "67d39b13-c288-4227-b987-b27052c18616", "aa2246cc-e9f7-4af3-8d53-2751189d281d", "User", "USER" }
                });
        }
    }
}

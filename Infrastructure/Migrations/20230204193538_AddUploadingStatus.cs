using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class AddUploadingStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "0fc6df17-6347-42b2-8413-20af080e23fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "50ccfd87-c2bc-4b69-b4e5-fa9b52cc49e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "8d260195-cdf8-4cf4-a56d-76fcef4ff5ec");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "2b8a5b77-33da-49c3-a4ca-268b4ff5b990", "96a0a063-031a-4c3c-b7b6-dc745c489131", "Moderator", "MODERATOR" },
                    { "5596677e-8128-4257-9724-e6232c4b4fd5", "6ab093ef-8c30-4c04-8405-1e2013aba4d1", "Administrator", "ADMINISTRATOR" },
                    { "67d39b13-c288-4227-b987-b27052c18616", "aa2246cc-e9f7-4af3-8d53-2751189d281d", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "advert_statuses",
                columns: new[] { "id", "name" },
                values: new object[] { 4, "Uploading" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "advert_statuses",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "0fc6df17-6347-42b2-8413-20af080e23fc", "607822e5-0fa1-4f4e-a0c6-c642e190b189", "Moderator", "MODERATOR" },
                    { "50ccfd87-c2bc-4b69-b4e5-fa9b52cc49e5", "33b59885-a778-4799-9fe1-6adc1308737a", "Administrator", "ADMINISTRATOR" },
                    { "8d260195-cdf8-4cf4-a56d-76fcef4ff5ec", "1c141fba-aef9-41e4-b33c-c81d2c06bf40", "User", "USER" }
                });
        }
    }
}

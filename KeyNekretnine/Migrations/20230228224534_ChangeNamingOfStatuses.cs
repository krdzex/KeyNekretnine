using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class ChangeNamingOfStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name_sr",
                table: "advert_statuses",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "9e89778e-8e39-4c1c-a96d-d35762954d17");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "f1922880-c8be-4a3e-b46c-654f7b64d888");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "05222fe9-bc14-484a-8cd3-eb25280bfc8d");

            migrationBuilder.UpdateData(
                table: "advert_statuses",
                keyColumn: "id",
                keyValue: 2,
                column: "name_en",
                value: "Pending");

            migrationBuilder.UpdateData(
                table: "advert_statuses",
                keyColumn: "id",
                keyValue: 4,
                column: "name_sr",
                value: "Dodavanje u toku");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name_sr",
                table: "advert_statuses",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "fd63f9e8-f914-453e-be99-0800e30f3567");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "41318f19-e587-41f4-9f0f-8a61cc26f48a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "eb86db73-d5ad-4104-bd2d-75099a914939");

            migrationBuilder.UpdateData(
                table: "advert_statuses",
                keyColumn: "id",
                keyValue: 2,
                column: "name_en",
                value: "On Waiting");

            migrationBuilder.UpdateData(
                table: "advert_statuses",
                keyColumn: "id",
                keyValue: 4,
                column: "name_sr",
                value: "Uploading");
        }
    }
}

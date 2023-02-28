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
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "38c6f069-1857-4c33-9112-076d4261c789");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "f933d2fc-ae77-4ee9-be0c-1bab2feea0e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "ce611ff0-c503-4482-9618-ffee162a88ed");

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
                value: "Dodavanje u procesu");
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
                oldType: "character varying(20)",
                oldMaxLength: 20);

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

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class AddingReferenceIdForAdvert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reference_id",
                table: "adverts",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "6a68e88d-ffd9-492e-849c-4e64cf2c7230");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "b1e8bd57-5848-4c58-878b-bb318234196b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "976af993-d2d3-4ba4-aa2d-e7e0df0b472c");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reference_id",
                table: "adverts");

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
        }
    }
}

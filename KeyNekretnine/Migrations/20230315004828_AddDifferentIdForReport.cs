using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class AddDifferentIdForReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_user_advert_reports",
                table: "user_advert_reports");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_advert_reports",
                table: "user_advert_reports",
                columns: new[] { "user_id", "advert_id", "reject_reason_id" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "2babbaf1-5aa7-4513-be89-46a71aa3ebcf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "582753e7-3053-43b5-bc3b-6aaf4be10d94");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "6daa0009-4643-4332-bed2-ba4158899927");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_user_advert_reports",
                table: "user_advert_reports");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_advert_reports",
                table: "user_advert_reports",
                columns: new[] { "user_id", "advert_id" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "53a7f2d3-c572-4a11-92e6-b9b4c1fc701a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "047219da-95a2-4886-9460-e7b892d60e22");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "ac56d57d-deb9-479c-9635-4bf45bdf9f25");
        }
    }
}

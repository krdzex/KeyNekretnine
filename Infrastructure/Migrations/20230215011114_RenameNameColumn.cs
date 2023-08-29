using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class RenameNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "advert_types",
                newName: "name_sr");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "advert_statuses",
                newName: "name_sr");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "advert_purposes",
                newName: "name_sr");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "63f9b119-3101-481b-b725-5679771bfe70");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "716a7738-0444-4a7d-8cb3-23ccfa775cb5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "f618fb5a-bc4e-423c-a229-194da9e6fcdf");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name_sr",
                table: "advert_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "name_sr",
                table: "advert_statuses",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "name_sr",
                table: "advert_purposes",
                newName: "name");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "464400f5-8b11-4377-a1e4-c3f4016f84c0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "5bd6e2a6-7803-4702-aad3-d86f61afdbdc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "81ec485c-62fd-4c5f-8a24-67ad4458b0d9");
        }
    }
}

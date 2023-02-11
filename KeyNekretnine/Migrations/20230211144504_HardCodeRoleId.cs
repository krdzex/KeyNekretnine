using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class HardCodeRoleId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "ff1bc08a-bbc1-4e2b-ae65-2604e4460663");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "0910e59c-8e6c-48d2-9879-11551171fb6d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "236e90b2-ca37-4e5a-922f-be2441ffbdae");
        }
    }
}

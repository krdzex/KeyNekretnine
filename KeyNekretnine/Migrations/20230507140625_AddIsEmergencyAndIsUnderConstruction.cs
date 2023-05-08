using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class AddIsEmergencyAndIsUnderConstruction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_emergency",
                table: "adverts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_under_construction",
                table: "adverts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "dac64113-8eff-411a-b415-3af0226f89e5");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "6be3363a-92c3-49a7-889a-d4d10ed7ad5f");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "2180eb63-e669-49ec-b6c4-740ea7416f61");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_emergency",
                table: "adverts");

            migrationBuilder.DropColumn(
                name: "is_under_construction",
                table: "adverts");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "32e618d2-07b3-4e48-b674-e00bccba3bfb");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "ab612800-4f20-4879-a17c-94d459b9654a");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "9d06f1a7-2cd0-456c-aefd-3f15c8a34707");
        }
    }
}

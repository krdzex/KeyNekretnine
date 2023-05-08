using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class RenameVilaToZemljiste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "advert_types",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "name_en", "name_sr" },
                values: new object[] { "Land lot", "Zemljiste" });

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "94afb0cc-c42e-43da-b1e5-916dae828c28");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "39615a95-bf9c-4a6d-ae62-7eb434793e49");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "8047a52f-858c-4289-9a59-0794bb889b9c");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "advert_types",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "name_en", "name_sr" },
                values: new object[] { "Villa", "Vila" });

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
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class RemoveBlurUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "blur_url",
                table: "images");

            migrationBuilder.DropColumn(
                name: "cover_image_blur_url",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "blur_url",
                table: "images",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cover_image_blur_url",
                table: "adverts",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "1bac20d9-537c-481a-bc00-4182606957c5");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "0b6189a6-08fe-4bf8-b030-4cea70b89f10");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "2a6e4bd8-e9a2-4a48-b1fe-a7e010fb5e03");
        }
    }
}

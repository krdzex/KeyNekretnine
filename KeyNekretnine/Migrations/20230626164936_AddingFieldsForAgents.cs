using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class AddingFieldsForAgents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "imaginary_agents",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "imaginary_agents",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "facebook_url",
                table: "imaginary_agents",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "instagram_url",
                table: "imaginary_agents",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "linkedin_url",
                table: "imaginary_agents",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "twitter_url",
                table: "imaginary_agents",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "dccd526d-7ccb-484c-ab05-4ec3df1d7414");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "f1e10ff8-ab18-4959-b9ac-b096849d695b");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "2e391e68-ee22-4bb3-99a0-b4133af72de4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "imaginary_agents");

            migrationBuilder.DropColumn(
                name: "email",
                table: "imaginary_agents");

            migrationBuilder.DropColumn(
                name: "facebook_url",
                table: "imaginary_agents");

            migrationBuilder.DropColumn(
                name: "instagram_url",
                table: "imaginary_agents");

            migrationBuilder.DropColumn(
                name: "linkedin_url",
                table: "imaginary_agents");

            migrationBuilder.DropColumn(
                name: "twitter_url",
                table: "imaginary_agents");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "bc2c1dd4-371e-4ca8-a20e-cbea3b57cc7b");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "903da649-3555-4da0-adaf-33e4f29d3897");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "0454e14f-6b07-4036-a281-7b8550e5057d");
        }
    }
}

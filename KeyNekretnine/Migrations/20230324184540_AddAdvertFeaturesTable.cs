using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class AddAdvertFeaturesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "advert_feature",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    advert_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_advert_feature", x => x.id);
                    table.ForeignKey(
                        name: "fk_advert_feature_adverts_advert_id",
                        column: x => x.advert_id,
                        principalTable: "adverts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "cccddb32-1a06-487d-ba90-cb659d06089b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "d0c656dd-ab38-4318-98ea-579216debd3a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "65ab4204-0337-4e8f-85e3-961d8204ed9d");

            migrationBuilder.CreateIndex(
                name: "ix_advert_feature_advert_id",
                table: "advert_feature",
                column: "advert_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "advert_feature");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "5d1c139c-dda1-4e69-b882-6bb9d7ae4217");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "a67a8f0c-c1b8-46da-a402-dd38403b7c51");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "1a1aedf5-d6e9-4569-a678-f4a2b46fa6de");
        }
    }
}

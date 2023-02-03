using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class AddTemporeryImageDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "94504e03-5dda-4852-ab5a-5174d05f2a7e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "b867b188-9915-4746-a0e4-1d686291c164");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "d142d8a3-103d-4437-b810-f44085c3cb82");

            migrationBuilder.CreateTable(
                name: "temporery_image_datas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    image_data = table.Column<byte[]>(type: "bytea", nullable: false),
                    advert_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_temporery_image_datas", x => x.id);
                    table.ForeignKey(
                        name: "fk_temporery_image_datas_adverts_advert_id",
                        column: x => x.advert_id,
                        principalTable: "adverts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "04cb92f3-414a-4567-9a75-ec87fd795304", "0f626c30-cf75-413f-8fbe-ccf205916733", "User", "USER" },
                    { "ca56384b-4706-4432-8c5a-4bdb573d2b76", "612c3afc-04fb-4a3b-8f6e-a4254a2e1662", "Administrator", "ADMINISTRATOR" },
                    { "fffd0651-1970-4a37-af7c-882c85849001", "03adb671-21d9-49c5-8c62-bf321748aaa2", "Moderator", "MODERATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_temporery_image_datas_advert_id",
                table: "temporery_image_datas",
                column: "advert_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "temporery_image_datas");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "04cb92f3-414a-4567-9a75-ec87fd795304");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "ca56384b-4706-4432-8c5a-4bdb573d2b76");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "fffd0651-1970-4a37-af7c-882c85849001");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "94504e03-5dda-4852-ab5a-5174d05f2a7e", "1d74c002-0926-4313-9e5b-2d6625d0555d", "Administrator", "ADMINISTRATOR" },
                    { "b867b188-9915-4746-a0e4-1d686291c164", "192221b8-5f4d-49f0-8cb9-8ed946f598b5", "Moderator", "MODERATOR" },
                    { "d142d8a3-103d-4437-b810-f44085c3cb82", "4c35541b-dd43-47c8-ae98-8398fcf471b7", "User", "USER" }
                });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class AddingReportAndReasonRejectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rejection_reasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reason_sr = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    reason_en = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rejection_reasons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_advert_reports",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    advert_id = table.Column<int>(type: "integer", nullable: false),
                    rejection_reason_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_advert_reports", x => new { x.user_id, x.advert_id });
                    table.ForeignKey(
                        name: "fk_user_advert_reports_adverts_advert_id",
                        column: x => x.advert_id,
                        principalTable: "adverts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_advert_reports_rejection_reasons_rejection_reason_id",
                        column: x => x.rejection_reason_id,
                        principalTable: "rejection_reasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_advert_reports_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "81203546-2e57-494c-b844-eb5149e13487");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "54736aed-6f1f-475e-b620-e79d71c533c6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "246984ac-9b5d-4607-bb39-1b829c5a7a82");

            migrationBuilder.InsertData(
                table: "rejection_reasons",
                columns: new[] { "id", "reason_en", "reason_sr" },
                values: new object[,]
                {
                    { 1, "The advertisement similar to this already exists-", "Oglas slican ovome vec postoji." },
                    { 2, "The advert images are inappropriate or accurate.", "Slike za oglas su neprimjerene ili nisu tacne." },
                    { 3, "The advert informations are inappropriate or accurate.", "Podaci o oglasu su neprimjereni ili nisu tacni." }
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_advert_reports_advert_id",
                table: "user_advert_reports",
                column: "advert_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_advert_reports_rejection_reason_id",
                table: "user_advert_reports",
                column: "rejection_reason_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_advert_reports");

            migrationBuilder.DropTable(
                name: "rejection_reasons");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "74b2b1ce-a37d-4d07-83cd-d809d7bcc57c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "ea470940-9332-485a-a55d-6ae60e654a83");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "4f4c9688-5886-455f-a31a-32ea25a84055");
        }
    }
}

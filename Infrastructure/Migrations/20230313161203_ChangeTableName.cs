using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class ChangeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_advert_reports_rejection_reasons_rejection_reason_id",
                table: "user_advert_reports");

            migrationBuilder.DropTable(
                name: "rejection_reasons");

            migrationBuilder.CreateTable(
                name: "reject_reasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reason_sr = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    reason_en = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reject_reasons", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "2e1c17f9-9317-4910-bff0-a6c57773d49c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "2afd5471-278d-4bf8-afd1-aec15c37f777");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "716fd211-dc76-42be-a914-62ad13e80792");

            migrationBuilder.InsertData(
                table: "reject_reasons",
                columns: new[] { "id", "reason_en", "reason_sr" },
                values: new object[,]
                {
                    { 1, "The advertisement similar to this already exists.", "Oglas slican ovome vec postoji." },
                    { 2, "The advert images are inappropriate or accurate.", "Slike za oglas su neprimjerene ili nisu tacne." },
                    { 3, "The advert informations are inappropriate or accurate.", "Podaci o oglasu su neprimjereni ili nisu tacni." }
                });

            migrationBuilder.AddForeignKey(
                name: "fk_user_advert_reports_reject_reasons_rejection_reason_id",
                table: "user_advert_reports",
                column: "rejection_reason_id",
                principalTable: "reject_reasons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_advert_reports_reject_reasons_rejection_reason_id",
                table: "user_advert_reports");

            migrationBuilder.DropTable(
                name: "reject_reasons");

            migrationBuilder.CreateTable(
                name: "rejection_reasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reason_en = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    reason_sr = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rejection_reasons", x => x.id);
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

            migrationBuilder.AddForeignKey(
                name: "fk_user_advert_reports_rejection_reasons_rejection_reason_id",
                table: "user_advert_reports",
                column: "rejection_reason_id",
                principalTable: "rejection_reasons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

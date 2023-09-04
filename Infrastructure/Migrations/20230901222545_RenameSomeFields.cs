using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameSomeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_adverts_agents_agent_id",
                table: "adverts");

            migrationBuilder.DropForeignKey(
                name: "fk_agency_languages_languages_language_id",
                table: "agency_languages");

            migrationBuilder.DropForeignKey(
                name: "fk_agent_languages_languages_language_id",
                table: "agent_languages");

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_agent_agent_id",
                table: "adverts",
                column: "agent_id",
                principalTable: "agents",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_agency_languages_language_language_id",
                table: "agency_languages",
                column: "language_id",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_agent_languages_language_language_id",
                table: "agent_languages",
                column: "language_id",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_adverts_agent_agent_id",
                table: "adverts");

            migrationBuilder.DropForeignKey(
                name: "fk_agency_languages_language_language_id",
                table: "agency_languages");

            migrationBuilder.DropForeignKey(
                name: "fk_agent_languages_language_language_id",
                table: "agent_languages");

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_agents_agent_id",
                table: "adverts",
                column: "agent_id",
                principalTable: "agents",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_agency_languages_languages_language_id",
                table: "agency_languages",
                column: "language_id",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_agent_languages_languages_language_id",
                table: "agent_languages",
                column: "language_id",
                principalTable: "languages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

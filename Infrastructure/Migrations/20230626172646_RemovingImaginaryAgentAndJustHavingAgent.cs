using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class RemovingImaginaryAgentAndJustHavingAgent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_adverts_imaginary_agents_imaginary_agent_id",
                table: "adverts");

            migrationBuilder.DropTable(
                name: "imaginary_agent_languages");

            migrationBuilder.DropTable(
                name: "imaginary_agents");

            migrationBuilder.RenameColumn(
                name: "imaginary_agent_id",
                table: "adverts",
                newName: "agent_id");

            migrationBuilder.RenameIndex(
                name: "ix_adverts_imaginary_agent_id",
                table: "adverts",
                newName: "ix_adverts_agent_id");

            migrationBuilder.CreateTable(
                name: "agents",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    last_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    image_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    twitter_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    facebook_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    instagram_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    linkedin_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    agency_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agents", x => x.id);
                    table.ForeignKey(
                        name: "fk_agents_agencies_agency_id",
                        column: x => x.agency_id,
                        principalTable: "agencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "agent_languages",
                columns: table => new
                {
                    agent_id = table.Column<int>(type: "integer", nullable: false),
                    language_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agent_languages", x => new { x.agent_id, x.language_id });
                    table.ForeignKey(
                        name: "fk_agent_languages_agents_agent_id",
                        column: x => x.agent_id,
                        principalTable: "agents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_agent_languages_languages_language_id",
                        column: x => x.language_id,
                        principalTable: "languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "15eda9e3-70e6-43d0-938d-44353ca9e502");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "54eb5348-6c8a-4171-a882-609973362fa6");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "c3fbfc32-541a-43e6-964d-5ebce4830525");

            migrationBuilder.CreateIndex(
                name: "ix_agent_languages_language_id",
                table: "agent_languages",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "ix_agents_agency_id",
                table: "agents",
                column: "agency_id");

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_agents_agent_id",
                table: "adverts",
                column: "agent_id",
                principalTable: "agents",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_adverts_agents_agent_id",
                table: "adverts");

            migrationBuilder.DropTable(
                name: "agent_languages");

            migrationBuilder.DropTable(
                name: "agents");

            migrationBuilder.RenameColumn(
                name: "agent_id",
                table: "adverts",
                newName: "imaginary_agent_id");

            migrationBuilder.RenameIndex(
                name: "ix_adverts_agent_id",
                table: "adverts",
                newName: "ix_adverts_imaginary_agent_id");

            migrationBuilder.CreateTable(
                name: "imaginary_agents",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    agency_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    facebook_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    first_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    image_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    instagram_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    last_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    linkedin_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    twitter_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_imaginary_agents", x => x.id);
                    table.ForeignKey(
                        name: "fk_imaginary_agents_agencies_agency_id",
                        column: x => x.agency_id,
                        principalTable: "agencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "imaginary_agent_languages",
                columns: table => new
                {
                    imaginary_agent_id = table.Column<int>(type: "integer", nullable: false),
                    language_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_imaginary_agent_languages", x => new { x.imaginary_agent_id, x.language_id });
                    table.ForeignKey(
                        name: "fk_imaginary_agent_languages_imaginary_agents_imaginary_agent_",
                        column: x => x.imaginary_agent_id,
                        principalTable: "imaginary_agents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_imaginary_agent_languages_languages_language_id",
                        column: x => x.language_id,
                        principalTable: "languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_imaginary_agent_languages_language_id",
                table: "imaginary_agent_languages",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "ix_imaginary_agents_agency_id",
                table: "imaginary_agents",
                column: "agency_id");

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_imaginary_agents_imaginary_agent_id",
                table: "adverts",
                column: "imaginary_agent_id",
                principalTable: "imaginary_agents",
                principalColumn: "id");
        }
    }
}

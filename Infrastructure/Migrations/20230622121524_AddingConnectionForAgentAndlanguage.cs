using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class AddingConnectionForAgentAndlanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                        name: "fk_imaginary_agent_languages_imaginary_agents_imaginary_agent_id",
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
                value: "00997d4a-0ff0-4943-a162-509e1e04c455");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "3676746a-0884-4064-b529-421ca639c70c");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "aa51d6ea-cb9b-4793-8fb4-480b1ef2893d");

            migrationBuilder.InsertData(
                table: "languages",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "English" },
                    { 2, "Spanish" },
                    { 3, "French" },
                    { 4, "German" },
                    { 5, "Mandarin Chinese" },
                    { 6, "Arabic" },
                    { 7, "Russian" },
                    { 8, "Japanese" },
                    { 9, "Italian" },
                    { 10, "Portuguese" },
                    { 11, "Korean" },
                    { 12, "Dutch" },
                    { 13, "Swedish" },
                    { 14, "Norwegian" },
                    { 15, "Danish" },
                    { 16, "Finnish" },
                    { 17, "Greek" },
                    { 18, "Turkish" },
                    { 19, "Hindi" },
                    { 20, "Hebrew" },
                    { 21, "Polish" },
                    { 22, "Czech" },
                    { 23, "Thai" },
                    { 24, "Indonesian" },
                    { 25, "Vietnamese" },
                    { 26, "Romanian" },
                    { 27, "Hungarian" },
                    { 28, "Swahili" },
                    { 29, "Ukrainian" },
                    { 30, "Bulgarian" },
                    { 31, "Catalan" },
                    { 32, "Serbian" },
                    { 33, "Persian (Farsi)" },
                    { 34, "Tagalog" },
                    { 35, "Icelandic" },
                    { 36, "Irish" },
                    { 37, "Scottish Gaelic" },
                    { 38, "Welsh" },
                    { 39, "Latin" },
                    { 40, "Esperanto" },
                    { 41, "Bengali" },
                    { 42, "Gujarati" },
                    { 43, "Kannada" },
                    { 44, "Malayalam" },
                    { 45, "Punjabi" },
                    { 46, "Tamil" },
                    { 47, "Telugu" },
                    { 48, "Marathi" },
                    { 49, "Amharic" },
                    { 50, "Somali" },
                    { 51, "Croatian" },
                    { 52, "Montenegrin" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_imaginary_agent_languages_language_id",
                table: "imaginary_agent_languages",
                column: "language_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "imaginary_agent_languages");

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "languages",
                keyColumn: "id",
                keyValue: 52);

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "98dc8141-9803-450d-a640-81dbfa216388");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "0c32929f-e81b-482a-9e73-b3525bb76deb");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "c6f69816-064e-457c-bb60-efc8e6a418fa");
        }
    }
}

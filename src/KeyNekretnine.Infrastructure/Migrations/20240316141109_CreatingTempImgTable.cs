using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatingTempImgTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "temporery_images_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    advert_id = table.Column<Guid>(type: "uuid", nullable: true),
                    image_data = table.Column<byte[]>(type: "bytea", nullable: false),
                    is_cover = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_temporery_images_data", x => x.id);
                    table.ForeignKey(
                        name: "fk_temporery_images_data_adverts_advert_id",
                        column: x => x.advert_id,
                        principalTable: "adverts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_temporery_images_data_advert_id",
                table: "temporery_images_data",
                column: "advert_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "temporery_images_data");
        }
    }
}

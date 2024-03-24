using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRequiredFromAdvertIdInTemporeryImageData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "advert_id",
                table: "temporery_images_data",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "ix_temporery_images_data_advert_id",
                table: "temporery_images_data",
                column: "advert_id");

            migrationBuilder.AddForeignKey(
                name: "fk_temporery_images_data_adverts_advert_id",
                table: "temporery_images_data",
                column: "advert_id",
                principalTable: "adverts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_temporery_images_data_adverts_advert_id",
                table: "temporery_images_data");

            migrationBuilder.DropIndex(
                name: "ix_temporery_images_data_advert_id",
                table: "temporery_images_data");

            migrationBuilder.AlterColumn<Guid>(
                name: "advert_id",
                table: "temporery_images_data",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class AddColumnAndFixingSpelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_adverts_advert_purpose_advert_purpose_id",
                table: "adverts");

            migrationBuilder.DropForeignKey(
                name: "fk_adverts_advert_status_advert_status_id",
                table: "adverts");

            migrationBuilder.DropForeignKey(
                name: "fk_adverts_advert_type_advert_type_id",
                table: "adverts");

            migrationBuilder.DropForeignKey(
                name: "fk_temporery_image_datas_adverts_advert_id",
                table: "temporery_image_datas");

            migrationBuilder.DropPrimaryKey(
                name: "pk_temporery_image_datas",
                table: "temporery_image_datas");

            migrationBuilder.DropPrimaryKey(
                name: "pk_advert_type",
                table: "advert_type");

            migrationBuilder.DropPrimaryKey(
                name: "pk_advert_status",
                table: "advert_status");

            migrationBuilder.DropPrimaryKey(
                name: "pk_advert_purpose",
                table: "advert_purpose");

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

            migrationBuilder.RenameTable(
                name: "temporery_image_datas",
                newName: "temporery_images_data");

            migrationBuilder.RenameTable(
                name: "advert_type",
                newName: "advert_types");

            migrationBuilder.RenameTable(
                name: "advert_status",
                newName: "advert_statuses");

            migrationBuilder.RenameTable(
                name: "advert_purpose",
                newName: "advert_purposes");

            migrationBuilder.RenameIndex(
                name: "ix_temporery_image_datas_advert_id",
                table: "temporery_images_data",
                newName: "ix_temporery_images_data_advert_id");

            migrationBuilder.AddColumn<bool>(
                name: "is_cover",
                table: "temporery_images_data",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "pk_temporery_images_data",
                table: "temporery_images_data",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_advert_types",
                table: "advert_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_advert_statuses",
                table: "advert_statuses",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_advert_purposes",
                table: "advert_purposes",
                column: "id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "0fc6df17-6347-42b2-8413-20af080e23fc", "607822e5-0fa1-4f4e-a0c6-c642e190b189", "Moderator", "MODERATOR" },
                    { "50ccfd87-c2bc-4b69-b4e5-fa9b52cc49e5", "33b59885-a778-4799-9fe1-6adc1308737a", "Administrator", "ADMINISTRATOR" },
                    { "8d260195-cdf8-4cf4-a56d-76fcef4ff5ec", "1c141fba-aef9-41e4-b33c-c81d2c06bf40", "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_advert_purposes_advert_purpose_id",
                table: "adverts",
                column: "advert_purpose_id",
                principalTable: "advert_purposes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_advert_statuses_advert_status_id",
                table: "adverts",
                column: "advert_status_id",
                principalTable: "advert_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_advert_types_advert_type_id",
                table: "adverts",
                column: "advert_type_id",
                principalTable: "advert_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_temporery_images_data_adverts_advert_id",
                table: "temporery_images_data",
                column: "advert_id",
                principalTable: "adverts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_adverts_advert_purposes_advert_purpose_id",
                table: "adverts");

            migrationBuilder.DropForeignKey(
                name: "fk_adverts_advert_statuses_advert_status_id",
                table: "adverts");

            migrationBuilder.DropForeignKey(
                name: "fk_adverts_advert_types_advert_type_id",
                table: "adverts");

            migrationBuilder.DropForeignKey(
                name: "fk_temporery_images_data_adverts_advert_id",
                table: "temporery_images_data");

            migrationBuilder.DropPrimaryKey(
                name: "pk_temporery_images_data",
                table: "temporery_images_data");

            migrationBuilder.DropPrimaryKey(
                name: "pk_advert_types",
                table: "advert_types");

            migrationBuilder.DropPrimaryKey(
                name: "pk_advert_statuses",
                table: "advert_statuses");

            migrationBuilder.DropPrimaryKey(
                name: "pk_advert_purposes",
                table: "advert_purposes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "0fc6df17-6347-42b2-8413-20af080e23fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "50ccfd87-c2bc-4b69-b4e5-fa9b52cc49e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "8d260195-cdf8-4cf4-a56d-76fcef4ff5ec");

            migrationBuilder.DropColumn(
                name: "is_cover",
                table: "temporery_images_data");

            migrationBuilder.RenameTable(
                name: "temporery_images_data",
                newName: "temporery_image_datas");

            migrationBuilder.RenameTable(
                name: "advert_types",
                newName: "advert_type");

            migrationBuilder.RenameTable(
                name: "advert_statuses",
                newName: "advert_status");

            migrationBuilder.RenameTable(
                name: "advert_purposes",
                newName: "advert_purpose");

            migrationBuilder.RenameIndex(
                name: "ix_temporery_images_data_advert_id",
                table: "temporery_image_datas",
                newName: "ix_temporery_image_datas_advert_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_temporery_image_datas",
                table: "temporery_image_datas",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_advert_type",
                table: "advert_type",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_advert_status",
                table: "advert_status",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_advert_purpose",
                table: "advert_purpose",
                column: "id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "04cb92f3-414a-4567-9a75-ec87fd795304", "0f626c30-cf75-413f-8fbe-ccf205916733", "User", "USER" },
                    { "ca56384b-4706-4432-8c5a-4bdb573d2b76", "612c3afc-04fb-4a3b-8f6e-a4254a2e1662", "Administrator", "ADMINISTRATOR" },
                    { "fffd0651-1970-4a37-af7c-882c85849001", "03adb671-21d9-49c5-8c62-bf321748aaa2", "Moderator", "MODERATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_advert_purpose_advert_purpose_id",
                table: "adverts",
                column: "advert_purpose_id",
                principalTable: "advert_purpose",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_advert_status_advert_status_id",
                table: "adverts",
                column: "advert_status_id",
                principalTable: "advert_status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_advert_type_advert_type_id",
                table: "adverts",
                column: "advert_type_id",
                principalTable: "advert_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_temporery_image_datas_adverts_advert_id",
                table: "temporery_image_datas",
                column: "advert_id",
                principalTable: "adverts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class RenamingStatusPurposeAndTypeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "advert_type_id",
                table: "adverts",
                newName: "type_id");

            migrationBuilder.RenameColumn(
                name: "advert_status_id",
                table: "adverts",
                newName: "status_id");

            migrationBuilder.RenameColumn(
                name: "advert_purpose_id",
                table: "adverts",
                newName: "purpose_id");

            migrationBuilder.RenameIndex(
                name: "ix_adverts_advert_type_id",
                table: "adverts",
                newName: "ix_adverts_type_id");

            migrationBuilder.RenameIndex(
                name: "ix_adverts_advert_status_id",
                table: "adverts",
                newName: "ix_adverts_status_id");

            migrationBuilder.RenameIndex(
                name: "ix_adverts_advert_purpose_id",
                table: "adverts",
                newName: "ix_adverts_purpose_id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "9b1c2002-dd4e-4c1c-84cb-8eba24ebc53d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "1907e9c8-2f35-4206-8c21-c3d4b0e525a0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "1384d1eb-a32a-4b0a-893b-c8d4261a5146");

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_advert_purposes_purpose_id",
                table: "adverts",
                column: "purpose_id",
                principalTable: "advert_purposes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_advert_statuses_status_id",
                table: "adverts",
                column: "status_id",
                principalTable: "advert_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_advert_types_type_id",
                table: "adverts",
                column: "type_id",
                principalTable: "advert_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_adverts_advert_purposes_purpose_id",
                table: "adverts");

            migrationBuilder.DropForeignKey(
                name: "fk_adverts_advert_statuses_status_id",
                table: "adverts");

            migrationBuilder.DropForeignKey(
                name: "fk_adverts_advert_types_type_id",
                table: "adverts");

            migrationBuilder.RenameColumn(
                name: "type_id",
                table: "adverts",
                newName: "advert_type_id");

            migrationBuilder.RenameColumn(
                name: "status_id",
                table: "adverts",
                newName: "advert_status_id");

            migrationBuilder.RenameColumn(
                name: "purpose_id",
                table: "adverts",
                newName: "advert_purpose_id");

            migrationBuilder.RenameIndex(
                name: "ix_adverts_type_id",
                table: "adverts",
                newName: "ix_adverts_advert_type_id");

            migrationBuilder.RenameIndex(
                name: "ix_adverts_status_id",
                table: "adverts",
                newName: "ix_adverts_advert_status_id");

            migrationBuilder.RenameIndex(
                name: "ix_adverts_purpose_id",
                table: "adverts",
                newName: "ix_adverts_advert_purpose_id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "6a68e88d-ffd9-492e-849c-4e64cf2c7230");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "b1e8bd57-5848-4c58-878b-bb318234196b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "976af993-d2d3-4ba4-aa2d-e7e0df0b472c");

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
        }
    }
}

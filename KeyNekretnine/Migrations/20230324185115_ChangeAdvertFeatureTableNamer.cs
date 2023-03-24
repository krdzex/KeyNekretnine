using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class ChangeAdvertFeatureTableNamer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_advert_feature_adverts_advert_id",
                table: "advert_feature");

            migrationBuilder.DropPrimaryKey(
                name: "pk_advert_feature",
                table: "advert_feature");

            migrationBuilder.RenameTable(
                name: "advert_feature",
                newName: "advert_features");

            migrationBuilder.RenameIndex(
                name: "ix_advert_feature_advert_id",
                table: "advert_features",
                newName: "ix_advert_features_advert_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_advert_features",
                table: "advert_features",
                column: "id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "1fbbfb78-e8e0-4e45-b602-1ac73e540145");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "c44c8277-b72b-47e9-aa11-6a6cff2a989b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "baec8fef-0d43-40c9-9f47-36ec3941c1d3");

            migrationBuilder.AddForeignKey(
                name: "fk_advert_features_adverts_advert_id",
                table: "advert_features",
                column: "advert_id",
                principalTable: "adverts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_advert_features_adverts_advert_id",
                table: "advert_features");

            migrationBuilder.DropPrimaryKey(
                name: "pk_advert_features",
                table: "advert_features");

            migrationBuilder.RenameTable(
                name: "advert_features",
                newName: "advert_feature");

            migrationBuilder.RenameIndex(
                name: "ix_advert_features_advert_id",
                table: "advert_feature",
                newName: "ix_advert_feature_advert_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_advert_feature",
                table: "advert_feature",
                column: "id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "cccddb32-1a06-487d-ba90-cb659d06089b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "d0c656dd-ab38-4318-98ea-579216debd3a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "65ab4204-0337-4e8f-85e3-961d8204ed9d");

            migrationBuilder.AddForeignKey(
                name: "fk_advert_feature_adverts_advert_id",
                table: "advert_feature",
                column: "advert_id",
                principalTable: "adverts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

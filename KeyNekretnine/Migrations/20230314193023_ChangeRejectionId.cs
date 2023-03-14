using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class ChangeRejectionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_advert_reports_reject_reasons_rejection_reason_id",
                table: "user_advert_reports");

            migrationBuilder.RenameColumn(
                name: "rejection_reason_id",
                table: "user_advert_reports",
                newName: "reject_reason_id");

            migrationBuilder.RenameIndex(
                name: "ix_user_advert_reports_rejection_reason_id",
                table: "user_advert_reports",
                newName: "ix_user_advert_reports_reject_reason_id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "be4f402e-7eec-44f8-96aa-a56598495a5a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "5afe9407-5bc9-4d4e-bdbc-88b6bd9b94ec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "35215852-1ad1-4e8b-8cb9-64f3b03a1ca4");

            migrationBuilder.AddForeignKey(
                name: "fk_user_advert_reports_reject_reasons_reject_reason_id",
                table: "user_advert_reports",
                column: "reject_reason_id",
                principalTable: "reject_reasons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_advert_reports_reject_reasons_reject_reason_id",
                table: "user_advert_reports");

            migrationBuilder.RenameColumn(
                name: "reject_reason_id",
                table: "user_advert_reports",
                newName: "rejection_reason_id");

            migrationBuilder.RenameIndex(
                name: "ix_user_advert_reports_reject_reason_id",
                table: "user_advert_reports",
                newName: "ix_user_advert_reports_rejection_reason_id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "c29f1834-d40a-4511-8871-c6b2f1a2220b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "8fb31b92-23be-490b-8b1f-4b99608333c1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "da03954f-acab-4c5f-a1a4-2c37a03634f6");

            migrationBuilder.AddForeignKey(
                name: "fk_user_advert_reports_reject_reasons_rejection_reason_id",
                table: "user_advert_reports",
                column: "rejection_reason_id",
                principalTable: "reject_reasons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

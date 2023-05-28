using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class AddingConnectionWithAdvertAndImaginaryAgents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_adverts_users_user_id",
                table: "adverts");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "adverts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "imaginary_agent_id",
                table: "adverts",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "b4a970a6-4401-44d6-a129-c42a0fdb27d1");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "dc480549-7816-46b7-b7b0-a757e452df1a");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "0fd0434a-2920-4ec1-b536-337ce9922f0c");

            migrationBuilder.CreateIndex(
                name: "ix_adverts_imaginary_agent_id",
                table: "adverts",
                column: "imaginary_agent_id");

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_imaginary_agents_imaginary_agent_id",
                table: "adverts",
                column: "imaginary_agent_id",
                principalTable: "imaginary_agents",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_users_user_id",
                table: "adverts",
                column: "user_id",
                principalTable: "asp_net_users",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_adverts_imaginary_agents_imaginary_agent_id",
                table: "adverts");

            migrationBuilder.DropForeignKey(
                name: "fk_adverts_users_user_id",
                table: "adverts");

            migrationBuilder.DropIndex(
                name: "ix_adverts_imaginary_agent_id",
                table: "adverts");

            migrationBuilder.DropColumn(
                name: "imaginary_agent_id",
                table: "adverts");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "adverts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "770a8aa0-d2ac-4c14-ada8-889f9e1e8bd2");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "690de828-6136-4d0c-bb51-d6cdec568ced");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "4379d961-94e2-4410-968f-a5d305c5a4f0");

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_users_user_id",
                table: "adverts",
                column: "user_id",
                principalTable: "asp_net_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

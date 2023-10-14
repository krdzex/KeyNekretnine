using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNotnullFromUserIdInAdverts : Migration
    {
        /// <inheritdoc />
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

            migrationBuilder.AddForeignKey(
                name: "fk_adverts_users_user_id",
                table: "adverts",
                column: "user_id",
                principalTable: "asp_net_users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_adverts_users_user_id",
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

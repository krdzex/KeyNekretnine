using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "agencies",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "approved_on_date",
                table: "adverts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_premium",
                table: "adverts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "rejected_on_date",
                table: "adverts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_on_date",
                table: "adverts",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "agencies");

            migrationBuilder.DropColumn(
                name: "approved_on_date",
                table: "adverts");

            migrationBuilder.DropColumn(
                name: "is_premium",
                table: "adverts");

            migrationBuilder.DropColumn(
                name: "rejected_on_date",
                table: "adverts");

            migrationBuilder.DropColumn(
                name: "updated_on_date",
                table: "adverts");
        }
    }
}

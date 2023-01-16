using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class AddBannedFetureInDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "0d790e50-4e3c-4f41-a882-73e72b467b73");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "36ea0e0f-84ff-4863-8b64-2d4169cad474");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "71d921b3-4203-4a94-bdd7-638204f85520");

            migrationBuilder.AddColumn<DateTime>(
                name: "ban_end",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_banned",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "0418feaf-769e-45db-b938-6cc14f6c1907", "5d67edd4-6a24-49ba-b538-07f7247890d8", "User", "USER" },
                    { "72d862df-3cf7-4a6b-a416-d14fe099f625", "1e56d8dd-5453-4a0f-b835-2147ba1bc08b", "Moderator", "MODERATOR" },
                    { "8761a132-f848-40d7-9864-63f1cc409573", "2325990c-ba8c-4228-abb2-138d41f62582", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "0418feaf-769e-45db-b938-6cc14f6c1907");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "72d862df-3cf7-4a6b-a416-d14fe099f625");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "8761a132-f848-40d7-9864-63f1cc409573");

            migrationBuilder.DropColumn(
                name: "ban_end",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "is_banned",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "0d790e50-4e3c-4f41-a882-73e72b467b73", "b7f4d043-bdb9-40a9-abaa-68313e3a87cc", "Administrator", "ADMINISTRATOR" },
                    { "36ea0e0f-84ff-4863-8b64-2d4169cad474", "0d8ab276-47b2-4daa-a6d7-cd0dc5ddd89d", "User", "USER" },
                    { "71d921b3-4203-4a94-bdd7-638204f85520", "972a03e0-3c1a-48dd-a7b9-239866f8128e", "Moderator", "MODERATOR" }
                });
        }
    }
}

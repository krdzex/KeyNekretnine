using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class AddSingleImageForAllCitiesForTestingPurpose : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "007dd351-0f5b-4ea3-b83b-11d530bc37a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "5633f6db-66b0-4c20-ba14-1130d12a635d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "71fb4e29-1775-41f6-b63d-97c7529dc285");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "548e52a6-485a-49a5-b204-8994eaa79a12", "ff1bc08a-bbc1-4e2b-ae65-2604e4460663", "User", "USER" },
                    { "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93", "0910e59c-8e6c-48d2-9879-11551171fb6d", "Administrator", "ADMINISTRATOR" },
                    { "f78fff4a-06dc-4b5d-864c-d70cd9ced860", "236e90b2-ca37-4e5a-922f-be2441ffbdae", "Moderator", "MODERATOR" }
                });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 5,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 6,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 7,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 8,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 9,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 10,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 11,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 12,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 13,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 14,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 15,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 16,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 17,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 18,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 19,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 20,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 21,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 22,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 23,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 24,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "007dd351-0f5b-4ea3-b83b-11d530bc37a5", "2306fd87-1855-46f9-97e5-951eee91406d", "User", "USER" },
                    { "5633f6db-66b0-4c20-ba14-1130d12a635d", "80f40a74-9d3c-459d-afb4-26762232af2a", "Administrator", "ADMINISTRATOR" },
                    { "71fb4e29-1775-41f6-b63d-97c7529dc285", "606cf4bd-aa78-4c45-bbd7-fa8193247dc6", "Moderator", "MODERATOR" }
                });

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 1,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 3,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 4,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 5,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 6,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 7,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 8,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 9,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 10,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 11,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 12,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 13,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 14,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 15,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 16,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 17,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 18,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 19,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 20,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 21,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 22,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 23,
                column: "image_url",
                value: "");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 24,
                column: "image_url",
                value: "");
        }
    }
}

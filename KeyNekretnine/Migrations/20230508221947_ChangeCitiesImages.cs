using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Migrations
{
    public partial class ChangeCitiesImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "971cc8be-a8a6-476d-8a76-698770c71eec");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "582bc224-287a-4d07-b337-9152635af129");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "71648bfb-e289-4d11-a93a-b86717aef9b2");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/bar_e1p0d8.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 9,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/kolasin_jejcpn.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 10,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/kotor_vgtdaz.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 12,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/niksic_hbkxzq.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 16,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/podgorica_qs4sij.webp");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 21,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/budva_kyvacy.webp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "548e52a6-485a-49a5-b204-8994eaa79a12",
                column: "concurrency_stamp",
                value: "94afb0cc-c42e-43da-b1e5-916dae828c28");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93",
                column: "concurrency_stamp",
                value: "39615a95-bf9c-4a6d-ae62-7eb434793e49");

            migrationBuilder.UpdateData(
                table: "asp_net_roles",
                keyColumn: "id",
                keyValue: "f78fff4a-06dc-4b5d-864c-d70cd9ced860",
                column: "concurrency_stamp",
                value: "8047a52f-858c-4289-9a59-0794bb889b9c");

            migrationBuilder.UpdateData(
                table: "cities",
                keyColumn: "id",
                keyValue: 2,
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
                keyValue: 12,
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
                keyValue: 21,
                column: "image_url",
                value: "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp");
        }
    }
}

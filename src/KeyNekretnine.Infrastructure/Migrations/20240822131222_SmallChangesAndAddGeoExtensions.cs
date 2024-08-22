using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SmallChangesAndAddGeoExtensions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "about",
                table: "asp_net_users",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS \"cube\";");
            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS \"earthdistance\";");

            migrationBuilder.Sql(@"
                CREATE INDEX IF NOT EXISTS idx_adverts_location
                ON adverts USING gist (
                    ll_to_earth(location_latitude, location_longitude)
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "about",
                table: "asp_net_users",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.Sql("DROP INDEX IF EXISTS idx_adverts_location;");

            migrationBuilder.Sql("DROP EXTENSION IF EXISTS \"earthdistance\";");
            migrationBuilder.Sql("DROP EXTENSION IF EXISTS \"cube\";");
        }
    }
}

﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KeyNekretnine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "asp_net_roles",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_users",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    account_created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    profile_image_url = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    about = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    is_banned = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ban_end = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    geo_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    image_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "images_to_delete",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    added_on_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    deleted_on_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_images_to_delete", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "languages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_languages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "outbox_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    occurred_on_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "json", nullable: false),
                    processed_on_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reject_reasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reason_sr = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    reason_en = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reject_reasons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "temporery_images_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    advert_id = table.Column<Guid>(type: "uuid", nullable: false),
                    image_data = table.Column<byte[]>(type: "bytea", nullable: false),
                    is_cover = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_temporery_images_data", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_role_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "asp_net_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "agencies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    location_address = table.Column<string>(type: "text", nullable: true),
                    location_latitude = table.Column<double>(type: "double precision", nullable: true),
                    location_longitude = table.Column<double>(type: "double precision", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    website_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    image_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    work_hour_start = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    work_hour_end = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    social_media_twitter = table.Column<string>(type: "text", nullable: true),
                    social_media_facebook = table.Column<string>(type: "text", nullable: true),
                    social_media_instagram = table.Column<string>(type: "text", nullable: true),
                    social_media_linkedin = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agencies", x => x.id);
                    table.ForeignKey(
                        name: "fk_agencies_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_roles",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "asp_net_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_tokens",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "neighborhoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    city_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_neighborhoods", x => x.id);
                    table.ForeignKey(
                        name: "fk_neighborhoods_cities_city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "agency_languages",
                columns: table => new
                {
                    agency_id = table.Column<Guid>(type: "uuid", nullable: false),
                    language_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agency_languages", x => new { x.agency_id, x.language_id });
                    table.ForeignKey(
                        name: "fk_agency_languages_agencies_agency_id",
                        column: x => x.agency_id,
                        principalTable: "agencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_agency_languages_language_language_id",
                        column: x => x.language_id,
                        principalTable: "languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "agents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    last_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    image_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    social_media_twitter = table.Column<string>(type: "text", nullable: true),
                    social_media_facebook = table.Column<string>(type: "text", nullable: true),
                    social_media_instagram = table.Column<string>(type: "text", nullable: true),
                    social_media_linkedin = table.Column<string>(type: "text", nullable: true),
                    agency_id = table.Column<Guid>(type: "uuid", nullable: false),
                    creted_on_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agents", x => x.id);
                    table.ForeignKey(
                        name: "fk_agents_agencies_agency_id",
                        column: x => x.agency_id,
                        principalTable: "agencies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "adverts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    description_value = table.Column<string>(type: "text", nullable: false),
                    no_of_bedrooms = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    floor_space = table.Column<double>(type: "double precision", maxLength: 10000, nullable: false),
                    no_of_bathrooms = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    has_terrace = table.Column<bool>(type: "boolean", nullable: false),
                    has_garage = table.Column<bool>(type: "boolean", nullable: false),
                    is_furnished = table.Column<bool>(type: "boolean", nullable: false),
                    has_wifi = table.Column<bool>(type: "boolean", nullable: false),
                    has_elevator = table.Column<bool>(type: "boolean", nullable: false),
                    building_floor = table.Column<int>(type: "integer", nullable: false),
                    is_urgent = table.Column<bool>(type: "boolean", nullable: false),
                    is_under_construction = table.Column<bool>(type: "boolean", nullable: false),
                    year_of_building_created = table.Column<int>(type: "integer", maxLength: 3000, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    purpose = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    created_on_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    agent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    neighborhood_id = table.Column<int>(type: "integer", nullable: false),
                    cover_image_url = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    location_address = table.Column<string>(type: "text", nullable: true),
                    location_latitude = table.Column<double>(type: "double precision", nullable: true),
                    location_longitude = table.Column<double>(type: "double precision", nullable: true),
                    reference_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adverts", x => x.id);
                    table.ForeignKey(
                        name: "fk_adverts_agent_agent_id",
                        column: x => x.agent_id,
                        principalTable: "agents",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_adverts_neighborhoods_neighborhood_id",
                        column: x => x.neighborhood_id,
                        principalTable: "neighborhoods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_adverts_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "agent_languages",
                columns: table => new
                {
                    agent_id = table.Column<Guid>(type: "uuid", nullable: false),
                    language_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_agent_languages", x => new { x.agent_id, x.language_id });
                    table.ForeignKey(
                        name: "fk_agent_languages_agents_agent_id",
                        column: x => x.agent_id,
                        principalTable: "agents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_agent_languages_language_language_id",
                        column: x => x.language_id,
                        principalTable: "languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "advert_features",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    advert_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_advert_features", x => x.id);
                    table.ForeignKey(
                        name: "fk_advert_features_adverts_advert_id",
                        column: x => x.advert_id,
                        principalTable: "adverts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    advert_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_images_adverts_advert_id",
                        column: x => x.advert_id,
                        principalTable: "adverts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_advert_favorites",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    advert_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_favorite_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_advert_favorites", x => new { x.user_id, x.advert_id });
                    table.ForeignKey(
                        name: "fk_user_advert_favorites_adverts_advert_id",
                        column: x => x.advert_id,
                        principalTable: "adverts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_advert_favorites_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_advert_reports",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    advert_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reject_reason_id = table.Column<int>(type: "integer", nullable: false),
                    created_report_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_advert_reports", x => new { x.user_id, x.advert_id, x.reject_reason_id });
                    table.ForeignKey(
                        name: "fk_user_advert_reports_adverts_advert_id",
                        column: x => x.advert_id,
                        principalTable: "adverts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_advert_reports_reject_reasons_reject_reason_id",
                        column: x => x.reject_reason_id,
                        principalTable: "reject_reasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_advert_reports_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "asp_net_roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "548e52a6-485a-49a5-b204-8994eaa79a12", null, "User", "USER" },
                    { "62558fd6-61f6-42fe-8cb7-8bc5fea7fb93", null, "Administrator", "ADMINISTRATOR" },
                    { "f78fff4a-06dc-4b5d-864c-d70cd9ced860", null, "Moderator", "MODERATOR" }
                });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "id", "geo_id", "image_url", "name" },
                values: new object[,]
                {
                    { 1, "2319358", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Andrijevica" },
                    { 2, "2319526", "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/bar_e1p0d8.webp", "Bar" },
                    { 3, "2319540", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Žabljak" },
                    { 4, "2319539", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Šavnik" },
                    { 5, "2319359", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Berane" },
                    { 6, "2319529", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Cetinje" },
                    { 7, "2319530", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Danilovgrad" },
                    { 8, "2187901", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Herceg Novi" },
                    { 9, "2319531", "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/kolasin_jejcpn.webp", "Kolašin" },
                    { 10, "2319532", "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/kotor_vgtdaz.webp", "Kotor" },
                    { 11, "2319533", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Mojkovac" },
                    { 12, "2319534", "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/niksic_hbkxzq.webp", "Nikšić" },
                    { 13, "2317882", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Plav" },
                    { 14, "2319535", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Pljevlja" },
                    { 15, "2319536", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Plužine" },
                    { 16, "2319360", "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/podgorica_qs4sij.webp", "Podgorica" },
                    { 17, "2317936", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Rožaje" },
                    { 18, "2319537", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Tivat" },
                    { 19, "2319538", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Ulcinj" },
                    { 20, "2319527", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Bijelo Polje" },
                    { 21, "2319528", "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/budva_kyvacy.webp", "Budva" },
                    { 22, "10141812", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Tuzi" },
                    { 23, "7463938", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Petnjica" },
                    { 24, "7460668", "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp", "Gusinje" }
                });

            migrationBuilder.InsertData(
                table: "languages",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "English" },
                    { 2, "Spanish" },
                    { 3, "French" },
                    { 4, "German" },
                    { 5, "Mandarin Chinese" },
                    { 6, "Arabic" },
                    { 7, "Russian" },
                    { 8, "Japanese" },
                    { 9, "Italian" },
                    { 10, "Portuguese" },
                    { 11, "Korean" },
                    { 12, "Dutch" },
                    { 13, "Swedish" },
                    { 14, "Norwegian" },
                    { 15, "Danish" },
                    { 16, "Finnish" },
                    { 17, "Greek" },
                    { 18, "Turkish" },
                    { 19, "Hindi" },
                    { 20, "Hebrew" },
                    { 21, "Polish" },
                    { 22, "Czech" },
                    { 23, "Thai" },
                    { 24, "Indonesian" },
                    { 25, "Vietnamese" },
                    { 26, "Romanian" },
                    { 27, "Hungarian" },
                    { 28, "Swahili" },
                    { 29, "Ukrainian" },
                    { 30, "Bulgarian" },
                    { 31, "Catalan" },
                    { 32, "Serbian" },
                    { 33, "Persian (Farsi)" },
                    { 34, "Tagalog" },
                    { 35, "Icelandic" },
                    { 36, "Irish" },
                    { 37, "Scottish Gaelic" },
                    { 38, "Welsh" },
                    { 39, "Latin" },
                    { 40, "Esperanto" },
                    { 41, "Bengali" },
                    { 42, "Gujarati" },
                    { 43, "Kannada" },
                    { 44, "Malayalam" },
                    { 45, "Punjabi" },
                    { 46, "Tamil" },
                    { 47, "Telugu" },
                    { 48, "Marathi" },
                    { 49, "Amharic" },
                    { 50, "Somali" },
                    { 51, "Croatian" },
                    { 52, "Montenegrin" }
                });

            migrationBuilder.InsertData(
                table: "reject_reasons",
                columns: new[] { "id", "reason_en", "reason_sr" },
                values: new object[,]
                {
                    { 1, "The advertisement similar to this already exists.", "Oglas slican ovome vec postoji." },
                    { 2, "The advert images are inappropriate or accurate.", "Slike za oglas su neprimjerene ili nisu tacne." },
                    { 3, "The advert informations are inappropriate or accurate.", "Podaci o oglasu su neprimjereni ili nisu tacni." }
                });

            migrationBuilder.InsertData(
                table: "neighborhoods",
                columns: new[] { "id", "city_id", "name" },
                values: new object[,]
                {
                    { 1, 1, "Andželati" },
                    { 2, 1, "Božići" },
                    { 3, 1, "Bojovići" },
                    { 4, 1, "Gnjili Potok" },
                    { 5, 1, "Gračanica" },
                    { 6, 1, "Dulipolje" },
                    { 7, 1, "Đulići" },
                    { 8, 1, "Zabrđe" },
                    { 9, 1, "Jošanica" },
                    { 10, 1, "Košutići" },
                    { 11, 1, "Kralje" },
                    { 12, 1, "Kuti" },
                    { 13, 1, "Gornje Luge" },
                    { 14, 1, "Rijeka Marsenića" },
                    { 15, 1, "Prisoja" },
                    { 16, 1, "Seoca" },
                    { 17, 1, "Sjenožeta" },
                    { 18, 1, "Slatina" },
                    { 19, 1, "Trepča" },
                    { 20, 1, "Trešnjevo" },
                    { 21, 1, "Ulotina" },
                    { 22, 1, "Cecuni" },
                    { 23, 2, "Arbnež" },
                    { 24, 2, "Bar" },
                    { 25, 2, "Bartula" },
                    { 26, 2, "Besa" },
                    { 27, 2, "Bjeliši" },
                    { 28, 2, "Bobovište" },
                    { 29, 2, "Boljevići" },
                    { 30, 2, "Braćeni" },
                    { 31, 2, "Brijege" },
                    { 32, 2, "Brca" },
                    { 33, 2, "Bukovik" },
                    { 34, 2, "Burtaiši" },
                    { 35, 2, "Velembusi" },
                    { 36, 2, "Veliki Mikulići" },
                    { 37, 2, "Veliki Ostros" },
                    { 38, 2, "Velja Gorana" },
                    { 39, 2, "Velje Selo" },
                    { 40, 2, "Virpazar" },
                    { 41, 2, "Gluhi Do" },
                    { 42, 2, "Godinje" },
                    { 43, 2, "Gornja Briska" },
                    { 44, 2, "Gornji Brčeli" },
                    { 45, 2, "Gornji Murići" },
                    { 46, 2, "Grdovići" },
                    { 47, 2, "Gurza" },
                    { 48, 2, "Dabezići" },
                    { 49, 2, "Dedići" },
                    { 50, 2, "Dobra Voda" },
                    { 51, 2, "Donja Briska" },
                    { 52, 2, "Donji Brčeli" },
                    { 53, 2, "Donji Murići" },
                    { 54, 2, "Dračevica" },
                    { 55, 2, "Dupilo" },
                    { 56, 2, "Đenđinovići" },
                    { 57, 2, "Đuravci" },
                    { 58, 2, "Đurmani" },
                    { 59, 2, "Zagrađe" },
                    { 60, 2, "Zaljevo" },
                    { 61, 2, "Zankovići" },
                    { 62, 2, "Zgrade" },
                    { 63, 2, "Zupci" },
                    { 64, 2, "Karanikići" },
                    { 65, 2, "Komarno" },
                    { 66, 2, "Koštanjica" },
                    { 67, 2, "Krnjice" },
                    { 68, 2, "Kruševica" },
                    { 69, 2, "Kunje" },
                    { 70, 2, "Livari" },
                    { 71, 2, "Limljani" },
                    { 72, 2, "Lukići" },
                    { 73, 2, "Mala Gorana" },
                    { 74, 2, "Mali Mikulići" },
                    { 75, 2, "Mali Ostros" },
                    { 76, 2, "Marstijepovići" },
                    { 77, 2, "Martići" },
                    { 78, 2, "Mačuge" },
                    { 79, 2, "Miljevci" },
                    { 80, 2, "Mišići" },
                    { 81, 2, "Ovtočići" },
                    { 82, 2, "Orahovo" },
                    { 83, 2, "Papani" },
                    { 84, 2, "Pelinkovići" },
                    { 85, 2, "Pečurice" },
                    { 86, 2, "Pinčići" },
                    { 87, 2, "Podi" },
                    { 88, 2, "Polje" },
                    { 89, 2, "Popratnica" },
                    { 90, 2, "Seoca" },
                    { 91, 2, "Sozina" },
                    { 92, 2, "Sotonići" },
                    { 93, 2, "Stari Bar" },
                    { 94, 2, "Sustaš" },
                    { 95, 2, "Sutomore" },
                    { 96, 2, "Tejani" },
                    { 97, 2, "Tomba" },
                    { 98, 2, "Tomići" },
                    { 99, 2, "Trnovo" },
                    { 100, 2, "Tuđemili" },
                    { 101, 2, "Turčini" },
                    { 102, 2, "Utrg" },
                    { 103, 2, "Ckla" },
                    { 104, 2, "Čeluga" },
                    { 105, 2, "Šušanj" },
                    { 106, 3, "Brajkovača" },
                    { 107, 3, "Virak" },
                    { 108, 3, "Vrela" },
                    { 109, 3, "Gomile" },
                    { 110, 3, "Gradina" },
                    { 111, 3, "Dobri Nugo" },
                    { 112, 3, "Zminica" },
                    { 113, 3, "Krš" },
                    { 114, 3, "Motički Gaj" },
                    { 115, 3, "Ninkovići" },
                    { 116, 3, "Novakovići" },
                    { 117, 3, "Njegovuđa" },
                    { 118, 3, "Palež" },
                    { 119, 3, "Pašina Voda" },
                    { 120, 3, "Pašino Polje" },
                    { 121, 3, "Pitomine" },
                    { 122, 3, "Podgora" },
                    { 123, 3, "Pošćenski Kraj" },
                    { 124, 3, "Rasova" },
                    { 125, 3, "Rudanci" },
                    { 126, 3, "Suvodo" },
                    { 127, 3, "Tepačko Polje" },
                    { 128, 3, "Tepca" },
                    { 129, 3, "Mala Crna Gora" },
                    { 130, 3, "Šljivansko" },
                    { 131, 3, "Šumanovac" },
                    { 132, 3, "Borje" },
                    { 133, 4, "Bare" },
                    { 134, 4, "Boan" },
                    { 135, 4, "Godijelji" },
                    { 136, 4, "Gornja Bijela" },
                    { 137, 4, "Gornja Bukovica" },
                    { 138, 4, "Grabovica" },
                    { 139, 4, "Dobra Sela" },
                    { 140, 4, "Donja Bijela" },
                    { 141, 4, "Donja Bukovica" },
                    { 142, 4, "Dubrovsko" },
                    { 143, 4, "Duži" },
                    { 144, 4, "Komarnica" },
                    { 145, 4, "Krnja Jela" },
                    { 146, 4, "Malinsko" },
                    { 147, 4, "Miloševići" },
                    { 148, 4, "Mljetičak" },
                    { 149, 4, "Mokro" },
                    { 150, 4, "Petnjica" },
                    { 151, 4, "Pošćenje" },
                    { 152, 4, "Previš" },
                    { 153, 4, "Pridvorica" },
                    { 154, 4, "Provalija" },
                    { 155, 4, "Slatina" },
                    { 156, 4, "Strug" },
                    { 157, 4, "Timar" },
                    { 158, 4, "Tušina" },
                    { 159, 5, "Azane" },
                    { 160, 5, "Babino" },
                    { 161, 5, "Bastahe" },
                    { 162, 5, "Beran Selo" },
                    { 163, 5, "Bor" },
                    { 164, 5, "Bubanje" },
                    { 165, 5, "Budimlja" },
                    { 166, 5, "Buče" },
                    { 167, 5, "Veliđe" },
                    { 168, 5, "Vinicka" },
                    { 169, 5, "Vrševo" },
                    { 170, 5, "Vuča" },
                    { 171, 5, "Glavaca" },
                    { 172, 5, "Godočelje" },
                    { 173, 5, "Goražde" },
                    { 174, 5, "Gornja Vrbica" },
                    { 175, 5, "Gornje Zaostro" },
                    { 176, 5, "Dapsići" },
                    { 177, 5, "Dašča Rijeka" },
                    { 178, 5, "Dobrodole" },
                    { 179, 5, "Dolac" },
                    { 180, 5, "Donja Vrbica" },
                    { 181, 5, "Donja Ržanica" },
                    { 182, 5, "Donje Zaostro" },
                    { 183, 5, "Dragosava" },
                    { 184, 5, "Zagorje" },
                    { 185, 5, "Zagrad" },
                    { 186, 5, "Zagrađe" },
                    { 187, 5, "Javorova" },
                    { 188, 5, "Johovica" },
                    { 189, 5, "Jašovići" },
                    { 190, 5, "Kalica" },
                    { 191, 5, "Kaludra" },
                    { 192, 5, "Krušča" },
                    { 193, 5, "Kurikuće" },
                    { 194, 5, "Lagatori" },
                    { 195, 5, "Lazi" },
                    { 196, 5, "Lubnice" },
                    { 197, 5, "Donje Luge" },
                    { 198, 5, "Lužac" },
                    { 199, 5, "Lješnica" },
                    { 200, 5, "Mašte" },
                    { 201, 5, "Mezgalji" },
                    { 202, 5, "Murovac" },
                    { 203, 5, "Orah" },
                    { 204, 5, "Orahovo" },
                    { 205, 5, "Pahulj" },
                    { 206, 5, "Petnjik" },
                    { 207, 5, "Petnjica" },
                    { 208, 5, "Pešca" },
                    { 209, 5, "Ponor" },
                    { 210, 5, "Poroče" },
                    { 211, 5, "Praćevac" },
                    { 212, 5, "Radmanci" },
                    { 213, 5, "Radmuževići" },
                    { 214, 5, "Rovca" },
                    { 215, 5, "Rujišta" },
                    { 216, 5, "Savin Bor" },
                    { 217, 5, "Skakavac" },
                    { 218, 5, "Tmušići" },
                    { 219, 5, "Trpezi" },
                    { 220, 5, "Tucanje" },
                    { 221, 5, "Crvljevine" },
                    { 222, 5, "Crni Vrh" },
                    { 223, 5, "Štitari" },
                    { 224, 6, "Bajice" },
                    { 225, 6, "Barjamovica" },
                    { 226, 6, "Bijele Poljane" },
                    { 227, 6, "Bjeloši" },
                    { 228, 6, "Bobija" },
                    { 229, 6, "Boguti" },
                    { 230, 6, "Bokovo" },
                    { 231, 6, "Velestovo" },
                    { 232, 6, "Vignjevići" },
                    { 233, 6, "Vojkovići" },
                    { 234, 6, "Vrba" },
                    { 235, 6, "Vrela" },
                    { 236, 6, "Vuči Do" },
                    { 237, 6, "Gadji" },
                    { 238, 6, "Gornja Zaljut" },
                    { 239, 6, "Gornji Ceklin" },
                    { 240, 6, "Grab" },
                    { 241, 6, "Gradina" },
                    { 242, 6, "Gradjani" },
                    { 243, 6, "Dide" },
                    { 244, 6, "Dobrska Župa" },
                    { 245, 6, "Dobrsko Selo" },
                    { 246, 6, "Dodoši" },
                    { 247, 6, "Donja Zaljut" },
                    { 248, 6, "Donje Selo" },
                    { 249, 6, "Dragomi Do" },
                    { 250, 6, "Drušići" },
                    { 251, 6, "Dubovik" },
                    { 252, 6, "Dubovo" },
                    { 253, 6, "Dugi Do" },
                    { 254, 6, "Dujeva" },
                    { 255, 6, "Djalci" },
                    { 256, 6, "Djinovići" },
                    { 257, 6, "Erakovići" },
                    { 258, 6, "Žanjev Do" },
                    { 259, 6, "Zabrdje" },
                    { 260, 6, "Zagora" },
                    { 261, 6, "Začir" },
                    { 262, 6, "Izvori" },
                    { 263, 6, "Jankovići" },
                    { 264, 6, "Jezer" },
                    { 265, 6, "Kobilji Do" },
                    { 266, 6, "Kopito" },
                    { 267, 6, "Kosijeri" },
                    { 268, 6, "Kranji Do" },
                    { 269, 6, "Kućišta" },
                    { 270, 6, "Lastva" },
                    { 271, 6, "Lipa" },
                    { 272, 6, "Lješev Stub" },
                    { 273, 6, "Majstori" },
                    { 274, 6, "Malošin Do" },
                    { 275, 6, "Markovina" },
                    { 276, 6, "Meterizi" },
                    { 277, 6, "Mikulići" },
                    { 278, 6, "Milijevići" },
                    { 279, 6, "Mužovići" },
                    { 280, 6, "Njeguši" },
                    { 281, 6, "Obzovica" },
                    { 282, 6, "Ožegovice" },
                    { 283, 6, "Oćevići" },
                    { 284, 6, "Očinići" },
                    { 285, 6, "Pačaradje" },
                    { 286, 6, "Pejovići" },
                    { 287, 6, "Petrov Do" },
                    { 288, 6, "Poda" },
                    { 289, 6, "Podbukovica" },
                    { 290, 6, "Prevlaka" },
                    { 291, 6, "Prediš" },
                    { 292, 6, "Prekornica" },
                    { 293, 6, "Prentin Do" },
                    { 294, 6, "Proseni Do" },
                    { 295, 6, "Radomir" },
                    { 296, 6, "Raičevići" },
                    { 297, 6, "Rvaši" },
                    { 298, 6, "Resna" },
                    { 299, 6, "Ržani Do" },
                    { 300, 6, "Rijeka Crnojevića" },
                    { 301, 6, "Riječani" },
                    { 302, 6, "Rokoči" },
                    { 303, 6, "Smokovci" },
                    { 304, 6, "Tomići" },
                    { 305, 6, "Trešnjevo" },
                    { 306, 6, "Trnjine" },
                    { 307, 6, "Uba" },
                    { 308, 6, "Ubli" },
                    { 309, 6, "Ublice" },
                    { 310, 6, "Ugnji" },
                    { 311, 6, "Ulići" },
                    { 312, 6, "Čevo" },
                    { 313, 6, "Češljari" },
                    { 314, 6, "Šinđon" },
                    { 315, 6, "Štitari" },
                    { 316, 7, "Bare Šumanovića" },
                    { 317, 7, "Begovina" },
                    { 318, 7, "Bileća" },
                    { 319, 7, "Bobulja" },
                    { 320, 7, "Bogićevići" },
                    { 321, 7, "Boronjina" },
                    { 322, 7, "Brajovići" },
                    { 323, 7, "Braćani" },
                    { 324, 7, "Brijestovo" },
                    { 325, 7, "Veleta" },
                    { 326, 7, "Vinići" },
                    { 327, 7, "Viš" },
                    { 328, 7, "Vučica" },
                    { 329, 7, "Gorica" },
                    { 330, 7, "Gornji Martinići" },
                    { 331, 7, "Gornji Rsojevići" },
                    { 332, 7, "Gostilje Brajovićko" },
                    { 333, 7, "Gostilje Martinićko" },
                    { 334, 7, "Gradina" },
                    { 335, 7, "Grbe" },
                    { 336, 7, "Gruda" },
                    { 337, 7, "Dabojevići" },
                    { 338, 7, "Daljam" },
                    { 339, 7, "Do Pješivački" },
                    { 340, 7, "Dolovi" },
                    { 341, 7, "Donje Selo" },
                    { 342, 7, "Donji Martinići" },
                    { 343, 7, "Donji Rsojevići" },
                    { 344, 7, "Drakovići" },
                    { 345, 7, "Djedjezi" },
                    { 346, 7, "Đuričkovići" },
                    { 347, 7, "Župa" },
                    { 348, 7, "Zagorak" },
                    { 349, 7, "Zagreda" },
                    { 350, 7, "Jabuke" },
                    { 351, 7, "Jastreb" },
                    { 352, 7, "Jelenak" },
                    { 353, 7, "Jovanovići" },
                    { 354, 7, "Klikovače" },
                    { 355, 7, "Kopito" },
                    { 356, 7, "Kosić" },
                    { 357, 7, "Kujava" },
                    { 358, 7, "Kupinovo" },
                    { 359, 7, "Lazarev Krst" },
                    { 360, 7, "Lalevići" },
                    { 361, 7, "Livade" },
                    { 362, 7, "Lubovo" },
                    { 363, 7, "Malenza" },
                    { 364, 7, "Mandići" },
                    { 365, 7, "Mijokusovići" },
                    { 366, 7, "Miogost" },
                    { 367, 7, "Mokanje" },
                    { 368, 7, "Mosori" },
                    { 369, 7, "Musterovići" },
                    { 370, 7, "Novo Selo" },
                    { 371, 7, "Orja Luka" },
                    { 372, 7, "Pitome Loze" },
                    { 373, 7, "Povrhpoljina" },
                    { 374, 7, "Podvraće" },
                    { 375, 7, "Podglavica" },
                    { 376, 7, "Požar" },
                    { 377, 7, "Poljica" },
                    { 378, 7, "Potkula" },
                    { 379, 7, "Potočilo" },
                    { 380, 7, "Ržišta" },
                    { 381, 7, "Rošca" },
                    { 382, 7, "Sekulići" },
                    { 383, 7, "Sladojevo Kopito" },
                    { 384, 7, "Slap" },
                    { 385, 7, "Slatina" },
                    { 386, 7, "Spuž" },
                    { 387, 7, "Sretnja" },
                    { 388, 7, "Strahinići" },
                    { 389, 7, "Tvorilo" },
                    { 390, 7, "Ćurilac" },
                    { 391, 7, "Ćurčići" },
                    { 392, 7, "Frutak" },
                    { 393, 7, "Šobaići" },
                    { 394, 7, "Šume" },
                    { 395, 8, "Baošići" },
                    { 396, 8, "Bijela" },
                    { 397, 8, "Bijelske Kruševice" },
                    { 398, 8, "Djenovići" },
                    { 399, 8, "Djurići" },
                    { 400, 8, "Žlijebi" },
                    { 401, 8, "Zelenika" },
                    { 402, 8, "Igalo" },
                    { 403, 8, "Jošice" },
                    { 404, 8, "Kameno" },
                    { 405, 8, "Kruševice" },
                    { 406, 8, "Kumbor" },
                    { 407, 8, "Kuti" },
                    { 408, 8, "Luštica" },
                    { 409, 8, "Meljine" },
                    { 410, 8, "Mojdež" },
                    { 411, 8, "Mokrine" },
                    { 412, 8, "Podi" },
                    { 413, 8, "Prijevor" },
                    { 414, 8, "Provodina" },
                    { 415, 8, "Ratiševina" },
                    { 416, 8, "Sasovići" },
                    { 417, 8, "Sutorina" },
                    { 418, 8, "Sušćepan" },
                    { 419, 8, "Trebesinj" },
                    { 420, 8, "Ubli" },
                    { 421, 9, "Babljak" },
                    { 422, 9, "Bakovići" },
                    { 423, 9, "Bare" },
                    { 424, 9, "Lipovska Bistrica" },
                    { 425, 9, "Moračka Bistrica" },
                    { 426, 9, "Blatina" },
                    { 427, 9, "Bojići" },
                    { 428, 9, "Breza" },
                    { 429, 9, "Gornja Rovca Bulatovići" },
                    { 430, 9, "Velje Duboko" },
                    { 431, 9, "Višnje" },
                    { 432, 9, "Vladoš" },
                    { 433, 9, "Vlahovići" },
                    { 434, 9, "Vojkovići" },
                    { 435, 9, "Vranještica" },
                    { 436, 9, "Vrujica" },
                    { 437, 9, "Gornje Lipovo" },
                    { 438, 9, "Donje Lipovo" },
                    { 439, 9, "Dragovića Polje" },
                    { 440, 9, "Drijenak" },
                    { 441, 9, "Drpe" },
                    { 442, 9, "Dulovine" },
                    { 443, 9, "Djudjevina" },
                    { 444, 9, "Žirci" },
                    { 445, 9, "Izlasci" },
                    { 446, 9, "Jabuka" },
                    { 447, 9, "Jasenova" },
                    { 448, 9, "Kos" },
                    { 449, 9, "Bare Kraljske" },
                    { 450, 9, "Ljevišta" },
                    { 451, 9, "Liješnje" },
                    { 452, 9, "Ljuta" },
                    { 453, 9, "Mateševo" },
                    { 454, 9, "Medjuriječje" },
                    { 455, 9, "Mioska" },
                    { 456, 9, "Moračko Trebaljevo" },
                    { 457, 9, "Mujića Rečine" },
                    { 458, 9, "Mušovića Rijeka" },
                    { 459, 9, "Osretci" },
                    { 460, 9, "Oćiba" },
                    { 461, 9, "Padež" },
                    { 462, 9, "Petrova Ravan" },
                    { 463, 9, "Plana" },
                    { 464, 9, "Ulica" },
                    { 465, 9, "Požnja" },
                    { 466, 9, "Pčinja" },
                    { 467, 9, "Ravni" },
                    { 468, 9, "Raško" },
                    { 469, 9, "Radigojno" },
                    { 470, 9, "Redice" },
                    { 471, 9, "Rovačko Trebaljevo" },
                    { 472, 9, "Svrke" },
                    { 473, 9, "Sela" },
                    { 474, 9, "Selišta" },
                    { 475, 9, "Sjerogošte" },
                    { 476, 9, "Skrbuša" },
                    { 477, 9, "Smailagića Polje" },
                    { 478, 9, "Smrče" },
                    { 479, 9, "Sreteška Gora" },
                    { 480, 9, "Starče" },
                    { 481, 9, "Tara" },
                    { 482, 9, "Trnovica" },
                    { 483, 9, "Uvač" },
                    { 484, 9, "Cerovice" },
                    { 485, 9, "Crkvine" },
                    { 486, 9, "Manastir Morača" },
                    { 487, 9, "Mrtvo Duboko" },
                    { 488, 9, "Ocka Gora" },
                    { 489, 9, "Raičevina" },
                    { 490, 10, "Bratešići" },
                    { 491, 10, "Veliki Zalazi" },
                    { 492, 10, "Višnjeva" },
                    { 493, 10, "Vranovići" },
                    { 494, 10, "Glavati" },
                    { 495, 10, "Glavatičići" },
                    { 496, 10, "Gornji Morinj" },
                    { 497, 10, "Gornji Orahovac" },
                    { 498, 10, "Gornji Stoliv" },
                    { 499, 10, "Gorovići" },
                    { 500, 10, "Dobrota" },
                    { 501, 10, "Donji Morinj" },
                    { 502, 10, "Donji Orahovac" },
                    { 503, 10, "Donji Stoliv" },
                    { 504, 10, "Dragalj" },
                    { 505, 10, "Dub" },
                    { 506, 10, "Zagora" },
                    { 507, 10, "Zvečava" },
                    { 508, 10, "Kavač" },
                    { 509, 10, "Knežlaz" },
                    { 510, 10, "Kovači" },
                    { 511, 10, "Kostanjica" },
                    { 512, 10, "Krimovica" },
                    { 513, 10, "Kubasi" },
                    { 514, 10, "Lastva Grbaljska" },
                    { 515, 10, "Ledenice" },
                    { 516, 10, "Lješevići" },
                    { 517, 10, "Mali Zalazi" },
                    { 518, 10, "Malov Do" },
                    { 519, 10, "Mirac" },
                    { 520, 10, "Muo" },
                    { 521, 10, "Nalježići" },
                    { 522, 10, "Pelinovo" },
                    { 523, 10, "Perast" },
                    { 524, 10, "Pištet" },
                    { 525, 10, "Pobrdje" },
                    { 526, 10, "Prijeradi" },
                    { 527, 10, "Prčanj" },
                    { 528, 10, "Radanovići" },
                    { 529, 10, "Risan" },
                    { 530, 10, "Strp" },
                    { 531, 10, "Sutvara" },
                    { 532, 10, "Han" },
                    { 533, 10, "Čavori" },
                    { 534, 10, "Šišići" },
                    { 535, 10, "Bigova" },
                    { 536, 10, "Bunovići" },
                    { 537, 10, "Dražin Vrt" },
                    { 538, 10, "Kolužunj" },
                    { 539, 10, "Trešnjica" },
                    { 540, 10, "Ukropci" },
                    { 541, 10, "Lipci" },
                    { 542, 10, "Unijerina" },
                    { 543, 10, "Špiljari" },
                    { 544, 10, "Škaljari" },
                    { 545, 11, "Bistrica" },
                    { 546, 11, "Gojakovići" },
                    { 547, 11, "Dobrilovina" },
                    { 548, 11, "Žari" },
                    { 549, 11, "Lepenac" },
                    { 550, 11, "Podbišće" },
                    { 551, 11, "Stevanovac" },
                    { 552, 11, "Štitarica" },
                    { 553, 11, "Bjelojevići" },
                    { 554, 11, "Bojna Njiva" },
                    { 555, 11, "Brskovo" },
                    { 556, 11, "Prošćenje" },
                    { 557, 11, "Uroševina" },
                    { 558, 11, "Polja" },
                    { 559, 12, "Balosave" },
                    { 560, 12, "Bare" },
                    { 561, 12, "Bastaji" },
                    { 562, 12, "Bjeloševina" },
                    { 563, 12, "Bobotovo Groblje" },
                    { 564, 12, "Bogetići" },
                    { 565, 12, "Bogmilovići" },
                    { 566, 12, "Brezovik" },
                    { 567, 12, "Brestice" },
                    { 568, 12, "Broćanac Viluški" },
                    { 569, 12, "Broćanac Nikšićki" },
                    { 570, 12, "Bršno" },
                    { 571, 12, "Bubrežak" },
                    { 572, 12, "Busak" },
                    { 573, 12, "Vasiljevići" },
                    { 574, 12, "Velimlje" },
                    { 575, 12, "Vidne" },
                    { 576, 12, "Vilusi" },
                    { 577, 12, "Vir" },
                    { 578, 12, "Vitasojevići" },
                    { 579, 12, "Višnjića Do" },
                    { 580, 12, "Vraćenovići" },
                    { 581, 12, "Vrbica" },
                    { 582, 12, "Vučji Do" },
                    { 583, 12, "Gvozd" },
                    { 584, 12, "Gornja Trepča" },
                    { 585, 12, "Gornje Polje" },
                    { 586, 12, "Gornje Crkvice" },
                    { 587, 12, "Gornje Čarađe" },
                    { 588, 12, "Goslić" },
                    { 589, 12, "Gradačka Poljana" },
                    { 590, 12, "Granice" },
                    { 591, 12, "Grahovac" },
                    { 592, 12, "Grahovo" },
                    { 593, 12, "Dolovi" },
                    { 594, 12, "Donja Trepča" },
                    { 595, 12, "Donje Crkvice" },
                    { 596, 12, "Donje Čaradje" },
                    { 597, 12, "Dragovoljići" },
                    { 598, 12, "Drenoštica" },
                    { 599, 12, "Dubočke" },
                    { 600, 12, "Duga" },
                    { 601, 12, "Dučice" },
                    { 602, 12, "Zavrh" },
                    { 603, 12, "Zagora" },
                    { 604, 12, "Zagrad" },
                    { 605, 12, "Zaljutnica" },
                    { 606, 12, "Zaslap" },
                    { 607, 12, "Zlostup" },
                    { 608, 12, "Ivanje" },
                    { 609, 12, "Jabuke" },
                    { 610, 12, "Javljem" },
                    { 611, 12, "Jasenovo Polje" },
                    { 612, 12, "Jugovići" },
                    { 613, 12, "Kazanci" },
                    { 614, 12, "Kamensko" },
                    { 615, 12, "Klenak" },
                    { 616, 12, "Kovači" },
                    { 617, 12, "Koprivice" },
                    { 618, 12, "Koravlica" },
                    { 619, 12, "Kunak" },
                    { 620, 12, "Kuside" },
                    { 621, 12, "Kuta" },
                    { 622, 12, "Laz" },
                    { 623, 12, "Liverovići" },
                    { 624, 12, "Lukovo" },
                    { 625, 12, "Macavare" },
                    { 626, 12, "Medjedje" },
                    { 627, 12, "Milojevići" },
                    { 628, 12, "Miločani" },
                    { 629, 12, "Miljanići" },
                    { 630, 12, "Miruše" },
                    { 631, 12, "Mokri Do" },
                    { 632, 12, "Morakovo" },
                    { 633, 12, "Nudo" },
                    { 634, 12, "Oblatno" },
                    { 635, 12, "Ozrinići" },
                    { 636, 12, "Orah" },
                    { 637, 12, "Orlina" },
                    { 638, 12, "Petrovići" },
                    { 639, 12, "Pilatovci" },
                    { 640, 12, "Povija" },
                    { 641, 12, "Podbožur" },
                    { 642, 12, "Podvrš" },
                    { 643, 12, "Ponikvica" },
                    { 644, 12, "Počekovići" },
                    { 645, 12, "Praga" },
                    { 646, 12, "Prigradina" },
                    { 647, 12, "Prisoje" },
                    { 648, 12, "Rastovac" },
                    { 649, 12, "Riđani" },
                    { 650, 12, "Riječani" },
                    { 651, 12, "Rudine" },
                    { 652, 12, "Sjenokosi" },
                    { 653, 12, "Smrduša" },
                    { 654, 12, "Somina" },
                    { 655, 12, "Spila" },
                    { 656, 12, "Srijede" },
                    { 657, 12, "Staro Selo" },
                    { 658, 12, "Stuba" },
                    { 659, 12, "Stubica" },
                    { 660, 12, "Tupan" },
                    { 661, 12, "Ubli" },
                    { 662, 12, "Carine" },
                    { 663, 12, "Cerovo" },
                    { 664, 12, "Crnodoli" },
                    { 665, 12, "Šipačno" },
                    { 666, 12, "Štedim" },
                    { 667, 12, "Štitari" },
                    { 668, 13, "Bogajići" },
                    { 669, 13, "Brezojevica" },
                    { 670, 13, "Velika" },
                    { 671, 13, "Višnjevo" },
                    { 672, 13, "Vojno Selo" },
                    { 673, 13, "Vusanje" },
                    { 674, 13, "Gornja Rženica" },
                    { 675, 13, "Grnčar" },
                    { 676, 13, "Gusinje" },
                    { 677, 13, "Dolja" },
                    { 678, 13, "Dosudje" },
                    { 679, 13, "Djurička Rijeka" },
                    { 680, 13, "Kolenovići" },
                    { 681, 13, "Kruševo" },
                    { 682, 13, "Martinovići" },
                    { 683, 13, "Mašnica" },
                    { 684, 13, "Meteh" },
                    { 685, 13, "Murino" },
                    { 686, 13, "Novšići" },
                    { 687, 13, "Prnjavor" },
                    { 688, 13, "Skić" },
                    { 689, 13, "Hoti" },
                    { 690, 14, "Alići" },
                    { 691, 14, "Boščinovići" },
                    { 692, 14, "Beljkovići" },
                    { 693, 14, "Bjeloševina" },
                    { 694, 14, "Bobovo" },
                    { 695, 14, "Boljanići" },
                    { 696, 14, "Borišići" },
                    { 697, 14, "Borova" },
                    { 698, 14, "Borovica" },
                    { 699, 14, "Brda" },
                    { 700, 14, "Bujaci" },
                    { 701, 14, "Burići" },
                    { 702, 14, "Bušnje" },
                    { 703, 14, "Varine" },
                    { 704, 14, "Vaškovo" },
                    { 705, 14, "Velike Krće" },
                    { 706, 14, "Vidre" },
                    { 707, 14, "Vijenac" },
                    { 708, 14, "Vilići" },
                    { 709, 14, "Višnjica" },
                    { 710, 14, "Vodno" },
                    { 711, 14, "Vojtina" },
                    { 712, 14, "Vrba" },
                    { 713, 14, "Vrbica" },
                    { 714, 14, "Vrulja" },
                    { 715, 14, "Vukšići" },
                    { 716, 14, "Geuši" },
                    { 717, 14, "Glibaći" },
                    { 718, 14, "Glisnica" },
                    { 719, 14, "Gornja Brvenica" },
                    { 720, 14, "Gornje Selo" },
                    { 721, 14, "Gotovuša" },
                    { 722, 14, "Gradac" },
                    { 723, 14, "Gradina" },
                    { 724, 14, "Grevo" },
                    { 725, 14, "Donja Brvenica" },
                    { 726, 14, "Dragaši" },
                    { 727, 14, "Dubac" },
                    { 728, 14, "Dubočica" },
                    { 729, 14, "Dubrava" },
                    { 730, 14, "Dužice" },
                    { 731, 14, "Durutovići" },
                    { 732, 14, "Đuli" },
                    { 733, 14, "Đurđevića Tara" },
                    { 734, 14, "Židovići" },
                    { 735, 14, "Zabrđe" },
                    { 736, 14, "Zaselje" },
                    { 737, 14, "Zbljevo" },
                    { 738, 14, "Zekavice" },
                    { 739, 14, "Zenica" },
                    { 740, 14, "Zorlovići" },
                    { 741, 14, "Jabuka" },
                    { 742, 14, "Jagodni Do" },
                    { 743, 14, "Jasen" },
                    { 744, 14, "Jahovići" },
                    { 745, 14, "Jugovo" },
                    { 746, 14, "Kakmuži" },
                    { 747, 14, "Kalušići" },
                    { 748, 14, "Katun" },
                    { 749, 14, "Klakorina" },
                    { 750, 14, "Kovači" },
                    { 751, 14, "Kovačevići" },
                    { 752, 14, "Kozica" },
                    { 753, 14, "Komine" },
                    { 754, 14, "Kordovina" },
                    { 755, 14, "Kosanica" },
                    { 756, 14, "Kotlajići" },
                    { 757, 14, "Kotline" },
                    { 758, 14, "Kotorac" },
                    { 759, 14, "Košare" },
                    { 760, 14, "Kržava" },
                    { 761, 14, "Krćevina" },
                    { 762, 14, "Krupice" },
                    { 763, 14, "Kruševo" },
                    { 764, 14, "Kukavica" },
                    { 765, 14, "Ladjana" },
                    { 766, 14, "Lever Tara" },
                    { 767, 14, "Leovo Brdo" },
                    { 768, 14, "Lijeska" },
                    { 769, 14, "Lugovi" },
                    { 770, 14, "Ljutići" },
                    { 771, 14, "Ljuće" },
                    { 772, 14, "Male Krće" },
                    { 773, 14, "Maoče" },
                    { 774, 14, "Mataruge" },
                    { 775, 14, "Madžari" },
                    { 776, 14, "Meljak" },
                    { 777, 14, "Metaljka" },
                    { 778, 14, "Mijakovići" },
                    { 779, 14, "Milakovići" },
                    { 780, 14, "Milunići" },
                    { 781, 14, "Mironići" },
                    { 782, 14, "Moraice" },
                    { 783, 14, "Moćevići" },
                    { 784, 14, "Mrzovići" },
                    { 785, 14, "Mrčevo" },
                    { 786, 14, "Mrčići" },
                    { 787, 14, "Nange" },
                    { 788, 14, "Obarde" },
                    { 789, 14, "Ogradjenica" },
                    { 790, 14, "Orlja" },
                    { 791, 14, "Otilovići" },
                    { 792, 14, "Odžak" },
                    { 793, 14, "Petine" },
                    { 794, 14, "Pauče" },
                    { 795, 14, "Pižure" },
                    { 796, 14, "Plakala" },
                    { 797, 14, "Planjsko" },
                    { 798, 14, "Pliješevina" },
                    { 799, 14, "Pliješ" },
                    { 800, 14, "Poblaće" },
                    { 801, 14, "Podborova" },
                    { 802, 14, "Popov Do" },
                    { 803, 14, "Potkovač" },
                    { 804, 14, "Potkrajci" },
                    { 805, 14, "Potoci" },
                    { 806, 14, "Potpeće" },
                    { 807, 14, "Potrlica" },
                    { 808, 14, "Pračica" },
                    { 809, 14, "Premćani" },
                    { 810, 14, "Prehari" },
                    { 811, 14, "Prisoji" },
                    { 812, 14, "Prošće" },
                    { 813, 14, "Pušanjski Do" },
                    { 814, 14, "Rabitlje" },
                    { 815, 14, "Rađevići" },
                    { 816, 14, "Romač" },
                    { 817, 14, "Rudnica" },
                    { 818, 14, "Rujevica" },
                    { 819, 14, "Selac" },
                    { 820, 14, "Selišta" },
                    { 821, 14, "Sirčići" },
                    { 822, 14, "Slatina" },
                    { 823, 14, "Srećanje" },
                    { 824, 14, "Stančani" },
                    { 825, 14, "Stranice" },
                    { 826, 14, "Strahov Do" },
                    { 827, 14, "Tvrdakovići" },
                    { 828, 14, "Trnovice" },
                    { 829, 14, "Uremovići" },
                    { 830, 14, "Horevina" },
                    { 831, 14, "Hoćevina" },
                    { 832, 14, "Cerovci" },
                    { 833, 14, "Crljenice" },
                    { 834, 14, "Crni Vrh" },
                    { 835, 14, "Crno Brdo" },
                    { 836, 14, "Crnobori" },
                    { 837, 14, "Čavanj" },
                    { 838, 14, "Čardak" },
                    { 839, 14, "Čerjenci" },
                    { 840, 14, "Čestin" },
                    { 841, 14, "Šljivansko" },
                    { 842, 14, "Šljuke" },
                    { 843, 14, "Šula" },
                    { 844, 14, "Šumani" },
                    { 845, 14, "Paljevine" },
                    { 846, 14, "Kolijevka" },
                    { 847, 14, "Tatarovina" },
                    { 848, 15, "Babići" },
                    { 849, 15, "Bajovo Polje" },
                    { 850, 15, "Barni Do" },
                    { 851, 15, "Bezuje" },
                    { 852, 15, "Bojati" },
                    { 853, 15, "Boričje" },
                    { 854, 15, "Borkovići" },
                    { 855, 15, "Brijeg" },
                    { 856, 15, "Brljevo" },
                    { 857, 15, "Bukovac" },
                    { 858, 15, "Vojinovići" },
                    { 859, 15, "Goransko" },
                    { 860, 15, "Gornja Brezna" },
                    { 861, 15, "Donja Brezna" },
                    { 862, 15, "Dubljevići" },
                    { 863, 15, "Žeično" },
                    { 864, 15, "Zabrdje" },
                    { 865, 15, "Zukva" },
                    { 866, 15, "Jerinići" },
                    { 867, 15, "Kneževići" },
                    { 868, 15, "Kovači" },
                    { 869, 15, "Lisina" },
                    { 870, 15, "Miloševići" },
                    { 871, 15, "Miljkovac" },
                    { 872, 15, "Mratinje" },
                    { 873, 15, "Nedajno" },
                    { 874, 15, "Nikovići" },
                    { 875, 15, "Osojni Orah" },
                    { 876, 15, "Pišče" },
                    { 877, 15, "Poljana" },
                    { 878, 15, "Crkvičko Polje" },
                    { 879, 15, "Prisojni Orah" },
                    { 880, 15, "Ravno" },
                    { 881, 15, "Rudinice" },
                    { 882, 15, "Seljani" },
                    { 883, 15, "Smriječno" },
                    { 884, 15, "Stabna" },
                    { 885, 15, "Stolac" },
                    { 886, 15, "Stubica" },
                    { 887, 15, "Trsa" },
                    { 888, 15, "Unač" },
                    { 889, 15, "Šarići" },
                    { 890, 16, "Arza" },
                    { 891, 16, "Balabani" },
                    { 892, 16, "Baloči" },
                    { 893, 16, "Barlaj" },
                    { 894, 16, "Begova Glavica" },
                    { 895, 16, "Bezjovo" },
                    { 896, 16, "Benkaj" },
                    { 897, 16, "Beri" },
                    { 898, 16, "Berislavci" },
                    { 899, 16, "Bigor" },
                    { 900, 16, "Bioče" },
                    { 901, 16, "Bistrice" },
                    { 902, 16, "Blizna" },
                    { 903, 16, "Bolesestra" },
                    { 904, 16, "Botun" },
                    { 905, 16, "Brežine" },
                    { 906, 16, "Bridje" },
                    { 907, 16, "Brskut" },
                    { 908, 16, "Budza" },
                    { 909, 16, "Buronji" },
                    { 910, 16, "Velje Brdo" },
                    { 911, 16, "Veruša" },
                    { 912, 16, "Vidijenje" },
                    { 913, 16, "Vilac" },
                    { 914, 16, "Vladni" },
                    { 915, 16, "Vranj" },
                    { 916, 16, "Vranjina" },
                    { 917, 16, "Vrbica" },
                    { 918, 16, "Vukovci" },
                    { 919, 16, "Vuksanlekići" },
                    { 920, 16, "Golubovci" },
                    { 921, 16, "Goljemadi" },
                    { 922, 16, "Goričani" },
                    { 923, 16, "Gornje Stravče" },
                    { 924, 16, "Gornji Kokoti" },
                    { 925, 16, "Gornji Milješ" },
                    { 926, 16, "Gostilj" },
                    { 927, 16, "Gradac" },
                    { 928, 16, "Grbavci" },
                    { 929, 16, "Grbi Do" },
                    { 930, 16, "Gurec" },
                    { 931, 16, "Delaj" },
                    { 932, 16, "Dinoša" },
                    { 933, 16, "Dolovi" },
                    { 934, 16, "Donje Stravče" },
                    { 935, 16, "Donji Kokoti" },
                    { 936, 16, "Donji Milješ" },
                    { 937, 16, "Draževina" },
                    { 938, 16, "Drešaj" },
                    { 939, 16, "Drume" },
                    { 940, 16, "Duga" },
                    { 941, 16, "Dučići" },
                    { 942, 16, "Dušići" },
                    { 943, 16, "Duške" },
                    { 944, 16, "Đurkovići" },
                    { 945, 16, "Zagreda" },
                    { 946, 16, "Zaugao" },
                    { 947, 16, "Kiselica" },
                    { 948, 16, "Klopot" },
                    { 949, 16, "Kopilje" },
                    { 950, 16, "Kornet" },
                    { 951, 16, "Kosor" },
                    { 952, 16, "Kotrabudan" },
                    { 953, 16, "Koći" },
                    { 954, 16, "Kržanja" },
                    { 955, 16, "Kruse" },
                    { 956, 16, "Krševo" },
                    { 957, 16, "Kurilo" },
                    { 958, 16, "Ljajkovići" },
                    { 959, 16, "Lekići" },
                    { 960, 16, "Lijeva Rijeka" },
                    { 961, 16, "Liješnje" },
                    { 962, 16, "Liješta" },
                    { 963, 16, "Lovka" },
                    { 964, 16, "Lopate" },
                    { 965, 16, "Lužnica" },
                    { 966, 16, "Lutovo" },
                    { 967, 16, "Mataguži" },
                    { 968, 16, "Mahala" },
                    { 969, 16, "Medun" },
                    { 970, 16, "Mileti" },
                    { 971, 16, "Mitrovići" },
                    { 972, 16, "Mojanovići" },
                    { 973, 16, "Momče" },
                    { 974, 16, "Mrke" },
                    { 975, 16, "Mužeška" },
                    { 976, 16, "Nabon" },
                    { 977, 16, "Nikmaraš" },
                    { 978, 16, "Ožezi" },
                    { 979, 16, "Omerbožovići" },
                    { 980, 16, "Opasanica" },
                    { 981, 16, "Oraovice" },
                    { 982, 16, "Orasi" },
                    { 983, 16, "Orahovo" },
                    { 984, 16, "Poprat" },
                    { 985, 16, "Parci" },
                    { 986, 16, "Pelev Brijeg" },
                    { 987, 16, "Petrovići" },
                    { 988, 16, "Pikalj" },
                    { 989, 16, "Podhum" },
                    { 990, 16, "Ponari" },
                    { 991, 16, "Prisoja" },
                    { 992, 16, "Prifti" },
                    { 993, 16, "Progonovići" },
                    { 994, 16, "Radeća" },
                    { 995, 16, "Radovče" },
                    { 996, 16, "Rakića Kuće" },
                    { 997, 16, "Raći" },
                    { 998, 16, "Releza" },
                    { 999, 16, "Rijeka Piperska" },
                    { 1000, 16, "Rudine" },
                    { 1001, 16, "Selište" },
                    { 1002, 16, "Seoca" },
                    { 1003, 16, "Seoštica" },
                    { 1004, 16, "Sjenice" },
                    { 1005, 16, "Skorać" },
                    { 1006, 16, "Slacko" },
                    { 1007, 16, "Spinja" },
                    { 1008, 16, "Srpska" },
                    { 1009, 16, "Staniselići" },
                    { 1010, 16, "Stanjevića Rupa" },
                    { 1011, 16, "Stijena" },
                    { 1012, 16, "Stjepovo" },
                    { 1013, 16, "Stupovi" },
                    { 1014, 16, "Sukuruć" },
                    { 1015, 16, "Trabojin" },
                    { 1016, 16, "Trmanje" },
                    { 1018, 16, "Tuzi Ljevorečke" },
                    { 1019, 16, "Ćafa" },
                    { 1020, 16, "Ćepetići" },
                    { 1021, 16, "Ubalac" },
                    { 1022, 16, "Ubli" },
                    { 1023, 16, "Farmaci" },
                    { 1024, 16, "Fundina" },
                    { 1025, 16, "Helmnica" },
                    { 1026, 16, "Cvilin" },
                    { 1027, 16, "Cijevna" },
                    { 1028, 16, "Crvena Paprat" },
                    { 1029, 16, "Crnci" },
                    { 1030, 16, "Šušunja" },
                    { 1031, 17, "Balotići" },
                    { 1032, 17, "Bandžov" },
                    { 1033, 17, "Bać" },
                    { 1034, 17, "Bašča" },
                    { 1035, 17, "Besnik" },
                    { 1036, 17, "Bijela Crkva" },
                    { 1037, 17, "Biševo" },
                    { 1038, 17, "Bogaji" },
                    { 1039, 17, "Bukovica" },
                    { 1040, 17, "Vuča" },
                    { 1041, 17, "Gornja Lovnica" },
                    { 1042, 17, "Grahovo" },
                    { 1043, 17, "Grižice" },
                    { 1044, 17, "Dacići" },
                    { 1045, 17, "Donja Lovnica" },
                    { 1046, 17, "Ibarac" },
                    { 1047, 17, "Jablanica" },
                    { 1048, 17, "Kalače" },
                    { 1049, 17, "Koljeno" },
                    { 1050, 17, "Paučina" },
                    { 1051, 17, "Plumci" },
                    { 1052, 17, "Radetina" },
                    { 1053, 17, "Seošnica" },
                    { 1054, 17, "Sinanovići" },
                    { 1055, 17, "Crnokrpe" },
                    { 1056, 18, "Bogdašići" },
                    { 1057, 18, "Bogišići" },
                    { 1058, 18, "Gornja Lastva" },
                    { 1059, 18, "Gošići" },
                    { 1060, 18, "Donja Lastva" },
                    { 1061, 18, "Đuraševići" },
                    { 1062, 18, "Krasići" },
                    { 1063, 18, "Lepetani" },
                    { 1064, 18, "Milovići" },
                    { 1065, 18, "Mrčevac" },
                    { 1066, 18, "Radovići" },
                    { 1067, 19, "Ambula" },
                    { 1068, 19, "Bijela Gora" },
                    { 1069, 19, "Bojke" },
                    { 1070, 19, "Brajše" },
                    { 1071, 19, "Bratica" },
                    { 1072, 19, "Briska Gora" },
                    { 1073, 19, "Vladimir" },
                    { 1074, 19, "Gornja Klezna" },
                    { 1075, 19, "Gornji Štoj" },
                    { 1076, 19, "Darza" },
                    { 1077, 19, "Donja Klezna" },
                    { 1078, 19, "Donji Štoj" },
                    { 1079, 19, "Draginje" },
                    { 1080, 19, "Zoganj" },
                    { 1081, 19, "Kaliman" },
                    { 1082, 19, "Kodre" },
                    { 1083, 19, "Kolonza" },
                    { 1084, 19, "Kosići" },
                    { 1085, 19, "Kravari" },
                    { 1086, 19, "Kruta" },
                    { 1087, 19, "Krute" },
                    { 1088, 19, "Kruče" },
                    { 1089, 19, "Leskovac" },
                    { 1090, 19, "Lisna Bore" },
                    { 1091, 19, "Međreč" },
                    { 1092, 19, "Mide" },
                    { 1093, 19, "Možura" },
                    { 1094, 19, "Pistula" },
                    { 1095, 19, "Rastiš" },
                    { 1096, 19, "Reč" },
                    { 1097, 19, "Salč" },
                    { 1098, 19, "Sveti Đorđe" },
                    { 1099, 19, "Sukobin" },
                    { 1100, 19, "Sutjel" },
                    { 1101, 19, "Ćurke" },
                    { 1102, 19, "Fraskanjel" },
                    { 1103, 19, "Šas" },
                    { 1104, 19, "Štodra" },
                    { 1105, 20, "Babaici" },
                    { 1106, 20, "Barice" },
                    { 1107, 20, "Bijedici" },
                    { 1108, 20, "Bliskovo" },
                    { 1109, 20, "Bojista" },
                    { 1110, 20, "Boljanina" },
                    { 1111, 20, "Boturici" },
                    { 1112, 20, "Voljavac" },
                    { 1113, 20, "Vrh" },
                    { 1114, 20, "Godijevo" },
                    { 1115, 20, "Godusa" },
                    { 1116, 20, "Grab Bijelo Polje" },
                    { 1117, 20, "Grancarevo" },
                    { 1118, 20, "Gubavac" },
                    { 1119, 20, "Dobrakovo" },
                    { 1120, 20, "Dobrinje" },
                    { 1121, 20, "Dolac - Bijelo Polje" },
                    { 1122, 20, "Dubovo Bijelo Polje" },
                    { 1123, 20, "Djalovici" },
                    { 1124, 20, "ziljak" },
                    { 1125, 20, "zurena" },
                    { 1126, 20, "Zaton" },
                    { 1127, 20, "Zminac" },
                    { 1128, 20, "Ivanje - BIJELO Polje" },
                    { 1129, 20, "Jablanovo" },
                    { 1130, 20, "Jabucno" },
                    { 1131, 20, "Jagoce" },
                    { 1132, 20, "Kanje" },
                    { 1133, 20, "Kicava" },
                    { 1134, 20, "Kovren" },
                    { 1135, 20, "Kostenica" },
                    { 1136, 20, "Kostici" },
                    { 1137, 20, "Kukulje" },
                    { 1138, 20, "Lazovici" },
                    { 1139, 20, "Laholo" },
                    { 1140, 20, "Lekovina" },
                    { 1141, 20, "Lijeska" },
                    { 1142, 20, "Lozna" },
                    { 1143, 20, "Loznica" },
                    { 1144, 20, "Ljesnica - Bijelo Polje" },
                    { 1145, 20, "Majstorovina" },
                    { 1146, 20, "Metanjac" },
                    { 1147, 20, "Milovo" },
                    { 1148, 20, "Mioce" },
                    { 1149, 20, "Mirojevici" },
                    { 1150, 20, "Mojstir" },
                    { 1151, 20, "Mokri Lug" },
                    { 1152, 20, "Muslici" },
                    { 1153, 20, "Negobratina" },
                    { 1154, 20, "Nedakusi" },
                    { 1155, 20, "Njegnjevo" },
                    { 1156, 20, "Obrov" },
                    { 1157, 20, "Okladi" },
                    { 1158, 20, "Orahovica" },
                    { 1159, 20, "Osmanbegovo Selo" },
                    { 1160, 20, "Ostrelj" },
                    { 1161, 20, "Pavino Polje" },
                    { 1162, 20, "Pali" },
                    { 1163, 20, "Pape" },
                    { 1164, 20, "Pecarska" },
                    { 1165, 20, "Pobretici" },
                    { 1166, 20, "Poda - Bijelo Polje" },
                    { 1167, 20, "Pozeginja" },
                    { 1168, 20, "Potkrajci - Bijelo Polje" },
                    { 1169, 20, "Potrk" },
                    { 1170, 20, "Prijelozi" },
                    { 1171, 20, "Pripcici" },
                    { 1172, 20, "Ravna Rijeka" },
                    { 1173, 20, "Radojeva Glava" },
                    { 1174, 20, "Radulici" },
                    { 1175, 20, "Rakita" },
                    { 1176, 20, "Rakonje" },
                    { 1177, 20, "Rasovo" },
                    { 1178, 20, "Rastoka" },
                    { 1179, 20, "Resnik" },
                    { 1180, 20, "Rodijelja" },
                    { 1181, 20, "Sadici" },
                    { 1182, 20, "Sela - Bijelo Polje" },
                    { 1183, 20, "Sipanje" },
                    { 1184, 20, "Sokolac" },
                    { 1185, 20, "Srdjevac" },
                    { 1186, 20, "Stozer" },
                    { 1187, 20, "Stubo" },
                    { 1188, 20, "Tomasevo" },
                    { 1189, 20, "Trubine" },
                    { 1190, 20, "Ujnice" },
                    { 1191, 20, "Unevine" },
                    { 1192, 20, "Femica Krs" },
                    { 1193, 20, "Cerovo - Bijelo Polje" },
                    { 1194, 20, "Crnis" },
                    { 1195, 20, "Crnca - Bijelo Polje" },
                    { 1196, 20, "Crhalj" },
                    { 1197, 20, "Ceoce" },
                    { 1198, 20, "Cokrlije" },
                    { 1199, 20, "Dzafica Brdo" },
                    { 1200, 20, "Sipovice" },
                    { 1201, 20, "Babića brijeg" },
                    { 1202, 20, "Banje selo" },
                    { 1203, 20, "Biokovac" },
                    { 1204, 20, "Bioča" },
                    { 1205, 20, "Bistrica" },
                    { 1206, 20, "Brestovik" },
                    { 1207, 20, "Brzava" },
                    { 1208, 20, "Brčve" },
                    { 1209, 20, "Vergaševići" },
                    { 1210, 20, "Vrbe" },
                    { 1211, 20, "Gojevići" },
                    { 1212, 20, "Gorice" },
                    { 1213, 20, "Gornji dio grada" },
                    { 1214, 20, "Dupljaci" },
                    { 1215, 20, "Kaševari" },
                    { 1216, 20, "Kneževići" },
                    { 1217, 20, "Kradenik" },
                    { 1218, 20, "Krstače" },
                    { 1219, 20, "Kruševo" },
                    { 1220, 20, "Livadice" },
                    { 1221, 20, "Lipnica" },
                    { 1222, 20, "Ličine" },
                    { 1223, 20, "Mahala" },
                    { 1224, 20, "Medanovići" },
                    { 1225, 20, "Nikoljac" },
                    { 1226, 20, "Ograde" },
                    { 1227, 20, "Oluja" },
                    { 1228, 20, "Pisana jela" },
                    { 1229, 20, "Pruška" },
                    { 1230, 20, "Raklja" },
                    { 1231, 20, "Ribarevine" },
                    { 1232, 20, "Rijeke" },
                    { 1233, 20, "Slatka" },
                    { 1234, 20, "Strojtanica" },
                    { 1235, 20, "Sutivan" },
                    { 1236, 20, "Sušica" },
                    { 1237, 20, "Ćukovac" },
                    { 1238, 20, "Ušanovići" },
                    { 1239, 20, "Centar grada" },
                    { 1240, 20, "Čampar" },
                    { 1241, 20, "Šolja" },
                    { 1242, 21, "Bečići" },
                    { 1243, 21, "Blizikuće" },
                    { 1244, 21, "Boreti" },
                    { 1245, 21, "Brajići I" },
                    { 1246, 21, "Brajici II" },
                    { 1247, 21, "Brajici III" },
                    { 1248, 21, "Brajici IV" },
                    { 1249, 21, "Brda" },
                    { 1250, 21, "Buljarica I" },
                    { 1251, 21, "Buljarica II" },
                    { 1252, 21, "Viti Do" },
                    { 1253, 21, "Drobnići" },
                    { 1254, 21, "Đenaći" },
                    { 1255, 21, "Zukovica" },
                    { 1256, 21, "Ilino Brdo" },
                    { 1257, 21, "Kaluderac I" },
                    { 1258, 21, "Kaluderac II" },
                    { 1259, 21, "Katun Rezevići" },
                    { 1260, 21, "Krstac" },
                    { 1261, 21, "Kuljače" },
                    { 1262, 21, "Kuljačce Dapkovici" },
                    { 1263, 21, "Lapčići" },
                    { 1264, 21, "Markovići" },
                    { 1265, 21, "Markovici Duletici" },
                    { 1266, 21, "Novoselje I" },
                    { 1267, 21, "Novoselje II" },
                    { 1268, 21, "Petrovac na Moru" },
                    { 1269, 21, "Pobori" },
                    { 1270, 21, "Pobori Gornji" },
                    { 1271, 21, "Podbabac" },
                    { 1272, 21, "Podostrog I" },
                    { 1273, 21, "Podostrog II" },
                    { 1274, 21, "Przno I" },
                    { 1275, 21, "Przno II" },
                    { 1276, 21, "Prijevor I" },
                    { 1277, 21, "Prijevor II" },
                    { 1278, 21, "Radjenovići" },
                    { 1279, 21, "Rijeka Rezevići" },
                    { 1280, 21, "Sveti Stefan" },
                    { 1281, 21, "Stanisići" },
                    { 1282, 21, "Tudorovići" },
                    { 1283, 21, "Cami Do" },
                    { 1284, 21, "Celobrdo" },
                    { 1285, 21, "Cucuke" },
                    { 1286, 1, "Andrijevica" },
                    { 1287, 2, "Bar" },
                    { 1288, 3, "Žabljak" },
                    { 1289, 4, "Šavnik" },
                    { 1290, 5, "Berane" },
                    { 1291, 6, "Cetinje" },
                    { 1292, 7, "Danilovgrad" },
                    { 1293, 8, "Herceg Novi" },
                    { 1294, 9, "Kolašin" },
                    { 1295, 10, "Kotor" },
                    { 1296, 11, "Mojkovac" },
                    { 1297, 12, "Nikšić" },
                    { 1298, 13, "Plav" },
                    { 1299, 14, "Pljevlja" },
                    { 1300, 15, "Plužine" },
                    { 1301, 16, "Podgorica" },
                    { 1302, 17, "Rožaje" },
                    { 1303, 18, "Tivat" },
                    { 1304, 19, "Ulcinj" },
                    { 1305, 20, "Bijelo Polje" },
                    { 1306, 21, "Budva" },
                    { 1307, 22, "Tuzi" },
                    { 1308, 23, "Petnjica" },
                    { 1309, 24, "Gusinje" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_advert_features_advert_id",
                table: "advert_features",
                column: "advert_id");

            migrationBuilder.CreateIndex(
                name: "ix_adverts_agent_id",
                table: "adverts",
                column: "agent_id");

            migrationBuilder.CreateIndex(
                name: "ix_adverts_neighborhood_id",
                table: "adverts",
                column: "neighborhood_id");

            migrationBuilder.CreateIndex(
                name: "ix_adverts_reference_id",
                table: "adverts",
                column: "reference_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_adverts_user_id",
                table: "adverts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_agencies_user_id",
                table: "agencies",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_agency_languages_language_id",
                table: "agency_languages",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "ix_agent_languages_language_id",
                table: "agent_languages",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "ix_agents_agency_id",
                table: "agents",
                column: "agency_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                table: "asp_net_role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "asp_net_roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "asp_net_user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "asp_net_user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "asp_net_user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "asp_net_users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "asp_net_users",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_images_advert_id",
                table: "images",
                column: "advert_id");

            migrationBuilder.CreateIndex(
                name: "ix_neighborhoods_city_id",
                table: "neighborhoods",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_advert_favorites_advert_id",
                table: "user_advert_favorites",
                column: "advert_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_advert_reports_advert_id",
                table: "user_advert_reports",
                column: "advert_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_advert_reports_reject_reason_id",
                table: "user_advert_reports",
                column: "reject_reason_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "advert_features");

            migrationBuilder.DropTable(
                name: "agency_languages");

            migrationBuilder.DropTable(
                name: "agent_languages");

            migrationBuilder.DropTable(
                name: "asp_net_role_claims");

            migrationBuilder.DropTable(
                name: "asp_net_user_claims");

            migrationBuilder.DropTable(
                name: "asp_net_user_logins");

            migrationBuilder.DropTable(
                name: "asp_net_user_roles");

            migrationBuilder.DropTable(
                name: "asp_net_user_tokens");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "images_to_delete");

            migrationBuilder.DropTable(
                name: "outbox_messages");

            migrationBuilder.DropTable(
                name: "temporery_images_data");

            migrationBuilder.DropTable(
                name: "user_advert_favorites");

            migrationBuilder.DropTable(
                name: "user_advert_reports");

            migrationBuilder.DropTable(
                name: "languages");

            migrationBuilder.DropTable(
                name: "asp_net_roles");

            migrationBuilder.DropTable(
                name: "adverts");

            migrationBuilder.DropTable(
                name: "reject_reasons");

            migrationBuilder.DropTable(
                name: "agents");

            migrationBuilder.DropTable(
                name: "neighborhoods");

            migrationBuilder.DropTable(
                name: "agencies");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "asp_net_users");
        }
    }
}

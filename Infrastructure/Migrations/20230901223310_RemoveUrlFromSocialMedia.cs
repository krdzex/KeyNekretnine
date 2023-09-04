using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUrlFromSocialMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "social_media_twitter_url",
                table: "agents",
                newName: "social_media_twitter");

            migrationBuilder.RenameColumn(
                name: "social_media_linkedin_url",
                table: "agents",
                newName: "social_media_linkedin");

            migrationBuilder.RenameColumn(
                name: "social_media_instagram_url",
                table: "agents",
                newName: "social_media_instagram");

            migrationBuilder.RenameColumn(
                name: "social_media_facebook_url",
                table: "agents",
                newName: "social_media_facebook");

            migrationBuilder.RenameColumn(
                name: "social_media_twitter_url",
                table: "agencies",
                newName: "social_media_twitter");

            migrationBuilder.RenameColumn(
                name: "social_media_linkedin_url",
                table: "agencies",
                newName: "social_media_linkedin");

            migrationBuilder.RenameColumn(
                name: "social_media_instagram_url",
                table: "agencies",
                newName: "social_media_instagram");

            migrationBuilder.RenameColumn(
                name: "social_media_facebook_url",
                table: "agencies",
                newName: "social_media_facebook");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "social_media_twitter",
                table: "agents",
                newName: "social_media_twitter_url");

            migrationBuilder.RenameColumn(
                name: "social_media_linkedin",
                table: "agents",
                newName: "social_media_linkedin_url");

            migrationBuilder.RenameColumn(
                name: "social_media_instagram",
                table: "agents",
                newName: "social_media_instagram_url");

            migrationBuilder.RenameColumn(
                name: "social_media_facebook",
                table: "agents",
                newName: "social_media_facebook_url");

            migrationBuilder.RenameColumn(
                name: "social_media_twitter",
                table: "agencies",
                newName: "social_media_twitter_url");

            migrationBuilder.RenameColumn(
                name: "social_media_linkedin",
                table: "agencies",
                newName: "social_media_linkedin_url");

            migrationBuilder.RenameColumn(
                name: "social_media_instagram",
                table: "agencies",
                newName: "social_media_instagram_url");

            migrationBuilder.RenameColumn(
                name: "social_media_facebook",
                table: "agencies",
                newName: "social_media_facebook_url");
        }
    }
}

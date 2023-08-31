namespace Repository.RawQuery
{
    public static class AgencyQuery
    {
        public const string CreateAgencyQuery = @"
        INSERT INTO agencies (name, created_date, user_id)
        VALUES(@name,@createdDate,@userId);";

        public const string GetAgencyImageQuery = @"
        SELECT image_url FROM agencies
        WHERE id = @agencyId;";

        public const string DoesAgencyExistQuery = @"
        SELECT COUNT(id)
        FROM agencies
        WHERE id = @agencyId;";


        public const string IsUserAgencyOwnerQuery = @"
        SELECT id
        FROM agencies
        WHERE id = @agencyId
        AND user_id = @userId;";


        public const string UpdateAgencyQuery = @"
        UPDATE agencies
        SET name = @name, address = @address, description = @description, email =  @email, website_url = @websiteUrl, work_start_time = @workStartTime, work_end_time = @workEndTime, twitter_url = @twitterUrl, facebook_url = @facebookUrl, instagram_url = @instagramUrl,linkedin_url = @linkedinUrl, latitude = @latitude,longitude = @longitude, image_url = @imageUrl
        WHERE id = @agencyId;";

        public const string AssignLanguageToAgencyQuery = @"
        INSERT INTO agency_languages (agency_id, language_id)
        VALUES(@agencyId,@languageId);";


        public const string DeleteLanguagesForAgencyQuery = @"
        DELETE FROM agency_languages
        WHERE agency_id = @agencyId;";
    }
}
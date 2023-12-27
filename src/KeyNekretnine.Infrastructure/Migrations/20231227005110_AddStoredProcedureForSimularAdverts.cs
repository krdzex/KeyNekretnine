using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyNekretnine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProcedureForSimularAdverts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var functionSql = @"
				CREATE OR REPLACE FUNCTION get_recommended_adverts(input_reference_id VARCHAR)
				RETURNS TABLE(
					city_id INT,
					purpose INT,
					referenceId VARCHAR(10),
					price DOUBLE PRECISION,
					floorSpace DOUBLE PRECISION,
					noOfBedrooms INT,
					noOfBathrooms INT,
					createdOnDate TIMESTAMP WITH TIME ZONE,
					coverImageUrl VARCHAR,
					cityAndNeighborhood TEXT,
					address VARCHAR,
					isUrgent BOOL,
					is_premium BOOL,
					type INT,
					matching_points NUMERIC
				) 
				LANGUAGE plpgsql
				AS $$
				DECLARE
					ref_advert adverts;
				BEGIN
					SELECT 
						*
					INTO ref_advert
					FROM adverts a
					WHERE a.reference_id = input_reference_id;
	
					RETURN QUERY
					SELECT 
						c.id AS city_id,
						a.purpose,
						a.reference_id AS referenceId,
						a.price,
						a.floor_space AS floorSpace,
						a.no_of_bedrooms AS noOfBedrooms,
						a.no_of_bathrooms AS noOfBathrooms,
						a.created_on_date AS createdOnDate,
						a.cover_image_url AS  coverImageUrl,
						CONCAT(c.name, ', ', n.name) AS cityAndNeighborhood,
						a.location_address AS address,
						a.is_urgent AS isUrgent,
						a.is_premium AS isPremium,
						a.type,
					(
						CASE WHEN a.neighborhood_id = ref_advert.neighborhood_id THEN 2 ELSE 0 END +
						CASE WHEN a.floor_space BETWEEN ref_advert.floor_space * 0.9 AND ref_advert.floor_space * 1.1 THEN 1 ELSE 0 END +
						CASE WHEN a.no_of_bathrooms BETWEEN ref_advert.no_of_bathrooms - 1 AND ref_advert.no_of_bathrooms + 1 THEN 1 ELSE 0 END +
						CASE WHEN a.no_of_bedrooms BETWEEN ref_advert.no_of_bedrooms - 1 AND ref_advert.no_of_bedrooms + 1 THEN 1 ELSE 0 END +
						CASE WHEN a.purpose = ref_advert.purpose THEN 1 ELSE 0 END +
						CASE WHEN a.price BETWEEN ref_advert.price * 0.9 AND ref_advert.price * 1.1 THEN 1 ELSE 0 END +
						CASE WHEN a.type = ref_advert.type THEN 1 ELSE 0 END
					) / 7.0 AS matching_points
					FROM adverts a
					INNER JOIN neighborhoods AS n ON a.neighborhood_id = n.id
					INNER JOIN cities AS c ON n.city_id = c.id
					ORDER BY 
						matching_points DESC, 
						price ASC
					LIMIT 8;
				END;
				$$;";

            migrationBuilder.Sql(functionSql);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS get_recommended_adverts(VARCHAR);");
        }
    }
}

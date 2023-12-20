namespace KeyNekretnine.Domain.ValueObjects;
public record class Location
{
    public const int MaxLength = 300;
    private Location()
    {
    }

    public string? Address { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }

    public static Location Create(string? address, double? latitude, double? longitude)
    {
        if (!string.IsNullOrEmpty(address))
        {
            if (address.Length > MaxLength)
            {
                throw new ApplicationException($"Address cannot exceed 300 characters.");

            }
        }
        else
        {
            address = null;
        }

        if ((latitude == 500 && longitude != 500) || (latitude != 500 && longitude == 500))
        {
            throw new ApplicationException("You need to provide both latitude and longtitude to update location");
        }
        if (latitude == 500 && longitude == 500)
        {
            latitude = null;
            longitude = null;
        }
        else
        {
            if (latitude < -90 || latitude > 90)
            {
                throw new ApplicationException("Latitude must be between -90 and 90.");
            }

            if (longitude < -180 || longitude > 180)
            {
                throw new ApplicationException("Longitude must be between -180 and 180.");
            }
        }


        return new Location
        {
            Address = address,
            Latitude = latitude,
            Longitude = longitude
        };
    }
}
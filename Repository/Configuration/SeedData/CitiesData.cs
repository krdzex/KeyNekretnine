using Entities.Models;

namespace Repository.Configuration.SeedData;
public class CitiesData
{
    public static List<City> GetCities()
    {
        var cities = new List<City>()
            {
                new City{ Id = 1, Name = "Andrijevica", GeoId = "2319358", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 2, Name = "Bar", GeoId = "2319526", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 3, Name = "Žabljak", GeoId = "2319540", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 4, Name = "Šavnik", GeoId = "2319539", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 5, Name = "Berane", GeoId = "2319359", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 6, Name = "Cetinje", GeoId = "2319529", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 7, Name = "Danilovgrad", GeoId = "2319530", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 8, Name = "Herceg Novi", GeoId = "2187901", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 9, Name = "Kolašin", GeoId = "2319531", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 10, Name = "Kotor", GeoId = "2319532", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 11, Name = "Mojkovac", GeoId = "2319533", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 12, Name = "Nikšić", GeoId = "2319534", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 13, Name = "Plav", GeoId = "2317882", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 14, Name = "Pljevlja", GeoId = "2319535", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 15, Name = "Plužine", GeoId = "2319536", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 16, Name = "Podgorica", GeoId = "2319360", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 17, Name = "Rožaje", GeoId = "2317936", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 18, Name = "Tivat", GeoId = "2319537", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 19, Name = "Ulcinj", GeoId = "2319538", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 20, Name = "Bijelo Polje", GeoId = "2319527", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 21, Name = "Budva", GeoId = "2319528", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 22, Name = "Tuzi", GeoId = "10141812", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 23, Name = "Petnjica", GeoId = "7463938", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 24, Name = "Gusinje", GeoId = "7460668", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"}
            };

        return cities;
    }
}
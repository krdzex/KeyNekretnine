using KeyNekretnine.Domain.Cities;

namespace KeyNekretnine.Infrastructure.Configuration.SeedData;
public class CitiesData
{
    public static List<City> GetCities()
    {
        var cities = new List<City>()
            {
                new City{ Id = 1, Name = "Andrijevica", Slug = "andrijevica", GeoId = "2319358", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 2, Name = "Bar", Slug = "bar", GeoId = "2319526", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/bar_e1p0d8.webp"},
                new City{ Id = 3, Name = "Žabljak", Slug = "zabljak", GeoId = "2319540", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 4, Name = "Šavnik", Slug = "savnik", GeoId = "2319539", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 5, Name = "Berane", Slug = "berane", GeoId = "2319359", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 6, Name = "Cetinje", Slug = "cetinje", GeoId = "2319529", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 7, Name = "Danilovgrad", Slug = "danilovgrad", GeoId = "2319530", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 8, Name = "Herceg Novi", Slug = "herceg-novi", GeoId = "2187901", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 9, Name = "Kolašin", Slug = "kolasin", GeoId = "2319531", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/kolasin_jejcpn.webp"},
                new City{ Id = 10, Name = "Kotor", Slug = "kotor", GeoId = "2319532", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/kotor_vgtdaz.webp"},
                new City{ Id = 11, Name = "Mojkovac", Slug = "mojkovac", GeoId = "2319533", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 12, Name = "Nikšić", Slug = "niksic", GeoId = "2319534", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/niksic_hbkxzq.webp"},
                new City{ Id = 13, Name = "Plav", Slug = "plav", GeoId = "2317882", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 14, Name = "Pljevlja", Slug = "pljevlja", GeoId = "2319535", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 15, Name = "Plužine", Slug = "pluzine", GeoId = "2319536", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 16, Name = "Podgorica", Slug = "podgorica", GeoId = "2319360", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/podgorica_qs4sij.webp"},
                new City{ Id = 17, Name = "Rožaje", Slug = "rozaje", GeoId = "2317936", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 18, Name = "Tivat", Slug = "tivat", GeoId = "2319537", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 19, Name = "Ulcinj", Slug = "ulcinj", GeoId = "2319538", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 20, Name = "Bijelo Polje", Slug = "bijelo-polje", GeoId = "2319527", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 21, Name = "Budva", Slug = "budva", GeoId = "2319528", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1683584242/Cities/budva_kyvacy.webp"},
                new City{ Id = 22, Name = "Tuzi", Slug = "tuzi", GeoId = "10141812", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 23, Name = "Petnjica", Slug = "petnjica", GeoId = "7463938", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 24, Name = "Gusinje", Slug = "gusinje", GeoId = "7460668", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"}
            };

        return cities;
    }
}
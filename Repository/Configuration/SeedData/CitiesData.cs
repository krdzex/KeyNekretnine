using Entities.Models;

namespace Repository.Configuration.SeedData;
public class CitiesData
{
    public static List<City> GetCities()
    {
        var cities = new List<City>()
            {
                new City{ Id = 1, Name = "Andrijevica", GeoId = "297983629", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 2, Name = "Bar", GeoId = "298324414", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 3, Name = "Žabljak", GeoId = "298076995", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 4, Name = "Šavnik", GeoId = "299079819", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 5, Name = "Berane", GeoId = "297983360", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 6, Name = "Cetinje", GeoId = "298008175", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 7, Name = "Danilovgrad", GeoId = "298134912", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 8, Name = "Herceg Novi", GeoId = "298246430", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 9, Name = "Kolašin", GeoId = "298271503", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 10, Name = "Kotor", GeoId = "297988513", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 11, Name = "Mojkovac", GeoId = "298230379", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 12, Name = "Nikšić", GeoId = "297979150", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 13, Name = "Plav", GeoId = "297986966", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 14, Name = "Pljevlja", GeoId = "298438579", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 15, Name = "Plužine", GeoId = "298163670", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 16, Name = "Podgorica", GeoId = "298233944", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 17, Name = "Rožaje", GeoId = "297978984", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 18, Name = "Tivat", GeoId = "298016342", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 19, Name = "Ulcinj", GeoId = "298023651", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 20, Name = "Bijelo Polje", GeoId = "298265596", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 21, Name = "Budva", GeoId = "297988603", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 22, Name = "Tuzi", GeoId = "298871101", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 23, Name = "Petnjica", GeoId = "298605656", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"},
                new City{ Id = 24, Name = "Gusinje", GeoId = "299016015", ImageUrl = "https://res.cloudinary.com/agencija108/image/upload/v1675218057/l9upr2bcojm038khjak8.webp"}
            };

        return cities;
    }
}
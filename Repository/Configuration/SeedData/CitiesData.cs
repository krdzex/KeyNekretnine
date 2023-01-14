using Entities.Models;

namespace Repository.Configuration.SeedData;
public class CitiesData
{
    public static List<City> GetCities()
    {
        var cities = new List<City>()
            {
                new City{ Id = 1, Name = "Andrijevica", GeoId = "297983629"},
                new City{ Id = 2, Name = "Bar", GeoId = "298324414"},
                new City{ Id = 3, Name = "Žabljak", GeoId = "298076995"},
                new City{ Id = 4, Name = "Šavnik", GeoId = "299079819"},
                new City{ Id = 5, Name = "Berane", GeoId = "297983360"},
                new City{ Id = 6, Name = "Cetinje", GeoId = "298008175"},
                new City{ Id = 7, Name = "Danilovgrad", GeoId = "298134912"},
                new City{ Id = 8, Name = "Herceg Novi", GeoId = "298246430"},
                new City{ Id = 9, Name = "Kolašin", GeoId = "298271503"},
                new City{ Id = 10, Name = "Kotor", GeoId = "297988513"},
                new City{ Id = 11, Name = "Mojkovac", GeoId = "298230379"},
                new City{ Id = 12, Name = "Nikšić", GeoId = "297979150"},
                new City{ Id = 13, Name = "Plav", GeoId = "297986966"},
                new City{ Id = 14, Name = "Pljevlja", GeoId = "298438579"},
                new City{ Id = 15, Name = "Plužine", GeoId = "298163670"},
                new City{ Id = 16, Name = "Podgorica", GeoId = "298233944"},
                new City{ Id = 17, Name = "Rožaje", GeoId = "297978984"},
                new City{ Id = 18, Name = "Tivat", GeoId = "298016342"},
                new City{ Id = 19, Name = "Ulcinj", GeoId = "298023651"},
                new City{ Id = 20, Name = "Bijelo Polje", GeoId = "298265596"},
                new City{ Id = 21, Name = "Budva", GeoId = "297988603"},
                new City{ Id = 22, Name = "Tuzi", GeoId = "298871101"},
                new City{ Id = 23, Name = "Petnjica", GeoId = "298605656"},
                new City{ Id = 24, Name = "Gusinje", GeoId = "299016015"}
            };

        return cities;
    }
}
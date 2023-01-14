namespace Entities.Models;
public class AdvertStatus : EntityBase
{
    public string Name { get; set; }
    public List<Advert> Adverts { get; set; }
}


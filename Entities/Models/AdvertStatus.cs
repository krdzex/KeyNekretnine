namespace Entities.Models;
public class AdvertStatus : EntityBase
{
    public string NameSr { get; set; }
    public string NameEn { get; set; }
    public List<Advert> Adverts { get; set; }
}


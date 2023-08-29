using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Models;

namespace KeyNekretnine.Domain.AdvertStatuses;
public class AdvertStatus : EntityBase
{
    public string NameSr { get; set; }
    public string NameEn { get; set; }
    public List<Advert> Adverts { get; set; }
}
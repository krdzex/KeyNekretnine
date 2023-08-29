using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Models;

namespace KeyNekretnine.Domain.AdvertTypes;
public class AdvertType : EntityBase
{
    public string NameSr { get; set; }
    public string NameEn { get; set; }
    public List<Advert> Adverts { get; set; }
}
namespace Entities.Models;
public class ImaginaryAgent : EntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string ImageUrl { get; set; }

    public IEnumerable<Advert> Adverts { get; set; }

    public Agency Agency { get; set; }
    public int AgencyId { get; set; }
}

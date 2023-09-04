using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts;
using KeyNekretnine.Domain.Agencies;

namespace KeyNekretnine.Domain.Agents;
public class Agent : Entity
{
    public FirstName FirstName { get; set; }
    public LastName LastName { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public ImageUrl ImageUrl { get; set; }
    public Description Description { get; set; }
    public Email Email { get; set; }
    public SocialMedia SocialMedia { get; set; }
    public Guid AgencyId { get; set; }
    public IEnumerable<Advert> Adverts { get; set; }
}
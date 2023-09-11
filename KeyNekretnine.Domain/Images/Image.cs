using KeyNekretnine.Domain.Models;

namespace KeyNekretnine.Domain.Images;
public class Image : EntityBase
{
    public string Url { get; set; }
    public Guid AdvertId { get; set; }
}
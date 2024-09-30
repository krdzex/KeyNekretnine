using KeyNekretnine.Domain.Models;

namespace KeyNekretnine.Domain.Images;
public class Image : EntityBase
{
    public string Url { get; set; }
    public Guid AdvertId { get; set; }
    public bool IsForUpdate { get; set; }


    public void UpdateUrl(string newUrl)
    {
        Url = newUrl;
    }
}
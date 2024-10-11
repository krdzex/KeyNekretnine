using KeyNekretnine.Domain.Models;

namespace KeyNekretnine.Domain.Images;
public class Image : EntityBase
{
    public string Url { get; private set; }
    public Guid AdvertId { get; private set; }

    public Image(string url)
    {
        Url = url;
    }

    public void UpdateUrl(string newUrl)
    {
        Url = newUrl;
    }
}
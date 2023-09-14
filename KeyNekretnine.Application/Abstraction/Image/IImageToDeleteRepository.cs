namespace KeyNekretnine.Application.Abstraction.Image;
public interface IImageToDeleteRepository
{
    void Add(string imageUrl, DateTime addedOnTime);
}

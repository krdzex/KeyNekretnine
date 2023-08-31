using Shared.RequestFeatures;

namespace KeyNekretnine.Application.Core.Shared.Pagination;
public class Pagination<T>
{
    public IEnumerable<T> Data { get; set; }
    public MetaData MetaData { get; set; }
}

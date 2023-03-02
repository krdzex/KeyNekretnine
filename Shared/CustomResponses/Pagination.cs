using Shared.RequestFeatures;

namespace Shared.CustomResponses;
public class Pagination<T>
{
    public IEnumerable<T> Data { get; set; }
    public MetaData MetaData { get; set; }
}
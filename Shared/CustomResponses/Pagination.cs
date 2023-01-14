using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Shared.CustomResponses
{
    public class Pagination
    {
        public IEnumerable<MinimalInformationsAboutAdvertDto> Adverts { get; set; }
        public MetaData MetaData { get; set; }
    }
}

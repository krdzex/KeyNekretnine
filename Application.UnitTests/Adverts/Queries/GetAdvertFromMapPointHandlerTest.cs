using Application.Core.Adverts.Queries.GetAdvertFromMap;
using Contracts;
using Entities.DomainErrors;
using FluentAssertions;
using Moq;
using Shared.DataTransferObjects.Advert;

namespace Application.UnitTests.Adverts.Queries;
public class GetAdvertFromMapPointHandlerTest
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;

    public GetAdvertFromMapPointHandlerTest()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenAdvertExists()
    {
        var advertId = 1;

        var advert = new MinimalInformationsAboutAdvertDto
        {
            Id = advertId,
        };

        var query = new GetAdvertFromMapQuery(advertId);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetAdvertFromMapPoint(advertId, default))
            .ReturnsAsync(advert);

        var handler = new GetAdvertFromMapHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenAdvertDoesNotExist()
    {
        var query = new GetAdvertFromMapQuery(-1);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetAdvertFromMapPoint(-1, default))
            .ReturnsAsync((MinimalInformationsAboutAdvertDto)null!);

        var handler = new GetAdvertFromMapHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Advert.AdvertNotFound(-1));
    }
}
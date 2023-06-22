using Application.Adverts.Queries.GetAdvertById;
using Contracts;
using Entities.DomainErrors;
using FluentAssertions;
using Moq;
using Shared.DataTransferObjects.Advert;

namespace Application.UnitTests.Adverts.Queries;
public class GetAdvertByIdHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;

    public GetAdvertByIdHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenAdvertExists()
    {
        var advertId = 1;

        var advert = new AllInfomrationsAboutAdvertDto
        {
            Id = advertId,
        };

        var query = new GetAdvertByIdQuery(advertId);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetAdvert(advertId, default))
            .ReturnsAsync(advert);

        var handler = new GetAdvertHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(advert);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenAdvertDoesNotExist()
    {
        var query = new GetAdvertByIdQuery(-1);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetAdvert(-1, default))
            .ReturnsAsync((AllInfomrationsAboutAdvertDto)null!);

        var handler = new GetAdvertHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Advert.AdvertNotFound(-1));
    }
}
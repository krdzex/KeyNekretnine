using Application.Core.Adverts.Queries.GetmapPoints;
using Contracts;
using FluentAssertions;
using Moq;
using Shared.DataTransferObjects.Advert;

namespace Application.UnitTests.Adverts.Queries;
public class GetMapPointsHandlerTest
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;

    public GetMapPointsHandlerTest()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenThereIsNoErrors()
    {
        var query = new GetMapPointsQuery();

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetMapPoints(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ShowAdvertLocationOnMapDto>());

        var handler = new GetMapPointsHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
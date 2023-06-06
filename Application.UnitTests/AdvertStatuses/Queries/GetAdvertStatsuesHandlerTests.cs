using Application.Core.AdvertStatuses.Queries.GetAdvertStatuses;
using Contracts;
using FluentAssertions;
using Moq;
using Shared.DataTransferObjects.AdvertStatus;

namespace Application.UnitTests.AdvertStatuses.Queries;
public class GetAdvertStatsuesHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;
    public GetAdvertStatsuesHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenThereIsNoErrors()
    {
        // Arrange
        var query = new GetAdvertStatusesQuery();

        var advertStatuses = new List<AdvertStatusDto>
        {
            new AdvertStatusDto { Id = 1, Name = { En = "test", Sr = "test" } },
            new AdvertStatusDto { Id = 2, Name = { En = "test", Sr = "test" } },
            new AdvertStatusDto { Id = 3, Name = { En = "test", Sr = "test" } }
        };

        _reposistoryManagerMock.Setup(
            x => x.AdvertStatus.GetAdvertsStatuses(It.IsAny<CancellationToken>()))
            .ReturnsAsync(advertStatuses);

        var handler = new GetAdvertStatusesHandler(_reposistoryManagerMock.Object);


        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
        result.Value.Should().BeEquivalentTo(advertStatuses);
    }
}

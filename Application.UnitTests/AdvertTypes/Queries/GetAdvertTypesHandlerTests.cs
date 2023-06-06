using Application.Core.AdvertTypes.Queries.GetAdvertTypes;
using Contracts;
using FluentAssertions;
using Moq;
using Shared.DataTransferObjects.AdvertType;

namespace Application.UnitTests.AdvertTypes.Queries;
public class GetAdvertTypesHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;
    public GetAdvertTypesHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenThereIsNoErrors()
    {
        // Arrange
        var query = new GetAdvertTypesQuery();

        var advertTypes = new List<AdvertTypeDto>
        {
            new AdvertTypeDto { Id = 1, Name = { En = "test", Sr = "test" } },
            new AdvertTypeDto { Id = 2, Name = { En = "test", Sr = "test" } },
            new AdvertTypeDto { Id = 3, Name = { En = "test", Sr = "test" } }
        };

        _reposistoryManagerMock.Setup(
            x => x.AdvertType.GetAdvertTypes(It.IsAny<CancellationToken>()))
            .ReturnsAsync(advertTypes);

        var handler = new GetAdvertTypesHandler(_reposistoryManagerMock.Object);


        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
        result.Value.Should().BeEquivalentTo(advertTypes);
    }
}

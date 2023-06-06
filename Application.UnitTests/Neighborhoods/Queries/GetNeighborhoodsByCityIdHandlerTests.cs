using Application.Core.Neighborhoods.Queries;
using Contracts;
using Entities.DomainErrors;
using FluentAssertions;
using Moq;
using Shared.DataTransferObjects.Neighborhood;

namespace Applications.UnitTests.Neighborhoods.Queries
{
    public class GetNeighborhoodsByCityIdHandlerTests
    {
        private readonly Mock<IRepositoryManager> _reposistoryManagerMock;
        public GetNeighborhoodsByCityIdHandlerTests()
        {
            _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
        }

        [Fact]
        public async Task Handle_Should_ReturnFailureResult_WhenThereIsNoNeighborhoodsForCity()
        {
            // Arrange
            var query = new GetNeighborhoodsByCityIdQuery(0);

            _reposistoryManagerMock.Setup(
                x => x.Neighborhood.GetNeighborhoods(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<NeighborhoodDto>());

            var handler = new GetNeighborhoodsByCityIdHandler(_reposistoryManagerMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(DomainErrors.Neighborhood.NeighborhoodNotFound);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResult_WhenThereIsNeighborhoodsForCity()
        {
            // Arrange
            var query = new GetNeighborhoodsByCityIdQuery(0);

            _reposistoryManagerMock.Setup(
                x => x.Neighborhood.GetNeighborhoods(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<NeighborhoodDto>() { new NeighborhoodDto { Id = 5, Name = "Test" } });

            var handler = new GetNeighborhoodsByCityIdHandler(_reposistoryManagerMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeEmpty();
        }


        [Fact]
        public async Task Handle_Should_ReturnSuccessResult_WhenMultipleNeighborhoods()
        {
            // Arrange
            var query = new GetNeighborhoodsByCityIdQuery(0);

            _reposistoryManagerMock.Setup(
                x => x.Neighborhood.GetNeighborhoods(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<NeighborhoodDto>()
                {
            new NeighborhoodDto { Id = 1, Name = "Test1" },
            new NeighborhoodDto { Id = 2, Name = "Test2" },
            new NeighborhoodDto { Id = 3, Name = "Test3" }
                });

            var handler = new GetNeighborhoodsByCityIdHandler(_reposistoryManagerMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetNeighborhoods_Should_ReturnFailureResult_WhenNeighborhoodsExistDb()
        {
            // Arrange
            var query = new GetNeighborhoodsByCityIdQuery(0);

            var handler = new GetNeighborhoodsByCityIdHandler(_reposistoryManagerMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(DomainErrors.Neighborhood.NeighborhoodNotFound);
        }
    }
}
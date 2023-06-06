using Application.Cities.Queries.GetCities;
using Contracts;
using FluentAssertions;
using Moq;
using Shared.DataTransferObjects.City;

namespace Application.UnitTests.Cities.Queries;
public class GetCitiesHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;
    public GetCitiesHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenThereIsNoErrors()
    {
        // Arrange
        var query = new GetCitiesQuery();

        var cities = new List<CityDto>
        {
             new CityDto { Id = 1, Name = "City1" },
             new CityDto { Id = 2, Name = "City2" },
             new CityDto { Id = 3, Name = "City3" }
        };

        _reposistoryManagerMock.Setup(
            x => x.City.GetCities(It.IsAny<CancellationToken>()))
            .ReturnsAsync(cities.AsEnumerable);

        var handler = new GetCitiesHandler(_reposistoryManagerMock.Object);


        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
        result.Value.Should().BeEquivalentTo(cities);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenNoCitiesExist()
    {
        // Arrange
        var query = new GetCitiesQuery();

        var cities = new List<CityDto>();

        _reposistoryManagerMock.Setup(
                   x => x.City.GetCities(It.IsAny<CancellationToken>()))
                   .ReturnsAsync(cities);

        var handler = new GetCitiesHandler(_reposistoryManagerMock.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }
}

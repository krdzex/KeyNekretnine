using Application.Core.Cities.Queries.GetMostPopularCtities;
using Contracts;
using FluentAssertions;
using Moq;
using Shared.DataTransferObjects.City;

namespace Application.UnitTests.Cities.Queries;
public class GetMostPopularCitiesHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;
    public GetMostPopularCitiesHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenNoQueryErrors()
    {
        var query = new GetMostPopularCitiesQuery();

        var cities = new List<PopularCitiesDto>
        {
             new PopularCitiesDto { Id = 1, Name = "City1", Adverts_Count = 1, Image_Url = "Test1" },
             new PopularCitiesDto { Id = 2, Name = "City2", Adverts_Count = 2, Image_Url = "Test2" },
             new PopularCitiesDto { Id = 3, Name = "City3", Adverts_Count = 3, Image_Url = "Test3" },
             new PopularCitiesDto { Id = 4, Name = "City4", Adverts_Count = 1, Image_Url = "Test4" },
             new PopularCitiesDto { Id = 5, Name = "City5", Adverts_Count = 2, Image_Url = "Test5" }
        };

        _reposistoryManagerMock.Setup(
            x => x.City.GetMostPopularCities(It.IsAny<CancellationToken>()))
            .ReturnsAsync(cities);

        var handler = new GetMostPopularCitiesHandler(_reposistoryManagerMock.Object);


        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
        result.Value.Should().BeEquivalentTo(cities);
        result.Value.Count.Should().Be(5);
    }
}

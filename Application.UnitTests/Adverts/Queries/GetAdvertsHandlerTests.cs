using Application.Core.Adverts.Queries.GetAdverts;
using Contracts;
using FluentAssertions;
using Moq;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Application.UnitTests.Adverts.Queries;
public class GetAdvertsHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;
    public GetAdvertsHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenThereIsNoErrors()
    {
        var parameters = new AdvertParameters
        {

        };
        // Arrange
        var query = new GetAdvertsQuery(parameters);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetAdverts(parameters, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Pagination<MinimalInformationsAboutAdvertDto>());

        var handler = new GetAdvertsHandler(_reposistoryManagerMock.Object);


        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}

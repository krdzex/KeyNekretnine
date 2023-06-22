using Application.Core.Adverts.Queries.GetAdvertsCompare;
using Application.Core.Adverts.Queries.GetADvertsCompare;
using Contracts;
using FluentAssertions;
using Moq;
using Shared.DataTransferObjects.Advert;

namespace Application.UnitTests.Adverts.Queries;
public class GetAdvertsCompareHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;

    public GetAdvertsCompareHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenThereIsNoErrors()
    {
        var query = new GetAdvertsCompareQuery(2, 1);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetAdvertsCompare(2, 1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<CompareAdvertDto>());

        var handler = new GetAdvertsCompareHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
    }
}
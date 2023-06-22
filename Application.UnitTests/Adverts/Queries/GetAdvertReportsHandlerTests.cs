using Application.Core.Adverts.Queries.GetAdvertReports;
using Contracts;
using FluentAssertions;
using Moq;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Application.UnitTests.Adverts.Queries;
public class GetAdvertReportsHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;

    public GetAdvertReportsHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenThereIsNoErrors()
    {
        var parameters = new ReportParameters
        { };
        var query = new GetAdvertReportsQuery(parameters);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetAdvertReports(parameters, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Pagination<AdvertReportsDto>());

        var handler = new GetAdvertReportsHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
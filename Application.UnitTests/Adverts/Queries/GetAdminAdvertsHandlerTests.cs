using Application.Core.Adverts.Queries.GetAdminAdverts;
using Contracts;
using FluentAssertions;
using Moq;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Application.UnitTests.Adverts.Queries;
public class GetAdminAdvertsHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;

    public GetAdminAdvertsHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenThereIsNoErrors()
    {
        var parameters = new AdminAdvertParameters
        { };

        var query = new GetAdminAdvertsQuery(parameters);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetAdminAdverts(parameters, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Pagination<AdminTableAdvertDto>());

        var handler = new GetAdminAdvertsHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
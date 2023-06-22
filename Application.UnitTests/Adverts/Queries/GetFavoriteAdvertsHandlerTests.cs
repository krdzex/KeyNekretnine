using Application.Core.Adverts.Queries.GetFavoriteAdverts;
using Contracts;
using Entities.DomainErrors;
using FluentAssertions;
using Moq;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Application.UnitTests.Adverts.Queries;
public class GetFavoriteAdvertsHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;

    public GetFavoriteAdvertsHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenUserDoesntExist()
    {
        var parameters = new FavoriteAdvertsParameters
        { };

        var email = "test@test.com";
        var userId = "test";

        var query = new GetFavoriteAdvertsQuery(parameters, email);

        _reposistoryManagerMock.Setup(
            x => x.User.GetUserIdFromEmail(email, default))
            .ReturnsAsync((string)null!);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetFavoriteAdverts(parameters, userId, default))
            .ReturnsAsync(new Pagination<MinimalInformationsAboutAdvertDto>());

        var handler = new GetFavoriteAdvertsHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.UserNotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccesseResult_WhenUserExist()
    {
        var parameters = new FavoriteAdvertsParameters
        { };

        var email = "test@test.com";
        var userId = "test";

        var query = new GetFavoriteAdvertsQuery(parameters, email);

        _reposistoryManagerMock.Setup(
            x => x.User.GetUserIdFromEmail(email, default))
            .ReturnsAsync(userId);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetFavoriteAdverts(parameters, userId, default))
            .ReturnsAsync(new Pagination<MinimalInformationsAboutAdvertDto>());

        var handler = new GetFavoriteAdvertsHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
    }
}
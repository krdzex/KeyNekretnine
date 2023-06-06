using Application.Core.Adverts.Queries.GetIsFavorite;
using Contracts;
using Entities.DomainErrors;
using FluentAssertions;
using Moq;

namespace Application.UnitTests.Adverts.Queries;
public class GetIsAdvertFavoriteHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;
    public GetIsAdvertFavoriteHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenUserDoesntExists()
    {
        var advertId = 5;
        var email = "test@test.com";
        var userId = "test";
        // Arrange
        var query = new GetIsAdvertFavoriteQuery(advertId, email);

        _reposistoryManagerMock.Setup(
            x => x.User.GetUserIdFromEmail(email, default))
            .ReturnsAsync((string)null!);

        _reposistoryManagerMock.Setup(
            x => x.Advert.ChackIfAdvertIsFavorite(userId, advertId, default))
            .ReturnsAsync(true);

        var handler = new GetIsAdvertFavoriteHandler(_reposistoryManagerMock.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.UserNotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenUserExistAndAdvertIsFavorite()
    {
        var advertId = 5;
        var email = "test@test.com";
        var userId = "test";
        // Arrange
        var query = new GetIsAdvertFavoriteQuery(advertId, email);

        _reposistoryManagerMock.Setup(
            x => x.User.GetUserIdFromEmail(email, default))
            .ReturnsAsync(userId);

        _reposistoryManagerMock.Setup(
            x => x.Advert.ChackIfAdvertIsFavorite(userId, advertId, default))
            .ReturnsAsync(true);

        var handler = new GetIsAdvertFavoriteHandler(_reposistoryManagerMock.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenUserExistAndAdvertIsNotFavorite()
    {
        var advertId = 5;
        var email = "test@test.com";
        var userId = "test";
        // Arrange
        var query = new GetIsAdvertFavoriteQuery(advertId, email);

        _reposistoryManagerMock.Setup(
            x => x.User.GetUserIdFromEmail(email, default))
            .ReturnsAsync(userId);

        _reposistoryManagerMock.Setup(
            x => x.Advert.ChackIfAdvertIsFavorite(userId, advertId, default))
            .ReturnsAsync(false);

        var handler = new GetIsAdvertFavoriteHandler(_reposistoryManagerMock.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeFalse();
    }
}
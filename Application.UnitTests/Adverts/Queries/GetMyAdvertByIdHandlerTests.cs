using Application.Core.Adverts.Queries.GetMyAdvertById;
using Contracts;
using Entities.DomainErrors;
using FluentAssertions;
using Moq;
using Shared.DataTransferObjects.Advert;

namespace Application.UnitTests.Adverts.Queries;
public class GetMyAdvertByIdHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;

    public GetMyAdvertByIdHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenUserDoesntExists()
    {
        var advertId = 5;
        var email = "test@test.com";
        var userId = "test";

        var query = new GetMyAdvertByIdQuery(advertId, email);

        _reposistoryManagerMock.Setup(
            x => x.User.GetUserIdFromEmail(email, default))
            .ReturnsAsync((string)null!);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetMyAdvertById(advertId, userId, default))
            .ReturnsAsync(new MyAdvertDto { });

        var handler = new GetMyAdvertByIdHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.UserNotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenAdvertDoesntExists()
    {
        var advertId = 5;
        var email = "test@test.com";
        var userId = "test";

        var query = new GetMyAdvertByIdQuery(advertId, email);

        _reposistoryManagerMock.Setup(
            x => x.User.GetUserIdFromEmail(email, default))
            .ReturnsAsync(userId);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetMyAdvertById(advertId, userId, default))
            .ReturnsAsync((MyAdvertDto)null!);

        var handler = new GetMyAdvertByIdHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Advert.AdvertNotFound(advertId));
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenAdvertAndUserExist()
    {
        var advertId = 5;
        var email = "test@test.com";
        var userId = "test";

        var query = new GetMyAdvertByIdQuery(advertId, email);

        _reposistoryManagerMock.Setup(
            x => x.User.GetUserIdFromEmail(email, default))
            .ReturnsAsync(userId);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetMyAdvertById(advertId, userId, default))
            .ReturnsAsync(new MyAdvertDto { });

        var handler = new GetMyAdvertByIdHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Error.Should().NotBeNull();
    }
}
using Application.Core.Adverts.Queries.GetMyAdverts;
using Contracts;
using Entities.DomainErrors;
using FluentAssertions;
using Moq;
using Shared.CustomResponses;
using Shared.DataTransferObjects.Advert;
using Shared.RequestFeatures;

namespace Application.UnitTests.Adverts.Queries;
public class GetMyAdvertsHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;
    public GetMyAdvertsHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenUserDoesntExis()
    {
        var parameters = new MyAdvertsParameters
        {

        };
        var email = "test@test.com";
        var userId = "test";
        // Arrange

        var query = new GetMyAdvertsQuery(parameters, email);

        _reposistoryManagerMock.Setup(
            x => x.User.GetUserIdFromEmail(email, default))
            .ReturnsAsync((string)null!);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetMyAdverts(parameters, userId, default))
            .ReturnsAsync(new Pagination<MyAdvertsDto>());

        var handler = new GetMyAdvertsHandler(_reposistoryManagerMock.Object);


        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.UserNotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenUserExist()
    {
        var parameters = new MyAdvertsParameters
        {

        };
        var email = "test@test.com";
        var userId = "test";
        // Arrange

        var query = new GetMyAdvertsQuery(parameters, email);

        _reposistoryManagerMock.Setup(
            x => x.User.GetUserIdFromEmail(email, default))
            .ReturnsAsync(userId);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetMyAdverts(parameters, userId, default))
            .ReturnsAsync(new Pagination<MyAdvertsDto>());

        var handler = new GetMyAdvertsHandler(_reposistoryManagerMock.Object);


        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}

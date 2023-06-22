using Application.Core.Users.Queries.GetUserByQuery;
using Contracts;
using Entities.DomainErrors;
using FluentAssertions;
using Moq;
using Shared.DataTransferObjects.User;

namespace Application.UnitTests.Users.Queries;
public class GetUserByIdHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;

    public GetUserByIdHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenUserExists()
    {
        var advertId = "testId";

        var query = new GetUserByIdQuery(advertId);

        _reposistoryManagerMock.Setup(
            x => x.User.GetUserById(advertId, default))
            .ReturnsAsync(new UserDto { });

        var handler = new GetUserByIdHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenUserDoesntExists()
    {
        var advertId = "testId";

        var query = new GetUserByIdQuery(advertId);

        _reposistoryManagerMock.Setup(
            x => x.User.GetUserById(advertId, default))
            .ReturnsAsync((UserDto)null!);

        var handler = new GetUserByIdHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.UserNotFound);
    }
}
using Application.Core.Users.Queries.GetCurrentUserQuery;
using Contracts;
using Entities.DomainErrors;
using FluentAssertions;
using Moq;
using Shared.DataTransferObjects.User;
using System.Security.Claims;

namespace Application.UnitTests.Users.Queries;
public class GetCurrentUserHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;

    public GetCurrentUserHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenUserExists()
    {

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,"test"),
            new Claim(ClaimTypes.Email,"test@gmail.com"),
            new Claim(ClaimTypes.Role,"Admin")
        };

        var query = new GetCurrentUserQuery(claims);

        _reposistoryManagerMock.Setup(
            x => x.User.GetLoggedUserInformationsByEmail("test@gmail.com", default))
            .ReturnsAsync(new UserInformationDto { Email = "test@test.com" });

        var handler = new GetCurrentUserHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenUserDoesntExists()
    {

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,"test"),
            new Claim(ClaimTypes.Email,"test@gmail.com"),
            new Claim(ClaimTypes.Role,"Admin")
        };

        var query = new GetCurrentUserQuery(claims);

        _reposistoryManagerMock.Setup(
            x => x.User.GetLoggedUserInformationsByEmail("test", default))
            .ReturnsAsync((UserInformationDto)null!);

        var handler = new GetCurrentUserHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.UserNotFound);
    }
}
using Application.Core.Users.Queries.ConfirmEmailQuery;
using Application.Core.Users.Queries.ConfirmUserEmail;
using Contracts;
using Entities.DomainErrors;
using Entities.Models;
using FluentAssertions;
using Moq;

namespace Application.UnitTests.Users.Commands;
public class ConfirmUserEmailHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;

    public ConfirmUserEmailHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUserDoesntExists()
    {
        var email = "test@gmail.com";

        var command = new ConfirmUserEmailCommand("testtoken", email);

        _reposistoryManagerMock.Setup(
            x => x.User.GetUserByEmail("test@gmail.com"))
            .ReturnsAsync((User)null!);

        var handler = new ConfirmUserEmailHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.UserNotFound);
    }

    //[Fact]
    //public async Task Handle_Should_ReturnTrueAsSuccessResult_WhenUserExists()
    //{
    //    var token = "testtoken";
    //    var email = "test@gmail.com";

    //    // Arrange
    //    var command = new ConfirmUserEmailCommand(token, email);

    //    _reposistoryManagerMock.Setup(
    //        x => x.User.GetUserByEmail("test@gmail.com"))
    //        .ReturnsAsync(new User { });

    //    _reposistoryManagerMock.Setup(
    //        x => x.User.ConfrimUserEmail(new User { }, token))
    //        .Returns(Task.FromResult(IdentityResult.Success));

    //    var handler = new ConfirmUserEmailHandler(_reposistoryManagerMock.Object);

    //    // Act
    //    var result = await handler.Handle(command, default);

    //    // Assert
    //    result.IsSuccess.Should().BeTrue();
    //    result.Value.Should().BeTrue();
    //}
}
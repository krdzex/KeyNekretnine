using Application.Core.Users.Queries.GetUsersQuery;
using Contracts;
using FluentAssertions;
using Moq;
using Shared.CustomResponses;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;

namespace Application.UnitTests.Users.Queries;
public class GetUsersHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;

    public GetUsersHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenNoErrors()
    {
        var parameters = new UserParameters
        { };

        var query = new GetUsersQuery(parameters);

        _reposistoryManagerMock.Setup(
            x => x.User.GetUsers(parameters, default))
            .ReturnsAsync(new Pagination<UserForListDto>());

        var handler = new GetUsersHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
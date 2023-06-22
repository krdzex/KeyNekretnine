using Application.Core.AdvertPurposes.Queries.GetAdvertPurposes;
using Contracts;
using FluentAssertions;
using Moq;
using Shared.DataTransferObjects.AdvertPurpose;

namespace Application.UnitTests.AdvertPurposes.Queries;
public class GetAdvertPurposesHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;

    public GetAdvertPurposesHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenThereIsNoErrors()
    {
        var query = new GetAdvertPurposesQuery();

        var advertPurposes = new List<AdvertPurposeDto>
        {
            new AdvertPurposeDto { Id = 1, Name = { En = "test", Sr = "test" } },
            new AdvertPurposeDto { Id = 2, Name = { En = "test", Sr = "test" } },
            new AdvertPurposeDto { Id = 3, Name = { En = "test", Sr = "test" } }
        };

        _reposistoryManagerMock.Setup(
            x => x.AdvertPurpose.GetAdvertPurposes(It.IsAny<CancellationToken>()))
            .ReturnsAsync(advertPurposes);

        var handler = new GetAdvertPurposesHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
        result.Value.Should().BeEquivalentTo(advertPurposes);
    }
}
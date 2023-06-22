using Application.Core.Adverts.Queries.GetAdminAdvert;
using Contracts;
using Entities.DomainErrors;
using FluentAssertions;
using Moq;
using Shared.DataTransferObjects.Advert;

namespace Application.UnitTests.Adverts.Queries;
public class GetAdminAdvertByIdHandlerTests
{
    private readonly Mock<IRepositoryManager> _reposistoryManagerMock;

    public GetAdminAdvertByIdHandlerTests()
    {
        _reposistoryManagerMock = new Mock<IRepositoryManager> { DefaultValue = DefaultValue.Mock };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenAdvertExists()
    {
        var advertId = 1;

        var advert = new AdminAllInformationsAboutAdvertDto
        {
            Id = advertId,
        };

        var query = new GetAdminAdvertByIdQuery(advertId);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetAdminAdvertById(advertId, default))
            .ReturnsAsync(advert);

        var handler = new GetAdminAdvertByIdHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(advert);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenAdvertDoesNotExist()
    {
        var query = new GetAdminAdvertByIdQuery(-1);

        _reposistoryManagerMock.Setup(
            x => x.Advert.GetAdminAdvertById(-1, default))
            .ReturnsAsync((AdminAllInformationsAboutAdvertDto)null!);

        var handler = new GetAdminAdvertByIdHandler(_reposistoryManagerMock.Object);

        var result = await handler.Handle(query, default);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Advert.AdminAdvertNotFound(-1));
    }
}
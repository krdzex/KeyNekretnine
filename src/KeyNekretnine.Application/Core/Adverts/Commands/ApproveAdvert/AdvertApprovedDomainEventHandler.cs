using Dapper;
using KeyNekretnine.Application.Abstraction.Data;
using KeyNekretnine.Application.Abstraction.Email;
using KeyNekretnine.Domain.Adverts.Events;
using MediatR;

namespace KeyNekretnine.Application.Core.Adverts.Commands.ApproveAdvert;
internal sealed class AdvertApprovedDomainEventHandler : INotificationHandler<AdvertApprovedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public AdvertApprovedDomainEventHandler(
        IEmailService emailService,
        ISqlConnectionFactory sqlConnectionFactory)
    {
        _emailService = emailService;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task Handle(AdvertApprovedDomainEvent notification, CancellationToken cancellationToken)
    {

        using var connection = _sqlConnectionFactory.CreateConnection();

        var sql = $"""                
            SELECT 
                a.id,
                a.price,
                a.floor_space AS floorSpace,
            	a.no_of_bedrooms AS noOfBedrooms,
            	a.no_of_bathrooms AS noOfBathrooms,
                a.cover_image_url AS coverImageUrl,
                a.location_address AS address,
                CONCAT(c.name, ', ', n.name) AS cityAndNeighborhood,
                u.email as creatorEmail,
                a.reference_id as referenceId
            FROM adverts AS a
            INNER JOIN neighborhoods AS n ON a.neighborhood_id = n.id
            INNER JOIN cities AS c ON n.city_id = c.id
            LEFT JOIN asp_net_users AS u ON u.id = a.user_id
            WHERE a.id = @advertId
            """
        ;

        var advertInfo = await connection.QueryFirstOrDefaultAsync<ApproveSendEmailInfo>(sql, new { notification.AdvertId });

        if (advertInfo is null)
        {
            return;
        }

        var sendStatus = advertInfo.Purpose switch
        {
            1 => await _emailService.SendRentApproveAdvertEmail(advertInfo, cancellationToken),
            2 => await _emailService.SendSaleApproveAdvertEmail(advertInfo, cancellationToken),
            _ => await _emailService.SendDailyRentApproveAdvertEmail(advertInfo, cancellationToken)
        };

        if (!sendStatus)
        {
            return;
        }

        await Task.CompletedTask;
    }
}
namespace KeyNekretnine.Application.Abstraction.Authentication;
public interface IUserContext
{
    string UserId { get; }
    Guid? AgencyId { get; }
}

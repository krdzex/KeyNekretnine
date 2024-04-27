using KeyNekretnine.Application.Abstraction.Messaging;

namespace KeyNekretnine.Application.Core.Users.Queries.GetAboutUser;
public sealed record GetAboutUserQuery() : IQuery<AboutUserResponse>;
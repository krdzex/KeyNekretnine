using KeyNekretnine.Domain.Abstraction;
using MediatR;

namespace KeyNekretnine.Application.Abstraction.Messaging;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
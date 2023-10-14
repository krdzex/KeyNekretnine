using KeyNekretnine.Domain.Abstraction;
using MediatR;

namespace KeyNekretnine.Application.Abstraction.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
using MediatR;
using Shared.Error;

namespace Application.Abstraction.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{ }
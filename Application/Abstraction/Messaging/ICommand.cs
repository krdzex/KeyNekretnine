using KeyNekretnine.Domain.Abstraction;
using MediatR;

namespace KeyNekretnine.Application.Abstraction.Messaging;
public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TReponse> : IRequest<Result<TReponse>>, IBaseCommand
{
}

public interface IBaseCommand
{
}
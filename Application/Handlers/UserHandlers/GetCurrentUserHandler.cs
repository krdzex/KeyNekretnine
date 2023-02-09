using Application.Queries.UserQueries;
using Contracts;
using MediatR;
using Shared.DataTransferObjects.User;

namespace Application.Handlers.UserHandlers
{
    internal sealed class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, UserInformationDto>
    {
        private readonly IRepositoryManager _repository;

        public GetCurrentUserHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }
        public async Task<UserInformationDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.User.GetLoggedUserInformations(request.UserClaims);

            return user;
        }
    }
}

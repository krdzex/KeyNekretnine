using Application.Queries;
using MediatR;
using Service.Contracts;
using Shared.DataTransferObjects.User;

namespace Application.Handlers
{
    internal sealed class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, UserInformationDto>
    {
        private readonly IServiceManager _service;

        public GetCurrentUserHandler(IServiceManager service)
        {
            _service = service;
        }
        public async Task<UserInformationDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _service.UserService.GetCurrentUserInformations(request.Email);

            return user;
        }
    }
}

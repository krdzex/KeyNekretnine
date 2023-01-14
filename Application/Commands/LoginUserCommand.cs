﻿using MediatR;
using Shared.DataTransferObjects.Auth;
using Shared.RequestFeatures;

namespace Application.Commands;
public sealed record LoginUserCommand(UserForAuthenticationDto LoginUser) : IRequest<TokenRequest>;


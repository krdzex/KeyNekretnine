﻿using KeyNekretnine.Application.Abstraction.Messaging;
using MediatR;
using Shared.DataTransferObjects.Agency;

namespace KeyNekretnine.Application.Core.Agents.Commands.UpdateAgent;
public sealed record UpdateAgentCommand(UpdateAgentDto Agent, int AgentId, string Email) : ICommand<Unit>;

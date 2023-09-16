﻿using Shared.DataTransferObjects.Agency;
using Shared.DataTransferObjects.PhoneNumber;

namespace Contracts;
public interface IPhoneNumberRepository
{
    Task<IEnumerable<PhoneNumberDto>> GetAll(CancellationToken cancellationToken);
    Task<string> MakeNumber(CreateNumberDto numberDto, CancellationToken cancellationToken);
}
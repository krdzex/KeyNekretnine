﻿namespace Shared.DataTransferObjects.Auth;
public class GoogleLoginDto
{
    public string? Provider { get; set; }
    public string? IdToken { get; set; }
}

﻿namespace Shared.DataTransferObjects.User;
public class BanUsersDto
{
    public IEnumerable<string> Emails { get; set; }
    public int Days { get; set; }
}

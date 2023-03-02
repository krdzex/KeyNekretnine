﻿namespace Shared.DataTransferObjects.AdvertType;

public class ShowAdvertTypeDto
{
    public int Id { get; set; }
    private string Name_Sr { get; set; }
    private string Name_En { get; set; }
    public DifferentLanguagesDto Name { get { return new DifferentLanguagesDto { Sr = Name_Sr, En = Name_En }; } }
}
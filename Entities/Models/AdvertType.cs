﻿namespace Entities.Models;
public class AdvertType : EntityBase
{
    public string Name { get; set; }
    public string NameEn { get; set; }
    public List<Advert> Adverts { get; set; }
}

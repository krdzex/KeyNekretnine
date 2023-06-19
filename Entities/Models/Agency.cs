﻿namespace Entities.Models;
public class Agency : EntityBase
{
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Location { get; set; }
    public string Email { get; set; }
    public string WebsiteUrl { get; set; }
    public TimeOnly WorkStartTime { get; set; }
    public TimeOnly WorkEndTime { get; set; }
    public string TwitterUrl { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string LinkedlnUrl { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public IEnumerable<AgencyLanguage> AgencyLanguage { get; set; }
    public User User { get; set; }
    public string UserId { get; set; }

    public IEnumerable<ImaginaryAgent> ImaginaryAgents { get; set; }
}
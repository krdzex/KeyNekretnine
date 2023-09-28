﻿using KeyNekretnine.Domain.Abstraction;
using KeyNekretnine.Domain.Adverts.Events;
using KeyNekretnine.Domain.Agencies;
using KeyNekretnine.Domain.Shared;

namespace KeyNekretnine.Domain.Adverts;
public class Advert : Entity
{
    public Advert()
    : base()
    {
    }

    //private Advert()
    //{
    //}

    public double Price { get; private set; }

    public AdvertDescription Description { get; private set; }

    public int NoOfBedrooms { get; private set; }

    public double FloorSpace { get; private set; }

    public int NoOfBathrooms { get; private set; }

    public bool HasTerrace { get; private set; }

    public bool HasGarage { get; private set; }

    public bool IsFurnished { get; private set; }

    public bool HasWifi { get; private set; }

    public bool HasElevator { get; private set; }

    public int BuildingFloor { get; private set; }

    public bool IsUrgent { get; private set; }

    public bool IsUnderConstruction { get; private set; }

    public bool IsPremium { get; private set; }

    public int? YearOfBuildingCreated { get; private set; }

    public AdvertStatus Status { get; private set; }

    public AdvertPurpose Purpose { get; private set; }

    public AdvertType Type { get; private set; }

    public string? UserId { get; private set; }

    public Guid? AgentId { get; private set; }

    public int NeighborhoodId { get; private set; }

    public ImageUrl CoverImageUrl { get; private set; }

    public Location Location { get; private set; }

    public DateTime CreatedOnDate { get; private set; }

    public DateTime? ApprovedOnDate { get; private set; }

    public DateTime? RejectedOnDate { get; private set; }

    public DateTime? UpdatedOnDate { get; private set; }

    //public List<TemporeryImageData> TemporeryImageDatas { get; set; }
    public string ReferenceId { get; private set; }

    public Result Approve(DateTime approvedDate)
    {
        if (Status == AdvertStatus.Accepted)
        {
            return Result.Failure(AdvertErrors.AlreadyAccepted);
        }

        Status = AdvertStatus.Accepted;
        ApprovedOnDate = approvedDate;

        RaiseDomainEvent(new AdvertApprovedDomainEvent(Id, UserId));
        return Result.Success();
    }
}
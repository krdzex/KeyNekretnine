﻿namespace KeyNekretnine.Application.Core.Adverts.Commands.UpdateAdvertBasic;
public record UpdateAdvertBasicRequest(
    string DescriptionSr,
    string DescriptionEn,
    double? Price,
    double FloorSpace,
    int NoOfBedrooms,
    int NoOfBathrooms,
    int Type,
    int Purpose,
    int? YearOfBuildingCreated,
    int BuildingFloor,
    bool HasGarage,
    bool IsFurnished,
    bool HasWifi,
    bool HasElevator,
    bool IsUrgent,
    bool HasTerrace,
    bool IsUnderConstruction);
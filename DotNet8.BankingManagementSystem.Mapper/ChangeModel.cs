﻿using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using DotNet8.BankingManagementSystem.Models.State;
using DotNet8.BankingManagementSystem.Models.TownShip;

namespace DotNet8.BankingManagementSystem.Mapper;

public static class ChangeModel
{
    public static StateModel Change(this TblPlaceState item)
    {
        return new StateModel()
        {
            StateCode = item.StateCode,
            StateName = item.StateName,
            StateId = item.StateId,
        };
    }
    public static TownShipModel Change(this TblPlaceTownship item)
    {
        return new TownShipModel()
        {
            TownshipId = item.TownshipId,
            TownshipCode = item.TownshipCode,
            TownshipName = item.TownshipName,
            StateCode = item.StateCode,
        };
    }
}
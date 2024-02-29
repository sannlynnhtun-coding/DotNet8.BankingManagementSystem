using DotNet8.BankingManagementSystem.BackendApi.Models;
using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using DotNet8.BankingManagementSystem.Models.State;
using DotNet8.BankingManagementSystem.Models.Township;
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

    #region State

    public static TblPlaceState Change(this StateRequestModel item)
    {
        var model = new TblPlaceState()
        {
            StateCode = item.StateCode,
            StateName = item.StateName,
        };
        return model;
    }
    public static TownshipModel Change(this TblPlaceTownship item)

    #endregion

    public static TownShipModel Change(this TblPlaceTownship item)
    {
        return new TownshipModel()
        {
            TownshipId = item.TownshipId,
            TownshipCode = item.TownshipCode,
            TownshipName = item.TownshipName,
            StateCode = item.StateCode,
        };
    }
}
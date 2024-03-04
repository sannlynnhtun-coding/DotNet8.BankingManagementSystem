using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using DotNet8.BankingManagementSystem.Models.State;
using DotNet8.BankingManagementSystem.Models.Township;
using DotNet8.BankingManagementSystem.Models.Users;

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
    #endregion

    #region Township
    public static TownshipModel Change(this TblPlaceTownship item)
    {
        return new TownshipModel()
        {
            TownshipId = item.TownshipId,
            TownshipCode = item.TownshipCode,
            TownshipName = item.TownshipName,
            StateCode = item.StateCode,
        };
    }

    #endregion

    #region Users
    public static UserModel Change(this TblUser item)
    {
        var model = new UserModel()
        {
            UserName = item.UserName,
            FullName = item.FullName,
            Email = item.Email,
            Password = item.Password,
            Address = item.Address,
            MobileNo = item.MobileNo,
            Nrc = item.Nrc,
        };
        return model;
    }
    public static TblUser Change(this UserRequestModel item)
    {
        return new TblUser()
        {
            UserName = item.UserName,
            FullName = item.FullName,
            Email = item.Email,
            Password = item.Password,
            Address = item.Address,
            MobileNo = item.MobileNo,
            Nrc = item.Nrc,
        };
    }
    #endregion

}
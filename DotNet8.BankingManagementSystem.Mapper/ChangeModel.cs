using System.Data;
using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using DotNet8.BankingManagementSystem.Models.Account;
using DotNet8.BankingManagementSystem.Models.AdminUser;
using DotNet8.BankingManagementSystem.Models.State;
using DotNet8.BankingManagementSystem.Models.TownShip;
using DotNet8.BankingManagementSystem.Models.TransactionHistory;
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

    public static TblPlaceTownship Change(this TownshipRequestModel item)
    {
        var model = new TblPlaceTownship()
        {
            TownshipCode = item.TownshipCode,
            TownshipName = item.TownshipName,
            StateCode = item.StateCode
        };
        return model;
    }

    #endregion

    #region Users

    public static UserModel Change(this TblUser item)
    {
        var model = new UserModel()
        {
            UserId = item.UserId,
            UserCode = item.UserCode,
            UserName = item.UserName,
            CustomerId = item.CustomerId,
            FullName = item.FullName,
            Email = item.Email,
            Address = item.Address,
            MobileNo = item.MobileNo,
            Nrc = item.Nrc,
            StateCode = item.StateCode,
            TownshipCode = item.TownshipCode,
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
            Address = item.Address,
            MobileNo = item.MobileNo,
            Nrc = item.Nrc,
            StateCode = item.StateCode,
            TownshipCode = item.TownshipCode
        };
    }

    #endregion

    #region Account

    public static TblAccount Change(this AccountRequestModel item)
    {
        return new TblAccount()
        {
            CustomerCode = item.CustomerCode,
            CustomerName = item.CustomerName,
            Balance = item.Balance
        };
    }

    public static AccountModel Change(this TblAccount item)
    {
        return new AccountModel()
        {
            CustomerCode = item.CustomerCode,
            CustomerName = item.CustomerName,
            AccountNo = item.AccountNo,
            Balance = item.Balance,
            AccountId = item.AccountId
        };
    }

    #endregion

    #region AdminUser

    public static AdminUserModel Change(this TblAdminUser item)
    {
        return new AdminUserModel()
        {
            AdminUserId = item.AdminUserId,
            AdminUserCode = item.AdminUserCode,
            AdminUserName = item.AdminUserName,
            MobileNo = item.MobileNo,
            UserRoleCode = item.UserRoleCode
        };
    }

    public static TblAdminUser Change(this AdminUserRequestModel item)
    {
        var model = new TblAdminUser
        {
            AdminUserCode = item.AdminUserCode,
            AdminUserName = item.AdminUserName,
            MobileNo = item.MobileNo,
            UserRoleCode = item.UserRoleCode
        };
        return model;
    }

    #endregion

    #region TransactionHistory

    public static TransactionHistoryModel Change(this TblTransactionHistory item)
    {
        return new TransactionHistoryModel()
        {
            Amount = item.Amount,
            TransactionDate = item.TransactionDate,
            TransactionType = item.TransactionType,
            AdminUserCode = item.AdminUserCode,
            FromAccountNo = item.FromAccountNo,
            ToAccountNo = item.ToAccountNo,
        };
    }

    #endregion
}
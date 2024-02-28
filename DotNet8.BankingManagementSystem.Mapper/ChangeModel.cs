using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using DotNet8.BankingManagementSystem.Models.State;

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
}
using DotNet8.BankingManagementSystem.Models.Users;
using Refit;


namespace DotNet8.BankingManagementSystem.App.Api
{
    public interface IUserApi
    {
        [Get("/api/state/{pageNo}/{pageSize}")]
        Task<UserListResponseModel> GetStates(int pageNo, int pageSize);
    }
}

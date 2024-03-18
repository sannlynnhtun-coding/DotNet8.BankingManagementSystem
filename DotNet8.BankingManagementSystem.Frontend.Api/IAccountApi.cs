using DotNet8.BankingManagementSystem.Models.Account;
using DotNet8.BankingManagementSystem.Models.State;
using DotNet8.BankingManagementSystem.Models.Users;
using Refit;

namespace DotNet8.BankingManagementSystem.Frontend.Api;

public interface IAccountApi
{
    // [Get("/api/account")]
    // Task<AccountListResponseModel> GetAccounts();

    [Get("/api/account/{pageNo}/{pageSize}")]
    Task<AccountListResponseModel> GetAccountList(int pageNo, int pageSize);

    [Get("/api/account/{accountNo}")]
    Task<AccountResponseModel> GetAccount(string accountNo);

    [Post("/api/account")]
    Task<AccountResponseModel> CreateAccount(AccountRequestModel requestModel);

    [Put("/api/account/{accountNo}")]
    Task<AccountResponseModel> UpdateAccount(string accountNo, AccountRequestModel requestModel);

    [Delete("/api/account/{accountNo}")]
    Task<AccountResponseModel> DeleteAccount(string accountNo);
    
    [Get("/api/account/UserCode/{userCode}")]
    Task<UserListResponseModel> GetUserNameByUserCode(string userCode);
}
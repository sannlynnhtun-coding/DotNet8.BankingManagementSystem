using Blazored.LocalStorage;
using DotNet8.BankingManagementSystem.Models.Account;
using DotNet8.BankingManagementSystem.Models.Users;

namespace DotNet8.BankingManagementSystem.Database.Frontend;

public class LocalStorageService
{
    private readonly ILocalStorageService _localStorageService;

    public LocalStorageService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<List<AccountModel>> GetAccountList(string keyName)
    {
        return await _localStorageService.GetItemAsync<List<AccountModel>>(keyName);
    }
    public async Task<List<UserModel>> GetUserList(string keyName)
    {
        return await _localStorageService.GetItemAsync<List<UserModel>>(keyName);
    }

    public async Task SetAccount(List<AccountModel> lst)
    {
        await _localStorageService.SetItemAsync(EnumService.Tbl_Account.GetKeyName(), lst);
    }
    
    public async Task SetUser(List<UserModel> lst)
    {
        await _localStorageService.SetItemAsync(EnumService.Tbl_Account.GetKeyName(), lst);
    }
}
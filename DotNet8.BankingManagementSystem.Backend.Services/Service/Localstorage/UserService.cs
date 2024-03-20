using Blazored.LocalStorage;

namespace DotNet8.BankingManagementSystem.Backend.Services.Service.Localstorage;

public class UserService
{
    private readonly ILocalStorageService _localStorageService;

    public UserService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task CreateUser(UserModel requestModel)
    {
        var lst = await _localStorageService.GetItemAsync<List<UserModel>>("Tbl_User");
        lst ??= new();
        lst.Add(requestModel);
        await _localStorageService.SetItemAsync("Tbl_User", lst);
    }
}
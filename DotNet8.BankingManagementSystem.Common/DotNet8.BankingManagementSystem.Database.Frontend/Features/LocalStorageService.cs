using System.Text.Json;
using Blazored.LocalStorage;

namespace DotNet8.BankingManagementSystem.Database.Frontend.Features;

public class LocalStorageService(ILocalStorageService localStorageService)
{
    public async Task SetList<T>(string keyName, List<T> list)
    {
        await localStorageService.SetItemAsync(keyName, list);
    }

    public async Task<List<T>> GetList<T>(string keyName)
    {
        var result = await localStorageService.GetItemAsync<List<T>>(keyName);
        return result!;
    }
}
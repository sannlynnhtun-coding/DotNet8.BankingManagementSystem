

namespace DotNet8.BankingManagementSystem.Frontend.Api.Services;

public class LocalStorageService
{
    private readonly IndexedDbService _indexedDbService;

    public LocalStorageService(IndexedDbService indexedDbService)
    {
        _indexedDbService = indexedDbService;
    }

    public async Task<List<T>> GetList<T>(string keyName)
    {
        return await _indexedDbService.GetObjectsAsync<T>(keyName);
    }

    public async Task SetList<T>(string keyName, List<T> lst)
    {
        await _indexedDbService.ClearStoreAsync(keyName);
        await _indexedDbService.AddObjectsAsync(keyName, lst);
    }
}
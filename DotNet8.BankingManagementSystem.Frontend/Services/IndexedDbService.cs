using Microsoft.JSInterop;

namespace DotNet8.BankingManagementSystem.Frontend.Services;

public class IndexedDbService
{
    private readonly IJSRuntime _jsRuntime;
    private const string JsModulePath = "./js/indexeddb.js";
    private const string DbName = "BankingDb";
    private const int DbVersion = 3;

    public IndexedDbService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    private async Task<IJSObjectReference> GetModule()
    {
        return await _jsRuntime.InvokeAsync<IJSObjectReference>("import", JsModulePath);
    }

    public async Task InitializeAsync(string[] stores)
    {
        var module = await GetModule();
        await module.InvokeVoidAsync("initDb", DbName, DbVersion, stores);
    }

    public async Task AddObjectAsync<T>(string storeName, T obj)
    {
        var module = await GetModule();
        await module.InvokeVoidAsync("addObject", DbName, storeName, obj);
    }

    public async Task AddObjectsAsync<T>(string storeName, List<T> objects)
    {
        var module = await GetModule();
        await module.InvokeVoidAsync("addObjects", DbName, storeName, objects);
    }

    public async Task<List<T>> GetObjectsAsync<T>(string storeName)
    {
        var module = await GetModule();
        return await module.InvokeAsync<List<T>>("getObjects", DbName, storeName);
    }

    public async Task ClearStoreAsync(string storeName)
    {
        var module = await GetModule();
        await module.InvokeVoidAsync("clearStore", DbName, storeName);
    }
}

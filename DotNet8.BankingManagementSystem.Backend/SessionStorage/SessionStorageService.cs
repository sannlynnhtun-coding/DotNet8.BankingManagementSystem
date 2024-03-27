using System.Text.Json;

public class SessionStorageService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionStorageService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    // public async Task<List<AccountModel>> GetAccountList(string keyName)
    // {
    //     var lst = _httpContextAccessor.HttpContext.Session.GetString(keyName);
    //     if (lst != null)
    //         return JsonSerializer.Deserialize<List<AccountModel>>(lst);
    //     return null;
    // }
    //
    // public async Task<List<UserModel>> GetUserList(string keyName)
    // {
    //     var lst = _httpContextAccessor.HttpContext.Session.GetString(keyName);
    //     if (lst != null)
    //         return JsonSerializer.Deserialize<List<UserModel>>(lst);
    //     return null;
    // }
    
    public async Task SetList<T>(string keyName, List<T> list)
    {
        var jsonStr = JsonSerializer.Serialize(list);
        _httpContextAccessor.HttpContext.Session.SetString(keyName, jsonStr);
        await _httpContextAccessor.HttpContext.Session.CommitAsync();
    }

    public async Task<List<T>> GetList<T>(string keyName)
    {
        var jsonStr = _httpContextAccessor.HttpContext.Session.GetString(keyName);
        if (jsonStr != null)
            return JsonSerializer.Deserialize<List<T>>(jsonStr);
        return null;
    }
}
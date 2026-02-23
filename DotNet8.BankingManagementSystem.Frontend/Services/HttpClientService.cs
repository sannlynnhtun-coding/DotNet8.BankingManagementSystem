namespace DotNet8.BankingManagementSystem.Frontend;

public class HttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<T>> GetAsync<T>(string url)
    {
        var result = await _httpClient.GetFromJsonAsync<T[]>(url);
        return result!.ToList();
    }
}

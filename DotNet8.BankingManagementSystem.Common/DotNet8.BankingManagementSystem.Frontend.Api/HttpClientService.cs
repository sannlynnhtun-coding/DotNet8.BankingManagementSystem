using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.BankingManagementSystem.Frontend.Api
{
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
}

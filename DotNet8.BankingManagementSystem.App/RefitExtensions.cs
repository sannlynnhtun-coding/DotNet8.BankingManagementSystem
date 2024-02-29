using Refit;

namespace DotNet8.BankingManagementSystem.App;

public static class RefitExtensions
{
    public static IHttpClientBuilder AddRefitService<T>(this IServiceCollection services, IConfiguration configuration) where T : class
    {
        var httpClientBuilder = services.AddRefitClient<T>()
            //.ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("ApiUrl").Value!));
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7162"));
        return httpClientBuilder;
    }
}
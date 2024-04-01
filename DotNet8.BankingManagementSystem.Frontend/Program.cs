using DotNet8.BankingManagementSystem.Frontend.Api.Features;
using DotNet8.BankingManagementSystem.Frontend.Api.Features.Account;
using DotNet8.BankingManagementSystem.Frontend.Api.Features.AdminUser;
using DotNet8.BankingManagementSystem.Frontend.Api.Features.State;
using DotNet8.BankingManagementSystem.Frontend.Api.Features.Township;
using DotNet8.BankingManagementSystem.Frontend.Api.Features.Transaction;
using DotNet8.BankingManagementSystem.Frontend.Api.Features.User;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

string apiType = "Api"; // Api or LocalStorage
switch (apiType)
{
    case "Api":
        builder.Services.AddRefitService<IStateApi>(builder.Configuration);
        builder.Services.AddRefitService<IUserApi>(builder.Configuration);
        builder.Services.AddRefitService<ITownshipApi>(builder.Configuration);
        builder.Services.AddRefitService<IAccountApi>(builder.Configuration);
        builder.Services.AddRefitService<IAdminUser>(builder.Configuration);
        builder.Services.AddRefitService<ITransactionApi>(builder.Configuration);
        break;
    case "LocalStorage":
        builder.Services.AddBlazoredLocalStorage(config =>
        {
            config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
            config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
            config.JsonSerializerOptions.WriteIndented = false;
        });

        builder.Services.AddScoped<AccountService>();
        builder.Services.AddScoped<TransactionService>();
        builder.Services.AddScoped<UserService>();
        break;
    default:
        throw new Exception("Invalid Api Type. Choose Api or LocalStorage.");
}

builder.Services.AddSingleton<Config>();
builder.Services.AddScoped<ApiService>();

builder.Services.AddScoped<InjectService>();

await builder.Build().RunAsync();

using DotNet8.BankingManagementSystem.App;
using DotNet8.BankingManagementSystem.App.Api;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddRefitService<IStateApi>(builder.Configuration);
builder.Services.AddRefitService<IUserApi>(builder.Configuration);

await builder.Build().RunAsync();

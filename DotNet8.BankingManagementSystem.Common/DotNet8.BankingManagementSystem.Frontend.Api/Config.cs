using DotNet8.BankingManagementSystem.Frontend.Api.Services;

namespace DotNet8.BankingManagementSystem.Frontend
{
    public class Config
    {
        public EnumApiType EnumApiType { get; set; } = EnumApiType.LocalStorage;
    }
}

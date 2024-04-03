namespace DotNet8.BankingManagementSystem.Frontend.Api.Features.AdminUser;

public class AdminUserService
{
    private readonly LocalStorageService _localStorageService;

    public AdminUserService(LocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }
}
using DotNet8.BankingManagementSystem.Models.AdminUser;
using Refit;

namespace DotNet8.BankingManagementSystem.Frontend.Api.Features.AdminUser;

public interface IAdminUser
{
    [Get("/api/AdminUser")]
    Task<AdminUserListResponseModel> GetAdminUser();

    [Get("/api/AdminUser/{AdminUserCode}")]
    Task<AdminUserResponseModel> GetAdminUser(string AdminUserCode);

    [Get("/api/AdminUser/{pageNo}/{pageSize}")]
    Task<AdminUserListResponseModel> GetAdminUserList(int pageNo, int pageSize);

    [Post("/api/AdminUser")]
    Task<AdminUserResponseModel> CreateAdminUser(AdminUserRequestModel requestModel);

    [Delete("/api/AdminUser/{AdminUserCode}")]
    Task<AdminUserResponseModel> DeleteAdminUser(string AdminUserCode);

    [Put("/api/AdminUser/{AdminUserCode}")]
    Task<AdminUserResponseModel> UpdateAdminUser(string AdminUserCode, AdminUserRequestModel requestModel);
}
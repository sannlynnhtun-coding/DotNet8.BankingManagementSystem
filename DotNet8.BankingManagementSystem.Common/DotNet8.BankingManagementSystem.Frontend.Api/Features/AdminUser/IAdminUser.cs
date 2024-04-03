namespace DotNet8.BankingManagementSystem.Frontend.Api.Features.AdminUser;

public interface IAdminUser
{
    [Get("/api/AdminUser")]
    Task<AdminUserListResponseModel> GetAdminUsers();

    [Get("/api/AdminUser/{pageNo}/{pageSize}")]
    Task<AdminUserListResponseModel> GetAdminUserList(int pageNo, int pageSize);

    [Get("/api/AdminUser/{AdminUserCode}")]
    Task<AdminUserResponseModel> GetAdminUserByCode(string adminUserCode);

    [Post("/api/AdminUser")]
    Task<AdminUserResponseModel> CreateAdminUser(AdminUserRequestModel requestModel);

    [Put("/api/AdminUser/{AdminUserCode}")]
    Task<AdminUserResponseModel> UpdateAdminUser(AdminUserRequestModel requestModel);

    [Delete("/api/AdminUser/{AdminUserCode}")]
    Task<AdminUserResponseModel> DeleteAdminUser(string adminUserCode);
}
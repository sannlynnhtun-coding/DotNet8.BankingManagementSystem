using DotNet8.BankingManagementSystem.Models.AdminUser;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.BankingManagementSystem.Frontend.Api;

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
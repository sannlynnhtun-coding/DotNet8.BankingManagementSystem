using DotNet8.BankingManagementSystem.Models.AdminUser;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.BankingManagementSystem.App.Api
{
    public interface IAdminUser
    {
        [Get("/api/AdminUser/{AdminUserCode}")]
        Task<AdminUserResponseModel> GetAdminUser(string AdminUserCode);

        [Get("/api/AdminUser/{pageNo}/{pageSize}")]
        Task<AdminUserListResponseModel> GetAdminUsers();
    }
}

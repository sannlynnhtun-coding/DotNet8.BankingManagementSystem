using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.BankingManagementSystem.Models.AdminUser
{
    public class AdminUserRequestModel
    {
        public string AdminUserCode { get; set; } = null!;

        public string AdminUserName { get; set; } = null!;

        public string MobileNo { get; set; } = null!;

        public string UserRoleCode { get; set; } = null!;
    }
}

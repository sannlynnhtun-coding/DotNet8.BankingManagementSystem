using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.BankingManagementSystem.Models.AdminUser;

public class AdminUserModel
{
    public int AdminUserId { get; set; }

    public string AdminUserCode { get; set; }

    public string AdminUserName { get; set; }

    public string MobileNo { get; set; }

    public string UserRoleCode { get; set; }
}
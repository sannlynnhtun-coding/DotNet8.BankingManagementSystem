using System;
using System.Collections.Generic;

namespace DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;

public partial class TblAdminUser
{
    public int AdminUserId { get; set; }

    public string AdminUserCode { get; set; } = null!;

    public string AdminUserName { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public string UserRoleCode { get; set; } = null!;
}

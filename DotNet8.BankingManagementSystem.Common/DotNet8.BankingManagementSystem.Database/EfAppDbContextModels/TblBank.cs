using System;
using System.Collections.Generic;

namespace DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;

public partial class TblBank
{
    public int BankId { get; set; }

    public string BankCode { get; set; } = null!;

    public string BankName { get; set; } = null!;
}

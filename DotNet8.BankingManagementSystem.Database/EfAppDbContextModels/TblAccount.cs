using System;
using System.Collections.Generic;

namespace DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;

public partial class TblAccount
{
    public int AccountId { get; set; }

    public string AccountNo { get; set; } = null!;

    public string AccountType { get; set; } = null!;

    public string CustomerCode { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Balance { get; set; }
}

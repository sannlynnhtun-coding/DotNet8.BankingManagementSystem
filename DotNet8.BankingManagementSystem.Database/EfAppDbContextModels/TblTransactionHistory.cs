using System;
using System.Collections.Generic;

namespace DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;

public partial class TblTransactionHistory
{
    public int TransactionHistoryId { get; set; }

    public string FromAccountNo { get; set; } = null!;

    public string ToAccountNo { get; set; } = null!;

    public DateTime TransactionDate { get; set; }

    public decimal Amount { get; set; }

    public string AdminUserCode { get; set; } = null!;

    public string TransactionType { get; set; } = null!;
}

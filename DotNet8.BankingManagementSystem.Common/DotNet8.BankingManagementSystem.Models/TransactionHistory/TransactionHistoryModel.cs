namespace DotNet8.BankingManagementSystem.Models.TransactionHistory;

public class TransactionHistoryModel
{
    public int TransactionHistoryId { get; set; }

    public string FromAccountNo { get; set; } = null!;

    public string ToAccountNo { get; set; } = null!;

    public DateTime TransactionDate { get; set; }

    public decimal Amount { get; set; }

    public string AdminUserCode { get; set; } = null!;

    public string TransactionType { get; set; } = null!;
}

public class TransactionHistorySearchModel
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? TransactionType { get; set; }
    public int PageNo { get; set; }
    public int PageSize { get; set; }
}
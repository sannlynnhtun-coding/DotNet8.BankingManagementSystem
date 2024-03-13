namespace DotNet8.BankingManagementSystem.Models.TransactionHistory;

public class TransactionHistoryRequestModel
{
    public string FromAccountNo { get; set; } = null!;

    public string ToAccountNo { get; set; } = null!;

    public DateTime TransactionDate { get; set; }

    public decimal Amount { get; set; }

    public string AdminUserCode { get; set; } = null!;

    public string TransactionType { get; set; } = null!;
}
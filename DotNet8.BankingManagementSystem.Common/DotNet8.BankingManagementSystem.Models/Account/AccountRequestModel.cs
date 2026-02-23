namespace DotNet8.BankingManagementSystem.Models.Account;

public class AccountRequestModel
{
     public string? AccountNo { get; set; }
     public string? FromAccount { get; set; }
     public string? ToAccount { get; set; }

    public string CustomerCode { get; set; } = null!;
    public string? CustomerName { get; set; }
    public string BranchCode { get; set; } = null!;
    public decimal Balance { get; set; }
}
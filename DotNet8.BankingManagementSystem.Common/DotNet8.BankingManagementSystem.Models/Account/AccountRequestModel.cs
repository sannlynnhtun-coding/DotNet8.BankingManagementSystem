namespace DotNet8.BankingManagementSystem.Models.Account;

public class AccountRequestModel
{
     public string? AccountNo { get; set; }

    public string CustomerCode { get; set; } = null!;
    public string? CustomerName { get; set; }

    public decimal Balance { get; set; }
}
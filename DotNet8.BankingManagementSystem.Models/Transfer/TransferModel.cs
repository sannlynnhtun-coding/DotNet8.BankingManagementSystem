namespace DotNet8.BankingManagementSystem.Models.Transfer;

public class TransferModel
{
    public string FromAccountNo { get; set; }
    public string ToAccountNo { get; set; }
    public decimal Amount { get; set; }
}
namespace DotNet8.BankingManagementSystem.Models.TransactionHistory;

public class TransactionHistoryListResponseModel
{
    public List<TransactionHistoryModel> Data { get; set; }
    public MessageResponseModel Response { get; set; }
    public PageSettingModel PageSetting { get; set; }
}
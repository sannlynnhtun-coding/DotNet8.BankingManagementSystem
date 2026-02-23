namespace DotNet8.BankingManagementSystem.Models.Bank;

public class BankModel
{
    public int BankId { get; set; }
    public string BankCode { get; set; } = null!;
    public string BankName { get; set; } = null!;
}

public class BankRequestModel
{
    public string BankCode { get; set; } = null!;
    public string BankName { get; set; } = null!;
}

public class BankResponseModel
{
    public BankModel Data { get; set; } = null!;
    public MessageResponseModel Response { get; set; } = null!;
}

public class BankListResponseModel
{
    public List<BankModel> Data { get; set; } = null!;
    public PageSettingModel PageSetting { get; set; } = null!;
    public MessageResponseModel Response { get; set; } = null!;
}

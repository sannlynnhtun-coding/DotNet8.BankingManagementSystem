namespace DotNet8.BankingManagementSystem.Models.Branch;

public class BranchModel
{
    public int BranchId { get; set; }
    public string BranchCode { get; set; } = null!;
    public string BranchName { get; set; } = null!;
    public string BankCode { get; set; } = null!;
    public string StateCode { get; set; } = null!;
    public string TownshipCode { get; set; } = null!;
}

public class BranchRequestModel
{
    public string BranchCode { get; set; } = null!;
    public string BranchName { get; set; } = null!;
    public string BankCode { get; set; } = null!;
    public string StateCode { get; set; } = null!;
    public string TownshipCode { get; set; } = null!;
}

public class BranchResponseModel
{
    public BranchModel Data { get; set; } = null!;
    public MessageResponseModel Response { get; set; } = null!;
}

public class BranchListResponseModel
{
    public List<BranchModel> Data { get; set; } = null!;
    public PageSettingModel PageSetting { get; set; } = null!;
    public MessageResponseModel Response { get; set; } = null!;
}

namespace DotNet8.BankingManagementSystem.Models.State;

public class StateListResponseModel
{
    public MessageResponseModel Response { get; set; }
    public PageSettingModel PageSetting { get; set; }
    public List<StateModel> Data { get; set; }
}
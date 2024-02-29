namespace DotNet8.BankingManagementSystem.Models.Users;

public class UserListResponseModel
{
    public MessageResponseModel Response { get; set; }
    public PageSettingModel PageSetting { get; set; }
    public List<UserModel> Data { get; set; }
}
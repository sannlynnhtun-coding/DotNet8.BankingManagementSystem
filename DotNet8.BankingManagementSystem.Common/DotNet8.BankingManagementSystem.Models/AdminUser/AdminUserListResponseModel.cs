namespace DotNet8.BankingManagementSystem.Models.AdminUser;

public class AdminUserListResponseModel
{
    public MessageResponseModel Response { get; set; }

    public PageSettingModel PageSetting { get; set; }

    public List<AdminUserModel> Data { get; set; }
}
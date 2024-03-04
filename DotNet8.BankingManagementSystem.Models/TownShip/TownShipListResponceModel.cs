namespace DotNet8.BankingManagementSystem.Models.TownShip
{
    public class TownshipListResponceModel
    {
        public MessageResponseModel Response { get; set; }
        public PageSettingModel PageSetting { get; set; }
        public List<TownshipModel> Data { get; set; }
    }
}

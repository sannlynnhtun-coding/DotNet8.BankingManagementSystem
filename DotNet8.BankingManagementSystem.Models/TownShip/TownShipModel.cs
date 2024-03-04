namespace DotNet8.BankingManagementSystem.Models.TownShip
{
    public class TownshipModel
    {
        public int TownshipId { get; set; }
        public string TownshipCode { get; set; } = null!;
        public string TownshipName { get; set; } = null!;
        public string StateCode { get; set; } = null!;
    }
}

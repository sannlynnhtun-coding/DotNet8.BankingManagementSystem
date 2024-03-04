namespace DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;

public partial class TblPlaceTownship
{
    public int TownshipId { get; set; }

    public string TownshipCode { get; set; } = null!;

    public string TownshipName { get; set; } = null!;

    public string StateCode { get; set; } = null!;
}

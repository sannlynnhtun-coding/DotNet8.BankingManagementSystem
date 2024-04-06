namespace DotNet8.BankingManagementSystem.Models.State;

public class StateRequestModel
{
    public int StateId { get; set; }
    public string StateCode { get; set; } = null!;
    public string StateName { get; set; } = null!;
}
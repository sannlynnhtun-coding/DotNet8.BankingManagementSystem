using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;

namespace DotNet8.BankingManagementSystem.BackendApi.Models;

public class MessageResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public TblPlaceState State { get; set; }
}
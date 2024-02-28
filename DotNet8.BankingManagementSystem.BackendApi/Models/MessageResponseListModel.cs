using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;

namespace DotNet8.BankingManagementSystem.BackendApi.Models;

public class MessageResponseListModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public int PageNo { get; set; }
    public int PageSize { get; set; }
    public List<TblPlaceState> StateList { get; set; }



}
namespace DotNet8.BankingManagementSystem.Models;

public class MessageResponseModel
{
    public MessageResponseModel(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public MessageResponseModel(bool isSuccess, Exception ex)
    {
        IsSuccess = isSuccess;
        Message = ex.ToString();
    }

    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}
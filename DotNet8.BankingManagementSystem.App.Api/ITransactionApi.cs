using DotNet8.BankingManagementSystem.Models.Account;
using DotNet8.BankingManagementSystem.Models.TransactionHistory;
using DotNet8.BankingManagementSystem.Models.Transfer;
using Refit;

namespace DotNet8.BankingManagementSystem.App.Api;

public interface ITransactionApi

{
    [Get("/api/Transaction/TransactionHistory")]
    Task<TransactionHistoryListResponseModel> TransactionHistory(int pageNo, int pageSize);

    [Post("/api/Transaction/Deposit")]
    Task<AccountResponseModel> Deposit(string accountNo, decimal amount);

    [Post("/api/Transaction/Withdraw")]
    Task<AccountResponseModel> Withdraw(string accountNo, decimal amount);

    [Post("/api/Transaction/Transfer")]
    Task<TransferResponseModel> Transfer(TransferModel requestModel);
}
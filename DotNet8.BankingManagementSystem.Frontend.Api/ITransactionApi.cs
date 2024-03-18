using DotNet8.BankingManagementSystem.Models.Account;
using DotNet8.BankingManagementSystem.Models.TransactionHistory;
using DotNet8.BankingManagementSystem.Models.Transfer;
using Refit;

namespace DotNet8.BankingManagementSystem.Frontend.Api;

public interface ITransactionApi

{
    [Get("/api/Transaction/TransactionHistory/{pageNo}/{pageSize}")]
    Task<TransactionHistoryListResponseModel> TransactionHistory(int pageNo, int pageSize);

    [Get("/api/Transaction/TransactionHistory/{date}/{pageNo}/{pageSize}")]
    Task<TransactionHistoryListResponseModel> TransactionHistoryWithDate(DateTime? date, int pageNo, int pageSize);

    [Post("/api/Transaction/Deposit")]
    Task<AccountResponseModel> Deposit(TransactionRequestModel requestModel);

    [Post("/api/Transaction/Withdraw")]
    Task<AccountResponseModel> Withdraw(TransactionRequestModel requestModel);

    [Post("/api/Transaction/Transfer")]
    Task<TransferResponseModel> Transfer(TransferModel requestModel);
}
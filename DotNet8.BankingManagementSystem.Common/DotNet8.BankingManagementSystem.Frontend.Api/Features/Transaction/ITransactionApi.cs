using Refit;

namespace DotNet8.BankingManagementSystem.Frontend.Api.Features.Transaction;

public interface ITransactionApi

{
    [Get("/api/Transaction/TransactionHistory/{pageNo}/{pageSize}")]
    Task<TransactionHistoryListResponseModel> TransactionHistory(int pageNo, int pageSize);

    [Post("/api/Transaction/TransactionHistory")]
    Task<TransactionHistoryListResponseModel> TransactionHistoryWithDate(TransactionHistorySearchModel requestModel);

    [Post("/api/Transaction/Deposit")]
    Task<AccountResponseModel> Deposit(TransactionRequestModel requestModel);

    [Post("/api/Transaction/Withdraw")]
    Task<AccountResponseModel> Withdraw(TransactionRequestModel requestModel);

    [Post("/api/Transaction/Transfer")]
    Task<TransferResponseModel> Transfer(TransferModel requestModel);

    [Post("/api/Transaction/TransactionHistory/DateRange")]
    Task<TransactionHistoryListResponseModel> TransactionHistoryWithDateRange(TransactionHistorySearchModel requestModel);
}
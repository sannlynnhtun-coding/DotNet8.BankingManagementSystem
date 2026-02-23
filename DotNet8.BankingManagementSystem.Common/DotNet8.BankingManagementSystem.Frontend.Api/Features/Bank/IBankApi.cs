using DotNet8.BankingManagementSystem.Models.Bank;
using Refit;

namespace DotNet8.BankingManagementSystem.Frontend.Api.Features.Bank;

public interface IBankApi
{
    [Get("/api/bank")]
    Task<BankListResponseModel> GetBanks();

    [Get("/api/bank/{bankCode}")]
    Task<BankResponseModel> GetBankByCode(string bankCode);
}

using DotNet8.BankingManagementSystem.Models.Branch;
using Refit;

namespace DotNet8.BankingManagementSystem.Frontend.Api.Features.Branch;

public interface IBranchApi
{
    [Get("/api/branch")]
    Task<BranchListResponseModel> GetBranches();

    [Get("/api/branch/{bankCode}")]
    Task<BranchListResponseModel> GetBranchesByBankCode(string bankCode);
}

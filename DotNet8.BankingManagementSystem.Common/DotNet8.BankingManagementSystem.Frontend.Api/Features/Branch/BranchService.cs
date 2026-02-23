using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using DotNet8.BankingManagementSystem.Frontend.Api.Services;
using DotNet8.BankingManagementSystem.Models.Branch;
using DotNet8.BankingManagementSystem.Mapper;

namespace DotNet8.BankingManagementSystem.Frontend.Api.Features.Branch;

public class BranchService
{
    private readonly LocalStorageService _localStorageService;

    public BranchService(LocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<BranchListResponseModel> GetBranches()
    {
        var query = await _localStorageService.GetList<TblBranch>(EnumService.Tbl_Branch.GetKeyName());
        var lst = query.Select(x => x.Change()).ToList();
        return new BranchListResponseModel
        {
            Data = lst,
            Response = new MessageResponseModel(true, "Success")
        };
    }

    public async Task<BranchListResponseModel> GetBranchesByBankCode(string bankCode)
    {
        var query = await _localStorageService.GetList<TblBranch>(EnumService.Tbl_Branch.GetKeyName());
        var lst = query.Where(x => x.BankCode == bankCode).Select(x => x.Change()).ToList();
        return new BranchListResponseModel
        {
            Data = lst,
            Response = new MessageResponseModel(true, "Success")
        };
    }

    public async Task<BranchResponseModel> GetBranchByCode(string branchCode)
    {
        var query = await _localStorageService.GetList<TblBranch>(EnumService.Tbl_Branch.GetKeyName());
        var item = query.FirstOrDefault(x => x.BranchCode == branchCode);
        if (item is null) return new BranchResponseModel { Response = new MessageResponseModel(false, "Branch not found") };
        return new BranchResponseModel { Data = item.Change(), Response = new MessageResponseModel(true, "Success") };
    }
}

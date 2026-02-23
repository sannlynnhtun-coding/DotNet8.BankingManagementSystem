namespace DotNet8.BankingManagementSystem.Frontend.Features;

public class BankService
{
    private readonly LocalStorageService _localStorageService;

    public BankService(LocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<BankListResponseModel> GetBanks()
    {
        var query = await _localStorageService.GetList<TblBank>(EnumService.Tbl_Bank.GetKeyName());
        var lst = query.Select(x => x.Change()).ToList();
        return new BankListResponseModel
        {
            Data = lst,
            Response = new MessageResponseModel(true, "Success")
        };
    }

    public async Task<BankResponseModel> GetBankByCode(string bankCode)
    {
        var query = await _localStorageService.GetList<TblBank>(EnumService.Tbl_Bank.GetKeyName());
        var item = query.FirstOrDefault(x => x.BankCode == bankCode);
        if (item is null) return new BankResponseModel { Response = new MessageResponseModel(false, "Bank not found") };
        return new BankResponseModel { Data = item.Change(), Response = new MessageResponseModel(true, "Success") };
    }
}

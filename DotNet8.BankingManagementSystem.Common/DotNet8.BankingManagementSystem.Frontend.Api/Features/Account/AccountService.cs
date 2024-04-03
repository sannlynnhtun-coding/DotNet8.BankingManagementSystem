namespace DotNet8.BankingManagementSystem.Frontend.Api.Features.Account;

public class AccountService
{
    private readonly LocalStorageService _localStorageService;

    public AccountService(LocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    #region GetAccounts

    public async Task<AccountListResponseModel> GetAccounts()
    {
        AccountListResponseModel model = new AccountListResponseModel();
        var lst = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.GetKeyName());
        model.Data = lst;
        model.Response = new MessageResponseModel(true, "Success.");
        return model;
    }

    #endregion

    #region GetAccountList

    public async Task<AccountListResponseModel> GetAccountList(int pageNo, int pageSize)
    {
        var query = await _localStorageService.GetList<TblAccount>(EnumService.Tbl_Account.ToString());
        var result = query
            .OrderByDescending(x => x.AccountNo)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var count = query.Count();
        int pageCount = count / pageSize;
        if (count % pageSize > 0) pageCount++;
        var lst = result.Select(x => x.Change()).ToList();

        AccountListResponseModel model = new AccountListResponseModel
        {
            Data = lst,
            PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
            Response = new MessageResponseModel(true, "Success")
        };
        return model;
    }

    #endregion

    #region GetAccount

    public async Task<AccountResponseModel> GetAccount(string accountNo)
    {
        AccountResponseModel model = new AccountResponseModel();
        var lst = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.GetKeyName());
        lst ??= new();
        var item = lst.FirstOrDefault(x => x.AccountNo == accountNo);
        if (item is null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        model.Data = item;
        model.Response = new MessageResponseModel(true, "Success.");
        return model;
    }

    #endregion

    #region CreateAccount

    public async Task<AccountResponseModel> CreateAccount(AccountRequestModel requestModel)
    {
        AccountResponseModel model = new AccountResponseModel();
        try
        {
            var item = requestModel.Change();
            var query = await _localStorageService.GetList<TblAccount>(EnumService.Tbl_Account.ToString());
            query ??= new List<TblAccount>();
            query.Add(item);
            await _localStorageService.SetList(EnumService.Tbl_Account.ToString(), query);
            model.Data = item.Change();
            model.Response = new MessageResponseModel(true, "Account has created successfully.");
        }
        catch (Exception ex)
        {
            model.Response = new MessageResponseModel(false, ex);
        }

        return model;
    }

    #endregion

    // public async Task<AccountResponseModel> CreateAccountv1(AccountRequestModel requestModel)
    // {
    //     AccountResponseModel model = new AccountResponseModel();
    //     var lst = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.GetKeyName());
    //     lst ??= new List<AccountModel>();
    //
    //     lst.Add(new AccountModel
    //     {
    //         AccountNo = requestModel.AccountNo,
    //         CustomerName = requestModel.CustomerName,
    //         Balance = requestModel.Balance,
    //         CustomerCode = requestModel.CustomerCode,
    //     });
    //
    //     await _localStorageService.SetList(EnumService.Tbl_Account.GetKeyName(), lst);
    //
    //     model.Response = new MessageResponseModel(true, "Account has been registered.");
    //     return model;
    // }

    #region UpdateAccount

    public async Task<AccountResponseModel> UpdateAccount(string accountNo, AccountRequestModel requestModel)
    {
        AccountResponseModel model = new AccountResponseModel();
        var lst = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.GetKeyName());
        var result = lst.FirstOrDefault(x => x.AccountNo == accountNo);
        var index = lst.FindIndex(x => result != null && x.AccountNo == result.AccountNo);
        if (result is null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        result.AccountNo = requestModel.AccountNo;
        result.Balance = requestModel.Balance;
        result.CustomerCode = requestModel.CustomerCode;
        result.CustomerName = requestModel.CustomerName;
        lst[index] = result;

        await _localStorageService.SetList(EnumService.Tbl_Account.GetKeyName(), lst);
        model = new AccountResponseModel
        {
            Data = result,
            Response = new MessageResponseModel(true, "Account has been removed.")
        };
        return model;
    }

    #endregion

    #region DeleteAccount

    public async Task<AccountResponseModel> DeleteAccount(string accountNo)
    {
        AccountResponseModel model = new AccountResponseModel();
        var lst = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.GetKeyName());
        lst ??= new();
        var item = lst.FirstOrDefault(x => x.AccountNo == accountNo);
        if (item is null)
        {
            model.Response = new MessageResponseModel(false, "No Data Found.");
            return model;
        }

        lst.Remove(item);
        await _localStorageService.SetList(EnumService.Tbl_Account.GetKeyName(), lst);

        model.Response = new MessageResponseModel(true, "Account has been removed.");
        return model;
    }

    #endregion
}
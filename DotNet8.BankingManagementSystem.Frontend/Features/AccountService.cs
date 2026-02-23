using System.Text;

namespace DotNet8.BankingManagementSystem.Frontend.Features;

public class AccountService
{
    private readonly LocalStorageService _localStorageService;

    public AccountService(LocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<AccountListResponseModel> GetAccounts()
    {
        AccountListResponseModel model = new AccountListResponseModel();
        var lst = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.ToString());
        model.Data = lst;
        model.Response = new MessageResponseModel(true, "Success.");
        return model;
    }

    public async Task<AccountListResponseModel> GetAccountList(int pageNo = 1, int pageSize = 5, string? bankCode = null, string? branchCode = null)
    {
        AccountListResponseModel model = new AccountListResponseModel();
        var query = await _localStorageService.GetList<TblAccount>(EnumService.Tbl_Account.ToString());
        
        if (!string.IsNullOrEmpty(branchCode))
        {
            query = query.Where(x => x.BranchCode == branchCode).ToList();
        }
        else if (!string.IsNullOrEmpty(bankCode))
        {
            var branches = await _localStorageService.GetList<TblBranch>(EnumService.Tbl_Branch.GetKeyName());
            var branchCodes = branches.Where(b => b.BankCode == bankCode).Select(b => b.BranchCode).ToList();
            query = query.Where(x => branchCodes.Contains(x.BranchCode)).ToList();
        }

        if (pageNo == 0)
        {
            model = new AccountListResponseModel
            {
                Data = query.Select(x => x.Change()).ToList(),
                Response = new MessageResponseModel(true, "Success")
            };
        }
        else
        {
            query ??= [];
            var count = query.Count();
            int pageCount = count / pageSize;
            if (count % pageSize > 0) pageCount++;
            var result = query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var lst = result.Select(x => x.Change()).ToList();
            model = new AccountListResponseModel
            {
                Data = lst,
                PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
                Response = new MessageResponseModel(true, "Success")
            };
        }
        return model;
    }

    public async Task<AccountResponseModel> GetAccount(string accountNo)
    {
        AccountResponseModel model = new AccountResponseModel();
        var lst = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.GetKeyName());
        lst ??= [];
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

    public async Task<AccountResponseModel> CreateAccount(AccountRequestModel requestModel)
    {
        AccountResponseModel model = new AccountResponseModel();
        try
        {
            var item = requestModel.Change();
            item.AccountNo = GenerateIBAN();
            var query = await _localStorageService.GetList<TblAccount>(EnumService.Tbl_Account.ToString());
            query ??= [];
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

    private string GenerateIBAN()
    {
        Random random = new Random();
        StringBuilder ibanBuilder = new StringBuilder();
        ibanBuilder.Append(GetRandomLetters(2));
        ibanBuilder.Append(random.Next(10));
        ibanBuilder.Append(random.Next(10));
        for (int i = 0; i < 22; i++)
        {
            ibanBuilder.Append(random.Next(10));
        }
        return ibanBuilder.ToString();
    }

    private string GetRandomLetters(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

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
            Response = new MessageResponseModel(true, "Account has been updated.")
        };
        return model;
    }

    public async Task<AccountResponseModel> DeleteAccount(string accountNo)
    {
        AccountResponseModel model = new AccountResponseModel();
        var lst = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.GetKeyName());
        lst ??= [];
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
}

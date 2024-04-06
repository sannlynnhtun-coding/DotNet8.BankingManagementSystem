using DotNet8.BankingManagementSystem.Shared;
using System.Collections.Generic;
using System.Text;

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
        var lst = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.ToString());
        model.Data = lst;
        model.Response = new MessageResponseModel(true, "Success.");
        return model;
    }

    #endregion

    #region GetAccountList

    public async Task<AccountListResponseModel> GetAccountList(int pageNo = 1, int pageSize = 5)
    {
        AccountListResponseModel model = new AccountListResponseModel();
        var query = await _localStorageService.GetList<TblAccount>(EnumService.Tbl_Account.ToString());
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

    #endregion

    #region GetAccount

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

    #endregion

    #region CreateAccount

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

    private string GenerateUniqueAccountNumber()
    {
        // Get current timestamp in ticks
        long timestamp = DateTime.Now.Ticks;

        // Generate a unique identifier (e.g., GUID)
        string uniqueId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6); // Using first 6 characters of GUID

        // Concatenate timestamp and unique identifier to form the account number
        string accountNumber = $"{timestamp}{uniqueId}";

        return accountNumber;
    }

    private string GenerateIBAN()
    {
        Random random = new Random();

        // Example format: CountryCode (2 letters) + CheckDigits (2 digits) + AccountNumber (22 digits)
        StringBuilder ibanBuilder = new StringBuilder();

        // Generate random country code (2 uppercase letters)
        ibanBuilder.Append(GetRandomLetters(2));

        // Add random check digits (2 digits)
        ibanBuilder.Append(random.Next(10));
        ibanBuilder.Append(random.Next(10));

        // Add random account number (22 digits)
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

    #endregion

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

    #endregion
}
using Blazored.LocalStorage;
using DotNet8.BankingManagementSystem.Database.Frontend;
using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.Account;
using DotNet8.BankingManagementSystem.Models.Transfer;

namespace DotNet8.BankingManagementSystem.Backend.Services.Service.Localstorage.Transaction;

public class TransactionService
{
    private readonly LocalStorageService _localStorageService;

    public TransactionService(LocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<AccountResponseModel> Withdraw(AccountModel requestModel, decimal amount)
    {
        AccountResponseModel model = new AccountResponseModel();
        var lst = await _localStorageService.GetAccountList(EnumService.Tbl_Account.GetKeyName());
        var result = lst.FirstOrDefault(x => x.AccountNo == requestModel.AccountNo);

        if (result is null)
        {
            model.Response = new MessageResponseModel(false, "Account not found.");
            return model;
        }

        if (result.Balance < amount)
        {
            model.Response = new MessageResponseModel(false, "Insufficient balance.");
            return model;
        }

        result.Balance -= amount;

        lst[lst.FindIndex(x => x.AccountNo == result.AccountNo)] = result;
        await _localStorageService.SetAccount(lst);

        model.Data = result;
        model.Response = new MessageResponseModel(true, "Withdrawal successful.");

        return model;
    }

    public async Task<AccountResponseModel> Deposit(AccountModel requestModel, decimal amount)
    {
        AccountResponseModel model = new AccountResponseModel();
        var lst = await _localStorageService.GetAccountList(EnumService.Tbl_Account.GetKeyName());
        var result = lst.FirstOrDefault(x => x.AccountNo == requestModel.AccountNo);

        if (result == null)
        {
            model.Response = new MessageResponseModel(false, "Account not found.");
            return model;
        }

        result.Balance += amount;

        lst[lst.FindIndex(x => x.AccountNo == result.AccountNo)] = result;

        await _localStorageService.SetAccount(lst);

        model.Data = result;
        model.Response = new MessageResponseModel(true, "Deposit successful.");

        return model;
    }

    public async Task<TransferResponseModel> Transfer(TransferModel requestModel)
    {
        TransferResponseModel model = new TransferResponseModel();

        var lst = await _localStorageService.GetAccountList(EnumService.Tbl_Account.GetKeyName());
        var fromAccount = lst.FirstOrDefault(x => x.AccountNo == requestModel.FromAccountNo);

        var lst1 = await _localStorageService.GetAccountList(EnumService.Tbl_Account.GetKeyName());
        var toAccount = lst1.FirstOrDefault(x => x.AccountNo == requestModel.ToAccountNo);

        if (fromAccount is null)
        {
            model.Response = new MessageResponseModel(false, "Source account not found.");
            return model;
        }

        if (toAccount is null)
        {
            model.Response = new MessageResponseModel(false, "Destination account not found.");
            return model;
        }

        if (fromAccount.Balance < requestModel.Amount)
        {
            model.Response = new MessageResponseModel(false, "Insufficient balance in source account.");
            return model;
        }

        fromAccount.Balance -= requestModel.Amount;
        toAccount.Balance += requestModel.Amount;

        lst[lst.FindIndex(x => x.AccountNo == fromAccount.AccountNo)] = fromAccount;
        await _localStorageService.SetAccount(lst);

        lst1[lst1.FindIndex(x => x.AccountNo == toAccount.AccountNo)] = toAccount;
        await _localStorageService.SetAccount(lst1);

        model.Data.FromAccountNo = Convert.ToString(fromAccount);
        model.Data.ToAccountNo = Convert.ToString(toAccount);
        model.Response = new MessageResponseModel(true, "Transfer successful.");

        return model;
    }
}
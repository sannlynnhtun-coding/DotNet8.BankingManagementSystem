using DotNet8.BankingManagementSystem.Frontend.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.BankingManagementSystem.Frontend.Api.Features.Transaction;

public class TransactionService
{
    private readonly LocalStorageService _localStorageService;

    public TransactionService(LocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<AccountResponseModel> Withdraw(TransactionRequestModel requestModel)
    {
        AccountResponseModel model = new AccountResponseModel();
        var lst = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.ToString());
        var result = lst.FirstOrDefault(x => x.AccountNo == requestModel.AccountNo);

        if (result is null)
        {
            model.Response = new MessageResponseModel(false, "Account not found.");
            return model;
        }

        if (result.Balance < requestModel.Amount)
        {
            model.Response = new MessageResponseModel(false, "Insufficient balance.");
            return model;
        }

        result.Balance -= requestModel.Amount;

        lst[lst.FindIndex(x => x.AccountNo == result.AccountNo)] = result;
        await _localStorageService.SetList(EnumService.Tbl_Account.ToString(), lst);

        model.Data = result;
        model.Response = new MessageResponseModel(true, "Withdrawal successful.");

        return model;
    }

    public async Task<AccountResponseModel> Deposit(TransactionRequestModel requestModel)
    {
        AccountResponseModel model = new AccountResponseModel();
        var lst = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.ToString());
        var result = lst.FirstOrDefault(x => x.AccountNo == requestModel.AccountNo);

        if (result == null)
        {
            model.Response = new MessageResponseModel(false, "Account not found.");
            return model;
        }

        result.Balance += requestModel.Amount;

        lst[lst.FindIndex(x => x.AccountNo == result.AccountNo)] = result;

        await _localStorageService.SetList(EnumService.Tbl_Account.ToString(), lst);

        model.Data = result;
        model.Response = new MessageResponseModel(true, "Deposit successful.");

        return model;
    }

    public async Task<TransferResponseModel> Transfer(TransferModel requestModel)
    {
        TransferResponseModel model = new TransferResponseModel();

        var lst = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.ToString());
        var fromAccount = lst.FirstOrDefault(x => x.AccountNo == requestModel.FromAccountNo);

        var lst1 = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.ToString());
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
        await _localStorageService.SetList(EnumService.Tbl_Account.ToString(), lst);

        lst1[lst1.FindIndex(x => x.AccountNo == toAccount.AccountNo)] = toAccount;
        await _localStorageService.SetList(EnumService.Tbl_Account.ToString(), lst1);

        model.Data.FromAccountNo = Convert.ToString(fromAccount);
        model.Data.ToAccountNo = Convert.ToString(toAccount);
        model.Response = new MessageResponseModel(true, "Transfer successful.");

        return model;
    }

    public async Task<TransactionHistoryListResponseModel> TransactionHistory(int pageNo, int pageSize)
    {
        TransactionHistoryListResponseModel model = new TransactionHistoryListResponseModel();
        var query = await _localStorageService.GetList<TblTransactionHistory>(EnumService.Tbl_TransactionHistory.ToString());
        var result =  query.OrderByDescending(x => x.TransactionDate)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize).ToList();

        var count =  query.Count();
        int pageCount = count / pageSize;
        if (count % pageSize > 0) pageCount++;
        var lst = result.Select(x => x.Change()).ToList();
        model = new TransactionHistoryListResponseModel()
        {
            Data = lst,
            PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
            Response = new MessageResponseModel(true, "Success")
        };
        return model;
    }

    #region TransactionHistoryWithDate

    public async Task<TransactionHistoryListResponseModel> TransactionHistoryWithDate
        (TransactionHistorySearchModel requestModel)
    {
        TransactionHistoryListResponseModel model = new TransactionHistoryListResponseModel();
        var query = await _localStorageService.GetList<TblTransactionHistory>(EnumService.Tbl_TransactionHistory.ToString());

        if (requestModel.FromDate.HasValue)
        {
            query = query.Where(x => x.TransactionDate.Date == requestModel.FromDate.Value.Date).ToList();
        }

        var result =  query.OrderByDescending(x => x.TransactionDate)
            .Skip((requestModel.PageNo - 1) * requestModel.PageSize)
            .Take(requestModel.PageSize).ToList();

        var count =  query.Count();
        int pageCount = count / requestModel.PageSize;
        if (count % requestModel.PageSize > 0) pageCount++;
        var lst = result.Select(x => x.Change()).ToList();
        model = new TransactionHistoryListResponseModel()
        {
            Data = lst,
            PageSetting = new PageSettingModel(requestModel.PageNo, requestModel.PageSize, pageCount),
            Response = new MessageResponseModel(true, "Success")
        };
        return model;
    }

    #endregion

    #region Auto generate account

    public async Task<List<TblAccount>> GenerateAccounts(int count, int year)
    {
        Random random = new Random();
        List<TblAccount> model = new List<TblAccount>();

        for (int i = 0; i < count; i++)
        {
            string customerCode = GenerateCustomerCode();
            string customerName = GenerateCustomerName();
            decimal balance = (decimal)(random.Next(10000000, 100000000));
            if (balance < 0) balance *= -1;
            int num = balance.ToString().Length == 8 ? 2 : 3;
            balance = Convert.ToDecimal(balance.ToString().Substring(0, num)) * 100000;

            TblAccount item = new TblAccount
            {
                CustomerName = customerName,
                CustomerCode = customerCode,
                Balance = balance
            };

            model.Add(item);
        }
        var accountQuery = await _localStorageService.GetList<TblAccount>(EnumService.Tbl_Account.ToString());
        accountQuery ??= new List<TblAccount>();
        accountQuery.AddRange(model);
        await _localStorageService.SetList<TblAccount>(EnumService.Tbl_Account.ToString(), accountQuery);

        List<TblTransactionHistory> transactions = new List<TblTransactionHistory>();
        foreach (var item in model)
        {
            for (DateTime date = new DateTime(DateTime.Now.Year - year, 1, 1).Date;
                 date < DateTime.Now.Date;
                 date = date.AddDays(1))
            {
                string GetDifferentAccountNo(int currentAccountId)
                {
                    Random random = new Random();
                    int randomNumber;
                    do
                    {
                        randomNumber = random.Next(1, 100);
                    } while (randomNumber == currentAccountId);

                    return randomNumber.ToString("D6");
                }

                var fromAccountNo = GetDifferentAccountNo(item.AccountId);
                var toAccountNo = (Convert.ToInt32(fromAccountNo) + 1).ToString("D6");
                var amount = (decimal)random.NextDouble() * 1000000;
                TblTransactionHistory creditTransaction = new TblTransactionHistory
                {
                    FromAccountNo = fromAccountNo,
                    ToAccountNo = toAccountNo,
                    TransactionDate = date,
                    Amount = amount,
                    AdminUserCode = "Admin",
                    TransactionType = "Credit"
                };
                transactions.Add(creditTransaction);
                TblTransactionHistory debitTransaction = new TblTransactionHistory
                {
                    FromAccountNo = fromAccountNo,
                    ToAccountNo = toAccountNo,
                    TransactionDate = date,
                    Amount = amount,
                    AdminUserCode = "Admin",
                    TransactionType = "Debit"
                };
                transactions.Add(debitTransaction);
            }
        }
        var query = await _localStorageService.GetList<TblTransactionHistory>(EnumService.Tbl_TransactionHistory.ToString());
        query ??= new List<TblTransactionHistory>();
        query.AddRange(transactions);
        await _localStorageService.SetList(EnumService.Tbl_TransactionHistory.ToString(), query);
        return model;
    }

    private string GenerateCustomerCode()
    {
        string randomNumber = new Random().Next(1000000, 9999999).ToString();
        return "C" + randomNumber;
    }

    private static string GenerateCustomerName()
    {
        string[] firstNames = { "John", "Alice", "Michael", "Emily", "David", "Sarah" };

        Random rand = new Random();
        string name = firstNames[rand.Next(firstNames.Length)];

        return name;
    }

    #endregion

    #region get from date to date

    public async Task<TransactionHistoryListResponseModel> TransactionHistoryWithDateRange
        (TransactionHistorySearchModel requestModel)
    {
        TransactionHistoryListResponseModel model = new TransactionHistoryListResponseModel();
        var query = await _localStorageService.GetList<TblTransactionHistory>(EnumService.Tbl_TransactionHistory.ToString());
        var result =  query
            .Where(x => x.TransactionDate >= requestModel.FromDate && x.TransactionDate <= requestModel.ToDate)
            .Skip((requestModel.PageNo - 1) * requestModel.PageSize).Take(requestModel.PageSize)
            .ToList();
        var lst = result.Select(x => x.Change()).ToList();

        model = new TransactionHistoryListResponseModel()
        {
            Data = lst,
            Response = new MessageResponseModel(true, "Success")
        };
        return model;
    }

    #endregion
}
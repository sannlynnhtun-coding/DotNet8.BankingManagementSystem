using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using DotNet8.BankingManagementSystem.Mapper;
using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.Account;
using DotNet8.BankingManagementSystem.Models.Transfer;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.BankingManagementSystem.BackendApi.Features.Account;

public class AccountService
{
    private readonly AppDbContext _dbContext;

    public AccountService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Get Account list

    public async Task<AccountListResponseModel> GetAccounts()
    {
        var query = _dbContext.TblAccounts.AsNoTracking();
        var result = await query.OrderBy(x => x.AccountId).ToListAsync();
        var lst = result.Select(x => x.Change()).ToList();
        AccountListResponseModel model = new AccountListResponseModel()
        {
            Data = lst,
            Response = new MessageResponseModel(true, "Success")
        };
        return model;
    }

    #endregion

    #region GetAccountList By Pagination

    public async Task<AccountListResponseModel> GetAccountList(int pageNo, int pageSize)
    {
        var query = _dbContext.TblAccounts.AsNoTracking();
        var result = await query
            .OrderByDescending(x => x.AccountId)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var count = await query.CountAsync();
        int pageCount = count / pageSize;
        if (count % pageSize > 0) pageCount++;
        var lst = result.Select(x => x.Change()).ToList();
        AccountListResponseModel model = new AccountListResponseModel()
        {
            Data = lst,
            PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
            Response = new MessageResponseModel(true, "Success")
        };
        return model;
    }

    #endregion

    #region GetAccount by accountNo

    public async Task<AccountResponseModel> GetAccount(string accountNo)
    {
        var query = _dbContext.TblAccounts.AsNoTracking();
        var item = await query.FirstOrDefaultAsync(x => x.AccountNo == accountNo);
        if (item is null)
        {
            throw new Exception("Invalid Account");
        }

        AccountResponseModel model = new AccountResponseModel
        {
            Data = item!.Change(),
            Response = new MessageResponseModel(true, "Success")
        };
        return model;
    }

    #endregion

    #region Account Create

    public async Task<AccountResponseModel> CreateAccount(AccountRequestModel requestModel)
    {
        var item = requestModel.Change();
        // {
        //     AccountNo = requestModel.AccountNo,
        //     CustomerCode = requestModel.CustomerCode,
        //     Balance = requestModel.Balance
        // };
        await _dbContext.TblAccounts.AddAsync(item);
        var result = await _dbContext.SaveChangesAsync();
        AccountResponseModel model = new AccountResponseModel()
        {
            Data = item.Change(),
            Response = new MessageResponseModel(true, "Account has created successfully.")
        };
        return model;
    }

    #endregion

    #region Update Account info

    public async Task<AccountResponseModel> UpdateAccount(string accountNo, AccountRequestModel requestModel)
    {
        var query = _dbContext.TblAccounts.AsNoTracking();
        var item = await query.FirstOrDefaultAsync(x => x.AccountNo == accountNo);
        if (item is null)
        {
            throw new Exception("Invalid Account");
        }

        item.CustomerCode = requestModel.CustomerCode;
        item.Balance = requestModel.Balance;
        _dbContext.Entry(item).State = EntityState.Modified;
        _dbContext.TblAccounts.Update(item);
        var result = await _dbContext.SaveChangesAsync();
        AccountResponseModel model = new AccountResponseModel()
        {
            Data = item.Change(),
            Response = new MessageResponseModel(true, "Account has updated successfully.")
        };
        return model;
    }

    #endregion

    #region Delete Account

    public async Task<AccountResponseModel> DeleteAccount(string accountNo)
    {
        var query = _dbContext.TblAccounts.AsNoTracking();
        var item = await query.FirstOrDefaultAsync(x => x.AccountNo == accountNo);
        if (item is null)
        {
            throw new Exception("Invalid Account");
        }

        _dbContext.Entry(item).State = EntityState.Deleted;
        _dbContext.TblAccounts.Remove(item);
        var result = await _dbContext.SaveChangesAsync();
        AccountResponseModel model = new AccountResponseModel
        {
            Response = new MessageResponseModel(true, "Account has deleted successfully.")
        };
        return model;
    }

    #endregion

    #region Deposit

    public async Task<AccountResponseModel> Deposit(string accountNo, decimal amount)
    {
        var query = _dbContext.TblAccounts.AsNoTracking();
        var item = await query.FirstOrDefaultAsync(x => x.AccountNo == accountNo);
        if (item is null)
        {
            throw new Exception("Invalid Account");
        }

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            //decimal newBalance = item.Balance + amount;
            //item.Balance = newBalance;
            item.Balance += amount;
            _dbContext.TblAccounts.Update(item);
            int result = await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            // Console.WriteLine(ex.Message);
        }

        AccountResponseModel model = new AccountResponseModel()
        {
            Data = item.Change(),
            Response = new MessageResponseModel(true, "Deposit Successfully.")
        };
        return model;
    }

    #endregion

    #region Withdrawl

    public async Task<AccountResponseModel> Withdraw(string accountNo, decimal amount)
    {
        var query = _dbContext.TblAccounts.AsNoTracking();
        var item = await query.FirstOrDefaultAsync(x => x.AccountNo == accountNo);
        if (item is null)
        {
            throw new Exception("Account is not found.");
        }

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            //decimal newBalance = item.Balance - amount;
            //item.Balance = newBalance;
            item.Balance -= amount;
            _dbContext.TblAccounts.Update(item);
            int result = await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            // Console.WriteLine(ex.Message);
        }

        AccountResponseModel model = new AccountResponseModel()
        {
            Data = item.Change(),
            Response = new MessageResponseModel(true, "Deposit Successfully.")
        };
        return model;
    }

    #endregion

    #region Transfer

    public async Task<TransferResponseModel> Transfer(TransferModel requestModel)
    {
        TransferResponseModel model = new TransferResponseModel();
        var query = _dbContext.TblAccounts.AsNoTracking();
        var fromAccount = await query.FirstOrDefaultAsync(x => x.AccountNo == requestModel.FromAccountNo);
        if (fromAccount is null)
        {
            throw new Exception("Invalid From Account.");
        }

        var toAccount = await query.FirstOrDefaultAsync(x => x.AccountNo == requestModel.ToAccountNo);
        if (toAccount is null)
        {
            throw new Exception("Invalid To Account.");
        }

        if (fromAccount.Balance < requestModel.Amount)
        {
            model = new TransferResponseModel()
            {
                Response = new MessageResponseModel(false, "Insufficient Balance.")
            };
            goto result;
        }

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            fromAccount.Balance -= requestModel.Amount;
            _dbContext.TblAccounts.Update(fromAccount);

            toAccount.Balance += requestModel.Amount;
            _dbContext.TblAccounts.Update(toAccount);

            //int result = await _dbContext.SaveChangesAsync();

            TblTransactionHistory creditTransactionHistory = new TblTransactionHistory()
            {
                Amount = requestModel.Amount,
                TransactionDate = DateTime.Now,
                FromAccountNo = fromAccount.AccountNo!,
                ToAccountNo = toAccount.AccountNo!,
                AdminUserCode = "Admin",
                TransactionType = "Credit"
            };

            TblTransactionHistory dedbitTransactionHistory = new TblTransactionHistory()
            {
                Amount = requestModel.Amount,
                TransactionDate = DateTime.Now,
                ToAccountNo = toAccount.AccountNo!,
                FromAccountNo = fromAccount.AccountNo!,
                AdminUserCode = "Admin",
                TransactionType = "Debit"
            };
            await _dbContext.AddRangeAsync(dedbitTransactionHistory, creditTransactionHistory);
            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }

        model = new TransferResponseModel
        {
            Response = new MessageResponseModel(true, "Balance transfer successful.")
        };

        result:
        return model;
    }

    #endregion

    #region Auto generate account

    public async Task<List<TblAccount>> GenerateAccounts(int count)
    {
        Random random = new Random();
        List<TblAccount> model = new List<TblAccount>();

        for (int i = 0; i < count; i++)
        {
            string customerCode = GenerateCustomerCode();
            decimal balance = (decimal)(random.Next(10000000, 100000000) * 1000);
            if (balance < 0)
            {
                balance *= -1;
            }

            TblAccount item = new TblAccount
            {
                CustomerCode = customerCode,
                Balance = balance
            };

            model.Add(item);
        }

        await _dbContext.TblAccounts.AddRangeAsync(model);
        await _dbContext.SaveChangesAsync();

        List<TblTransactionHistory> transactions = new List<TblTransactionHistory>();
        foreach (var item in model)
        {
            for (DateTime date = new DateTime(2022, 1, 1); date < new DateTime(2024, 12, 31); date = date.AddDays(1))
            {
                string GetDifferentAccountNo(string currentAccountNo)
                {
                    Random random = new Random();
                    int randomNumber = random.Next(1, 10);
                    int result = Convert.ToInt32(currentAccountNo) + randomNumber;
                    return result.ToString("D6");
                }

                TblTransactionHistory creditTransaction = new TblTransactionHistory
                {
                    FromAccountNo = GetDifferentAccountNo(item.AccountNo),
                    ToAccountNo = GetDifferentAccountNo(item.AccountNo),
                    TransactionDate = date,
                    Amount = (decimal)random.NextDouble() * 10000,
                    AdminUserCode = "Admin",
                    TransactionType = "Credit"
                };
                transactions.Add(creditTransaction);
                TblTransactionHistory debitTransaction = new TblTransactionHistory
                {
                    FromAccountNo = GetDifferentAccountNo(item.AccountNo),
                    ToAccountNo = GetDifferentAccountNo(item.AccountNo),
                    TransactionDate = date,
                    Amount = (decimal)random.NextDouble() * 10000,
                    AdminUserCode = "Admin",
                    TransactionType = "Debit"
                };
                transactions.Add(debitTransaction);
            }
        }

        await _dbContext.TblTransactionHistories.AddRangeAsync(transactions);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    private string GenerateCustomerCode()
    {
        string randomNumber = new Random().Next(1000000, 9999999).ToString();
        return "C" + randomNumber;
    }

    #endregion
}
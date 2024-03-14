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
}
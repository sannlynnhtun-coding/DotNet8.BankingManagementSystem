using DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;
using DotNet8.BankingManagementSystem.Mapper;
using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.TransactionHistory;
using DotNet8.BankingManagementSystem.Models.Transfer;
using Microsoft.EntityFrameworkCore;

// var result = await query
//     .OrderByDescending(x => x.AccountId)
//     .Skip((pageNo - 1) * pageSize)
//     .Take(pageSize)
//     .ToListAsync();
// var count = await query.CountAsync();
// int pageCount = count / pageSize;
// if (count % pageSize > 0) pageCount++;
// var lst = result.Select(x => x.Change()).ToList();
// AccountListResponseModel model = new AccountListResponseModel()
// {
//     Data = lst,
//     PageSetting = new PageSettingModel(pageNo, pageSize, pageCount),
//     Response = new MessageResponseModel(true, "Success")
// };
// return model;

namespace DotNet8.BankingManagementSystem.BackendApi.Features.Transaction;

public class TransactionHistoryService
{
    private readonly AppDbContext _dbContext;

    public TransactionHistoryService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region TransactionHistory List

    public async Task<TransactionHistoryListResponseModel> GetTransactionHistoryList(int pageNo, int pageSize)
    {
        TransactionHistoryListResponseModel model = new TransactionHistoryListResponseModel();
        var query = _dbContext.TblTransactionHistories.AsNoTracking();
        var result = await query.OrderByDescending(x => x.TransactionDate)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize).ToListAsync();

        var count = await query.CountAsync();
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

    #endregion
}
namespace DotNet8.BankingManagementSystem.Frontend.Features;

public class TransactionService
{
    private readonly LocalStorageService _localStorageService;

    public TransactionService(LocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<AccountResponseModel> Withdraw(AccountRequestModel requestModel)
    {
        AccountResponseModel model = new AccountResponseModel();
        var lst = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.ToString());
        var result = lst.FirstOrDefault(x => x.AccountNo == requestModel.AccountNo);

        if (result is null)
        {
            model.Response = new MessageResponseModel(false, "Account not found.");
            return model;
        }

        if (result.Balance < requestModel.Balance)
        {
            model.Response = new MessageResponseModel(false, "Insufficient balance.");
            return model;
        }

        result.Balance -= requestModel.Balance;

        lst[lst.FindIndex(x => x.AccountNo == result.AccountNo)] = result;
        await _localStorageService.SetList(EnumService.Tbl_Account.ToString(), lst);

        model.Data = result;
        model.Response = new MessageResponseModel(true, "Withdrawal successful.");

        return model;
    }

    public async Task<AccountResponseModel> Deposit(AccountRequestModel requestModel)
    {
        AccountResponseModel model = new AccountResponseModel();
        var lst = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.ToString());
        var result = lst.FirstOrDefault(x => x.AccountNo == requestModel.AccountNo);

        if (result is null)
        {
            model.Response = new MessageResponseModel(false, "Account not found.");
            return model!;
        }

        result.Balance += requestModel.Balance;

        lst[lst.FindIndex(x => x.AccountNo == result.AccountNo)] = result;

        await _localStorageService.SetList(EnumService.Tbl_Account.ToString(), lst);

        model.Data = result;
        model.Response = new MessageResponseModel(true, "Deposit successful.");

        return model!;
    }

    public async Task<TransferResponseModel> Transfer(TransferModel requestModel)
    {
        TransferResponseModel model = new TransferResponseModel();

        var lst = await _localStorageService.GetList<AccountModel>(EnumService.Tbl_Account.ToString());
        var fromAccount = lst.FirstOrDefault(x => x.AccountNo == requestModel.FromAccountNo);
        var toAccount = lst.FirstOrDefault(x => x.AccountNo == requestModel.ToAccountNo);

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
        lst[lst.FindIndex(x => x.AccountNo == toAccount.AccountNo)] = toAccount;

        await _localStorageService.SetList(EnumService.Tbl_Account.ToString(), lst);

        var lstTransaction = await _localStorageService.GetList<TblTransactionHistory>(EnumService.Tbl_TransactionHistory.ToString());
        lstTransaction.Add(new TblTransactionHistory
        {
            FromAccountNo = fromAccount.AccountNo!,
            ToAccountNo = toAccount.AccountNo!,
            Amount = requestModel.Amount,
            TransactionDate = DateTime.Now,
            TransactionHistoryId = lstTransaction.Count == 0 ? 1 : lstTransaction.Max(x => x.TransactionHistoryId) + 1,
            AdminUserCode = "admin"
        });
        await _localStorageService.SetList(EnumService.Tbl_TransactionHistory.ToString(), lstTransaction);

        model.Data = new TransferModel
        {
            FromAccountNo = fromAccount.AccountNo!,
            ToAccountNo = toAccount.AccountNo!
        };
        model.Response = new MessageResponseModel(true, "Transfer successful.");

        return model;
    }

    public async Task<TransactionHistoryListResponseModel> TransactionHistory(int pageNo, int pageSize)
    {
        TransactionHistoryListResponseModel model = new TransactionHistoryListResponseModel();
        var query = await _localStorageService.GetList<TblTransactionHistory>(EnumService.Tbl_TransactionHistory.ToString());
        var result = query.OrderByDescending(x => x.TransactionDate)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize).ToList();

        var count = query.Count();
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

    public async Task<TransactionHistoryListResponseModel> TransactionHistoryWithDateRange
        (TransactionHistorySearchModel requestModel)
    {
        TransactionHistoryListResponseModel model = new TransactionHistoryListResponseModel();
        var query = await _localStorageService.GetList<TblTransactionHistory>(EnumService.Tbl_TransactionHistory.ToString());
        var accounts = await _localStorageService.GetList<TblAccount>(EnumService.Tbl_Account.ToString());
        
        var filteredQuery = query.AsQueryable();

        if (!string.IsNullOrEmpty(requestModel.BranchCode))
        {
            var branchAccountNos = accounts.Where(x => x.BranchCode == requestModel.BranchCode).Select(x => x.AccountNo).ToList();
            filteredQuery = filteredQuery.Where(x => branchAccountNos.Contains(x.FromAccountNo) || branchAccountNos.Contains(x.ToAccountNo));
        }
        else if (!string.IsNullOrEmpty(requestModel.BankCode))
        {
            var branches = await _localStorageService.GetList<TblBranch>(EnumService.Tbl_Branch.GetKeyName());
            var bankBranchCodes = branches.Where(b => b.BankCode == requestModel.BankCode).Select(b => b.BranchCode).ToList();
            var bankAccountNos = accounts.Where(x => bankBranchCodes.Contains(x.BranchCode)).Select(x => x.AccountNo).ToList();
            filteredQuery = filteredQuery.Where(x => bankAccountNos.Contains(x.FromAccountNo) || bankAccountNos.Contains(x.ToAccountNo));
        }

        if (!requestModel.IsAll)
        {
            var fromDate = requestModel.FromDate ?? DateTime.Today;
            var toDate = requestModel.ToDate ?? DateTime.Today;
            filteredQuery = filteredQuery.Where(x => x.TransactionDate.Date >= fromDate.Date && x.TransactionDate.Date <= toDate.Date);
        }
        
        if (!string.IsNullOrEmpty(requestModel.TransactionType))
        {
            filteredQuery = filteredQuery.Where(x => x.TransactionType == requestModel.TransactionType);
        }

        var result = filteredQuery.OrderByDescending(x => x.TransactionDate)
            .Skip((requestModel.PageNo - 1) * requestModel.PageSize).Take(requestModel.PageSize)
            .ToList();
            
        var count = filteredQuery.Count();
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
}

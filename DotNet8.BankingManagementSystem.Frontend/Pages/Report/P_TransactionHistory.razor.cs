using Microsoft.AspNetCore.Components;
using DotNet8.BankingManagementSystem.Models.Bank;
using DotNet8.BankingManagementSystem.Models.Branch;
using DotNet8.BankingManagementSystem.Models.TransactionHistory;
using DotNet8.BankingManagementSystem.Models.Account;
using DotNet8.BankingManagementSystem.Frontend.Features;

namespace DotNet8.BankingManagementSystem.Frontend.Pages.Report;

public partial class P_TransactionHistory : ComponentBase
{
    private PageSettingModel _setting = new()
    {
        PageNo = 1,
        PageSize = 10
    };
    private DateTime? fromDate;
    private DateTime? toDate;
    private string? transactionType;
    private string? bankCode;
    private string? branchCode;
    private bool isAll = false;

    private List<BankModel> Banks { get; set; } = new();
    private List<BranchModel> Branches { get; set; } = new();

    private TransactionHistoryListResponseModel? _model;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadInitialData();
            await List(_setting.PageNo, _setting.PageSize);
            StateHasChanged();
        }
    }

    private async Task LoadInitialData()
    {
        var bankResponse = await ApiService.GetBanks();
        if (bankResponse.Response.IsSuccess)
        {
            Banks = bankResponse.Data;
        }
    }

    private async Task OnBankChanged(ChangeEventArgs e)
    {
        bankCode = e.Value?.ToString();
        branchCode = null;
        Branches.Clear();
        if (!string.IsNullOrEmpty(bankCode))
        {
            var branchResponse = await ApiService.GetBranchesByBankCode(bankCode);
            if (branchResponse.Response.IsSuccess)
            {
                Branches = branchResponse.Data;
            }
        }
        await Search();
    }

    private async Task List(int pageNo, int pageSize)
    {
        await InjectService.EnableLoading();
        _model = await ApiService.TransactionHistoryWithDateRange(new TransactionHistorySearchModel
        {
            FromDate = fromDate,
            ToDate = toDate,
            TransactionType = transactionType,
            BankCode = bankCode,
            BranchCode = branchCode,
            IsAll = isAll,
            PageNo = pageNo,
            PageSize = pageSize
        });
        if (_model.Response.IsError)
        {
            //
            return;
        }

        StateHasChanged();
        await InjectService.DisableLoading();
    }

    private async Task Search()
    {
        _setting.PageNo = 1;
        await List(_setting.PageNo, _setting.PageSize);
    }

    private async Task PageChanged(int i)
    {
        _setting.PageNo = i;
        await List(_setting.PageNo, _setting.PageSize);
    }
}
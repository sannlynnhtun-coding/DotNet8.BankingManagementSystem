using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.TransactionHistory;
using Microsoft.AspNetCore.Components;

namespace DotNet8.BankingManagementSystem.Frontend.Pages.Report;

public partial class P_TransactionHistory : ComponentBase
{
    private PageSettingModel _setting = new()
    {
        PageNo = 1,
        PageSize = 10
    };
    private DateTime? date = DateTime.Now;

    private TransactionHistoryListResponseModel? _model;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await List(_setting.PageNo, _setting.PageSize);
            StateHasChanged();
        }
    }

    private async Task List(int pageNo, int pageSize)
    {
        await InjectService.EnableLoading();
        _model = await TransactionAPI.TransactionHistory(pageNo, pageSize);
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
        await InjectService.EnableLoading();
        //_model = await TransactionAPI.TransactionHistoryWithDate(new TransactionHistorySearchModel
        //{
        //    FromDate = date,
        //    PageNo = 1,
        //    PageSize = 10
        //});
        _model = await TransactionAPI.TransactionHistoryWithDateRange(new TransactionHistorySearchModel
        {
            FromDate = date,
            ToDate = date,
            PageNo = 1,
            PageSize = 10
        });
        if (_model.Response.IsError)
        {
            //
            return;
        }

        StateHasChanged();
        await InjectService.DisableLoading();
    }

    private async Task PageChanged(int i)
    {
        _setting.PageNo = i;
        await List(_setting.PageNo, _setting.PageSize);
    }
}
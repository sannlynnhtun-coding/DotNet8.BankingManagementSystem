namespace DotNet8.BankingManagementSystem.Frontend.Pages.Report;

public partial class TransactionHistoryDateRange
{
    private PageSettingModel _setting = new()
    {
        PageNo = 1,
        PageSize = 10
    };
    private DateTime? date = DateTime.Now;
    DateTime? fromDate;
    DateTime? toDate;

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
        _model = await ApiService.TransactionHistory(pageNo, pageSize);
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
        _model = await ApiService.TransactionHistoryWithDateRange(new TransactionHistorySearchModel
        {
            FromDate = fromDate,
            ToDate = toDate,
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
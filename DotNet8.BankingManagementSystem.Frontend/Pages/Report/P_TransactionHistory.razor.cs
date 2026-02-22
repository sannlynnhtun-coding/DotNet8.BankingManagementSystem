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
        _model = await ApiService.TransactionHistoryWithDateRange(new TransactionHistorySearchModel
        {
            FromDate = fromDate,
            ToDate = toDate,
            TransactionType = transactionType,
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
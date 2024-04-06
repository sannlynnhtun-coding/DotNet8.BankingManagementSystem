namespace DotNet8.BankingManagementSystem.Frontend.Pages.GeneralSetup.Township;

public partial class P_Township
{
    private PageSettingModel _setting = new()
    {
        PageNo = 1,
        PageSize = 10
    };

    private TownshipListResponceModel? _model;

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
        _model = await ApiService.GetTownShipList(pageNo, pageSize);
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

    private async Task Delete(string TownshipCode)
    {
        var parameters = new DialogParameters<Dialog>();
        parameters.Add(x => x.ContentText,
            "Are you sure want to delete?");

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<Dialog>("Confirm", parameters, options);
        var result = await dialog.Result;
        if (result.Canceled) return;

        var response = await ApiService.DeleteTownship(TownshipCode);
        if (result is not null)
        {
            await InjectService.EnableLoading();
            await List(_setting.PageNo, _setting.PageSize);
            await InjectService.DisableLoading();
            await InjectService.SuccessMessage("Deleting Successful.");
        }

        StateHasChanged();
    }

    private async Task Generate()
    {
        await InjectService.EnableLoading();
        var lst = await HttpClientService.GetAsync<TownshipRequestModel>("data/township.json");
        await ApiService.CreateTownships(lst);
        await List(_setting.PageNo, _setting.PageSize);
    }
}
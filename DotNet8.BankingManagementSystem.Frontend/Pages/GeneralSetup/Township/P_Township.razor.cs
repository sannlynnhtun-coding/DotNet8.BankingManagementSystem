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
        var isConfirmed = await InjectService.ConfirmMessage("Are you sure want to delete?");
        if (!isConfirmed) return;

        await InjectService.EnableLoading();
        var response = await ApiService.DeleteTownship(TownshipCode);
        if (response is not null)
        {
            await List(_setting.PageNo, _setting.PageSize);
            await InjectService.SuccessMessage("Deleting Successful.");
        }
        await InjectService.DisableLoading();
        StateHasChanged();
    }

    private async Task Generate()
    {
        await InjectService.EnableLoading();
        var lst = await HttpClientService.GetAsync<TownshipRequestModel>("https://raw.githubusercontent.com/sannlynnhtun-coding/Banking-Management-System/main/Township.json");
        await ApiService.CreateTownships(lst);
        await List(_setting.PageNo, _setting.PageSize);
    }
}
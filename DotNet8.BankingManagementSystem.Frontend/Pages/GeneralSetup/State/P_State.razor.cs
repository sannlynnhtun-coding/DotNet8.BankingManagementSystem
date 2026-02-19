namespace DotNet8.BankingManagementSystem.Frontend.Pages.GeneralSetup.State;

public partial class P_State : ComponentBase
{
    private PageSettingModel _setting = new()
    {
        PageNo = 1,
        PageSize = 10
    };

    private StateListResponseModel? _model;

    //[Inject]
    //public InjectService InjectService { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await List(_setting.PageNo, _setting.PageSize);
        }
    }

    private async Task List(int pageNo, int pageSize)
    {
        await InjectService.EnableLoading();
        _model = await ApiService.GetStates(pageNo, pageSize);
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

    private async Task IsConfirmed(string stateCode)
    {
        var isConfirmed = await InjectService.ConfirmMessage("Are you sure want to delete?");
        if (!isConfirmed) return;
        
        await Delete(stateCode);
    }

    private async Task Delete(string stateCode)
    {
        var result = await ApiService.DeleteState(stateCode);
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
        var lst = await HttpClientService.GetAsync<StateRequestModel>("https://raw.githubusercontent.com/sannlynnhtun-coding/Banking-Management-System/main/State.json");
        await ApiService.CreateStates(lst);
        await List(_setting.PageNo, _setting.PageSize);
    }
}
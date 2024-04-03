using DotNet8.BankingManagementSystem.Models.State;

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
        // await InjectService.IsConfirmed(stateCode);
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
}
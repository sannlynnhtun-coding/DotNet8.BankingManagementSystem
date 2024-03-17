using DotNet8.BankingManagementSystem.Models.State;
using Microsoft.AspNetCore.Components;

namespace DotNet8.BankingManagementSystem.Frontend.Pages.GeneralSetup.State;

public partial class P_StateEdit
{
    [Parameter] public string stateCode { get; set; }
    private StateModel _model = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await GetStateByCode(stateCode);
            StateHasChanged();
        }
    }

    private async Task GetStateByCode(string stateCode)
    {
        await InjectService.EnableLoading();
        var result = await StateApi.GetStateByCode(stateCode);
        if (result.Response.IsError)
        {
            //
            return;
        }

        _model = result.Data;
        StateHasChanged();
        await InjectService.DisableLoading();
    }

    private async Task OnValidSubmit()
    {
        var reqModel = new StateRequestModel
        {
            StateCode = _model.StateCode,
            StateName = _model.StateName
        };

        var response = await StateApi.UpdateState(stateCode, reqModel);
        await InjectService.Go("/general-setup/state");
        await InjectService.SuccessMessage("Updating Successful.");
        StateHasChanged();
    }
}
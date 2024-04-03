namespace DotNet8.BankingManagementSystem.Frontend.Pages.GeneralSetup.Township;

public partial class P_TownshipEdit
{
    [Parameter] public string townShipCode { get; set; }

    private TownshipModel _model = new TownshipModel();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await GetTownShipByCode(townShipCode);
            StateHasChanged();
        }
    }

    private async Task GetTownShipByCode(string townshipCode)
    {
        await InjectService.EnableLoading();
        var result = await ApiService.GetTownShipByCode(townShipCode);
        if (result.Response.IsError)
        {
            return;
        }

        _model = result.Data;
        StateHasChanged();
        await InjectService.DisableLoading();
    }

    private async Task OnValidSubmit()
    {
        var reqModel = new TownshipRequestModel
        {
            TownshipCode = _model.TownshipCode,
            TownshipName = _model.TownshipName,
            StateCode = _model.StateCode,
        };
        var response = await ApiService.UpdateTownship(townShipCode, reqModel);
        await InjectService.SuccessMessage("Updating Successful.");
        Nav.NavigateTo("/general-setup/township");
        StateHasChanged();
    }
}
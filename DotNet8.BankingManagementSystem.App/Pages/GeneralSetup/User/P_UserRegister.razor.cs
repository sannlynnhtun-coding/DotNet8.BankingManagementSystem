using DotNet8.BankingManagementSystem.Models.State;
using DotNet8.BankingManagementSystem.Models.Users;
using DotNet8.BankingManagementSystem.Models.TownShip;
using Microsoft.AspNetCore.Components;

namespace DotNet8.BankingManagementSystem.App.Pages.GeneralSetup.User;

public partial class P_UserRegister
{
    private UserRequestModel _model = new();
    private StateListResponseModel? _stateListResponseModel;
    private TownshipListResponceModel? _townshipListResponceModel;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _stateListResponseModel = await StateApi.GetStates();
            StateHasChanged();
        }
    }

    private async Task OnValidSubmit()
    {
        try
        {
            var response = await UserApi.CreateUser(_model);
            await InjectService.Go("/general-setup/user");
            await InjectService.SuccessMessage("Registration Successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async Task ChangeState(string stateCode)
    {
        _model.StateCode = stateCode;
        _townshipListResponceModel = await TownshipApi.GetTownShipByStateCode(_model.StateCode);
        StateHasChanged();
    }
}
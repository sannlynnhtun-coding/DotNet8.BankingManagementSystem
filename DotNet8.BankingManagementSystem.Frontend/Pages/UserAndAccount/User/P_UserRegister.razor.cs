namespace DotNet8.BankingManagementSystem.Frontend.Pages.UserAndAccount.User;

public partial class P_UserRegister : ComponentBase
{
    private UserRequestModel _model = new();
    private StateListResponseModel? _stateListResponseModel;
    private TownshipListResponceModel? _townshipListResponceModel;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _stateListResponseModel = await ApiService.GetStates();
            StateHasChanged();
        }
    }

    private async Task OnValidSubmit()
    {
        try
        {
            await InjectService.EnableLoading();
            var response = await ApiService.CreateUser(_model);
            if (response.Response.IsError) return;

            await InjectService.SuccessMessage("Registration Successful.");
            await InjectService.Go("/user-and-account/user");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            await InjectService.DisableLoading();
        }
    }

    private async Task ChangeState(string stateCode)
    {
        _model.StateCode = stateCode;
        _townshipListResponceModel = await ApiService.GetTownShipByStateCode(_model.StateCode);
        StateHasChanged();
    }
}
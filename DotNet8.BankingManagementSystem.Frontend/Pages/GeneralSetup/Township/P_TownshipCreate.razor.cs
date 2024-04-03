namespace DotNet8.BankingManagementSystem.Frontend.Pages.GeneralSetup.Township;

public partial class P_TownshipCreate
{
    private TownshipRequestModel _model = new();

    private async Task OnValidSubmit(EditContext context)
    {
        try
        {
            var response = await ApiService.CreateTownship(_model);
            StateHasChanged();
        }
        catch (Exception ex)
        {
            //Error
            return;
        }

        await InjectService.SuccessMessage("Create Township Success");
        Nav.NavigateTo("/general-setup/township");
    }
}
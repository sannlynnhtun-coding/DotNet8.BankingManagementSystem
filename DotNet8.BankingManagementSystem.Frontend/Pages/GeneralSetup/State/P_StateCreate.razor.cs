using DotNet8.BankingManagementSystem.Models.State;

namespace DotNet8.BankingManagementSystem.Frontend.Pages.GeneralSetup.State;

public partial class P_StateCreate
{
    private StateRequestModel _model = new();

    private async Task OnValidSubmit()
    {
        try
        {
            var response = await ApiService.CreateState(_model);
            await InjectService.Go("/general-setup/state");
            await InjectService.SuccessMessage("Creating Successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
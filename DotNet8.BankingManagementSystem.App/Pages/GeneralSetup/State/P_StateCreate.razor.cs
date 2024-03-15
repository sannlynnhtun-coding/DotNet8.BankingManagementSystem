using DotNet8.BankingManagementSystem.Models.State;
using Microsoft.AspNetCore.Components.Forms;

namespace DotNet8.BankingManagementSystem.App.Pages.GeneralSetup.State;

public partial class P_StateCreate
{
    private StateRequestModel _model = new();

    private async Task OnValidSubmit()
    {
        try
        {
            var response = await StateApi.CreateState(_model);
            await InjectService.Go("/general-setup/state");
            await InjectService.SuccessMessage("Creating Successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
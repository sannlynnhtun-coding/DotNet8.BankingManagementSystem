using DotNet8.BankingManagementSystem.Models.State;
using Microsoft.AspNetCore.Components.Forms;

namespace DotNet8.BankingManagementSystem.App.Pages.GeneralSetup.State
{
    public partial class P_StateCreate
    {
        private StateRequestModel _model = new StateRequestModel();
        private async Task OnValidSubmit(EditContext context)
        {
            try
            {
                var response = await StateApi.CreateState(_model);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                //Error
                return;
            }
            await InjectService.Go("/general-setup/state");
        }
    }
}

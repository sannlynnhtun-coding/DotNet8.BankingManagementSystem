using DotNet8.BankingManagementSystem.App.Api;
using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.State;
using DotNet8.BankingManagementSystem.Models.TownShip;
using DotNet8.BankingManagementSystem.Models.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DotNet8.BankingManagementSystem.App.Pages.GeneralSetup.Township
{
    public partial class P_TownshipCreate
    {
        private TownshipRequestModel _model = new();

        private async Task OnValidSubmit(EditContext context)
        {
            try
            {
                var response = await TownshipApi.CreateTownship(_model);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                //Error
                return;
            }

            await InjectService.SuccessMessage("Create Township Success");
            NavigationManager.NavigateTo("/general-setup/township");
        }
    }
}
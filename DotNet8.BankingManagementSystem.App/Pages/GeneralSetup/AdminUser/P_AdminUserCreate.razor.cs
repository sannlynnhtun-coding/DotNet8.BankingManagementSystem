using DotNet8.BankingManagementSystem.App.Api;
using DotNet8.BankingManagementSystem.Models.AdminUser;
using DotNet8.BankingManagementSystem.Models.TownShip;
using Microsoft.AspNetCore.Components.Forms;

namespace DotNet8.BankingManagementSystem.App.Pages.GeneralSetup.AdminUser
{
    public partial class P_AdminUserCreate
    {
        private AdminUserRequestModel _model = new();

        private async Task OnValidSubmit(EditContext context)
        {
            try
            {
                var response = await AdminUserAPI.CreateAdminUser(_model);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                //Error
                return;
            }

            await InjectService.SuccessMessage("Create AdminUser Success");
            NavigationManager.NavigateTo("/general-setup/adminuser");
        }
    }
}
using DotNet8.BankingManagementSystem.Models.AdminUser;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DotNet8.BankingManagementSystem.Frontend.Pages.UserAndAccount.Admin;

public partial class P_AdminUserCreate : ComponentBase
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
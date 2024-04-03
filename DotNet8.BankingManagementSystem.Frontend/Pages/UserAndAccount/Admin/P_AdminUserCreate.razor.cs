namespace DotNet8.BankingManagementSystem.Frontend.Pages.UserAndAccount.Admin;

public partial class P_AdminUserCreate : ComponentBase
{
    private AdminUserRequestModel _model = new();

    private async Task OnValidSubmit(EditContext context)
    {
        try
        {
            var response = await ApiService.CreateAdminUser(_model);
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
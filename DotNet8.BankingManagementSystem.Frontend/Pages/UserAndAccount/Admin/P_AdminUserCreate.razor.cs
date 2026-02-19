namespace DotNet8.BankingManagementSystem.Frontend.Pages.UserAndAccount.Admin;

public partial class P_AdminUserCreate : ComponentBase
{
    private AdminUserRequestModel _model = new();

    private async Task OnValidSubmit(EditContext context)
    {
        try
        {
            await InjectService.EnableLoading();
            var response = await ApiService.CreateAdminUser(_model);
            if (response.Response.IsError) return;

            await InjectService.SuccessMessage("Create AdminUser Success");
            await InjectService.Go("/user-and-account/admin-user");
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
}
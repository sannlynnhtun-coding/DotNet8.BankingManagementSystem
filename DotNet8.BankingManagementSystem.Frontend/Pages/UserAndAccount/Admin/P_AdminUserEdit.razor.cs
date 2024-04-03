namespace DotNet8.BankingManagementSystem.Frontend.Pages.UserAndAccount.Admin;

public partial class P_AdminUserEdit : ComponentBase
{
    [Parameter] public string adminUserCode { get; set; }

    private AdminUserModel _model = new AdminUserModel();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await GetAdminUserByCode(adminUserCode);
            StateHasChanged();
        }
    }

    private async Task GetAdminUserByCode(string adminUserCode)
    {
        await InjectService.EnableLoading();
        var result = await ApiService.GetAdminUserByCode(adminUserCode);
        if (result.Response.IsError)
        {
            return;
        }

        _model = result.Data;
        StateHasChanged();
        await InjectService.DisableLoading();
    }

    private async Task OnValidSubmit()
    {
        var reqModel = new AdminUserRequestModel
        {
            AdminUserCode = _model.AdminUserCode,
            AdminUserName = _model.AdminUserName,
            MobileNo = _model.MobileNo,
            UserRoleCode = _model.UserRoleCode,
        };
        var response = await ApiService.UpdateAdminUser(reqModel);
        await InjectService.SuccessMessage("Updating Successful.");
        Nav.NavigateTo("/general-setup/adminUser");
        StateHasChanged();
    }
}
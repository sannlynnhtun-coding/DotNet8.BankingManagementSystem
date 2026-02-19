namespace DotNet8.BankingManagementSystem.Frontend.Pages.UserAndAccount.Admin;

public partial class P_AdminUser : ComponentBase
{
    private PageSettingModel _setting = new PageSettingModel
    {
        PageNo = 1,
        PageSize = 10
    };

    private AdminUserListResponseModel _model = new AdminUserListResponseModel();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await List(_setting.PageNo, _setting.PageSize);
            StateHasChanged();
        }
    }

    private async Task List(int pageNo, int pageSize)
    {
        await InjectService.EnableLoading();
        _model = await ApiService.GetAdminUserList(pageNo, pageSize);
        if (_model.Response.IsError)
        {
            return;
        }

        StateHasChanged();
        await InjectService.DisableLoading();
    }

    private async Task Delete(string adminUserCode)
    {
        var isConfirmed = await InjectService.ConfirmMessage("Are you sure want to delete?");
        if (!isConfirmed) return;

        var result = await ApiService.DeleteAdminUser(adminUserCode);
        if (result.Response.IsError) return;

        await InjectService.EnableLoading();
        await List(_setting.PageNo, _setting.PageSize);
        await InjectService.DisableLoading();
        await InjectService.SuccessMessage("Deleting Successful.");

        StateHasChanged();
    }

    private async Task PageChanged(int i)
    {
        _setting.PageNo = i;
        await List(_setting.PageNo, _setting.PageSize);
    }
}
using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.AdminUser;
using Microsoft.AspNetCore.Components;
using MudBlazor;

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
        _model = await AdminUserAPI.GetAdminUserList(pageNo, pageSize);
        if (_model.Response.IsError)
        {
            return;
        }

        StateHasChanged();
        await InjectService.DisableLoading();
    }

    private async Task Delete(string AdminUserCode)
    {
        var parameters = new DialogParameters<Dialog>();
        parameters.Add(x => x.ContentText,
            "Are you sure want to delete?");

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<Dialog>("Confirm", parameters, options);
        var result = await dialog.Result;
        if (result.Canceled) return;

        //var adminUserResult = await AdminUserAPI.DeleteAdminUser(AdminUserCode);
        var adminUserResult = await AdminUserAPI.DeleteAdminUser(AdminUserCode);
        if (result is not null)
        {
            await InjectService.EnableLoading();
            await List(_setting.PageNo, _setting.PageSize);
            await InjectService.DisableLoading();
            await InjectService.SuccessMessage("Deleting Successful.");
        }

        StateHasChanged();
    }

    private async Task PageChanged(int i)
    {
        _setting.PageNo = i;
        await List(_setting.PageNo, _setting.PageSize);
    }
}
using DotNet8.BankingManagementSystem.Models.Users;
using MudBlazor;

namespace DotNet8.BankingManagementSystem.Frontend.Pages.UserAndAccount.User;

public partial class P_User : ComponentBase
{
    private PageSettingModel _setting = new()
    {
        PageNo = 1,
        PageSize = 10
    };

    private UserListResponseModel? _model;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            //CallbackMethodNo();
            await List(_setting.PageNo, _setting.PageSize);
        }
    }

    private async Task List(int pageNo, int pageSize)
    {
        await InjectService.EnableLoading();
        _model = await UserApi.GetUserList(pageNo, pageSize);
        if (_model.Response.IsError)
        {
            //
            return;
        }

        StateHasChanged();
        await InjectService.DisableLoading();
    }

    private async Task PageChanged(int i)
    {
        _setting.PageNo = i;
        await List(_setting.PageNo, _setting.PageSize);
        //Nav.NavigateTo("/general-setup/user");
    }

    private async Task Delete(string UserCode)
    {
        var parameters = new DialogParameters<Dialog>();
        parameters.Add(x => x.ContentText,
            "Are you sure want to delete?");

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<Dialog>("Confirm", parameters, options);
        var result = await dialog.Result;
        if (result.Canceled) return;

        var response = await UserApi.DeleteUser(UserCode);
        if (result is not null)
        {
            await InjectService.EnableLoading();
            await List(_setting.PageNo, _setting.PageSize);
            await InjectService.DisableLoading();
            await InjectService.SuccessMessage("Deleting Successful.");
        }

        StateHasChanged();
    }
}
using DotNet8.BankingManagementSystem.Frontend.Pages.UserAndAccount.User;

namespace DotNet8.BankingManagementSystem.Frontend.Pages.UserAndAccount.Account;

public partial class P_Account : ComponentBase
{
    private PageSettingModel _setting = new()
    {
        PageNo = 1,
        PageSize = 10
    };

    private AccountListResponseModel? _model;
    private UserListResponseModel _users;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // await InjectService.LoadJavaScript();
            _users = await ApiService.GetUserList(0);
            await List(_setting.PageNo, _setting.PageSize);
        }
    }

    private async Task List(int pageNo, int pageSize)
    {
        await InjectService.EnableLoading();
        _model = await ApiService.GetAccountList(pageNo, pageSize);
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
    }

    private async Task IsConfirmed(string accountNo)
    {
        var isConfirmed = await InjectService.ConfirmMessage("Are you sure want to delete?");
        if (!isConfirmed) return;
        
        await Delete(accountNo);
    }

    private async Task Delete(string accountNo)
    {
        var result = await ApiService.DeleteAccount(accountNo);
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
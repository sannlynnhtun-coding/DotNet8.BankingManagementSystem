using DotNet8.BankingManagementSystem.Models.Account;
using Microsoft.AspNetCore.Components;

namespace DotNet8.BankingManagementSystem.Frontend.Pages.UserAndAccount.Account;

public partial class P_AccountEdit : ComponentBase
{
    [Parameter] public string accountNo { get; set; }
    private AccountModel _model = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await GetAccount(accountNo);
            StateHasChanged();
        }
    }

    private async Task GetAccount(string accountNo)
    {
        await InjectService.EnableLoading();
        var result = await AccountApi.GetAccount(accountNo);
        if (result.Response.IsError)
        {
            //
            return;
        }

        _model = result.Data;
        StateHasChanged();
        await InjectService.DisableLoading();
    }

    private async Task OnValidSubmit()
    {
        var reqModel = new AccountRequestModel()
        {
            CustomerCode = _model.CustomerCode,
            Balance = _model.Balance
        };

        var response = await AccountApi.UpdateAccount(accountNo, reqModel);
        await InjectService.Go("/general-setup/account");
        await InjectService.SuccessMessage("Updating Successful.");
        StateHasChanged();
    }
}
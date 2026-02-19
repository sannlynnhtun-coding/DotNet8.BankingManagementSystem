using DotNet8.BankingManagementSystem.Models.Users;

namespace DotNet8.BankingManagementSystem.Frontend.Pages.Transaction.Transfer;

public partial class P_Transfer : ComponentBase
{
    private TransferModel reqModel = new();
    private AccountRequestModel _model = new();
    private AccountListResponseModel _accountListResponseModel = new AccountListResponseModel();
    private UserListResponseModel _userListResponseModel = new UserListResponseModel();
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _accountListResponseModel = await ApiService.GetAccountList(0);
            _userListResponseModel = await ApiService.GetUserList(0);
            StateHasChanged();
        }
    }

    private async Task OnValidSubmit(EditContext context)
    {
        try
        {
            await InjectService.EnableLoading();
            var response = await ApiService.Transfer(reqModel);
            if (response.Response.IsError) return;

            await InjectService.SuccessMessage("Balance transfer Successful.");
            await InjectService.Go("/user-and-account/account");
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

    private decimal GetFromAccountBalance()
    {
        if (string.IsNullOrEmpty(reqModel.FromAccountNo) || _accountListResponseModel?.Data == null)
            return 0;

        return _accountListResponseModel.Data
            .FirstOrDefault(x => x.AccountNo == reqModel.FromAccountNo)?
            .Balance ?? 0;
    }

    private decimal GetToAccountBalance()
    {
        if (string.IsNullOrEmpty(reqModel.ToAccountNo) || _accountListResponseModel?.Data == null)
            return 0;

        return _accountListResponseModel.Data
            .FirstOrDefault(x => x.AccountNo == reqModel.ToAccountNo)?
            .Balance ?? 0;
    }
}
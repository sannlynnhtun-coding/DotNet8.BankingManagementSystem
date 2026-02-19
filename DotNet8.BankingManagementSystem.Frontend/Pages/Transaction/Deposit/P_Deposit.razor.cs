namespace DotNet8.BankingManagementSystem.Frontend.Pages.Transaction.Deposit;

public partial class P_Deposit : ComponentBase
{
    private AccountRequestModel _model = new();
    private AccountListResponseModel? _accountListResponseModel;
    private UserListResponseModel? _userListResponseModel;

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
            var response = await ApiService.Deposit(_model);
            if (response.Response.IsError) return;

            await InjectService.SuccessMessage("Deposit Successful.");
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

    private decimal GetAccountBalance()
    {
        if (string.IsNullOrEmpty(_model.AccountNo) || _accountListResponseModel?.Data == null)
            return 0;

        return _accountListResponseModel.Data
            .FirstOrDefault(x => x.AccountNo == _model.AccountNo)?
            .Balance ?? 0;
    }
}
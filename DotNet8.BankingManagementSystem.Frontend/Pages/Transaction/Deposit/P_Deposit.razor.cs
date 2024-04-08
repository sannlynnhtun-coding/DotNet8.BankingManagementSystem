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
            var response = await ApiService.Deposit(_model);
            await InjectService.Go("/user-and-account/account");
            await InjectService.SuccessMessage("Deposit Successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
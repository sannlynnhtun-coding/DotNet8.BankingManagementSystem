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
        }
    }

    private async Task OnValidSubmit(EditContext context)
    {
        try
        {
            var response = await ApiService.Transfer(reqModel);
            await InjectService.SuccessMessage("Balance transfer Successful.");
            await InjectService.Go("/report/transaction-history");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
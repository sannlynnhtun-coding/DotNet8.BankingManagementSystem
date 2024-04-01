namespace DotNet8.BankingManagementSystem.Frontend.Pages.Transaction.Withdraw;

public partial class P_Withdraw : ComponentBase
{
    private TransactionRequestModel _model = new();

    private async Task OnValidSubmit(EditContext context)
    {
        try
        {
            var response = await TransactionApi.Withdraw(_model);
            await InjectService.Go("/user-and-account/account");
            await InjectService.SuccessMessage("Withdraw Successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
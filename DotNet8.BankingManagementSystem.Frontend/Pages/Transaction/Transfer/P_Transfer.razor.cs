namespace DotNet8.BankingManagementSystem.Frontend.Pages.Transaction.Transfer;

public partial class P_Transfer : ComponentBase
{
    private TransferModel _model = new();

    private async Task OnValidSubmit(EditContext context)
    {
        try
        {
            var response = await ApiService.Transfer(_model);
            await InjectService.SuccessMessage("Balance transfer Successful.");
            await InjectService.Go("/report/transaction-history");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
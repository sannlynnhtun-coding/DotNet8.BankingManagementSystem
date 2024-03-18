using DotNet8.BankingManagementSystem.Models.Transfer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DotNet8.BankingManagementSystem.Frontend.Pages.Transaction.Transfer;

public partial class P_Transfer : ComponentBase
{
    private TransferModel _model = new();

    private async Task OnValidSubmit(EditContext context)
    {
        try
        {
            var response = await TransactionApi.Transfer(_model);
             await InjectService.Go("/report/transaction-history");
            await InjectService.SuccessMessage("Balance transfer Successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
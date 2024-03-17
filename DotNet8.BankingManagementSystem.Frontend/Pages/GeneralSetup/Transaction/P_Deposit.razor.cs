using DotNet8.BankingManagementSystem.Models.Account;
using Microsoft.AspNetCore.Components.Forms;

namespace DotNet8.BankingManagementSystem.Frontend.Pages.GeneralSetup.Transaction;

public partial class P_Deposit

{
    private TransactionRequestModel _model = new();

    private async Task OnValidSubmit(EditContext context)
    {
        try
        {
            var response = await TransactionApi.Deposit(_model);
            await InjectService.SuccessMessage("Deposit Successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
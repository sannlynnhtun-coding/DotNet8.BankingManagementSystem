using DotNet8.BankingManagementSystem.Models.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DotNet8.BankingManagementSystem.Frontend.Pages.Transaction.Deposit;

public partial class P_Deposit : ComponentBase
{
    private TransactionRequestModel _model = new();

    private async Task OnValidSubmit(EditContext context)
    {
        try
        {
            var response = await TransactionApi.Deposit(_model);
            await InjectService.Go("/user-and-account/account");
            await InjectService.SuccessMessage("Deposit Successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
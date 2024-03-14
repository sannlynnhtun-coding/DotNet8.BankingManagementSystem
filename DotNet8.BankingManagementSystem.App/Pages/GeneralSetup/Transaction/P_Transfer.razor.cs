using DotNet8.BankingManagementSystem.Models.Transfer;
using Microsoft.AspNetCore.Components.Forms;

namespace DotNet8.BankingManagementSystem.App.Pages.GeneralSetup.Transaction
{
    public partial class P_Transfer
    {
        private TransferModel _model = new();

        private async Task OnValidSubmit(EditContext context)
        {
            try
            {
                var response = await TransactionApi.Transfer(_model);
                // await InjectService.Go("/general-setup/tran");
                await InjectService.SuccessMessage("Balance transfer Successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
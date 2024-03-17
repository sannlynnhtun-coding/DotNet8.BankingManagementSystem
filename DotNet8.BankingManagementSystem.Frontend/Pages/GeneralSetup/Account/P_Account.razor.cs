using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.Account;
using Microsoft.AspNetCore.Components;

namespace DotNet8.BankingManagementSystem.Frontend.Pages.GeneralSetup.Account
{
    public partial class P_Account : ComponentBase
    {
        private PageSettingModel _setting = new()
        {
            PageNo = 1,
            PageSize = 10
        };

        private AccountListResponseModel? _model;

        //[Inject]
        //public InjectService InjectService { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // await InjectService.LoadJavaScript();
                await List(_setting.PageNo, _setting.PageSize);
            }
        }

        private async Task List(int pageNo, int pageSize)
        {
            await InjectService.EnableLoading();
            _model = await AccountApi.GetAccountList(pageNo, pageSize);
            if (_model.Response.IsError)
            {
                //
                return;
            }

            StateHasChanged();
            await InjectService.DisableLoading();
        }

        private async Task PageChanged(int i)
        {
            _setting.PageNo = i;
            await List(_setting.PageNo, _setting.PageSize);
        }

        private async Task IsConfirmed(string accountNo)
        {
            // await InjectService.IsConfirmed(stateCode);
            await Delete(accountNo);
        }

        private async Task Delete(string accountNo)
        {
            var result = await AccountApi.DeleteAccount(accountNo);
            if (result is not null)
            {
                await InjectService.EnableLoading();
                await List(_setting.PageNo, _setting.PageSize);
                await InjectService.DisableLoading();
                await InjectService.SuccessMessage("Deleting Successful.");
            }

            StateHasChanged();
        }
    }
}
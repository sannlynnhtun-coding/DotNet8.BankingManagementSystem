using DotNet8.BankingManagementSystem.App.Api;
using DotNet8.BankingManagementSystem.Models.State;
using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.Users;

namespace DotNet8.BankingManagementSystem.App.Pages.GeneralSetup.Users
{
    public partial class P_User
    {
        private PageSettingModel _setting = new PageSettingModel()
        {
            PageNo = 1,
            PageSize = 10
        };

        private UserListResponseModel? _model;
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await List(_setting.PageNo, _setting.PageSize);
            }
        }
        private async Task List(int pageNo, int pageSize)
        {
            _model = await UserApi.GetStates(pageNo, pageSize);
            if (_model.Response.IsError)
            {
                //
                return;
            }
            StateHasChanged();
        }
        private async Task PageChanged(int i)
        {
            _setting.PageNo = i;
            await List(_setting.PageNo, _setting.PageSize);
        }

    }
}

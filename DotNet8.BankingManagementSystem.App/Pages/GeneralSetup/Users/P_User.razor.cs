using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.Users;
using Microsoft.AspNetCore.Components;

namespace DotNet8.BankingManagementSystem.App.Pages.GeneralSetup.Users
{
    public partial class P_User : ComponentBase
    {
        private PageSettingModel _setting = new PageSettingModel()
        {
            PageNo = 1,
            PageSize = 10
        };

        private UserListResponseModel? _model;
        protected override async Task OnAfterRenderAsync(bool firstRender)
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

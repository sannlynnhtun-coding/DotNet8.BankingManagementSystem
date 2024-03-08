using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DotNet8.BankingManagementSystem.App.Pages.GeneralSetup.User
{
    public partial class P_User : ComponentBase
    {
        private PageSettingModel _setting = new()
        {
            PageNo = 1,
            PageSize = 10
        };

        private UserListResponseModel? _model;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //CallbackMethodNo();
                await List(_setting.PageNo, _setting.PageSize);
            }
        }

        private async Task List(int pageNo, int pageSize)
        {
            await InjectService.EnableLoading();
            _model = await UserApi.GetStates(pageNo, pageSize);
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
            //Nav.NavigateTo("/general-setup/user");
        }

        //[JSInvokable]
        //public void CallbackMethodNo()
        //{
        //    NotificationService.ShowLoadingAsync("Please wait a moment!");
        //    Task.Run(async () =>
        //    {
        //        await Task.Delay(3000);
        //        await NotificationService.HideLoadingAsync();
        //        StateHasChanged();
        //    });
        //}
    }
}

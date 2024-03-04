using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.State;
using Microsoft.AspNetCore.Components;

namespace DotNet8.BankingManagementSystem.App.Pages.GeneralSetup.State
{
    public partial class P_State : ComponentBase
    {
        //protected override Task OnInitializedAsync()
        //{
        //    return base.OnInitializedAsync();
        //}

        private PageSettingModel _setting = new PageSettingModel()
        {
            PageNo = 1,
            PageSize = 10
        };

        private StateListResponseModel? _model;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await List(_setting.PageNo, _setting.PageSize);
            }
        }

        private async Task List(int pageNo, int pageSize)
        {
            _model = await StateApi.GetStates(pageNo, pageSize);
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

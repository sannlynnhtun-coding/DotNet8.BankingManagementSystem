using DotNet8.BankingManagementSystem.Models.State;
using DotNet8.BankingManagementSystem.Models.Users;
using DotNet8.BankingManagementSystem.Models;
using DotNet8.BankingManagementSystem.Models.TownShip;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;

namespace DotNet8.BankingManagementSystem.App.Pages.GeneralSetup.User
{
    public partial class P_UserRegister
    {
        private UserRequestModel _model = new UserRequestModel();
        private UserResponseModel? _item;
        private StateListResponseModel? _stateListResponseModel;
        private TownshipListResponceModel? _townshipListResponceModel;

        private PageSettingModel _setting = new PageSettingModel()
        {
            PageNo = 1,
            PageSize = 10
        };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _stateListResponseModel = await StateApi.GetStates();
                StateHasChanged();
            }
        }

        private async Task OnValidSubmit(EditContext context)
        {
            try
            {
                await Add(_model);
            }
            catch (Exception ex)
            {
                //Error
                return;
            }
        }

        public async Task Add(UserRequestModel requestModel)
        {
            _item = await UserApi.CreateUser(requestModel);

            StateHasChanged();
        }

        private async Task ChangeState(string stateCode)
        {
            _model.StateCode = stateCode;
            _townshipListResponceModel = await TownshipApi.GetTownShipByStateCode(_model.StateCode);
            StateHasChanged();
        }

        //[JSInvokable]
        //public void CallbackMethodSuccess(string param1, int param2)
        //{
        //    // This block run when click button in notiflix toast or dialog
        //}
        //public void CallbackMethodNo(string param1, int param2)
        //{
        //    NotificationService.ToastWarningAsync("[message]");
        //    NotificationService.ToastWarningAsync("[message]", this, nameof(CallbackMethodSuccess), "param 1 value", 2222);

        //}
        [JSInvokable]
        public void CallbackMethodNo()
        {
            NotificationService.ShowLoadingAsync("Please wait a moment!");
            Task.Run(async () =>
            {
                await Task.Delay(3000);
                await NotificationService.HideLoadingAsync();
                StateHasChanged();
            });

        }
    }
}

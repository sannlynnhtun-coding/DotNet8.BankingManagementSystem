using DotNet8.BankingManagementSystem.Models.State;
using Microsoft.AspNetCore.Components;

namespace DotNet8.BankingManagementSystem.App.Pages.GeneralSetup.State
{
    public partial class P_StateEdit
    {
        [Parameter]
        public string stateCode { get; set; }
        // private StateResponseModel _model = new StateResponseModel();
        private StateModel _model = new();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetStateByCode(stateCode);
            }
        }
        
        private async Task GetStateByCode(string stateCode)
        {
            await InjectService.EnableLoading();
            var result = await StateApi.GetStateByCode(stateCode);
            if (result.Response.IsError)
            {
                //
                return;
            }

            _model = result.Data;

            StateHasChanged();
            await InjectService.DisableLoading();
        }

        private async Task OnValidSubmit()
        {
            try
            {
                // var response = await StateApi.CreateState(_model);
                // await InjectService.Go("/general-setup/state");
                // await InjectService.SuccessMessage("Creating Successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
        }
    }
}
using DotNet8.BankingManagementSystem.Models.State;

namespace DotNet8.BankingManagementSystem.App.Pages.GeneralSetup.State
{
    public partial class P_StateEdit
    {
        private StateResponseModel _model;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetStateByCode(_model.Data.StateCode);
            }
        }
        private async Task GetStateByCode(string stateCode)
        {
            await InjectService.EnableLoading();
            _model = await StateApi.GetStateByCode(stateCode);
            if (_model.Response.IsError)
            {
                //
                return;
            }

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
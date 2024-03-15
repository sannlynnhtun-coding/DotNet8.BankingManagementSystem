using DotNet8.BankingManagementSystem.App.Api;
using DotNet8.BankingManagementSystem.Models.AdminUser;
using DotNet8.BankingManagementSystem.Models.TownShip;
using Microsoft.AspNetCore.Components;

namespace DotNet8.BankingManagementSystem.App.Pages.GeneralSetup.AdminUser
{
    public partial class P_AdminUserEdit
    {
        [Parameter] public string AdminUserCode { get; set; }

        private AdminUserModel _model = new AdminUserModel();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetAdminUser(AdminUserCode);
                StateHasChanged();
            }
        }

        private async Task GetAdminUser(string AdminUserCode)
        {
            await InjectService.EnableLoading();
            var result = await AdminUserAPI.GetAdminUser(AdminUserCode);
            if (result.Response.IsError)
            {
                return;
            }

            _model = result.Data;
            StateHasChanged();
            await InjectService.DisableLoading();
        }

        private async Task OnValidSubmit()
        {
            var reqModel = new AdminUserRequestModel
            {
                AdminUserCode = _model.AdminUserCode,
                AdminUserName = _model.AdminUserName,
                MobileNo = _model.MobileNo,
                UserRoleCode = _model.UserRoleCode,
            };
            var response = await AdminUserAPI.UpdateAdminUser(AdminUserCode, reqModel);
            await InjectService.SuccessMessage("Updating Successful.");
            Nav.NavigateTo("/general-setup/adminUser");
            StateHasChanged();
        }
    }
}
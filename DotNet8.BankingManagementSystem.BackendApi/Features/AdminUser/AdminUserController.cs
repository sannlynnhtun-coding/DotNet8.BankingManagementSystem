using DotNet8.BankingManagementSystem.Models.AdminUser;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.BankingManagementSystem.BackendApi.Features.AdminUser
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminUserController : BaseController
    {
        private readonly AdminUserService _adminUserService;

        public AdminUserController(AdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        #region GetAdminUsers

        [HttpGet]
        public async Task<IActionResult> GetAdminUser()
        {
            try
            {
                var model = await _adminUserService.GetAdminUsers();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region GetAdminUserszByPagination

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetAdminUserList(int pageNo, int pageSize)
        {
            try
            {
                var model = await _adminUserService.GetAdminUsersList(pageNo, pageSize);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region GetAdminUserByAdminUserCode

        [HttpGet("{AdminUserCode}")]
        public async Task<IActionResult> GetAdminUser(string AdminUserCode)
        {
            try
            {
                var model = await _adminUserService.GetAdminUser(AdminUserCode);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region CreateAdminUser

        [HttpPost]
        public async Task<IActionResult> CreateAdminUser([FromBody] AdminUserRequestModel requestModel)
        {
            try
            {
                var model = await _adminUserService.CreateAdminUser(requestModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region DeleteAdminUser

        [HttpDelete("{AdminUserCode}")]
        public async Task<IActionResult> DeleteAdminUser(string AdminUserCode)
        {
            try
            {
                var model = await _adminUserService.DeleteAdminUser(AdminUserCode);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region UpdateAdminUser

        [HttpPut("{AdminUserCode}")]
        public async Task<IActionResult> UpdateAdminUser(string AdminUserCode, AdminUserRequestModel reqModel)
        {
            try
            {
                var model = await _adminUserService.UpdateAdminUser(AdminUserCode, reqModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion
    }
}
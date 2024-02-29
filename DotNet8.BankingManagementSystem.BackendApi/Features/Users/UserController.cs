using Microsoft.AspNetCore.Mvc;

namespace DotNet8.BankingManagementSystem.BackendApi.Features.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly UserServices _userService;

        public UserController(UserServices userService)
        {
            _userService = userService;
        }

        #region GetUser

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetUserList(int pageNo, int pageSize)
        {
            try
            {
                var model = await _userService.GetUserList(pageNo, pageSize);
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
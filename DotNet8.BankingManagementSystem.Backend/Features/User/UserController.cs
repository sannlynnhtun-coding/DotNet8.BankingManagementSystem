namespace DotNet8.BankingManagementSystem.Backend.Features.User;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly UserServices _userService;

    public UserController(UserServices userService)
    {
        _userService = userService;
    }

    #region Get Users

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

    #region Get User

    [HttpGet("{userCode}")]
    public async Task<IActionResult> GetUserByCode(string userCode)
    {
        try
        {
            var model = await _userService.GetUserByCode(userCode);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion

    #region Create User

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserRequestModel requestModel)
    {
        try
        {
            var model = await _userService.CreateUser(requestModel);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion

    #region Update User info

    [HttpPut("{userCode}")]
    public async Task<IActionResult> UpdateUserInfo(string userCode, [FromBody] UserRequestModel requestModel)
    {
        try
        {
            var model = await _userService.UpdateUserInfo(userCode, requestModel);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion

    #region Delete User

    [HttpDelete("{userCode}")]
    public async Task<IActionResult> DeleteUser(string userCode)
    {
        try
        {
            var model = await _userService.DeleteUser(userCode);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion
}
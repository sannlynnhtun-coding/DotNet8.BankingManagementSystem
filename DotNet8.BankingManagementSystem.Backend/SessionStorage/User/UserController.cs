namespace DotNet8.BankingManagementSystem.Backend.Services.Service.Localstorage;

public class UserController : BaseController
{
    private readonly SessionUserService _userService;

    public UserController(SessionUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserList()
    {
        try
        {
            var model = await _userService.GetUserList();
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GerUser(UserModel requestModel)
    {
        try
        {
            var model = await _userService.GetUser(requestModel);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserModel requestModel)
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
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser(UserModel requestModel)
    {
        try
        {
            var model = await _userService.UpdateUser(requestModel);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(UserModel requestModel)
    {
        try
        {
            var model = await _userService.DeleteUser(requestModel);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
}
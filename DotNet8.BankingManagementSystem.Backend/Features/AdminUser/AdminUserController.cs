namespace DotNet8.BankingManagementSystem.Backend.Features.AdminUser;

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
    public async Task<IActionResult> GetAdminUsers()
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

    #region GetAdminUserList

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

    #region GetAdminUserByCode

    [HttpGet("{AdminUserCode}")]
    public async Task<IActionResult> GetAdminUserByCode(string adminUserCode)
    {
        try
        {
            var model = await _adminUserService.GetAdminUserByCode(adminUserCode);
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

    #region UpdateAdminUser

    [HttpPut("{AdminUserCode}")]
    public async Task<IActionResult> UpdateAdminUser(AdminUserRequestModel requestModel)
    {
        try
        {
            var model = await _adminUserService.UpdateAdminUser(requestModel);
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
    public async Task<IActionResult> DeleteAdminUser(string adminUserCode)
    {
        try
        {
            var model = await _adminUserService.DeleteAdminUser(adminUserCode);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion
}
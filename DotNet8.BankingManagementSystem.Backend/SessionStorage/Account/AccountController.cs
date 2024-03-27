namespace DotNet8.BankingManagementSystem.Backend.Services.Service.Localstorage.Account;

public class AccountController : BaseController
{
    private readonly SessionAccountService _accountService;

    public AccountController(SessionAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAccountList()
    {
        try
        {
            var model = await _accountService.GetAccountList();
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAccount(AccountModel requestModel)
    {
        try
        {
            var model = await _accountService.GetAccount(requestModel);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAccount(AccountModel requestModel)
    {
        try
        {
            var model = await _accountService.CreateAccount(requestModel);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAccount(AccountModel requestModel)
    {
        try
        {
            var model = await _accountService.UpdateAccount(requestModel);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteAccount(AccountModel requestModel)
    {
        try
        {
            var model = await _accountService.DeleteAccount(requestModel);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
}
using DotNet8.BankingManagementSystem.Models.Account;
using DotNet8.BankingManagementSystem.Models.Transfer;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.BankingManagementSystem.BackendApi.Features.Account;

[ApiController]
[Route("api/[controller]")]
public class AccountController : BaseController
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    #region Get account list

    [HttpGet]
    public async Task<IActionResult> GetAccounts()
    {
        try
        {
            var model = await _accountService.GetAccounts();
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion

    #region Get account list by Pagination

    [HttpGet("{pageNo}/{pageSize}")]
    public async Task<IActionResult> GetAccountList(int pageNo, int pageSize)
    {
        try
        {
            var model = await _accountService.GetAccountList(pageNo, pageSize);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion

    #region Get accounts

    [HttpGet("{accountNo}")]
    public async Task<IActionResult> GetAccount(string accountNo)
    {
        try
        {
            var model = await _accountService.GetAccount(accountNo);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion

    #region Account Create

    [HttpPost]
    public async Task<IActionResult> CreateAccount(AccountRequestModel requestModel)
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

    #endregion

    #region Update account info

    [HttpPut("{accountNo}")]
    public async Task<IActionResult> UpdateAccount(string accountNo, AccountRequestModel requestModel)
    {
        try
        {
            var model = await _accountService.UpdateAccount(accountNo, requestModel);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion

    #region Delete Account

    [HttpDelete("{accountNo}")]
    public async Task<IActionResult> DeleteAccount(string accountNo)
    {
        try
        {
            var model = await _accountService.DeleteAccount(accountNo);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion

    #region Deposit

    [HttpPost("Deposit")]
    public async Task<IActionResult> Deposit(string accountNo, decimal amount)
    {
        try
        {
            var model = await _accountService.Deposit(accountNo, amount);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion

    #region Withdrawl

    [HttpPost("Withdraw")]
    public async Task<IActionResult> Withdraw(string accountNo, decimal amount)
    {
        try
        {
            var model = await _accountService.Withdraw(accountNo, amount);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion

    #region Transfer

    [HttpPost("Transfer")]
    public async Task<IActionResult> TransferBalance(TransferModel requestModel)
    {
        try
        {
            var model = await _accountService.TransferBalance(requestModel);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion
}
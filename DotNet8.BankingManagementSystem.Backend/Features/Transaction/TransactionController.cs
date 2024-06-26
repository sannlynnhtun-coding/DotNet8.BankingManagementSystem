﻿using DotNet8.BankingManagementSystem.Models.TransactionHistory;
using DotNet8.BankingManagementSystem.Shared;

namespace DotNet8.BankingManagementSystem.Backend.Features.Transaction;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : BaseController
{
    private readonly TransactionService _transactionService;

    public TransactionController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    #region Get History

    [HttpGet("TransactionHistory/{pageNo}/{pageSize}")]
    public async Task<IActionResult> TransactionHistory(int pageNo, int pageSize)
    {
        try
        {
            var model = await _transactionService.TransactionHistory(pageNo, pageSize);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion

    #region Get History With Date

    [HttpPost("TransactionHistory")]
    public async Task<IActionResult> TransactionHistory(TransactionHistorySearchModel requestModel)
    {
        try
        {
            var model = await _transactionService.TransactionHistoryWithDate(
                requestModel.FromDate,
                requestModel.PageNo,
                requestModel.PageSize);
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
    public async Task<IActionResult> Deposit(TransactionRequestModel requestModel)
    {
        try
        {
            var model = await _transactionService.Deposit(requestModel);
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
    public async Task<IActionResult> Withdraw(TransactionRequestModel requestModel)
    {
        try
        {
            var model = await _transactionService.Withdraw(requestModel);
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
    public async Task<IActionResult> Transfer(TransferModel requestModel)
    {
        try
        {
            var model = await _transactionService.Transfer(requestModel);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion

    #region Generate accounts

    [HttpPost("Generate")]
    public async Task<IActionResult> GenerateAccounts(int count, int year)
    {
        var model = await _transactionService.GenerateAccounts(count, year);
        return Ok(model);
    }

    #endregion

    #region Date Range

    [HttpPost("TransactionHistory/DateRange")]
    public async Task<IActionResult> TransactionHistoryDateList(TransactionHistorySearchModel requestModel)
    {
        try
        {
            var model = await _transactionService.TransactionHistoryDateList(
                requestModel.FromDate.ToDateTime(),
                requestModel.ToDate.ToDateTime(), 
                requestModel.PageNo, 
                requestModel.PageSize);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    #endregion
}
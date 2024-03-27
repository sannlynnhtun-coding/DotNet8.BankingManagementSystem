namespace DotNet8.BankingManagementSystem.Backend.Services.Service.Localstorage.Transaction;

public class TransactionController : BaseController
{
    private readonly SessionTransactionService _transactionService;

    public TransactionController(SessionTransactionService transactionService)
    {
        _transactionService = transactionService;
    }


    [HttpPost]
    public async Task<IActionResult> Withdraw(AccountModel requestModel, decimal amount)
    {
        try
        {
            var model = await _transactionService.Withdraw(requestModel, amount);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Deposit(AccountModel requestModel, decimal amount)
    {
        try
        {
            var model = await _transactionService.Deposit(requestModel, amount);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }


    [HttpPost]
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
}
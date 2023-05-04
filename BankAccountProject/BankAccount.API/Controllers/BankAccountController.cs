using BankAccount.Core.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankAccount.API.Controllers
{
    [ApiController]
    [Controller]
    public class BankAccountController : ControllerBase
    {
        private readonly IAccountRepository _accountService;

        public BankAccountController(IAccountRepository accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("v1/bank/accounts/{accountId}/transactions")]
        public async Task<IActionResult> SaveTransaction(string accountId, int type, decimal amount)
        {
            Core.Data.TransactionState result;
            switch (type)
            {
                case (int)Core.Data.TransactionType.Withdraw:
                    result = await _accountService.Withdraw(accountId, type, amount);
                    break;
                case (int)Core.Data.TransactionType.Deposit:
                    result = await _accountService.Deposit(accountId, type, amount);
                    break;
                default:
                    return BadRequest("Invalid transaction type");
            }

            if (result == Core.Data.TransactionState.Success)
            {
                string typeValue = type == 1 ? "Deposit" : "Withdraw";
                return Ok($"The {typeValue} was a success");
            }
            else
                return BadRequest("An error has occured during the transaction");

        }

        [HttpGet("v1/bank/accounts/{accountId}/balance")]
        public IActionResult GetBalance(string accountId)
        {
            return Ok(_accountService.GetBalance(accountId));
        }

        [HttpGet("v1/bank/accounts/{accountId}/transactions")]
        public async Task<IActionResult> GePreviousTransactions(string accountId, int? type = null)
        {
            return Ok(await _accountService.GetPreviousTransactions(accountId, type));

        }
    }
}
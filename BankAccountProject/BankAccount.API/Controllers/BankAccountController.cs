using BankAccount.Core.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankAccount.API.Controllers
{
    [ApiController]
    [Controller]
    [Route("v1/bank/accounts")]
    public class BankAccountController : ControllerBase
    {
        private readonly IAccountRepository _accountService;

        public BankAccountController(IAccountRepository accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        [HttpPost("{accountId}/transaction/withdraw")]
        public async Task<IActionResult> WithdrawTransactionAsync(string accountId, decimal? amount)
        {
            if (amount == null || amount < 0)
                return BadRequest($"{nameof(amount)} cannot be null or below 0.");

            if (string.IsNullOrEmpty(accountId))
                return BadRequest($"{nameof(accountId)} account cannot be null or empty.");

            var accountBalance = _accountService.GetBalance(accountId);

            if (accountBalance < amount)
                return BadRequest($"{nameof(amount)} is smaller than account balance {accountBalance}");

            var result = await _accountService.WithdrawAsync(accountId, amount.Value);

            if (result != Core.Data.TransactionState.Success)
                return StatusCode(500, "An error has occured during the transaction");

            return NoContent();

        }

        [HttpPost("{accountId}/transaction/deposit")]
        public async Task<IActionResult> DepositTransactionAsync(string accountId, decimal? amount)
        {
            if (amount == null || amount < 0)
                return BadRequest($"{nameof(amount)} cannot be null or below 0.");

            if (string.IsNullOrEmpty(accountId))
                return BadRequest($"{nameof(accountId)} account cannot be null or empty.");

            Core.Data.TransactionState result = await _accountService.DepositAsync(accountId, amount.Value);

            if (result != Core.Data.TransactionState.Success)
                return StatusCode(500, "An error has occured during the transaction");

            return NoContent();

        }

        [HttpGet("{accountId}/balance")]
        public IActionResult GetBalance(string accountId)
        {
            if (string.IsNullOrEmpty(accountId))
                return BadRequest($"{nameof(accountId)} account cannot be null or empty.");

            var result = _accountService.GetBalance(accountId);
            return Ok(result);
        }

        [HttpGet("{accountId}/transactions")]
        public async Task<IActionResult> GetPreviousTransactions(string accountId, int? type = null)
        {
            if (string.IsNullOrEmpty(accountId))
                return BadRequest($"{nameof(accountId)} account cannot be null or empty.");

            var result = await _accountService.GetPreviousTransactionsAsync(accountId, type);
            return Ok(result);
        }
    }
}
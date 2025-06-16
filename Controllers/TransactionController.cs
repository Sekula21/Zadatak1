using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.Services;

namespace Zadatak1.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ExceptionHandlerService _exceptionHandler;

        public TransactionController(ITransactionService transactionService,
            ExceptionHandlerService exceptionHandlerService)
        {
            _transactionService = transactionService;
            _exceptionHandler = exceptionHandlerService;
        }

        public async Task<IActionResult> Transaction()
        {
            return View(await _transactionService.GetAllTransactions());
        }
        public async Task<IActionResult> PreviousTrans()
        {
            return View(await _transactionService.GetAllTransactions());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Transaction(Guid Name, int amount)
        {
            try
            {
                var result = await _transactionService.ProcessTransaction(Name, amount);

                return Ok(new
                {
                    ResultStatus = ActionResultStatus.Success,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                var handledResult = _exceptionHandler.HandleException<string>(ex);
                return handledResult.ResultStatus switch
                {
                    ActionResultStatus.BadRequest => BadRequest(handledResult),
                    ActionResultStatus.Unauthorized => Unauthorized(handledResult),
                    ActionResultStatus.NotFound => NotFound(handledResult),
                    _ => StatusCode(500, handledResult)
                };
            }
        }
    }
}

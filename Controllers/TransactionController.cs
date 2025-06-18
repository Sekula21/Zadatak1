using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadatak1.Interfaces;
using Zadatak1.Models;
using Zadatak1.Services;

namespace Zadatak1.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService,
            ExceptionHandler exceptionHandler):base(exceptionHandler)
        {
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Transaction()
        {
            var(success, view) = await TryExecuteWithResult(() => _transactionService.GetAll());
            return View(view);
        }
        public async Task<IActionResult> PreviousTrans()
        {
            var (success, view) = await TryExecuteWithResult(() => _transactionService.GetAll());
            return View(view);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Transaction(Guid Name, int amount)
        {
            var result = await TryExecute(() => _transactionService.ProcessTransaction(Name, amount));

            return Ok(result);
         
        }
    }
}

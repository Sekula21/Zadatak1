using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadatak1.Interfaces;
using Zadatak1.Models;

namespace Zadatak1.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Transaction()
        {
            return View(await _transactionService.GetAllTransactionsAsync());
        }
        public async Task<IActionResult> PrevTrans()
        {
            return View(await _transactionService.GetAllTransactionsAsync());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Transaction(Guid Name, int amount)
        {
            var result = await _transactionService.ProcessTransactionAsync(Name, amount);

            return result switch
            {
                "User or product not found." => NotFound(result),
                "Not enough stock available." => BadRequest(result),
                "Unauthorized" or "User ID not found in token." => Unauthorized(result),
                _ => Ok(result)
            };

        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadatak1.Interfaces;
using Zadatak1.Models;

namespace Zadatak1.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IResponseMessageService _responseMessageService;
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService, IResponseMessageService responseMessageService)
        {
            _transactionService = transactionService;
            _responseMessageService = responseMessageService;
        }

        public async Task<IActionResult> Transaction()
        {
            return View(await _transactionService.GetAllTransactionsAsync());
        }
        public async Task<IActionResult> PrevousTrans()
        {
            return View(await _transactionService.GetAllTransactionsAsync());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Transaction(Guid Name, int amount)
        {
            var result = await _transactionService.ProcessTransactionAsync(Name, amount);

            var notFoundMsg = _responseMessageService.Get("Errors", "UPNotFound");
            var stockMsg = _responseMessageService.Get("Errors", "NotEnoughStock");
            var unauthorizedMsg = _responseMessageService.Get("Errors", "Unauthorized");
            var userIdMsg = _responseMessageService.Get("Errors", "UserIdMissing");

            return result switch
            {
                var r when r == notFoundMsg => NotFound(r),
                var r when r == stockMsg => BadRequest(r),
                var r when r == unauthorizedMsg || r == userIdMsg => Unauthorized(r),
                _ => Ok(result)
            };

        }
    }
}

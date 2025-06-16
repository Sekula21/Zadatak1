using System.Security.Claims;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Zadatak1.Interfaces;
using Zadatak1.Models;

namespace Zadatak1.Services
{

    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserTokenService _userTokenService;
        private readonly IResponseMessageService _responseMessageService;
        private readonly ExceptionHandlerService _exceptionHandler;

        public TransactionService(ITransactionRepository transactionRepository, IUserTokenService userTokenService, 
            IResponseMessageService responseMessageService, ExceptionHandlerService exceptionHandlerService)
        {
            _transactionRepository = transactionRepository;
            _userTokenService = userTokenService;
            _responseMessageService = responseMessageService;
            _exceptionHandler = exceptionHandlerService;
        }
        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return await _transactionRepository.GetAll();
        }
        public async Task<ActionResult<string>> ProcessTransaction(Guid productId, int amount)
        {
            try
            {
                var userId = _userTokenService.GetCurrentUserId();

                var dbUser = await _transactionRepository.GetUserById((Guid)userId);
                var product = await _transactionRepository.GetProductById(productId);
                var notFoundMsg = _responseMessageService.Get("Errors", "UPNotFound");
                var notEnoughStockMsg = _responseMessageService.Get("Errors", "NotEnoughStock");

                if (dbUser == null || product == null)
                {
                    return new ActionResult<string>
                    {
                        ResultStatus = ActionResultStatus.NotFound,
                        ErrorMessage = notFoundMsg
                    };
                }

                if (product.Amount < amount)
                {
                    return new ActionResult<string>
                    {
                        ResultStatus = ActionResultStatus.BadRequest,
                        ErrorMessage = notEnoughStockMsg
                    };
                }

                var transaction = new Transaction
                {
                    Buyer = dbUser,
                    Product = product,
                    TransactionDate = DateTime.Now,
                    Amount = amount,
                    Price = product.Price * amount
                };

                product.Amount -= amount;
                await _transactionRepository.Add(transaction);
                _transactionRepository.UpdateProduct(product);
                await _transactionRepository.SaveChanges();

                var successMsg = _responseMessageService.Get("Success", "Purchase");

                return new ActionResult<string>
                {
                    ResultStatus = ActionResultStatus.Success,
                    Data = successMsg
                };

            }
            catch (Exception ex)
            {
                return _exceptionHandler.HandleException<string>(ex, "TransactionFailed");
            }
        }
    }
}
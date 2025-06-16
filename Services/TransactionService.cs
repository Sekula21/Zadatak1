using System.Security.Claims;
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

        public TransactionService(ITransactionRepository transactionRepository, IUserTokenService userTokenService, IResponseMessageService responseMessageService)
        {
            _transactionRepository = transactionRepository;
            _userTokenService = userTokenService;
            _responseMessageService = responseMessageService;
        }
        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllAsync();
        }

        public async Task<string> ProcessTransactionAsync(Guid productId, int amount)
        {
            var userId = _userTokenService.GetCurrentUserId();

            var dbUser = await _transactionRepository.GetUserByIdAsync((Guid)userId);
            var product = await _transactionRepository.GetProductByIdAsync(productId);

            var notFoundMsg = _responseMessageService.Get("Errors", "UPNotFound");
            var notEnoughStockMsg = _responseMessageService.Get("Errors", "NotEnoughStock");

            if (dbUser == null || product == null)
                return notFoundMsg;

            if (product.Amount < amount)
                return notEnoughStockMsg;

            var transaction = new Transaction
            {
                Buyer = dbUser,
                Product = product,
                TransactionDate = DateTime.Now,
                Amount = amount,
                Price = product.Price * amount
            };

            product.Amount -= amount;
            await _transactionRepository.AddAsync(transaction);
            _transactionRepository.UpdateProduct(product);
            await _transactionRepository.SaveChangesAsync();

            var purchaseSuccess = _responseMessageService.Get("Success", "Purchase");

            return purchaseSuccess;
        }
    }
}
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

        public TransactionService(ITransactionRepository transactionRepository, IUserTokenService userTokenService)
        {
            _transactionRepository = transactionRepository;
            _userTokenService = userTokenService;
        }
        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllAsync();
        }

        public async Task<string> ProcessTransactionAsync(Guid productId, int amount)
        {
            var userId = _userTokenService.GetCurrentUserId();
            if (userId == null)
                return "Unauthorized or invalid user.";

            var dbUser = await _transactionRepository.GetUserByIdAsync((Guid)userId);
            var product = await _transactionRepository.GetProductByIdAsync(productId);

            if (dbUser == null || product == null)
                return "User or product not found.";

            if (product.TotalAmount < amount)
                return "Not enough stock available.";

            var transaction = new Transaction
            {
                Buyer = dbUser,
                Product = product,
                TransDate = DateTime.Now,
                Amount = amount,
                Price = product.PricePerJar * amount
            };

            product.TotalAmount -= amount;
            await _transactionRepository.AddAsync(transaction);
            _transactionRepository.UpdateProduct(product);
            await _transactionRepository.SaveChangesAsync();

            return "Purchase successful!";
        }
    }
}
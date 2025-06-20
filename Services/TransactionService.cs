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
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserTokenService _userTokenService;
        private readonly IResponseMessageService _responseMessageService;
        public TransactionService(IUserRepository userRepository, IProductRepository productRepository, IUserTokenService userTokenService, 
            IResponseMessageService responseMessageService, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _userTokenService = userTokenService;
            _responseMessageService = responseMessageService;
            _transactionRepository = transactionRepository;
        }
        public async Task<IEnumerable<Transaction>> GetAll()
        {
            return await _transactionRepository.GetAll();
        }
        public async Task<ActionResult<string>> ProcessTransaction(Guid productId, int amount)
        {
            var userId = _userTokenService.GetCurrentUserId();

            var dbUser = await _userRepository.GetById((Guid)userId);
            var product = await _productRepository.GetById(productId);
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

            await _transactionRepository.CreateTransaction(dbUser, product, amount);

            var successMsg = _responseMessageService.Get("Success", "Purchase");

            return new ActionResult<string>
            {
                ResultStatus = ActionResultStatus.Success,
                Data = successMsg
            };
        }
    }
}
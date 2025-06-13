using Zadatak1.Models;

public interface ITransactionService
{
    Task<string> ProcessTransactionAsync(Guid productId, int amount);
    Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
}
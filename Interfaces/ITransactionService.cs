using Zadatak1.Models;

public interface ITransactionService
{
    Task<ActionResult<string>> ProcessTransaction(Guid productId, int amount);
    Task<IEnumerable<Transaction>> GetAll();
}
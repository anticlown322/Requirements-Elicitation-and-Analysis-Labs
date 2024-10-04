using PaymentMicroservice.Models;

namespace PaymentMicroservice.Repository;

/// <summary>
/// Standard payment database context.
/// </summary>
public class DefaultPaymentDbContext : IPaymentDbContext
{
    //is used for stashing transactions for later DB interacting
    private readonly Dictionary<int, TransactionModel> _transactionStore = new Dictionary<int, TransactionModel>();
    
    //is used for correct transaction adding
    private int _nextTransactionId = 1;

    /// <summary>
    /// IEnumerable list of transactions
    /// </summary>
    public IEnumerable<TransactionModel> Transactions => _transactionStore.Values;

    /// <summary>
    /// Get transaction by ID.
    /// </summary>
    /// <param name="id">ID[int]</param>
    /// <returns>Instance of found transaction</returns>
    public TransactionModel GetTransactionById(int id)
    {
        if (_transactionStore.TryGetValue(id, out var transaction))
        {
            return transaction;
        }
        return null; // Transaction with the given ID not found
    }

    /// <summary>
    /// Add a new transaction to the list.
    /// </summary>
    /// <param name="transactionModel">Transaction instance that must be added to the list</param>
    /// <returns>Copy of transaction instance that was added</returns>
    public TransactionModel AddTransaction(TransactionModel transactionModel)
    {
        transactionModel.Id = _nextTransactionId++;
        _transactionStore.Add(transactionModel.Id, transactionModel);
        return transactionModel;
    }

    /// <summary>
    /// Delete transaction by id. 
    /// </summary>
    /// <param name="id">ID of the transaction that must be deleted</param>
    public void DeleteTransaction(int id)
    {
        if (_transactionStore.ContainsKey(id))
        {
            _transactionStore.Remove(id);
        }
    }
}
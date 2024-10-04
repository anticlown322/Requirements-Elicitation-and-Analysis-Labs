using PaymentMicroservice.Models;
using PaymentMicroservice.Repository;

namespace PaymentMicroservice.Services;

/// <summary>
/// Standard transaction service. 
/// </summary>
/// <param name="context">DB context for service</param>
public class DefaultPaymentService(IPaymentDbContext context) : IPaymentService
{
    /// <summary>
    /// Get list of all transactions. List is IEnumerable. 
    /// </summary>
    /// <returns>IEnumerable list of all transactions</returns>    
    public IEnumerable<TransactionModel> GetAllTransactions()
    {
        return context.Transactions;
    }
    
    /// <summary>
    /// Get transaction by ID.
    /// </summary>
    /// <param name="id">ID[int]</param>
    /// <returns>Instance of found transaction</returns>
    public TransactionModel GetTransactionById(int id)
    {
        return context.Transactions.FirstOrDefault(transaction => transaction.Id == id);
    }

    /// <summary>
    /// Add a new transaction to the list.
    /// </summary>
    /// <param name="transactionModel">Transaction instance that must be added to the list</param>
    /// <returns>Copy of transaction instance that was added</returns>
    public TransactionModel AddTransaction(TransactionModel transactionModel)
    {
        context.AddTransaction(transactionModel);
        return transactionModel;
    }

    /// <summary>
    /// Delete transaction by id. 
    /// </summary>
    /// <param name="id">ID of the transaction that must be deleted</param>
    public void DeleteTransaction(int id)
    {
        context.DeleteTransaction(id);
    }
}
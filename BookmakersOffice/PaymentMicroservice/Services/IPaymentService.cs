using PaymentMicroservice.Models;

namespace PaymentMicroservice.Services;

/// <summary>
/// Interface that every transactionService must inherit
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Get list of all transactions. 
    /// </summary>
    /// <returns>IEnumerable list of all transactions</returns>
    IEnumerable<TransactionModel> GetAllTransactions();
    
    /// <summary>
    /// Get transaction by ID.
    /// </summary>
    /// <param name="id">ID[int]</param>
    /// <returns>Instance of found transaction</returns>
    TransactionModel GetTransactionById(int id);
    
    /// <summary>
    /// Create a new transaction account.
    /// </summary>
    /// <param name="transactionModel">Transaction instance that must be added to the list</param>
    /// <returns>Copy of transaction instance that was added</returns>
    TransactionModel AddTransaction(TransactionModel transactionModel);
    
    /// <summary>
    /// Delete transaction by id
    /// </summary>
    /// <param name="id">ID of the transaction that must be deleted</param>
    void DeleteTransaction(int id);
}

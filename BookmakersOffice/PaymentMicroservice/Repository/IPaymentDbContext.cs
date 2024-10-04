using PaymentMicroservice.Models;

namespace PaymentMicroservice.Repository;

using System.Collections.Generic;

/// <summary>
/// Interface for transaction DBContext
/// </summary>
public interface IPaymentDbContext
{
    /// <summary>
    /// Get list of all transactions. List is IEnumerable. 
    /// </summary>
    IEnumerable<TransactionModel> Transactions { get; }
    
    /// <summary>
    /// Get transaction by ID.
    /// </summary>
    /// <param name="id">ID[int]</param>
    /// <returns>Instance of found transaction</returns>
    TransactionModel GetTransactionById(int id);
    
    /// <summary>
    /// Add a new transaction to the list.
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
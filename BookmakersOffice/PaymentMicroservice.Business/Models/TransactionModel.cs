using System.ComponentModel.DataAnnotations;
using PaymentMicroservice.Data.Entities;

namespace PaymentMicroservice.Business.Models;

/// <summary>
/// Transaction info representation
/// </summary>
public class TransactionModel
{
    /// <summary>
    /// Transaction ID
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// ID of account that initiated the transaction
    /// </summary>
    public int AccountId { get; set; }
    
    /// <summary>
    /// Transaction amount in coins. 
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Transaction type
    /// </summary>
    public TransactionType Type { get; set; }
    
    /// <summary>
    /// Time when transaction was created
    /// </summary>
    public DateTime TransactionDateTime { get; set; }
}
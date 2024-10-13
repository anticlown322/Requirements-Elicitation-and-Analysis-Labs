using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PaymentMicroservice.Data.Entities;

/// <summary>
/// Transaction info representation
/// </summary>
//[Table("transactions", Schema = "dbo")]
public class TransactionEntity
{
    /// <summary>
    /// Transaction ID
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    /// <summary>
    /// ID of account that initiated the transaction
    /// </summary>
    [Required(ErrorMessage = "Initiator account id required!")]
    public int AccountId { get; set; }
    
    /// <summary>
    /// Transaction amount in coins. 
    /// </summary>
    [Required(ErrorMessage = "Amount required!")]
    [Range(0, 100_000_000, ErrorMessage = "Amount must be in [1; 100 000 000]")]
    [Precision(18, 2)]
    public decimal Amount { get; set; }

    /// <summary>
    /// Transaction type
    /// </summary>
    [Required(ErrorMessage = "Transaction type required!")]
    public TransactionType Type { get; set; }
    
    /// <summary>
    /// Time when transaction was created
    /// </summary>
    [Required(ErrorMessage = "Creation date required!")]
    public DateTime TransactionDateTime { get; set; }
}
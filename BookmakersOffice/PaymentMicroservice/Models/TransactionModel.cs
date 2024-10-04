using System.ComponentModel.DataAnnotations;

namespace PaymentMicroservice.Models;

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
    [Required(AllowEmptyStrings = false, ErrorMessage = "Initiator account id required!")]
    public int AccountId { get; set; }
    
    /// <summary>
    /// Transaction amount in coins. 
    /// <remarks>The conversion of coins to the RUS is 100 to 1</remarks>
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Amount required!")]
    [Range(0, 100_000_000, ErrorMessage = "Amount must be in [1; 100 000 000]")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Transaction type
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Transaction type required!")]
    public TransactionType Type { get; set; }
    
    /// <summary>
    /// Time when transaction was created
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Creation date required!")]
    public DateTime TransactionDateTime { get; set; }
}
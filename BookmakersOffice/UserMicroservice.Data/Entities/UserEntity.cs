using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UserMicroservice.Data.Entities;

/// <summary>
/// User account info representation
/// </summary>
[Table("UserDB", Schema = "dbo")]
public class UserEntity
{
    #region Basic info 
    /// <summary>
    /// User account ID
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    /// <summary>
    /// User account GUID for internal use
    /// </summary>
    [Column("System ID")]
    public Guid AppId { get; set; }
    
    /// <summary>
    /// Login or name or nick
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Login required!")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Login is 60 characters.")]
    public string Login { get; set; }
    
    /// <summary>
    /// Current balance in coins. 
    /// </summary>
    [Required(ErrorMessage = "Balance amount required!")]
    [Range(0, 100_000_000, ErrorMessage = "Balance amount must be in [1; 100 000 000]")]
    [Precision(18, 2)]
    public decimal Balance { get; set; }
    
    /// <summary>
    /// Indicates is user account verified or not. 
    /// <remarks>User has additional options and higher limits if user account is verified</remarks>
    /// </summary>
    [Required(ErrorMessage = "Info about verification required!")]
    public bool IsVerified { get; set; }

    #endregion
    
    #region Additional info about user

    /// <summary>
    /// Date when account was created. 
    /// </summary>
    [Required(ErrorMessage = "Registration date required!")]
    public DateTime RegistrationDate { get; set; }
    
    /// <summary>
    /// Last time when account was updated. 
    /// </summary>
    public DateTime? UpdateDate { get; set; }
    
    /// <summary>
    /// User email. 
    /// <remarks>Possible are: gmail, email</remarks>
    /// </summary>
    [MaxLength(255, ErrorMessage = "Maximum length for the email is 255 characters.")]
    public string? Email { get; set; }
    
    /// <summary>
    /// User's first name. 
    /// <remarks>Remember, it may consist of more than one word!</remarks>
    /// </summary>
    [MaxLength(100, ErrorMessage = "Maximum length for the first name is 100 characters.")]
    public string? FirstName { get; set; }
    
    /// <summary>
    /// User last name or surname. 
    /// <remarks>Remember, it may consist of more than one word!</remarks>
    /// </summary>
    [MaxLength(100, ErrorMessage = "Maximum length for the second name is 100 characters.")]
    public string? LastName { get; set; }
    
    /// <summary>
    /// User phone number. 
    /// </summary>
    [MaxLength(15, ErrorMessage = "Maximum length for the phone number is 15 characters.")]
    public string? Phone { get; set; }

    #endregion
}
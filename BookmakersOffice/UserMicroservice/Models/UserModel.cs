namespace UserMicroservice.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// User account info representation
/// </summary>
public class UserModel
{
    #region Basic info 
    /// <summary>
    /// User account ID
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Login or name or nick
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Login required!")]
    public string Login { get; set; }
    
    /// <summary>
    /// Password
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Password required!")]
    public string Password { get; set; }
    
    /// <summary>
    /// Current balance in coins. 
    /// <remarks>The conversion of coins to the RUS is 100 to 1</remarks>
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Balance amount required!")]
    [Range(0, 100_000_000, ErrorMessage = "Balance amount must be in [1; 100 000 000]")]
    public decimal Balance { get; set; }
    
    /// <summary>
    /// Indicates is user account verified or not. 
    /// <remarks>User has additional options and higher limits if user account is verified</remarks>
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Info about verification required!")]
    public bool IsVerified { get; set; }

    #endregion
    
    #region Additional info about user

    /// <summary>
    /// Date when account was created. 
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Registration date required!")]
    public DateTime RegistrationDate { get; set; }
    
    /// <summary>
    /// User email. 
    /// <remarks>Possible are: gmail, email</remarks>
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// User's first name. 
    /// <remarks>Remember, it may consist of more than one word!</remarks>
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// User last name or surname. 
    /// <remarks>Remember, it may consist of more than one word!</remarks>
    /// </summary>
    public string LastName { get; set; }

    #endregion
}
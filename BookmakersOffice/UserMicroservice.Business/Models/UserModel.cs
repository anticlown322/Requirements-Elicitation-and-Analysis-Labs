namespace UserMicroservice.Business.Models;

/// <summary>
/// User account info representation
/// </summary>
public class UserModel
{
    #region Basic info 
    /// <summary>
    /// User account ID
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// User account GUID for internal use
    /// </summary>
    public Guid AppId { get; set; }
    
    /// <summary>
    /// Login or name or nick
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Current balance in coins. 
    /// </summary>
    public decimal Balance { get; set; }
    
    /// <summary>
    /// Indicates is user account verified or not. 
    /// <remarks>User has additional options and higher limits if user account is verified</remarks>
    /// </summary>
    public bool IsVerified { get; set; }

    #endregion
    
    #region Additional info about user
    
    /// <summary>
    /// User email. 
    /// <remarks>Possible are: gmail, email</remarks>
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// User's first name. 
    /// <remarks>Remember, it may consist of more than one word!</remarks>
    /// </summary>
    public string? FirstName { get; set; }
    
    /// <summary>
    /// User last name or surname. 
    /// <remarks>Remember, it may consist of more than one word!</remarks>
    /// </summary>
    public string? LastName { get; set; }
    
    /// <summary>
    /// User phone number. 
    /// </summary>
    public string? Phone { get; set; }

    #endregion
}
using UserMicroservice.Models;

namespace UserMicroservice.Repository;

/// <summary>
/// Interface for user DBContext
/// </summary>
public interface IUserDbContext
{
    /// <summary>
    /// Get list of all users. List is IEnumerable. 
    /// </summary>
    IEnumerable<UserModel> Users { get; }
    
    /// <summary>
    /// Get user by ID.
    /// </summary>
    /// <param name="id">ID[int]</param>
    /// <returns>Instance of found user</returns>
    UserModel GetUsersById(int id);
    
    /// <summary>
    /// Add a new user to the list.
    /// </summary>
    /// <param name="userModel">User instance that must be added to the list</param>
    /// <returns>Copy of user instance that was added</returns>
    UserModel AddUser(UserModel userModel);
    
    /// <summary>
    /// Update info about user
    /// </summary>
    /// <param name="id">ID of the user that must be updated</param>
    /// <param name="userModel">Source for update. New user info</param>
    /// <returns>Copy of user instance that was updated</returns>
    UserModel UpdateUser(int id, UserModel userModel);
    
    /// <summary>
    /// Delete user by id
    /// </summary>
    /// <param name="id">ID of the user that must be deleted</param>
    void DeleteUser(int id);
}
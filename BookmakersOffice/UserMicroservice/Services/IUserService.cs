using UserMicroservice.Models;

namespace UserMicroservice.Services;

/// <summary>
/// Interface that every userService must inherit
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Get list of all users. 
    /// </summary>
    /// <returns>IEnumerable list of all users</returns>
    IEnumerable<UserModel> GetAllUsers();
    
    /// <summary>
    /// Get user by ID.
    /// </summary>
    /// <param name="id">ID[int]</param>
    /// <returns>Instance of found user</returns>
    UserModel GetUserById(int id);
    
    /// <summary>
    /// Get user by nickname.
    /// </summary>
    /// <param name="login">Login of the user that must be found</param>
    /// <returns>Instance of found user</returns>
    UserModel GetUserByLogin(string login);
    
    /// <summary>
    /// Create a new user account.
    /// </summary>
    /// <param name="user">User instance that must be added to the list</param>
    /// <returns>Copy of user instance that was added</returns>
    UserModel AddUser(UserModel user);
    
    /// <summary>
    /// Update info about user
    /// </summary>
    /// <param name="id">ID of the user that must be updated</param>
    /// <param name="user">Source for update. New user info</param>
    /// <returns>Copy of user instance that was updated</returns>
    UserModel UpdateUser(int id, UserModel user);
    
    /// <summary>
    /// Delete user by id
    /// </summary>
    /// <param name="id">ID of the user that must be deleted</param>
    void DeleteUser(int id);
}
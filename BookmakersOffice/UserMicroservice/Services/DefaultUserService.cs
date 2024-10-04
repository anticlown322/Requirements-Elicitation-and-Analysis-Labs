using UserMicroservice.Models;
using UserMicroservice.Repository;

namespace UserMicroservice.Services;

/// <summary>
/// Standard user service. 
/// </summary>
/// <param name="context">DB context for service</param>
public class DefaultUserService(IUserDbContext context) : IUserService
{
    /// <summary>
    /// Get list of all users. List is IEnumerable. 
    /// </summary>
    /// <returns>IEnumerable list of all users</returns>
    public IEnumerable<UserModel> GetAllUsers()
    {
        return context.Users;
    }

    /// <summary>
    /// Get user by ID.
    /// </summary>
    /// <param name="id">ID[int]</param>
    /// <returns>Instance of found user</returns>
    public UserModel GetUserById(int id)
    {
        return context.Users.FirstOrDefault(p => p.Id == id);
    }

    /// <summary>
    /// Get user by login
    /// </summary>
    /// <param name="login">Login[string]</param>
    /// <returns>Instance of found user</returns>
    public UserModel GetUserByLogin(string login)
    {
        return context.Users.FirstOrDefault(p => p.Login == login);
    }

    /// <summary>
    /// Add a new user to the list.
    /// </summary>
    /// <param name="user">User instance that must be added to the list</param>
    /// <returns>Copy of user instance that was added</returns>
    public UserModel AddUser(UserModel user)
    {
        context.AddUser(user);
        return user;
    }

    /// <summary>
    /// Update info about concrete user. 
    /// </summary>
    /// <param name="id">ID of the user that must be updated</param>
    /// <param name="user">Source for update. New user info</param>
    /// <returns>Copy of user instance that was updated</returns>
    public UserModel UpdateUser(int id, UserModel user)
    {
        var existingUser = context.GetUsersById(id);
        context.UpdateUser(id, user);
        return existingUser;
    }
    
    /// <summary>
    /// Delete user by id. 
    /// </summary>
    /// <param name="id">ID of the user that must be deleted</param>
    public void DeleteUser(int id)
    {
        context.DeleteUser(id);
    }
}
using UserMicroservice.Models;

namespace UserMicroservice.Repository;

/// <summary>
/// Standard user database context.
/// </summary>
public class DefaultUsersDbContext: IUserDbContext
{
    //is used for stashing users for later DB interacting
    private readonly Dictionary<int, UserModel> _userStore = new();
    
    //is used for correct user adding
    private int _nextUserId = 1;

    /// <summary>
    /// IEnumerable list of users
    /// </summary>
    public IEnumerable<UserModel> Users => _userStore.Values;
    
    /// <summary>
    /// Get user by ID.
    /// </summary>
    /// <param name="id">ID[int]</param>
    /// <returns>Instance of found user</returns>
    public UserModel GetUsersById(int id)
    {
        return _userStore.GetValueOrDefault(id);
    }

    /// <summary>
    /// Add a new user to the list.
    /// </summary>
    /// <param name="userModel">User instance that must be added to the list</param>
    /// <returns>Copy of user instance that was added</returns>
    public UserModel AddUser(UserModel userModel)
    {
        userModel.Id = _nextUserId++;
        _userStore.Add(userModel.Id, userModel);
        return userModel;
    }

    /// <summary>
    /// Update info about concrete user. 
    /// </summary>
    /// <param name="id">ID of the user that must be updated</param>
    /// <param name="userModel">Source for update. New user info</param>
    /// <returns>Copy of user instance that was updated</returns>
    public UserModel UpdateUser(int id, UserModel userModel)
    {
        if (_userStore.ContainsKey(id))
        {
            userModel.Id = id;
            _userStore[id] = userModel;
            return userModel;
        }
        
        return null; // user with the given ID not found
    }

    /// <summary>
    /// Delete user by id. 
    /// </summary>
    /// <param name="id">ID of the user that must be deleted</param>
    public void DeleteUser(int id)
    {
        if (_userStore.ContainsKey(id))
        {
            _userStore.Remove(id);
        }
    }
}
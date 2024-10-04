using System.Net;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Models;
using UserMicroservice.Services;

namespace UserMicroservice.Controllers;

/// <summary>
/// User service controller.
/// </summary>
/// <param name="userService">User service that must be interacted with.</param>
[Route("api/users")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    #region User get/put/post/delete
    
    /// <summary>
    /// Get list of all users.
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public IActionResult GetAllUsers()
    {
        var users = userService.GetAllUsers();
        return Ok(users);
    }
    
    /// <summary>
    /// Get user by ID.
    /// </summary>
    /// <param name="id">ID of the user that must be found</param>
    /// <returns></returns>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public IActionResult GetUserById(int id)
    {
        var user = userService.GetUserById(id);
        return Ok(user);
    }
    
    /// <summary>
    /// Get user by nickname.
    /// </summary>
    /// <param name="login">Login of the user that must be found</param>
    /// <returns></returns>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    [HttpGet("{login}")]
    [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public IActionResult GetUserById(string login)
    {
        var user = userService.GetUserByLogin(login);
        return Ok(user);
    }

    /// <summary>
    /// Create a new user account.
    /// </summary>
    /// <param name="userModel">User instance that must be added to the list</param>
    /// <returns></returns>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    [HttpPost("Create")]
    [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public IActionResult AddUser([FromBody] UserModel userModel)
    {
        var addUser = userService.AddUser(userModel);
        return CreatedAtAction(nameof(GetUserById), new { id = addUser.Id }, addUser);
    }

    /// <summary>
    /// Update info about user
    /// </summary>
    /// <param name="id">ID of the user that must be updated</param>
    /// <param name="userModel">New user info</param>
    /// <returns></returns>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public IActionResult UpdateUser(int id, [FromBody] UserModel userModel)
    {
        var updateUser = userService.UpdateUser(id, userModel);
        return Ok(updateUser);
    }

    /// <summary>
    /// Delete user by id
    /// </summary>
    /// <param name="id">ID of the user that must be deleted</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        userService.DeleteUser(id);
        return NoContent();
    }

    #endregion
}
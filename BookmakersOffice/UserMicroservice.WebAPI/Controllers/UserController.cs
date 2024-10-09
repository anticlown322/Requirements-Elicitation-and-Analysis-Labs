using System.Net;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Business.Models;
using UserMicroservice.Business.Services;
using UserMicroservice.Data.Entities;

namespace UserMicroservice.WebAPI.Controllers;

/// <summary>
/// User service controller.
/// </summary>
[Route("api/users")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    /// <summary>
    /// Get list of all users.
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    /// <response code="404">Not found</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> GetAll()
    {
        var users = await userService.GetAll();

        if (users == null)
            return NotFound();

        return Ok(users);
    }

    /// <summary>
    /// Get user by ID.
    /// </summary>
    /// <param name="id">ID of the user that must be found</param>
    /// <returns></returns>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    /// <response code="404">Not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<UserModel>> GetById(long id)
    {
        var userModel =  await userService.GetById(id);

        if (userModel == null)
            return NotFound();

        return Ok(userModel);
    }
    
    /// <summary>
    /// Update info about user
    /// </summary>
    /// <param name="id">ID of the user that must be updated</param>
    /// <param name="userModel">New user info</param>
    /// <returns></returns>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    /// <response code="404">Not found</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Update(long id, UserModel userModel)
    {
        if (id != userModel.Id)
        {
            return BadRequest();
        }

        //mapping without automapper
        UserEntity userEntity = new UserEntity
        {
            Id = userModel.Id,
            AppId = userModel.AppId,
            Balance = userModel.Balance,
            Login = userModel.Login,
            IsVerified = userModel.IsVerified,
            Email = userModel.Email,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Phone = userModel.Phone
        };
        
        var result = await userService.Update(userEntity);

        return Ok(result);
    }

    /// <summary>
    /// Create a new user account.
    /// </summary>
    /// <param name="userModel">User instance that must be added to the list</param>
    /// <returns></returns>
    /// <response code="200">Successful</response>
    /// <response code="201">Successful creation</response>
    /// <response code="400">API error</response>
    [HttpPost]
    public async Task<ActionResult<UserModel>> Create(UserModel userModel)
    {
        //mapping without automapper
        UserEntity userEntity = new UserEntity
        {
            AppId = userModel.AppId,
            Balance = userModel.Balance,
            Login = userModel.Login,
            IsVerified = userModel.IsVerified,
            Email = userModel.Email,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Phone = userModel.Phone
        };
        
        var result = await userService.Create(userEntity);

        return Created(string.Empty, result);
    }

    /// <summary>
    /// Delete user by id
    /// </summary>
    /// <param name="id">ID of the user that must be deleted</param>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    /// <response code="404">Not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> RemoveById(long id)
    {
        var userModel = await userService.GetById(id);
        
        if (userModel == null)
        {
            return NotFound();
        }
        
        var result = await userService.RemoveById(id);
        return Ok(result);
    }
}
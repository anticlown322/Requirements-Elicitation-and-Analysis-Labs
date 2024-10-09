using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserMicroservice.Models;
using UserMicroservice.Repositories;

namespace UserMicroservice.Controllers;

/// <summary>
/// User service controller.
/// </summary>
/// <param name="context">User service that must be interacted with.</param>
[Route("api/users")]
[ApiController]
public class UserController(DefaultUserContext context) : ControllerBase
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
    public async Task<ActionResult<IEnumerable<UserModel>>> GetTodoItems()
    {
        var users = await context.TodoItems.ToListAsync();

        if (users == null)
        {
            return NotFound();
        }

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
    [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<UserModel>> GetUserModel(int id)
    {
        var userModel = await context.TodoItems.FindAsync(id);

        if (userModel == null)
        {
            return NotFound();
        }

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
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> PutUserModel(int id, UserModel userModel)
    {
        if (id != userModel.Id)
        {
            return BadRequest();
        }

        context.Entry(userModel).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserModelExists(id))
            {
                return NotFound();
            }
            
            throw;
        }

        return NoContent();
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
    public async Task<ActionResult<UserModel>> PostUserModel(UserModel userModel)
    {
        context.TodoItems.Add(userModel);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetUserModel", new { id = userModel.Id }, userModel);
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
    public async Task<IActionResult> DeleteUserModel(int id)
    {
        var userModel = await context.TodoItems.FindAsync(id);
        if (userModel == null)
        {
            return NotFound();
        }

        context.TodoItems.Remove(userModel);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserModelExists(int id)
    {
        return context.TodoItems.Any(e => e.Id == id);
    }
}
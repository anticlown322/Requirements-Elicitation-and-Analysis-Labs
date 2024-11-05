using System.Net;
using Confluent.Kafka;
using Kafka.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaymentMicroservice.Business.Models;
using PaymentMicroservice.Data.Entities;
using UserMicroservice.Business.Models;
using UserMicroservice.Business.Services;
using UserMicroservice.Data.Entities;

namespace UserMicroservice.WebAPI.Controllers;

/// <summary>
/// User service controller.
/// </summary>
[Route("api/users")]
[ApiController]
public class UserController(
    IUserService userService,
    IKafkaProducer kafkaProducer,
    ILogger<UserController> logger) : ControllerBase
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
        /*for testing
        try
        {
            throw new Exception("Potential error");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Something went wrong", 666);
        }
         */
        
        var users = await userService.GetAll();

        if (users == null)
        {
            logger.LogError("User list can't be found and is null");
            return NotFound();
        }
            
        logger.LogInformation("User list successfully returned");
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
        var userModel = await userService.GetById(id);

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
            logger.LogError($"User with id = {id} not found");
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

        logger.LogInformation($"User with id = {id} was successfully updated");
        return Ok(result);
    }

    /// <summary>
    /// Create a new user account.
    /// </summary>
    /// <param name="userModel">User instance that must be added to the list</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<UserModel>> Create(UserModel userModel)
    {
        //mapping without automapper
        UserEntity userEntity = new UserEntity
        {
            Balance = userModel.Balance,
            Login = userModel.Login,
            IsVerified = userModel.IsVerified,
            Email = userModel.Email,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Phone = userModel.Phone
        };

        var result = await userService.Create(userEntity);

        logger.LogInformation("User was successfully created");
        return Created(string.Empty, result);
    }

    /// <summary>
    /// Delete user by id
    /// </summary>
    /// <param name="id">ID of the user that must be deleted</param>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    /// <response code="404">Not found</response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> RemoveById(long id)
    {
        var userModel = await userService.GetById(id);

        if (userModel == null)
        {
            logger.LogError($"User with id = {id} not found");
            return NotFound();
        }

        var result = await userService.RemoveById(id);
        
        logger.LogInformation("User was successfully removed");
        return Ok(result);
    }

    /// <summary>
    /// Deposit money to account.
    /// </summary>
    /// <param name="amount">Amount that will be deposited</param>
    /// <param name="accountId">Account that makes the deposit action</param>
    /// <returns></returns>
    [HttpPost("deposit")]
    public async Task<ActionResult<TransactionModel>> Deposit(decimal amount, int accountId)
    {
        var userModel = await userService.GetById(accountId);

        if (userModel == null)
        {
            logger.LogError($"User with id = {accountId} not found");
            return NotFound();
        }

        if (!(amount > 0))
        {
            logger.LogError("Incorrect amount for depositing");
            return BadRequest();
        }
        
        TransactionModel transaction = new()
        {
            Id = new Random().Next(0, 1_000_000_000), //don't do in such way, it's only for testing
            TransactionDateTime = DateTime.Now,
            AccountId = accountId,
            Amount = amount,
            Type = TransactionType.Deposit
        };

        Message<string, string> message = new()
        {
            Key = transaction.Id.ToString(), //use better id generation cause it is a war crime
            Value = JsonConvert.SerializeObject(transaction)
        };

        await kafkaProducer.ProduceAsync("transactionTopic", message); //better to specify topic by using config file

        logger.LogInformation("Deposit transaction successfully created");
        return Created(string.Empty, transaction);
    }

    /// <summary>
    /// Withdraw money to account.
    /// </summary>
    /// <param name="amount">Amount that will be withdrawn</param>
    /// <param name="accountId">Account that makes the withdraw action</param>
    /// <returns></returns>
    [HttpPost("withdraw")]
    public async Task<ActionResult<TransactionModel>> Withdraw(decimal amount, int accountId)
    {
        var userModel = await userService.GetById(accountId);

        if (userModel == null)
        {
            logger.LogError($"User with id = {accountId} not found");
            return NotFound();
        }

        if (userModel.Balance < amount)
        {
            logger.LogError($"Account #{accountId} has not enough money for withdrawing {amount}");
            return BadRequest();
        }

        TransactionModel transaction = new()
        {
            Id = new Random().Next(0, 1_000_000_000), //don't do in such way, it's only for testing
            TransactionDateTime = DateTime.Now,
            AccountId = accountId,
            Amount = amount,
            Type = TransactionType.Withdraw
        };

        Message<string, string> message = new()
        {
            Key = transaction.Id.ToString(), //use better id generation cause it is a war crime
            Value = JsonConvert.SerializeObject(transaction)
        };

        await kafkaProducer.ProduceAsync("transactionTopic", message); //better to specify topic by using config file

        logger.LogInformation("Withdraw transaction successfully created");
        return Created(string.Empty, transaction);
    }
}
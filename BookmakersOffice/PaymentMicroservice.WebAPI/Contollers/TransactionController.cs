using System.Net;
using Microsoft.AspNetCore.Mvc;
using PaymentMicroservice.Business.Models;
using PaymentMicroservice.Business.Services;
using PaymentMicroservice.Data.Entities;

namespace PaymentMicroservice.WebAPI.Contollers;

/// <summary>
/// Transaction service controller.
/// </summary>
[Route("api/transactions")]
[ApiController]
public class TransactionController(ITransactionService transactionService) : ControllerBase
{
    /// <summary>
    /// Get list of all transactions.
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    /// <response code="404">Not found</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TransactionModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> GetAll()
    {
        var transactions = await transactionService.GetAll();

        if (transactions == null)
            return NotFound();

        return Ok(transactions);
    }

    /// <summary>
    /// Get transaction by ID.
    /// </summary>
    /// <param name="id">ID of the transaction that must be found</param>
    /// <returns></returns>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    /// <response code="404">Not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TransactionModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<TransactionModel>> GetById(long id)
    {
        var transactionModel =  await transactionService.GetById(id);

        if (transactionModel == null)
            return NotFound();

        return Ok(transactionModel);
    }

    /// <summary>
    /// Create a new transaction account.
    /// </summary>
    /// <param name="transactionModel">Transaction instance that must be added to the list</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<TransactionModel>> Create(TransactionModel transactionModel)
    {
        //mapping without automapper
        TransactionEntity transactionEntity = new TransactionEntity
        {
            AccountId = transactionModel.AccountId,
            Amount = transactionModel.Amount,
            Type = transactionModel.Type,
            TransactionDateTime = transactionModel.TransactionDateTime
        };
        
        var result = await transactionService.Create(transactionEntity);

        return Created(string.Empty, result);
    }

    /// <summary>
    /// Delete transaction by id
    /// </summary>
    /// <param name="id">ID of the transaction that must be deleted</param>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    /// <response code="404">Not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(IEnumerable<TransactionModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> RemoveById(long id)
    {
        var transactionModel = await transactionService.GetById(id);

        if (transactionModel == null)
        {
            return NotFound();
        }

        var result = await transactionService.RemoveById(id);
        return Ok(result);
    }
}
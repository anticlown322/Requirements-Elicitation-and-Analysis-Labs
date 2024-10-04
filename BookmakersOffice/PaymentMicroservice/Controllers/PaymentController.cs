using System.Net;
using Microsoft.AspNetCore.Mvc;
using PaymentMicroservice.Models;
using PaymentMicroservice.Services;

namespace PaymentMicroservice.Controllers;

/// <summary>
/// Payment service controller
/// </summary>
/// <param name="transactionService"></param>
[Route("api/payments")]
[ApiController]
public class PaymentController(IPaymentService transactionService) : ControllerBase
{
    /// <summary>
    /// Get list of all transactions.
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TransactionModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public IActionResult GetAllTransactions()
    {
        var transactions = transactionService.GetAllTransactions();
        return Ok(transactions);
    }

    /// <summary>
    /// Get transaction by ID.
    /// </summary>
    /// <param name="id">ID of the transaction that must be found</param>
    /// <returns></returns>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IEnumerable<TransactionModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public IActionResult GetTransactionById(int id)
    {
        var transaction = transactionService.GetTransactionById(id);
        return Ok(transaction);
    }
    
    /// <summary>
    /// Create a new transaction.
    /// </summary>
    /// <param name="transactionModel">Transaction instance that must be added to the list</param>
    /// <returns></returns>
    /// <response code="200">Successful</response>
    /// <response code="400">API error</response>
    [HttpPost("Create")]
    [ProducesResponseType(typeof(TransactionModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public IActionResult AddTransaction([FromBody] TransactionModel transactionModel)
    {
        var addedTransaction = transactionService.AddTransaction(transactionModel);
        return CreatedAtAction(nameof(GetTransactionById), new { id = addedTransaction.Id }, addedTransaction);
    }

    /// <summary>
    /// Delete transaction by id
    /// </summary>
    /// <param name="id">ID of the transaction that must be deleted</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteTransaction(int id)
    {
        transactionService.DeleteTransaction(id);
        return NoContent();
    }
}
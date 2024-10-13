using PaymentMicroservice.Data.Entities;

namespace PaymentMicroservice.Business.Services;

public interface ITransactionService
{
    Task<List<TransactionEntity>> GetAll();
    Task<TransactionEntity?> GetById(long id);
    Task<long> Create(TransactionEntity some);
    Task<bool> RemoveById(long id);
}
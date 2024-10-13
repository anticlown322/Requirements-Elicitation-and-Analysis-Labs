using Microsoft.EntityFrameworkCore.ChangeTracking;
using PaymentMicroservice.Data.Entities;

namespace PaymentMicroservice.Data.Repositories;

public interface ITransactionRepository
{
    Task<EntityEntry<TransactionEntity>> Create(TransactionEntity someEntity);
    Task<bool> Update(TransactionEntity someEntity);
    Task<List<TransactionEntity>> GetAll();
    Task<TransactionEntity?> GetById(long id);
    Task<bool> RemoveById(long id);
}
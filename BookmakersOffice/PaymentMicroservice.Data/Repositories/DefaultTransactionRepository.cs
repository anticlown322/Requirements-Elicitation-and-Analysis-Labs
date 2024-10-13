using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PaymentMicroservice.Data.Entities;

namespace PaymentMicroservice.Data.Repositories;

public class DefaultTransactionRepository : ITransactionRepository
{
    private readonly TransactionRepositoryContext _dbContext;
    private readonly DbSet<TransactionEntity> _aEntity;
        
    public DefaultTransactionRepository(TransactionRepositoryContext dbContext)
    {
        _dbContext = dbContext;
        _aEntity = _dbContext.Set<TransactionEntity>();
    }
        
    public Task<EntityEntry<TransactionEntity>> Create(TransactionEntity someEntity)
    {
        Task<EntityEntry<TransactionEntity>> result = _aEntity.AddAsync(someEntity).AsTask();
        _dbContext.SaveChanges();
        return result;
    }

    public Task<bool> Update(TransactionEntity someEntity)
    {
        var existedA = _aEntity.FirstOrDefault(c => c.Id == someEntity.Id);
            
        if (existedA == null) 
            return Task.FromResult(false);
            
        existedA.AccountId = someEntity.AccountId;
        existedA.Amount = someEntity.Amount;
        existedA.Type = someEntity.Type;
        existedA.TransactionDateTime = someEntity.TransactionDateTime;
            
        if (_dbContext.SaveChanges() > 0) 
            return Task.FromResult(true);
            
        return Task.FromResult(false);
    }

    public Task<List<TransactionEntity>> GetAll()
    {
        return _aEntity.ToListAsync();
    }

    public Task<TransactionEntity?> GetById(long id)
    {
        return _aEntity.FirstOrDefaultAsync(a => a.Id == id);
    }

    public Task<bool> RemoveById(long id)
    {
        var existedA = _aEntity.FirstOrDefault(c => c.Id == id);
            
        if (existedA == null) 
            return Task.FromResult(false);
            
        _aEntity.Remove(existedA);
            
        if (_dbContext.SaveChanges() > 0) 
            return Task.FromResult(true);
            
        return Task.FromResult(false);
    }
}
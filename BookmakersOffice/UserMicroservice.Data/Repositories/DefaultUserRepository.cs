using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UserMicroservice.Data.Entities;

namespace UserMicroservice.Data.Repositories;

public class DefaultUserRepository : IUserRepository
{
    private readonly RepositoryContext _dbContext;
    private readonly DbSet<UserEntity> _aEntity;
        
    public DefaultUserRepository(RepositoryContext dbContext)
    {
        _dbContext = dbContext;
        _aEntity = _dbContext.Set<UserEntity>();
    }
        
    public Task<EntityEntry<UserEntity>> Create(UserEntity someEntity)
    {
        Task<EntityEntry<UserEntity>> result = _aEntity.AddAsync(someEntity).AsTask();
        _dbContext.SaveChanges();
        return result;
    }

    public Task<bool> Update(UserEntity someEntity)
    {
        var existedA = _aEntity.FirstOrDefault(c => c.Id == someEntity.Id);
            
        if (existedA == null) 
            return Task.FromResult(false);
            
        existedA.AppId = someEntity.AppId;
        existedA.Balance = someEntity.Balance;
        existedA.Login = someEntity.Login;
        existedA.Phone = someEntity.Phone;
        existedA.Email = someEntity.Email;
        existedA.IsVerified = someEntity.IsVerified;
        existedA.FirstName = someEntity.FirstName;
        existedA.LastName = someEntity.LastName;
            
        if (_dbContext.SaveChanges() > 0) 
            return Task.FromResult(true);
            
        return Task.FromResult(false);
    }

    public Task<List<UserEntity>> GetAll()
    {
        return _aEntity.ToListAsync();
    }

    public Task<List<UserEntity>> GetByAppId(Guid appId)
    {
        return _aEntity.Where(c => c.AppId == appId).ToListAsync();
    }

    public Task<UserEntity?> GetById(long id)
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
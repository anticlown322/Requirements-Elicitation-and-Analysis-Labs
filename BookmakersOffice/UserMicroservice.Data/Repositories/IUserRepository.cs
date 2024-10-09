using Microsoft.EntityFrameworkCore.ChangeTracking;
using UserMicroservice.Data.Entities;

namespace UserMicroservice.Data.Repositories;

public interface IUserRepository
{
    Task<EntityEntry<UserEntity>> Create(UserEntity someEntity);
    Task<bool> Update(UserEntity someEntity);
    Task<List<UserEntity>> GetAll();
    Task<List<UserEntity>> GetByAppId(Guid appId);
    Task<UserEntity> GetById(long id);
    Task<bool> RemoveById(long id);
}
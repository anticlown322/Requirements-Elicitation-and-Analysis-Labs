using UserMicroservice.Data.Entities;

namespace UserMicroservice.Business.Services;

public interface IUserService
{
    Task<List<UserEntity>> GetAll();
    Task<UserEntity?> GetById(long id);
    Task<long> Create(UserEntity some);
    Task<bool> Update(UserEntity some);
    Task<bool> RemoveById(long id);
}
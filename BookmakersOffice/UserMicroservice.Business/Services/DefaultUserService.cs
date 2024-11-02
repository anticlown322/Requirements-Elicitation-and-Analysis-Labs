using PaymentMicroservice.Business.Models;
using UserMicroservice.Data.Entities;
using UserMicroservice.Data.Repositories;

namespace UserMicroservice.Business.Services;

public class DefaultUserService : IUserService
{
    private readonly IUserRepository _iaRepository;

    public DefaultUserService(IUserRepository iaRepository)
    {
        _iaRepository = iaRepository ?? throw new ArgumentNullException(nameof(iaRepository));
    }

    public async Task<List<UserEntity>> GetAll()
    {
        return await _iaRepository.GetAll();
    }

    public async Task<UserEntity?> GetById(long id)
    {
        return await _iaRepository.GetById(id);
    }

    public async Task<long> Create(UserEntity some)
    {
        some.AppId = Guid.NewGuid();
        some.RegistrationDate = DateTime.Now;
        var result = await _iaRepository.Create(some);
        return result.Entity.Id;
    }

    public async Task<bool> Update(UserEntity some)
    {
        some.UpdateDate = new DateTime();
        return await _iaRepository.Update(some);
    }

    public async Task<bool> RemoveById(long id)
    {
        return await _iaRepository.RemoveById(id);
    }
}
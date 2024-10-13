using PaymentMicroservice.Data.Entities;
using PaymentMicroservice.Data.Repositories;

namespace PaymentMicroservice.Business.Services;

public class DefaultTransactionService : ITransactionService
{
    private readonly ITransactionRepository _iaRepository;

    public DefaultTransactionService(ITransactionRepository iaRepository)
    {
        _iaRepository = iaRepository ?? throw new ArgumentNullException(nameof(iaRepository));
    }

    public async Task<List<TransactionEntity>> GetAll()
    {
        return await _iaRepository.GetAll();
    }

    public async Task<TransactionEntity?> GetById(long id)
    {
        return await _iaRepository.GetById(id);
    }

    public async Task<long> Create(TransactionEntity some)
    {
        some.TransactionDateTime = DateTime.Now;
        var result = await _iaRepository.Create(some);
        return result.Entity.Id;
    }

    public async Task<bool> RemoveById(long id)
    {
        return await _iaRepository.RemoveById(id);
    }
}
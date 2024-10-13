using Microsoft.EntityFrameworkCore;
using PaymentMicroservice.Data.Entities;
using PaymentMicroservice.Data.EntityConfigurations;

namespace PaymentMicroservice.Data;

public class TransactionRepositoryContext : DbContext
{
    public TransactionRepositoryContext(DbContextOptions<TransactionRepositoryContext> options)
        : base(options)
    {
        Database.EnsureCreated(); 
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DefaultTransactionEntityConfig.Configure(modelBuilder.Entity<TransactionEntity>());
        base.OnModelCreating(modelBuilder);
    }
}
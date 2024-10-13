using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentMicroservice.Data.Entities;

namespace PaymentMicroservice.Data.EntityConfigurations;

public class DefaultTransactionEntityConfig
{
    public static void Configure(EntityTypeBuilder<TransactionEntity> entityBuilder)
    {
        entityBuilder.HasKey(t => t.Id);
        entityBuilder.Property(t => t.AccountId).IsRequired();
        entityBuilder.Property(t => t.Amount).IsRequired();
        entityBuilder.Property(t => t.Type).IsRequired();
        entityBuilder.Property(t => t.TransactionDateTime).IsRequired();
    }
}
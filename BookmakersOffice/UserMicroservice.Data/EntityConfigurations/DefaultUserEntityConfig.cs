using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserMicroservice.Data.Entities;

namespace UserMicroservice.Data.EntityConfigurations;

public class DefaultUserEntityConfig
{
    public static void Configure(EntityTypeBuilder<UserEntity> entityBuilder)
    {
        entityBuilder.HasKey(t => t.Id);
        entityBuilder.Property(t => t.AppId).IsRequired();
    }
}
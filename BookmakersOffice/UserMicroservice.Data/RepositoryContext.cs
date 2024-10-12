using Microsoft.EntityFrameworkCore;
using UserMicroservice.Data.Entities;
using UserMicroservice.Data.EntityConfigurations;

namespace UserMicroservice.Data;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
    {
        Database.EnsureCreated(); 
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DefaultUserEntityConfig.Configure(modelBuilder.Entity<UserEntity>());
        base.OnModelCreating(modelBuilder);
    }
}
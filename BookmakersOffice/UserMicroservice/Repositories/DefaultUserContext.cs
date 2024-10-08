using Microsoft.EntityFrameworkCore;
using UserMicroservice.Models;

namespace UserMicroservice.Repositories;

public class DefaultUserContext : DbContext
{
    public DefaultUserContext(DbContextOptions<DefaultUserContext> options)
        : base(options)
    {
    }

    public DbSet<UserModel> TodoItems { get; set; } = null!;
}
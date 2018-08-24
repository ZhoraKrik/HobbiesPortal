using Microsoft.EntityFrameworkCore;

namespace SqLiteRepository.Interfaces
{
    public interface IDesignTimeDbContextFactory <out TContext> where TContext : DbContext
    {
        TContext CreateDbContext( string[] args);
    }
}

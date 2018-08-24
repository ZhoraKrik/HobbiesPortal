using Microsoft.EntityFrameworkCore;
using SqLiteRepository.Interfaces;

namespace SqLiteRepository
{
    public class RepositoryContextFactory : IRepositoryContextFactory
    {
        public RepositoryContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new RepositoryContext(optionsBuilder.Options);
        }
    }
}

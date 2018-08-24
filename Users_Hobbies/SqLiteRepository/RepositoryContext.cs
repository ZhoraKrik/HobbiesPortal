using Models;
using Microsoft.EntityFrameworkCore;

namespace SqLiteRepository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<UserInfo> List_Users { get; set; }
        public DbSet<UserHobby> UserHobbies { get; set; }
        public DbSet<HobbyType> HobbyTypes { get; set; }
        public DbSet<HobbyName> HobbyNames { get; set; }

        protected override void OnModelCreating( ModelBuilder  modelBuilder )
        {
          // modelBuilder.Entity<UserHobby>().HasKey(uh => new { uh.UserId, uh.HobbyId });
        }
    }
}

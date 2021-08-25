using Microsoft.EntityFrameworkCore;

namespace RateSetup.Models
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }

        public DbSet<ContentType> ContentType { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Setup> Setup { get; set; }
        public DbSet<SetupContent> SetupContent { get; set; }
    }
}

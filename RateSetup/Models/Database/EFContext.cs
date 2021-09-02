using Microsoft.EntityFrameworkCore;
using RateSetup.Enums;

namespace RateSetup.Models.Database
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
        public DbSet<Hashtag> Hashtag { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.Activated)
                .HasDefaultValueSql("0");

            modelBuilder.Entity<User>()
                .Property(b => b.UserType)
                .HasDefaultValueSql(UserType.User.ToString());

            modelBuilder.Entity<User>()
                .Property(b => b.DateRegistered)
                .HasDefaultValueSql("getdate()");
        }
    }
}

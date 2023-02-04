using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext, IDataContext
    {
        public new DbSet<AppUser> Users { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new List<IdentityRole> {
                new IdentityRole
                {
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "USER",
                },

                new IdentityRole
                {
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                }
            });

            builder.Entity<Language>().HasData(new List<Language>
            {
                new Language { Id = 1, Name = "en" },
                new Language { Id = 2, Name= "ua" },
                new Language { Id = 3, Name = "ru" }
            });
        }
    }
}
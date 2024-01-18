using Microsoft.EntityFrameworkCore;
using Test.Rubens.Raizen.WebApi.Entities;

namespace Test.Rubens.Raizen.WebApi.Database
{
    public sealed class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }


    }
}

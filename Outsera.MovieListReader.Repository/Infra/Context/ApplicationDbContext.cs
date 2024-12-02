using Microsoft.EntityFrameworkCore;
using Outsera.MovieListReader.Borders.Models;
using Outsera.MovieListReader.Repository.Infra.EntitiesTypesConfiguration;

namespace Outsera.MovieListReader.Repository.Infra.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Studio> Studios { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new MovieTypeConfiguration());
        }
    }
}

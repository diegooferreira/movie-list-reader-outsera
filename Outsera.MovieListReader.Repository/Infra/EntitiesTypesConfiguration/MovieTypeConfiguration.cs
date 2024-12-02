using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Outsera.MovieListReader.Borders.Models;

namespace Outsera.MovieListReader.Repository.Infra.EntitiesTypesConfiguration
{
    public class MovieTypeConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasMany<Producer>()
                   .WithMany()
                   .UsingEntity<Dictionary<string, object>>(
                       "MovieProducer", 
                       j => j.HasOne<Producer>().WithMany().HasForeignKey("ProducerId"),
                       j => j.HasOne<Movie>().WithMany().HasForeignKey("MovieId")
                   );

            builder.HasMany<Studio>()
                   .WithMany()
                   .UsingEntity<Dictionary<string, object>>(
                       "MovieStudio",
                       j => j.HasOne<Studio>().WithMany().HasForeignKey("StudioId"),
                       j => j.HasOne<Movie>().WithMany().HasForeignKey("MovieId")
                   );
        }
    }
}

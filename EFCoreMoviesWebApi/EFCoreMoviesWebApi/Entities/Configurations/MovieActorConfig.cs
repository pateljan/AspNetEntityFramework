using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMoviesWebApi.Entities.Configurations
{
    public class MovieActorConfig : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            builder.HasKey(p => new { p.MovieId, p.ActorId });

            builder.HasOne(p => p.Actor).WithMany(a => a.MovieActors).HasForeignKey(p => p.ActorId);

            builder.HasOne(p => p.Movie).WithMany(m => m.MoviesActors).HasForeignKey(p => p.MovieId);
        }
    }
}

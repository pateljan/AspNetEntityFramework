using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMoviesWebApi.Entities.Configurations
{
    public class MovieConfig : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(250).IsRequired();
            //modelBuilder.Entity<Movie>().Property(p => p.ReleaseDate).HasColumnType("date");
            builder.Property(p => p.PosterURl).HasMaxLength(500).IsUnicode(false);
        }
    }
}

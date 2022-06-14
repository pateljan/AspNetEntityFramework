using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMoviesWebApi.Entities.Configurations
{
    public class CinemaDetailConfig : IEntityTypeConfiguration<CinemaDetail>
    {
        public void Configure(EntityTypeBuilder<CinemaDetail> builder)
        {
            builder.ToTable("Cinemas");
        }
    }
}

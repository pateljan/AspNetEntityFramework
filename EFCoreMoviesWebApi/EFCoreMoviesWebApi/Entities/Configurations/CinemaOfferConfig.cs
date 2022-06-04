using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMoviesWebApi.Entities.Configurations
{
    public class CinemaOfferConfig : IEntityTypeConfiguration<CinemaOffer>
    {
        public void Configure(EntityTypeBuilder<CinemaOffer> builder)
        {
            builder.Property(p => p.DiscountPercentage).HasPrecision(5, 2);
            
        }
    }
}

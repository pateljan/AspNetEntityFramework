using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMoviesWebApi.Entities.Configurations
{
    public class ActorConfig : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            //no need to specify this because already configure in convetion function above
            //modelBuilder.Entity<Actor>().Property(p => p.DataOfBirth).HasColumnType("date");
            builder.Property(p => p.Biography).HasColumnType("nvarchar(max)");
        }
    }
}

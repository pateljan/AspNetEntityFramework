using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMoviesWebApi.Entities.Configurations
{
    public class GenreConfig : IEntityTypeConfiguration<Genre>
    {

        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            //fluent Api anotation to specify field configuration
            //Give Different Table Name and schema
            //modelBuilder.Entity<Genre>().ToTable(name: "GenresTbl", schema: "movies");

            //Note:  Id Field Automatically configure to Primary key by convention, no need to write below statement
            builder.HasKey(p => p.Id);

            //configuration field with different fieldname in table and with spefying notnull and max length
            builder.Property(p => p.Name)
                //.HasColumnName("GenreName")
                .HasMaxLength(150).IsRequired();

            builder.HasQueryFilter(g => !g.IsDeleted);

            builder.HasIndex(p => p.Name).IsUnique().HasFilter("IsDeleted = 'false'");

            builder.Property<DateTime>("CreatedDate").HasDefaultValueSql("GetDate()").HasColumnType("datetime2");
        }
    }
}

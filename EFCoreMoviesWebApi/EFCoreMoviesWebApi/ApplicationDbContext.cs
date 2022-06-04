using EFCoreMoviesWebApi.Entities;
using EFCoreMoviesWebApi.Entities.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EFCoreMoviesWebApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //configure Conventions to specify default datatype and constraint to fields
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");
            configurationBuilder.Properties<string>().HaveMaxLength(150);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // for configuring class seperately
            //modelBuilder.ApplyConfiguration(new GenreConfig());
            //for configuring all class in one
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            /* below code is moved to Entity/Configuration for clean code  */

            //fluent Api anotation to specify field configuration
            //Give Different Table Name and schema
            //modelBuilder.Entity<Genre>().ToTable(name: "GenresTbl", schema: "movies");

            //Note:  Id Field Automatically configure to Primary key by convention, no need to write below statement
            //modelBuilder.Entity<Genre>().HasKey(p => p.Id);

            //configuration field with different fieldname in table and with spefying notnull and max length
            //modelBuilder.Entity<Genre>().Property(p => p.Name)
            //    //.HasColumnName("GenreName")
            //    .HasMaxLength(150).IsRequired();


            //Actor

            //modelBuilder.Entity<Actor>().Property(p => p.Name).IsRequired();
            ////no need to specify this because already configure in convetion function above
            ////modelBuilder.Entity<Actor>().Property(p => p.DataOfBirth).HasColumnType("date");
            //modelBuilder.Entity<Actor>().Property(p => p.Biography).HasColumnType("nvarchar(max)");

            ////Cinema
            //modelBuilder.Entity<Cinema>().Property(p => p.Name).IsRequired();

            ////cinemahall
            //modelBuilder.Entity<CinemaHall>().Property(p => p.Cost).HasPrecision(precision: 9, scale: 2);
            //modelBuilder.Entity<CinemaHall>().Property(p => p.CinemaHallType).HasDefaultValue(CinemaHallType.TwoDimensions);

            ////Movie
            //modelBuilder.Entity<Movie>().Property(p => p.Title).HasMaxLength(250).IsRequired();
            ////modelBuilder.Entity<Movie>().Property(p => p.ReleaseDate).HasColumnType("date");
            //modelBuilder.Entity<Movie>().Property(p => p.PosterURl).HasMaxLength(500).IsUnicode(false);

            ////Cinema Offer

            //modelBuilder.Entity<CinemaOffer>().Property(p => p.DiscountPercentage).HasPrecision(5, 2);
            ////modelBuilder.Entity<CinemaOffer>().Property(p => p.Begin).HasColumnType("date");
            ////modelBuilder.Entity<CinemaOffer>().Property(p => p.End).HasColumnType("date");

            ////MovieActor
            //modelBuilder.Entity<MovieActor>().HasKey(p => new { p.MovieId, p.ActorId });
            ////modelBuilder.Entity<MovieActor>().Property(p => p.Character).HasMaxLength(150);
        }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<CinemaOffer> CinemaOffers { get; set; }
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<MovieActor> MoviesActors { get; set; }
    }
}

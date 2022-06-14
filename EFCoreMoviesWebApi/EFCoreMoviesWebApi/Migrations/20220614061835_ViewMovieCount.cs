using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreMoviesWebApi.Migrations
{
    public partial class ViewMovieCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW dbo.MoviesWithCounts
as

Select Id, Title,
(Select count(*) FROM GenreMovie Where MoviesId = movies.Id) as AmountGenres,
(Select count(distinct moviesId) from CinemaHallMovie
		INNER JOIN CinemaHalls
		on CinemaHalls.Id = CinemaHallMovie.CinemaHallsId
		where MoviesId = movies.Id) as AmountCinemas,
(Select count(*) from MoviesActors where MovieId = movies.Id) as AmountActors
From Movies
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW dbo.MoviesWithCounts");
        }
    }
}

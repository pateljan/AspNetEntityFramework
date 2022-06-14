using Microsoft.EntityFrameworkCore;

namespace EFCoreMoviesWebApi.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool InCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        //[Unicode(false)]
        public string PosterURl { get; set; }

        //many to many relation with skip stype
        public List<Genre> Genres { get; set; }
        //many to many relation with skip stype
        public List<CinemaHall> CinemaHalls { get; set; }
        ////many to many relation with non-skip stype
        public List<MovieActor> MoviesActors { get; set; }
    }
}

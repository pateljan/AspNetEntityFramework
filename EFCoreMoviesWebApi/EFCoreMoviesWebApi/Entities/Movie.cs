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
        public HashSet<Genre> Genres { get; set; }
        //many to many relation with skip stype
        public HashSet<CinemaHall> CinemaHalls { get; set; }
        ////many to many relation with non-skip stype
        public HashSet<MovieActor> MovieActors { get; set; }
    }
}

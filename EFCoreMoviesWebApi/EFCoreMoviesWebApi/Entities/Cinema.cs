using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace EFCoreMoviesWebApi.Entities
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //[Precision(precision: 9, scale: 2)]
        public Point Location { get; set; }

        //Nevigation Property to indicate the relation with other table, one to one relation
        public CinemaOffer CinemaOffer { get; set; }

        //Nevigation Property to indicate the relation with other table, one to many relation
        public HashSet<CinemaHall> CinemaHalls { get; set; }
    }
}

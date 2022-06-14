using EFCoreMoviesWebApi.Entities;

namespace EFCoreMoviesWebApi.DTOs
{
    public class CinemaHallCreationDTO
    {
        public int Id { get; set; }
        public double Cost { get; set; }
        public CinemaHallType CinemaHallType { get; set; }
    }
}

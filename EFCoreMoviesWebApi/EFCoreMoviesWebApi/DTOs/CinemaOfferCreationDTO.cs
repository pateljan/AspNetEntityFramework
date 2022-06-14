using System.ComponentModel.DataAnnotations;

namespace EFCoreMoviesWebApi.DTOs
{
    public class CinemaOfferCreationDTO
    {
        [Range(1,100)]
        public double DiscountPercentage { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }
}

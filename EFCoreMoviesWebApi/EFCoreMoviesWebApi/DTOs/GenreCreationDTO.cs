using System.ComponentModel.DataAnnotations;

namespace EFCoreMoviesWebApi.DTOs
{
    public class GenreCreationDTO
    {
        [Required]
        public string Name { get; set; }
    }
}

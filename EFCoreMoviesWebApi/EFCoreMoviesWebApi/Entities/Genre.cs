using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMoviesWebApi.Entities
{
    //[Table("GenresTbl", Schema ="movies")]
    //[Index(nameof(Name), IsUnique = true)]
    public class Genre
    {
        //Data Annotation to Configure Field
        //[Key]
        public int Id { get; set; }
        //[StringLength(150)]
        //[Required]
        //[Column("GenreName")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        //many to many relation with skip stype
        public HashSet<Movie> Movies { get; set; }
    }
}

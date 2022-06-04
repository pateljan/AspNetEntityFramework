using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMoviesWebApi.Entities
{
    public class Actor
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Biography { get; set; }
        //[Column(TypeName = "Date")]
        public DateTime? DataOfBirth { get; set; }

        ////many to many relation with non-skip stype
        public HashSet<MovieActor> MovieActors { get; set; }

    }
}

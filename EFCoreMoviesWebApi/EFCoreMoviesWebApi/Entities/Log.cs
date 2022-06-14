using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMoviesWebApi.Entities
{
    public class Log
    {
        //If Not genreting from Database by default
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string  Messsage { get; set; }
    }
}

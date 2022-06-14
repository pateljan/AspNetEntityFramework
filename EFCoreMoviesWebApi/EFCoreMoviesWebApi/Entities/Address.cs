using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMoviesWebApi.Entities
{
    //[NotMapped]
    [Owned]
    public class Address
    {
        //public int Id { get; set; }
        public string Street { get; set; }
        public string Province { get; set; }
        [Required]
        public string Country { get; set; }
    }
}

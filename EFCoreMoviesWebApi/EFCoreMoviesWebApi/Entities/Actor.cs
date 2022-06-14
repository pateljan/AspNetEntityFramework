using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMoviesWebApi.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                // tOm hoLLand => Tom Holland
                _name = string.Join(' ',
                    value.Split(' ')
                    .Select(n => n[0].ToString().ToUpper() + n.Substring(1).ToLower()).ToArray());
            }
        }
        public string Biography { get; set; }
        //[Column(TypeName = "Date")]
        public DateTime? DataOfBirth { get; set; }

        ////many to many relation with non-skip stype
        ///for lazy loading related data we will configure navigation property with virtual keyword. Lazy loading requires 
        ///Microsoft.EntityFrameworkCore.Proxies nuget package
        public HashSet<MovieActor> MovieActors { get; set; }

        public string PictureURL { get; set; }

        [NotMapped]
        public int? Age
        {
            get
            {
                if (!DataOfBirth.HasValue)
                    return null;

                var dob = DataOfBirth.Value;

                var age = DateTime.Today.Year - dob.Year;
                if (new DateTime(DateTime.Today.Year, dob.Month, dob.Day) > DateTime.Today)
                {
                    age--;
                }
                return age;

            }
        }

        public Address BillingAddress { get; set; }
        public Address HomeAddress { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;

namespace EFCoreMoviesWebApi.Entities.Seeding
{
    public class Module6Seeding
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var felip = new Person() { Id =1, Name="Felip" };
            var claudia = new Person() { Id = 2, Name = "Claudia" };

            var message1 = new Message()
            {
                Id = 1,
                Content = "Hello, Claudia",
                SenderId = felip.Id,
                ReceiverId = claudia.Id
            };

            var message2 = new Message()
            {
                Id = 2,
                Content = "Hello, Felipe, how are you?",
                SenderId = claudia.Id,
                ReceiverId = felip.Id
            };
            var message3 = new Message()
            {
                Id = 3,
                Content = "All good, and you?",
                SenderId = felip.Id,
                ReceiverId = claudia.Id
            };
            var message4 = new Message()
            {
                Id = 4,
                Content = "Very good :)",
                SenderId = claudia.Id,
                ReceiverId = felip.Id
            };

            modelBuilder.Entity<Person>().HasData(felip, claudia);
            modelBuilder.Entity<Message>().HasData(message1, message2, message3, message4);
        }
    }
}

using EfCoreConsole.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreConsole
{
    internal class ApplicationDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-LK57Q47\\SQLEXPRESS;Database=EfCoreConsoleAppDb;Integrated Security=True");
        }

        public DbSet<Person> People { get; set; }
    }
}

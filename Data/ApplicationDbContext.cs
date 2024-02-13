using Persons.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Persons.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Person> Persons { get; set; }
    }
}

using Persons.Data;
using Persons.Models;
using Microsoft.EntityFrameworkCore;
using Persons.Interfaces;

namespace Persons.Services
{
    public class PersonService : IPersonService
    {
        public readonly ApplicationDbContext _context;
        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _context.Persons.ToListAsync();
        }
        public async Task<Person> GetByIdAsync(int id)
        {
            return await _context.Persons.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Person> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Persons.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public bool Add(Person person)
        {
            _context.Add(person);
            return Save();
        }
        public bool Delete(Person person)
        {
            _context.Remove(person);
            return Save();
        }
        public bool Update(Person person)
        {
            _context.Update(person);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
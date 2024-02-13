using Persons.Models;
using Microsoft.EntityFrameworkCore;

namespace Persons.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAll();
        Task<Person> GetByIdAsync(int id);
        Task<Person> GetByIdAsyncNoTracking(int id);

        bool Add(Person person);
        bool Delete(Person person);
        bool Update(Person person);
        bool Save();
    }
}
using Persons.Models;

namespace Persons.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();             
                if (!context.Persons.Any())
                {
                    context.Persons.AddRange(new List<Person>()
                    {
                        new Person()
                        {
                            Name = "Евгений",
                            Surname = "Галкин",
                            Email = "hhhhhh@gmail.com",
                            PhoneNumber = "+79848294999"
                        },
                        new Person()
                        {
                            Name = "Анастасия",
                            Surname = "Жекова",
                            Email = "jjyyyyy@gmail.com",
                            PhoneNumber = "+79473847382"
                        },
                        new Person()
                        {
                            Name = "Владислав",
                            Surname = "Скоморохов",
                            Email = "sndjdjj@gmail.com",
                            PhoneNumber = "+79858473734"
                        },
                        new Person()
                        {
                            Name = "Евдоким",
                            Surname = "Мосполитый",
                            Email = "ggggg@gmail.com",
                            PhoneNumber = "+79856756434"
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}

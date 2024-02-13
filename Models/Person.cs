using System.ComponentModel.DataAnnotations;

namespace Persons.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно для заполнения.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна для заполнения.")]
        public string Surname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}

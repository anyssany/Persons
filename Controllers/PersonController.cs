using Persons.Data;
using Persons.Models;
using Microsoft.AspNetCore.Mvc;
using Persons.Interfaces;
using System;
using Microsoft.AspNetCore.Http;

namespace Persons.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService) //обращение к БД
        {
            _personService = personService;
        }

        // GET: PersonController
        public async Task<ActionResult> Index()
        {
            IEnumerable<Person> persons = await _personService.GetAll();
            return View(persons); //передача модели данных из БД
        }

        // GET: PersonController/Details/5
        public async Task<ActionResult> Detail(int id)
        {
            Person person = await _personService.GetByIdAsync(id); //по ИД достается запись
            return View(person);
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            _personService.Add(person);
            return RedirectToAction(nameof(Index));
        }

        // GET: PersonController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _personService.GetByIdAsync(id);
            if (person == null)
                return View("Error");
            var personVM = new Person
            {
                Name = person.Name,
                Surname = person.Surname,
                Email = person.Email,
                PhoneNumber = person.PhoneNumber
            };
            return View(personVM);
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Person personVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ошибка редактирования");
                return View("Edit", personVM);
            }
            var userPerson = _personService.GetByIdAsyncNoTracking(id);
            var person = new Person
            {
                Id = id,
                Name = personVM.Name,
                Surname = personVM.Surname,
                Email = personVM.Email,
                PhoneNumber = personVM.PhoneNumber
            };
            _personService.Update(person);

            return RedirectToAction(nameof(Index));

        }

        // POST: PersonController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var redirectUrl = this.Url.Action(nameof(Index));
            var person = await _personService.GetByIdAsyncNoTracking(id);
            if (person == null)
                return View("Error");
            _personService.Delete(person);
            return Json(new { Url = redirectUrl });
        }
    }
}

using Harsha_MVC_26.Models;
using Microsoft.AspNetCore.Mvc;

namespace Harsha_MVC_26.Controllers
{
    public class StronglyTypedViewController : Controller
    {
        List<Person> persons = new List<Person>();
        public StronglyTypedViewController()
        {
            persons.Add(
                new Person() { Name = "Raj", DateOfBirth = DateTime.Now, PersonGender = Gender.Male }
                );
            persons.Add(
                new Person() { Name = "Kumar", DateOfBirth = DateTime.Now, PersonGender = Gender.Female }
                );
            persons.Add(
                new Person() { Name = "Rai", DateOfBirth = DateTime.Now, PersonGender = Gender.Male }
                );
        }


        [Route("home-2")]
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Asp Net Core";
            return View("Index", persons);
        }


        [Route("person-details/{name}")]
        public IActionResult Details(string? name)
        {
            if (name == null)
            {
                return Content("Person name cannot be null");
            };

            Person? people = persons.Where(temp => temp.Name == name).FirstOrDefault();
            return View(people);
        }


        // Strongle Typed Views - Multiple Models
        [Route("multiple-models")]
        public IActionResult MultipleModels()
        {
            Person person = new Person()
            {
                Name = "Kumar",
                DateOfBirth = DateTime.Now,
                PersonGender = Gender.Female
            };

            Product product = new Product()
            {
                ProductId = 1,
                ProductName = "Soap"
            };

            PersonAndProductWrapper personProductWrapper = new PersonAndProductWrapper()
            {
                PersonData = person,
                ProductData = product,
            };

            // contains info of two models
            return View(personProductWrapper);
        }
    }
}

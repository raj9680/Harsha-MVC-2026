using Harsha_MVC_26.Models;
using Microsoft.AspNetCore.Mvc;

namespace Harsha_MVC_26.Controllers
{
    public class ViewComponentResultController : Controller
    {
        [Route("view-component-result")]
        public IActionResult Index()
        {
            PersonGrid personGridModel = new PersonGrid()
            {
                GridTitle = "Persons",
                Persons = new List<Person>()
                {
                new Person() { Name = "Rohit", DateOfBirth = DateTime.Now, PersonGender = Gender.Female },
                new Person() { Name = "Malhotra", DateOfBirth = DateTime.Now, PersonGender = Gender.Male },
                new Person() { Name = "Roy", DateOfBirth = DateTime.Now, PersonGender = Gender.Male }
                }
            };

            return ViewComponent("ViewComponentResult", new { param = personGridModel });
        }
    }
}

using Harsha_MVC_26.Models;
using Microsoft.AspNetCore.Mvc;

namespace Harsha_MVC_26.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        public IActionResult Index()
        {
            return View(); // view-name default action name i.e Index.cshtml
            // It searches for location i.e /Views/ControllerName/ViewName.cshtml
            // OR
            // return new ViewResult() { ViewName = "Home" };
        }

        [Route("home2")]
        public IActionResult Index2()
        {
            ViewData["PageTitle"] = "Asp Net Core";
            List<Person> persons = new List<Person>()
            {
                new Person() { Name = "Raj", DateOfBirth = DateTime.Now, PersonGender= Gender.Male },
                new Person() { Name = "Kumar", DateOfBirth = DateTime.Now, PersonGender= Gender.Female },
                new Person() { Name = "Rai", DateOfBirth = DateTime.Now, PersonGender= Gender.Male },
            };
            ViewData["person"] = persons;

            // for ViewBag we can also do 
            // ViewBag.person = persons;            // ViewBag

            return View();
        }

        [Route("shared")]
        public IActionResult Test()
        {
            return View();
        }
    }
}

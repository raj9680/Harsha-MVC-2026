using Harsha_MVC_26.Models;
using Microsoft.AspNetCore.Mvc;

namespace Harsha_MVC_26.ViewComponents
{
    public class GridWithViewDataViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            PersonGrid personGrid = new PersonGrid()
            {
                GridTitle = "Employees",
                Persons = new List<Person>()
                {
                    new Person() { Name = "Raj", DateOfBirth = DateTime.Now, PersonGender= Gender.Male },
                    new Person() {Name = "Kumar", DateOfBirth = DateTime.UtcNow, PersonGender = Gender.Female},
                    new Person() {Name = "Rai", DateOfBirth = DateTime.Now, PersonGender = Gender.Male }
                }
            };
            
            ViewData["Grid"] = personGrid;

            return View("GridWithViewData");

            // passing Model to act in StronglyTypedViewComponent
            //return View("GridWithViewData", personGrid);
        }
    }
}

using Harsha_MVC_26.Models;
using Microsoft.AspNetCore.Mvc;

namespace Harsha_MVC_26.Controllers
{
    public class PartialViewResultController : Controller
    {
        [Route("partial-view-result")]
        public IActionResult Index()
        {
            ListModel listModel = new ListModel()
            {
                ListTitle = "Programming Languages",
                ListItems = new List<string>()
                {
                    "C#",
                    "C++",
                    "Java",
                    "Python"
                }
            };

            return PartialView("_ListPartialView", listModel);
            // This will go to _ListPartialView and set its model value to 'listModel'
        }
    }
}

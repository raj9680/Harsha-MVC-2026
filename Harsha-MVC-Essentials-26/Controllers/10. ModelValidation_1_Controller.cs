using harsha_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace harsha_mvc.Controllers
{
    public class ModelValidationController : Controller
    {
        [Route("model-validation")]
        public IActionResult Index(Person person)
        {
            return Content($"{person}");
        }
    }
}

using harsha_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace harsha_mvc.Controllers
{
public class FromHeaderController : Controller
{
        [Route("header-binding")]
        public IActionResult Index(Person person, [FromHeader(Name = "User-Agent")] string UserAgent)
        {
            if (!ModelState.IsValid)
            {
                string errors =
                    string.Join("\n", ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage).ToList());
                return BadRequest($"{errors}");
            }
            return Content($"{person}, {UserAgent}");
        }
    }
}

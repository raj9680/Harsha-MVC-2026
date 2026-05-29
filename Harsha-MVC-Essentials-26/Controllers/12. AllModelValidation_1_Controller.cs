using harsha_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace harsha_mvc.Controllers
{
    public class AllModelValidationController : Controller
    {
        [Route("model-validate")]
        public IActionResult Index(

        // [ModelBinder(BinderType = typeof(CustomPersonModelBinder))]
        // [Bind(nameof(Person.PersonName), nameof(Person.Age))]
        Person person

        )
        // Here Bind acts as , only given properties will receive values
        // BindNever to use in Modal to skip that prop. to receive value
        // ModelBinder: is custom model binder
        {
            if (!ModelState.IsValid)
            {
                
                string errors = 
                    string.Join("\n", ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage).ToList());

                return BadRequest($"{errors}");
            }

            return Content($"{person}");
        }
    }
}

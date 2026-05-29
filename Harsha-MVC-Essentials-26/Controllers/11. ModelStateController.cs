using harsha_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace harsha_mvc.Controllers
{
    /*
    Model State
    1. IsValid: Specifies whether there is at-least one validation error or not.

    2. Values: Contains each model property value with corresponding "Errors" 
       property that contains list of validation errors of that model property.

    3. ErrorCount: Returns number of Errors

    */
    public class ModelStateController : Controller
    {
        [Route("model-state")]
        public IActionResult Index(Person person)
        {
            if(!ModelState.IsValid)
            {
                List<string> errorsList = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach(var erros in value.Errors)
                    {
                        errorsList.Add(erros.ErrorMessage);
                    }
                }


                // OR -- Short Way using LINQ
                // string error = string.Join("\n", ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage).ToList());


                string errors = string.Join("\n", errorsList);
                return BadRequest($"{errors}");
            }

            return Content($"{person}");
        }
    }
}

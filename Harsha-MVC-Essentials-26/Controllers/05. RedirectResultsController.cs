using Microsoft.AspNetCore.Mvc;

namespace harsha_mvc.Controllers
{
    public class RedirectResultsController : Controller
    {
        [Route("action1")]
        public IActionResult ActionOne(int idd)
        {
            return Content($"This is ActionOne {idd}");
        }

        [Route("action2")] // RedirectToActionResult
        public IActionResult ActionTwo()
        {
            // return new RedirectToActionResult("ActionMethodName","ControllerName",new {});
            // OR
            return new RedirectToActionResult("ActionMethodName", "ControllerName", new { }, true); // for 301 by default its 302
        }

        // with passing route id
        [Route("action3")] // it wil pass id also to another action method
        public IActionResult ActionThree()
        {
            return new RedirectToActionResult("ActionOne", "RedirectResults", new { idd = 2 }, permanent:true);
        }


        /* All Redirect Methods

        1. RedirectToActionResult
            return new RedirectToActionResult("", "", new {route_values}, permanent);

        2. LocalRedirectResult
           return new LocalRedirectResult("local_url", permanent);

        3. RedirectResult
           return new RedirectResult("url", permanent);

        */
    }
}

using Microsoft.AspNetCore.Mvc;

namespace harsha_mvc.Controllers
{
    public class MultipleActionMethodsController : Controller
    {
        [Route("home")]
        public string Index()
        {
            return "Home Page";
        }

        [Route("about-us/{mobile:int}")]
        public string About()
        {
            return "About";
        }
    }
}

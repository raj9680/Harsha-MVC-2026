using Microsoft.AspNetCore.Mvc;

namespace harsha_mvc.Controllers
{
    public class HomeController : Controller
    {
        [Route("test")] // routing template "/test"
        public string Method1()
        {
            return "Hello World";
        }
    }
}

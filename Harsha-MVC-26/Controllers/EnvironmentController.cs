using Microsoft.AspNetCore.Mvc;

namespace Harsha_MVC_26.Controllers
{
    public class EnvironmentController : Controller
    {
        // IWebHostEnvironment - class is used to get the env. details
        private readonly IWebHostEnvironment _environment;

        public EnvironmentController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

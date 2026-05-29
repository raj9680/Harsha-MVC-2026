using Microsoft.AspNetCore.Mvc;

namespace Harsha_MVC_26.Controllers
{
    public class ProductController : Controller
    {
        [Route("/")]
        [Route("products")]
        public IActionResult Index()
        {
            ViewData["ListTitle"] = "Cities";
            ViewData["ListItems"] = new List<string>()
            {
                "Paris",
                "New York",
                "New Mumbai",
                "Rome"
            };

            return View();
        }

        [Route("about-company")]
        public IActionResult About()
        {
            return View();
        }

        [Route("search-products")]
        public IActionResult Search()
        {
            return View();
        }

        [Route("order-product")]
        public IActionResult Order()
        {
            return View();
        }
    }
}

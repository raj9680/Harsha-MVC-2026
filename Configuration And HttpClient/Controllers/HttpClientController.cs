using Microsoft.AspNetCore.Mvc;

namespace Configuration_And_HttpClient.Controllers
{
    public class HttpClientController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {

            return View();
        }
    }
}

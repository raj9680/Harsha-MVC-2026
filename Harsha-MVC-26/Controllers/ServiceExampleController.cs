using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace Harsha_MVC_26.Controllers
{
    public class ServiceExampleController : Controller
    {
        private readonly ICitiesService _citiesService1;
        private readonly ICitiesService _citiesService2;
        private readonly ICitiesService _citiesService3;
        public ServiceExampleController(ICitiesService citiesService1, ICitiesService citiesService2, ICitiesService citiesService3)
        {
            _citiesService1 = citiesService1;
            _citiesService2 = citiesService2;
            _citiesService3 = citiesService3;
        }

        [Route("cities")]
        public IActionResult Index()
        {
            List<string> cities = _citiesService1.GetCities();
            ViewBag.Guid1 = _citiesService1.ServiceInstanceId;
            ViewBag.Guid2 = _citiesService2.ServiceInstanceId;
            ViewBag.Guid3 = _citiesService3.ServiceInstanceId;

            return View(cities);
        }

        //
        //[Route("cities")]    - Method Injection From Service
        //public IActionResult Index([FromServices] ICitiesService _citiesService)
        //{
        //    List<string> cities = _citiesService.GetCities();
        //    return View(cities);
        //}
    }
}

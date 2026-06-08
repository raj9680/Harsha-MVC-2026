using Configuration_And_HttpClient.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Configuration_And_HttpClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly WeatherOptions _weatherOptions;
        public HomeController(IConfiguration configuration,
            IOptions<WeatherOptions> weatherOptions)
        {
            _configuration = configuration;
            _weatherOptions = weatherOptions.Value;
        }

        [Route("/home")]
        public IActionResult Index()
        {
            ViewBag.MyKey = _configuration["MyKey"];
            // With Default
            ViewBag.MyKey1 = _configuration.GetValue("MyKeyw", "Default");

            // Hierarchical
            ViewBag.Log = _configuration["weatherapi:ID"];

            // GetSection
            ViewBag.Section = _configuration.GetSection("weatherapi")["ID"];

            // Using OPtions - loads conf. value into new options object.
            WeatherOptions options = _configuration.GetSection("weatherapi").Get<WeatherOptions>();
            ViewBag.OptionsClientID = options.ClientID;
            ViewBag.OptionsClientSecret = options.ClientSecret;

            // Using Bind - loads conf. value into existing options object.
            WeatherOptions usingBind = new WeatherOptions();
            _configuration.GetSection("weatherapi").Bind(usingBind);
            ViewBag.OptionsClientID1 = usingBind.ClientID;
            ViewBag.OptionsClientSecret1 = usingBind.ClientSecret;

            return View();
        }

        [Route("/conf-service")]
        public IActionResult ConfigurationAsService()
        {
            ViewBag.ClientIDServiceConf = _weatherOptions.ClientID;
            ViewBag.ClientSecretServiceConf = _weatherOptions.ClientSecret;
            return View();
        }
    }
}

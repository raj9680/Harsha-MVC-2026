using Configuration_And_HttpClient.Models;
using Configuration_And_HttpClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace Configuration_And_HttpClient.Controllers
{
    public class HttpClientController : Controller
    {
        private readonly FinnHubService _services;
        public HttpClientController(FinnHubService services)
        {
            _services = services;
        }


        [Route("/")]
        public async Task<IActionResult> Index()
        {
            Dictionary<string, object> response = await _services.GetStockPriceQuote("AAPL");
            Stock stock = new Stock()
            {
                StockSymbol = "AAPL",
                CurrentPrice = Convert.ToDouble(response["c"].ToString()),
                HighestPrice = Convert.ToDouble(response["h"].ToString()),
                LowestPrice = Convert.ToDouble(response["l"].ToString()),
                OpenPrice = Convert.ToDouble(response["o"].ToString())
            };

            return View(stock);
        }
    }
}

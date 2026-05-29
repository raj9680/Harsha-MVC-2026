using Microsoft.AspNetCore.Mvc;

namespace harsha_mvc.Controllers
{
    public class FromQueryFromRouteController : Controller
    {
        [Route("from-query")] // /from-query?bookid=20
        public IActionResult FromQuery([FromQuery] int bookid)
        {
            return Content($"Data coming, From Query {bookid}");
        }


        [Route("from-route-parameter/{bookid}")] // /from-route-parameter/20
        public IActionResult FromRoute([FromRoute] int bookid)
        {
            return Content($"Data coming, From Route {bookid}");
        }


        [Route("combined/{bookid}")] // /combined/20?bookname=MyBook
        public IActionResult Combined([FromRoute] int bookid, [FromQuery] string bookName)
        {
            return Content($"Data coming, From Route {bookid}, Book Name coming from Query: {bookName}");
        }
    }
}

using harsha_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace harsha_mvc.Controllers
{
    public class FromBodyController : Controller
    {
        [Route("frombody")]
        public IActionResult Index([FromBody] Book book)
        {
            return Content($"Book Name is: {book.BookName +"\n"+ "Book Id is: " + book.BookId}");
        }
    }
}

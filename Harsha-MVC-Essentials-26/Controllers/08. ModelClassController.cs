using Microsoft.AspNetCore.Mvc;

namespace harsha_mvc.Controllers
{
    /*
    Model is a class that represents structure of data (as properties) that you would like to receive from the quest and/or send to the response. 
    Also known as POCO (Plain Old CLR Objects)
    */
    public class ModelClassController : Controller
    {
        [Route("book")] // /books?bookid=23&bookname=My%20Book
        public IActionResult Book([FromQuery] Books books)
        {
            return Json(books);
        }

        [Route("books/{bookid}/{bookname}")] // /books/23/MyBook
        public IActionResult Books([FromRoute] Books books)
        {
            return Json(books);
        }
    }

    // Modal Class
    public class Books
    {
        // [FromQuery]   // if we use here, then for all controllers it applied
        public int BookId { get; set; }
        public string? BookName { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace harsha_mvc.Controllers
{
    /*

    IActionResult: IActionResult is the parent of all the result classes 
    ex: ContentResult, JsonResult, RedirectResult, StatusCodeResult, 
    ViewResult etc.

    */
    public class IActionResultController : Controller
    {
        [Route("book")] // /book?bookid=20&isloggedin=true
        public IActionResult Index()
        {
            if (!Request.Query.ContainsKey("bookid"))
            {
                Response.StatusCode = 400;
                return Content("bookid: is not provided");
            }

            if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.HttpContext.Request.Query["bookid"])))
            {
                Response.StatusCode = 400;
                return Content("bookid should not be empty or null");
            }

            int bookId = Convert.ToInt32(ControllerContext.HttpContext.Request.Query["bookid"]);

            if(bookId < 1 || bookId > 100)
            {
                return Content("bookid should not less than 1 or greater than 100");
            }

            if(!Convert.ToBoolean(Request.Query["isloggedin"]) == true)
            {
                Response.StatusCode = 400;
                return Content("You are not authorised");
            }

            return File("/lead-journey.pdf", "application/pdf");
        }


        #region StatusCodeResult

        /*
        StatusCodeResult
        UnauthorisedResult
        BadRequestResult
        NotFoundResult
        */

        [Route("status-code")]
        public IActionResult StatusCodes()
        {
            return BadRequest("Not Allowed");
            //return NotFound("Not Found");
            //return Unauthorized("You are not authorised");
        }

        #endregion
    }
}

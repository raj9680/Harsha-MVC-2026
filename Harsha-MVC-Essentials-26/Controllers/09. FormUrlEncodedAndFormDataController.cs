using Microsoft.AspNetCore.Mvc;

namespace harsha_mvc.Controllers
{
    public class FormUrlEncodedAndFormDataController : Controller
    {
        [Route("form")] // by-default, form-url-encoded
        public IActionResult Index(Books book)
        {
            return Json(book);
        }
    }
}

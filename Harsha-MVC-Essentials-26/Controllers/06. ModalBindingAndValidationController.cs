using Microsoft.AspNetCore.Mvc;

namespace harsha_mvc.Controllers
{
    public class ModalBindingAndValidationController : Controller
    {
        [Route("modal-binding")]  // 'modal-binding?bookid=2' -- queryString way.
        public IActionResult ModalBinding(int bookid)
        {
            return Content($"Modal Binding {bookid}");
        }


        [Route("modal-binding/{id}")]  // 'modal-binding/2' -- routeParameter way. higher priority
        public IActionResult ModalBinding2(int id)
        {
            return Content($"Modal Binding {id}");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Harsha_MVC_26.ViewComponents
{
    public class GridViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View();
            //return View("DifferentViewName");
            // here view is partial view , location should Views/Shared/Components/Grid/Default.cshtml
        }
    }
}

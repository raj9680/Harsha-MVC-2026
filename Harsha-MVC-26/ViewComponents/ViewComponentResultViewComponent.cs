using Harsha_MVC_26.Models;
using Microsoft.AspNetCore.Mvc;

namespace Harsha_MVC_26.ViewComponents
{
    public class ViewComponentResultViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PersonGrid param)
        {
            return View("ViewComponentResult", param);
        }
    }
}

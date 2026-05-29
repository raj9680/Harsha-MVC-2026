using harsha_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace harsha_mvc.Controllers
{
    /* ContentResult can represent any type of response i.e text/plain, text/
     * html, pplication/json etc.
     */
    public class ContentResultController: Controller
    {
        #region ContentResult
        [Route("/")]
        public ContentResult Indexs()
        {
            //return new ContentResult()
            //{
            //    Content = "Hello From Content Result",
            //    ContentType = "text/plain",
            //};

            // OR
            // return Content("Hello", "text/plain");
            // OR
            return Content("<h1>Hello World</h1>", "text/html");
        }
        #endregion

        #region JsonResult
        [Route("json-result")]
        public JsonResult JsonResult()
        {
            Person person = new Person()
            {
                PersonName = "John Doe",
                Email = "john@doe.com"
            };

            // return new JsonResult(person);
            // OR
            return Json(person);
        }
        #endregion

        #region FileResults

        [Route("file-download")] // if file in wwwroot
        public VirtualFileResult FileDownload()
        {
            return new VirtualFileResult("/lead-journey.pdf", "application/pdf");
        }

        [Route("file-download2")] // if file is out-of wwwroot
        public PhysicalFileResult FileDownload2()
        {
            return new PhysicalFileResult(@"C:\Users\raj.kumar\Desktop\Harsha-MVC-26\wwwroot\lead-journey.pdf", "application/pdf");

            // OR

            //return PhysicalFile(@"C:\Users\raj.kumar\Desktop\Harsha-MVC-26\wwwroot\lead-journey.pdf", "application/pdf");
        }

        [Route("file-download3")]
        public FileContentResult FileContent()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"C:\Users\raj.kumar\Desktop\Harsha-MVC-26\wwwroot\lead-journey.pdf");

            return new FileContentResult(bytes, "application/pdf");
            // OR
            // return File(bytes, "application/pdf");
        }

        #endregion

    }
}

using harsha_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace harsha_mvc.Controllers
{
    public class Ecommerce : Controller
    {
        [Route("order")]
        public IActionResult Index(Order order)
        {
            if(!ModelState.IsValid)
            {
                List<string> errorsList = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var erros in value.Errors)
                    {
                        errorsList.Add(erros.ErrorMessage);
                    }
                }

                string errors = string.Join("\n", errorsList);
                return BadRequest($"{errors}");
            }

            Random rnd = new Random();
            order.OrderNo = rnd.Next(1, 101);

            double finalPrice = 0;
            foreach (var value in order.Products)
            {
                finalPrice += value.Price * value.Quantity;
            }

            if(finalPrice != order.InvoicePrice)
            {
                return BadRequest($"Invoice Price: {order.InvoicePrice} does'nt match with all Product Costs: {finalPrice}");
            }

            return new JsonResult(order.OrderNo);
        }
    }
}
using harsha_mvc.CustomValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace harsha_mvc.Models
{
    public class Order
    {
        [BindNever]
        public int? OrderNo { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public double InvoicePrice { get; set; }

        [Required]
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public int ProductID { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

    }
}

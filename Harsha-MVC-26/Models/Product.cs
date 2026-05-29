namespace Harsha_MVC_26.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
    }

    public class PersonAndProductWrapper
    {
        public Person? PersonData { get; set; }
        public Product? ProductData { get; set; }
    }
}

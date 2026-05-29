namespace Harsha_MVC_26.Models
{
    public class Person
    {
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender PersonGender { get; set; }

    }

    public enum Gender
    {
        Male, Female
    }


    // For View Component with ViewData
    public class PersonGrid
    {
        public string? GridTitle { get; set; } = "";
        public List<Person> Persons { get; set; } = new List<Person>();
    }
}

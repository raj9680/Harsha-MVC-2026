using harsha_mvc.CustomValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace harsha_mvc.Models
{
    public class Person : IValidatableObject
    {
        [Required(ErrorMessage = "{0} can't be empty")] // attribute validation
        [Display(Name = "Person Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters long")]
        [RegularExpression("^[A-Za-z .]*$", ErrorMessage = "{0} should contain only alphabets, spaces and dot(.)")]
        public string? PersonName { get; set; }

        [BindNever]
        [Phone(ErrorMessage = "{0} should contain 10 digits")]
        [ValidateNever]
        public string? Phone { get; set; }


        [EmailAddress(ErrorMessage = "{0} should be a proper email address")]
        public string? Email { get; set; }


        //[Required(ErrorMessage = "{0} cannot be blank")]
        //public string? Password { get; set; }


        //[Required(ErrorMessage = "{0} cannot be blank")]
        //[Compare("Password", ErrorMessage = "{0} and {1} should match")]
        //[Display(Name = "Re-enter Password")]
        //public string? ConfirmPassword { get; set; }


        [Range(0, 999.99, ErrorMessage = "{0} should be between ${1} and ${2}")]
        public double? Price  { get; set; }



        [MinimumYearValidatorAttribute(2005, ErrorMessage = "Min. Year allowed is {0}")]

        // With Default Error Message
        // [MinimumYearValidatorAttribute(2005)]
        public DateTime? DateOfBirth { get; set; }

        public int? Age { get; set; }


        public DateTime FromDate { get; set; }

        [DateRangeValidator("FromDate", ErrorMessage = "FromDate should be older than or equal to ToDate")]
        public DateTime ToDate { get; set; }

        // initialised too
        public List<string?> Tags { get; set; } = new List<string>();


        public override string ToString()
        {
            return $"Person Name: {PersonName}, Phone: {Phone}, Email: {Email}, Price: {Price}";
        }

        // From IValidatable interface
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(DateOfBirth.HasValue == false && Age.HasValue == false)
            {
                yield return new ValidationResult("Either of Date of Birthor Age must be supplied");
            }

            // can have other if check also.
        }
    }
}

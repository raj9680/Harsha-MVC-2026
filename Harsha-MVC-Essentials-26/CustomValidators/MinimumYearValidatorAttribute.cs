using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace harsha_mvc.CustomValidators
{
    public class MinimumYearValidatorAttribute: ValidationAttribute
    {
        // For Dynamic Value passing

        public int MinYear { get; set; } = 2000; // def. value
        public string DefaultErrorMessage { get; set; } = "Year should not be less than {0}";
        public MinimumYearValidatorAttribute() // PMTL ctor
        {
            
        }

        public MinimumYearValidatorAttribute(int minYear) // PMT ctor
        {
            MinYear = minYear;
        }


        // End
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value!= null)
            {
                DateTime date = (DateTime)value;

                //if(date.Year <= 2000) // OR Dynamic

                if (date.Year <= MinYear)
                {
                    // ErrorMessage param is predefined in ValidationResult
                    // return new ValidationResult(ErrorMessage);

                    // For Index to work
                    return new ValidationResult(String.Format(ErrorMessage ?? DefaultErrorMessage, MinYear));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            return null;
        }
    }
}

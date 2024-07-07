using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.Validations
{
    public class ValidName : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var firstLetter = value?.ToString()?[0].ToString();
            if (firstLetter != firstLetter?.ToUpper()) 
            {
                return new ValidationResult("First letter cannot be lowercase!");
            }

            return ValidationResult.Success;
        }
    }
}

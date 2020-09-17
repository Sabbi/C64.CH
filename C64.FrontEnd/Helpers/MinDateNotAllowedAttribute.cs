using System;
using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Helpers
{
    public class MinDateNotAllowedAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value.GetType() == typeof(DateTime))
            {
                if ((DateTime)value != DateTime.MinValue)
                    return ValidationResult.Success;
            }

            return new ValidationResult($"The {validationContext.DisplayName} field is not a valid date");
        }
    }
}
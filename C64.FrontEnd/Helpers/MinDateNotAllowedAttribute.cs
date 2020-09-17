using System;
using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Helpers
{
    public class MinDateNotAllowedAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return IsValid(value) ? ValidationResult.Success : new ValidationResult($"The {validationContext.DisplayName} field is not a valid date");
        }

        public override bool IsValid(object value)
        {
            if (value != null && value.GetType() == typeof(DateTime))
            {
                if ((DateTime)value != DateTime.MinValue && ((DateTime)value).Year > 1900)
                    return true;
            }

            return false;
        }
    }
}
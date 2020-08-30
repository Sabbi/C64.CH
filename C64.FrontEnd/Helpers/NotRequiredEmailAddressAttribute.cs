using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Helpers
{
    public class NotRequiredEmailAddressAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var baseValidation = new EmailAddressAttribute();

            if (string.IsNullOrEmpty(value?.ToString()))
                value = null;
            var result = baseValidation.IsValid(value);

            if (result)
                return ValidationResult.Success;

            return new ValidationResult($"The {validationContext.DisplayName} field is not a valid e-mail address");
        }
    }
}
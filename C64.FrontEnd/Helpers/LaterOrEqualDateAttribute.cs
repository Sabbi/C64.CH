using System;
using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Helpers
{
    public class LaterOrEqualDateAttribute : ValidationAttribute
    {
        private string otherProperty;

        public LaterOrEqualDateAttribute(string otherProperty, string errorMessage) : base(errorMessage)
        {
            this.otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var laterDate = (DateTime)value;
            var other = validationContext.ObjectType.GetProperty(otherProperty);
            if (other.PropertyType.Equals(new DateTime().GetType()))
            {
                var otherDateTime = (DateTime)other.GetValue(validationContext.ObjectInstance, null);

                if (laterDate >= otherDateTime)
                    return ValidationResult.Success;
                else
                    return new ValidationResult(ErrorMessageString);
            }
            else
                return new ValidationResult("Unable to compare the dates requested");
        }
    }
}
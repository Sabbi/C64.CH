using C64.FrontEnd.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace C64.Tests
{
    public class ValidationAttributeTests
    {
        [Fact]
        public void MinDateNotAllowed_MinDate_ShouldReturnError()
        {
            var validator = new MinDateNotAllowedAttribute();
            var input = DateTime.MinValue;

            var result = validator.GetValidationResult(DateTime.MinValue, new ValidationContext(input));

            Assert.False(result == ValidationResult.Success);
        }

        [Fact]
        public void MinDateNotAllowed_NullObject_ShouldReturnError()
        {
            var validator = new MinDateNotAllowedAttribute();
            var input = DateTime.MinValue;

            var result = validator.GetValidationResult(null, new ValidationContext(input));

            Assert.False(result == ValidationResult.Success);
        }

        [Fact]
        public void MinDateNotAllowed_ValidDate_ShouldReturnSuccess()
        {
            var validator = new MinDateNotAllowedAttribute();
            var input = new DateTime(2020, 01, 01);

            var result = validator.GetValidationResult(input, new ValidationContext(input));

            Assert.True(result == ValidationResult.Success);
        }
    }
}
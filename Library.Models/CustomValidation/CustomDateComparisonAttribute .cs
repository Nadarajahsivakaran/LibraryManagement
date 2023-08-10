using System;
using System.ComponentModel.DataAnnotations;

public class CustomDateComparisonAttribute : ValidationAttribute
{
    private readonly string _otherPropertyName;

    public CustomDateComparisonAttribute(string otherPropertyName)
    {
        _otherPropertyName = otherPropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);

        if (otherProperty == null)
        {
            return new ValidationResult($"Property {_otherPropertyName} not found.");
        }

        var comparisonValue = (DateTime?)otherProperty.GetValue(validationContext.ObjectInstance);

        if (value is DateTime date && comparisonValue.HasValue)
        {
            if (DateTime.Compare(date.Date, comparisonValue.Value.Date) <= 0)
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}

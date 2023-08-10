using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.CustomValidation
{
    public class PublishedYearRangeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (value is string yearString)
            {
                if (int.TryParse(yearString, out int year))
                {
                    int currentYear = DateTime.Now.Year;
                    return year >= 1000 && year <= currentYear;
                }
            }

            return false;
        }
    }
}

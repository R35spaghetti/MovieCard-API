using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MovieCard_API.Custom_attributes;

public class IsDateSetInFuture : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var dateString = value?.ToString() ?? string.Empty;
        
        if (!DateTime.TryParseExact(dateString, ["YYYY-MM-DD"], CultureInfo.InvariantCulture,
                                                                           DateTimeStyles.None, out var releaseDate))
        {
            return new ValidationResult($"Please enter a valid {validationContext.DisplayName}");
        }

        if (releaseDate > DateTime.UtcNow)
        {
            return new ValidationResult($"{validationContext.DisplayName} cannot be set in the future");
        }

        return ValidationResult.Success;
    }
}
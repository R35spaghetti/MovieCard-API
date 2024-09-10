using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MovieCard_API.Custom_attributes;

public class IsDateSetInFuture : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

        var dateTime = (DateTime)(value ?? string.Empty);
    
        var dateString = dateTime.ToString("yyyy-MM-dd");
        
        if (!DateTime.TryParseExact(dateString, ["yyyy-mm-dd"], CultureInfo.InvariantCulture,
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
using System.ComponentModel.DataAnnotations;
using MovieCard_API.Data;

namespace MovieCard_API.Custom_attributes;

public class IsTitleUnique : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var title = value as string;
        var dbContext = validationContext.GetService<MovieCardContext>();
        if (dbContext == null)
        {
            throw new InvalidOperationException($" {dbContext} is not available in the current scope.");
        }

        var movie = dbContext.Movies.FirstOrDefault(m => m.Title == title);
        if (movie != null)
        {
            return new ValidationResult($"{title} already exists as a movie title");
        }

        return ValidationResult.Success;
    }
}
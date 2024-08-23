using System.Text;
using Microsoft.EntityFrameworkCore;
using MovieCard_API.Data;
using MovieCard_API.Models;

namespace MovieCard_API.SeedData;

internal class SeedMovies
{
    private readonly Random _random = new();
    private readonly List<string> _genreNames = ["Action", "Adventure", "Comedy", "Drama", "Horror"];
    private readonly List<string> _randomNames = ["John Doe", "Jane Smith", "Robert Johnson", "Emily Williams"];

    internal async Task InitData(MovieCardContext context)
    {
        if (await context.Movies.AnyAsync()) return;

        InitDatabase(context);
    }

    private DateTime GenerateBirthday()
    {
        int year = _random.Next(DateTime.Now.Year - 100, DateTime.Now.Year);
        int month = _random.Next(1, 12);
        int day = _random.Next(1, 30);

        return new DateTime(year, month, day);
    }

    private string GenerateRandomEmail()
    {
        var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var sb = new StringBuilder(9);
        for (int i = 0; i < 6; i++)
        {
            sb.Append(chars[_random.Next(chars.Length)]);
        }

        sb.Append("@random.com");
        return sb.ToString();
    }

    private int GenerateRandomPhoneNumber()
    {
        var number = _random.Next(100000000, 999999999);
        return number;
    }

    private void InitDatabase(MovieCardContext context)
    {
        for (int i = 0; i < 10; i++)
        {
            var movie = new Movie
            {
                Id = i + 1,
                DirectorId = i + 1,
                Director = new Director
                {
                    Id = i + 1, ContactInformationId = i + 1,
                    Name = _randomNames[_random.Next(_randomNames.Count)],
                    Birthday = GenerateBirthday(),
                    ContactInformation = new ContactInformation
                    {
                        Id = i + 1,
                        DirectorId = i + 1,
                        Email = GenerateRandomEmail(), PhoneNumber = GenerateRandomPhoneNumber()
                    }
                },
                Actors = new List<Actor>
                {
                    new()
                    {
                        Id = i + 1, Name = _randomNames[_random.Next(_randomNames.Count)],
                        Birthday = GenerateBirthday()
                    },
                },
                Genres = new List<Genre>
                {
                    new() { Id = i + 1, Name = _genreNames[_random.Next(_genreNames.Count)] },
                },
                Title = $"Movie {i + 1}",
                Rating = $"{_random.Next(1, 10)}",
                ReleaseDate = new DateTime(_random.Next(DateTime.Now.Year - 100, DateTime.Now.Year),
                    _random.Next(1, 12), _random.Next(1, 30)),
                Description = $"Description for movie {i + 1}"
            };

            context.Movies.Add(movie);
        }


        context.SaveChanges();
    }
}
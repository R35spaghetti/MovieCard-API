using MovieCard_API.DTOs;
using MovieCard_API.Models;

namespace MovieCard_API.Features;

public static class DtoConverter
{
    public static IEnumerable<MovieDTO> ConvertMovies(this IEnumerable<Movie>? movies)
    {
        var listOfMovies = movies.ToList();
        if (!listOfMovies.Any())
        {
            return new List<MovieDTO>();
        }

        return listOfMovies.Select(movie => new MovieDTO
        {
            Id = movie.Id,
            Title = movie.Title,
            Rating = movie.Rating,
            ReleaseDate = movie.ReleaseDate,
            Description = movie.Description
        }).ToList();
    }

    public static MovieDTO ConvertOneMovie(this Movie movie)
    {
        return new MovieDTO
        {
            Id = movie.Id,
            Title = movie.Title,
            Rating = movie.Rating,
            ReleaseDate = movie.ReleaseDate,
            Description = movie.Description
        };
    }
}
using MovieCard_API.Models;

namespace MovieCard_API.Features;

public static class SortMovies
{
   
    public static IEnumerable<Movie> GetSortedMovies(MovieSortParameters sortBy, IEnumerable<Movie> movies)
    {
        if (sortBy.SortByTitle)
        {
            movies = sortBy.TitleOrder
                ? movies.OrderBy(m => m.Title)
                : movies.OrderByDescending(m => m.Title);
        }

        if (sortBy.SortByReleaseDate)
        {
            movies = sortBy.ReleaseDateOrder
                ? movies.OrderBy(m => m.ReleaseDate)
                : movies.OrderByDescending(m => m.ReleaseDate);
        }

        if (sortBy.SortByRating)
        {
            movies = sortBy.RatingOrder
                ? movies.OrderBy(m => m.Rating)
                : movies.OrderByDescending(m => m.Rating);
        }

        return movies;
    }

    public static IEnumerable<Movie> ApplyFilters(IEnumerable<Movie> movies, MovieSearchParameters parameters)
    {
     
        if (!string.IsNullOrEmpty(parameters.Title))
        {
            movies = movies.Where(m => m.Title.Contains(parameters.Title, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(parameters.Genre))
        {
            movies = movies.Where(m =>
            {
                var genreMatch = m.Genres.FirstOrDefault(g => g.Name.IndexOf(parameters.Genre, StringComparison.OrdinalIgnoreCase) >= 0);
                return genreMatch != null;
            });
        }


       
        return movies;
    }
  

}
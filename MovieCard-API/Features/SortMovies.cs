using MovieCard_API.DTOs;

namespace MovieCard_API.Features;

public static class SortMovies
{
    public static IQueryable<MovieDTO> GetSortedMovies(MovieSortParameters sortBy, IQueryable<MovieDTO> movies)
    {
        var moviesInOrder = movies.AsQueryable();

        if (sortBy.SortByTitle)
            moviesInOrder = moviesInOrder.OrderBy(m => m.Title).ApplySortingDirection(sortBy.TitleOrder);
        if (sortBy.SortByReleaseDate)
            moviesInOrder = moviesInOrder.OrderBy(m => m.ReleaseDate).ApplySortingDirection(sortBy.ReleaseDateOrder);
        if (sortBy.SortByRating)
            moviesInOrder = moviesInOrder.OrderBy(m => m.Rating).ApplySortingDirection(sortBy.RatingOrder);

        return moviesInOrder;
    }

    private static IQueryable<TSource> ApplySortingDirection<TSource>(this IQueryable<TSource> source,
        SortDirection direction)
    {
        if (direction == SortDirection.Ascending)
        {
            return direction == SortDirection.Ascending ? source : source.OrderByDescending(x => x);
        }

        return direction == SortDirection.Descending ? source : source.OrderBy(x => x);
    }
}
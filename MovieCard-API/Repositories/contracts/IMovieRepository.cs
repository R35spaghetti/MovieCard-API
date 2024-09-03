using MovieCard_API.DTOs;

namespace MovieCard_API.Repositories.contracts;

public interface IMovieRepository
{
    Task<List<MovieDTO>> GetAllMoviesAsync();
    Task<MovieDTO> GetMovieByIdAsync(int id);
    Task<MovieDTO> CreateMovieAsync(MovieDTO movie);
    Task<MovieDTO> UpdateMovieAsync(MovieDTO movie, int id);
    Task DeleteMovieAsync(int id);
}
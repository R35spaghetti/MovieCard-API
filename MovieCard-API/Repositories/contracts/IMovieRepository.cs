using MovieCard_API.DTOs;
using MovieCard_API.Models;

namespace MovieCard_API.Repositories.contracts;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAllMoviesAsync();
    Task<Movie> GetMovieByIdAsync(int id);
    Task<Movie> CreateMovieAsync(MovieCreateDTO movie);
    Task<Movie> UpdateMovieAsync(MovieDTO movie);
    Task DeleteMovieAsync(int id);
    
    Task AddActorToMovieAsync(int movieId,List<ActorCreateDTO> actorsTobeAdded);
}
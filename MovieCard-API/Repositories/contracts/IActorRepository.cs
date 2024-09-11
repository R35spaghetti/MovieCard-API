using MovieCard_API.DTOs;

namespace MovieCard_API.Repositories.contracts;

public interface IActorRepository
{
    Task AddActorToMovieAsync(int movieId,IEnumerable<ActorCreateDTO> actors);

}
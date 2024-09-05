
namespace MovieCard_API.DTOs;

public record MovieCreateDTO(
    string Title,
    string Rating,
    DirectorCreateDTO? Director,
    ICollection<ActorCreateDTO>? Actors,
    ICollection<GenreCreateDTO>? Genres,
    DateTime ReleaseDate,
    string Description
);
namespace MovieCard_API.DTOs;

public record ActorCreateDTO(
    string Name,
    DateTime Birthday,
    ICollection<MovieCreateDTO>? Movies
);
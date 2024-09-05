namespace MovieCard_API.DTOs;

public record GenreCreateDTO(
    string Name,
    ICollection<MovieCreateDTO>? Movies
    );
namespace MovieCard_API.DTOs;

public record GenreDTO(
    string Id,
    string Name,
    ICollection<MovieDTO> Movies
    );
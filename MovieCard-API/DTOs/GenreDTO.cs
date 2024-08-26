namespace MovieCard_API.DTOs;

public record GenreDTO(
    string Name,
    ICollection<MovieDTO> Movies
    );
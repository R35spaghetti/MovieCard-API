namespace MovieCard_API.DTOs;

public record GenreDTO(
    int Id,
    string Name,
    ICollection<MovieDTO> Movies
    );